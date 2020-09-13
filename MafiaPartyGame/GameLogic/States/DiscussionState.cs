using GameLogic.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class DiscussionState : StateAdapter
    {
        public DiscussionState(GameData gameData) : base(gameData)
        {
            gameData.VotingDiscussionFinished = VotingFactory.CreateVoting(gameData.PlayerManager.GetAlivePlayers());
        }

        public override IState VoteDiscussionFinished(string playerConnId)
        {
            gameData.VotingDiscussionFinished.VotePlayerReadyUnready(gameData.PlayerManager.GetPlayerByConnId(playerConnId));
            if (gameData.VotingDiscussionFinished.IsVotingFinished())
            {
                return new VotingState(gameData);
            }
            return this;
        }
    }
}
