using GameLogic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.States
{
    class VotingState : StateAdapter
    {
        public VotingState(GameData gameData) : base(gameData)
        {
            gameData.VotingMain = VotingFactory.CreateVoting(gameData.PlayerManager.GetAlivePlayers());
        }

        public override IState VoteMain(string playerConnId, string toBeVotedConnId)
        {
            gameData.VotingMain.AddVoteOnPlayer(gameData.PlayerManager.GetPlayerByConnId(playerConnId), gameData.PlayerManager.GetPlayerByConnId(toBeVotedConnId));
            if (gameData.VotingMain.IsVotingFinished())
            {
                var players = gameData.VotingMain.GetResultOfVoting();
                if (players.Count > 1)
                {
                    return new DrawState(gameData);
                }
                gameData.PlayerManager.ExecutePlayer(players.ElementAt(0));
                if (gameData.PlayerManager.isGameOver()) return new FinalBeforeGameOverState(gameData);

                return new FinalState(gameData);
            }

            return this;
        }

    }
}
