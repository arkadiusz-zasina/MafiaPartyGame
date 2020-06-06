using GameLogic.Factories;
using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class AwaitingPlayersReadyState : StateAdapter
    {
        public AwaitingPlayersReadyState(GameData gameData) : base(gameData)
        {
            gameData.VotingReady = VotingFactory.CreateVoting(gameData.PlayerManager.GetAlivePlayers());
        }

        public override IState VotePlayerReady(string playerConnId)
        {
            gameData.VotingReady.VotePlayerReady(gameData.PlayerManager.GetPlayerByConnId(playerConnId));
            if (gameData.VotingReady.IsVotingFinished())
            {
                if (gameData.PlayerManager.IsAgentAlive()) return new AgentChecksState(gameData);
                return new MafiaKillsState(gameData);
            }
            return this;
        }
    }
}
