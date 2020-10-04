using GameLogic.Factories;
using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.States
{
    public class DrawState : StateAdapter
    {
        public DrawState(GameData gameData) : base(gameData)
        {
            var playersForVote = gameData.PlayerManager.GetAlivePlayers().Where(x => gameData.PlayerManager.GetDrawPlayers().SingleOrDefault(y => y.Name == x.Name) == null).ToList();
            gameData.VotingDraw = VotingFactory.CreateVoting(playersForVote);
        }

        public override IState VoteDraw(string playerConnId, string toBeVotedConnId)
        {
            gameData.VotingDraw.AddVoteOnPlayer(gameData.PlayerManager.GetPlayerByConnId(playerConnId), gameData.PlayerManager.GetPlayerByConnId(toBeVotedConnId));
            if (gameData.VotingDraw.IsVotingFinished())
            {
                var players = gameData.VotingMain.GetResultOfVoting();
                Player playerToBeEliminated = players.ElementAt(new Random().Next(0, players.Count));
                gameData.PlayerManager.ExecutePlayer(playerToBeEliminated);

                if (gameData.PlayerManager.IsGameOver()) return new FinalBeforeGameOverState(gameData);

                return new FinalState(gameData);
            }

            return this;
        }
    }
}
