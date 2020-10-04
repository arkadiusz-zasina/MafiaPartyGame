using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class GameOverState : StateAdapter
    {
        public GameOverState(GameData gameData) : base(gameData)
        {
        }

        public override bool HaveMafiaWon()
        {
            return gameData.PlayerManager.HaveMafiaWon();
        }
    }
}
