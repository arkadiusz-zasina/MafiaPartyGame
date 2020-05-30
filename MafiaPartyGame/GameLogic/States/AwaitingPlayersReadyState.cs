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
        }


    }
}
