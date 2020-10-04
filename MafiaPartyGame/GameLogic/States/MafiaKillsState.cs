using GameLogic.Factories;
using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.States
{
    public class MafiaKillsState : StateAdapter
    {
        public MafiaKillsState(GameData gameData) : base(gameData)
        {
            gameData.VotingKilling = VotingFactory.CreateVoting(gameData.PlayerManager.GetAliveMafia());
        }

        public override IState VoteMafiaKills(string playerConnId, string toBeKilledConnId)
        {
            gameData.VotingKilling.AddVoteOnPlayer(gameData.PlayerManager.GetPlayerByConnId(playerConnId), gameData.PlayerManager.GetPlayerByConnId(toBeKilledConnId));
            if (gameData.VotingKilling.IsVotingFinished())
            {
                var players = gameData.VotingKilling.GetResultOfVoting();
                Player playerToBeEliminated = players.ElementAt(new Random().Next(0, players.Count));

                if(!gameData.PlayerManager.IsCurrentlyProtected(playerToBeEliminated.ConnID))
                {
                    gameData.PlayerManager.ExecutePlayer(playerToBeEliminated);
                }
                gameData.PlayerManager.SetAlmostExecuted(playerToBeEliminated);

                if (gameData.PlayerManager.IsGameOver()) return new GameOverState(gameData);

                return new DiscussionState(gameData);
            }
            return this;
        }
    }
}
