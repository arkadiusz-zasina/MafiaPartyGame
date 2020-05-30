using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class DrawState : StateAdapter
    {
        public DrawState(GameData gameData) : base(gameData)
        {
        }
    }
}
