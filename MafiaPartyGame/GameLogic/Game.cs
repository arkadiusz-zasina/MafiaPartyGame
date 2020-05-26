using GameLogic.Models;
using GameLogic.States;
using System;

namespace GameLogic
{
    public class Game
    {
        private GameData gameData;
        private IState state;
        private int gameCode;

        public Game(int gameCode)
        {
            this.gameCode = gameCode;
        }

        public void AddPlayer(Player player)
        {
            gameData.PlayerManager.AddPlayer(player);
        }
    }
}
