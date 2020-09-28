using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class FinalState : StateAdapter
    {
        public FinalState(GameData gameData) : base(gameData)
        {
        }
    }
}
