using GameLogic.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class FinalState : StateAdapter
    {
        public FinalState(GameData gameData) : base(gameData)
        {
            gameData.VotingReadyForNextRound = VotingFactory.CreateVoting(gameData.PlayerManager.GetAlivePlayers());
        }

        public override IState VoteReadyForNextRound(string playerConnId)
        {
            gameData.VotingReadyForNextRound.VotePlayerReadyUnready(gameData.PlayerManager.GetPlayerByConnId(playerConnId));
            if (gameData.VotingReadyForNextRound.IsVotingFinished())
            {
                return new AgentChecksState(gameData);
            }
            return this;
        }
    }
}
