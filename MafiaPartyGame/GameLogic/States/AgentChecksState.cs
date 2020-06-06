using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class AgentChecksState : StateAdapter
    {
        public AgentChecksState(GameData gameData) : base(gameData)
        {
        }

        public override IState CheckIfMafia(string myConnID)
        {
            if (!gameData.PlayerManager.CheckIfAgent(myConnID)) return this; //TODO: throw exception

            return new AgentProtectsState(gameData);
        }
    }
}
