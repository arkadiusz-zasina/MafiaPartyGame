using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class AgentProtectsState : StateAdapter
    {
        public AgentProtectsState(GameData gameData) : base(gameData)
        {
        }

        public override IState ProtectPlayer(string myConnID, string connID)
        {
            if (!gameData.PlayerManager.CheckIfAgent(myConnID)) return this; //TODO: throw exception
            gameData.PlayerManager.SetCurrentlyProtectedPlayer(connID);

            return new MafiaKillsState(gameData);
        }
    }
}
