using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Factories
{
    public static class GameFactory
    {
        public static Game CreateGame(int gameCode, string masterConnId)
        {
            return new Game(gameCode, masterConnId);
        }
    }
}
