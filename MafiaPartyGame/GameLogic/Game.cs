using GameLogic.Models;
using GameLogic.States;
using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class Game
    {
        private GameData gameData;
        private IState state;

        public Game(int gameCode, string masterConnId)
        {
            gameData = new GameData(gameCode, masterConnId);
        }

        public void AddPlayer(Player player)
        {
            gameData.PlayerManager.AddPlayer(player);
        }

        public string GetHostConnId()
        {
            return gameData.Master.ConnID;
        }

        public List<SecretPlayer> GetSecretPlayers()
        {
            List<SecretPlayer> players = new List<SecretPlayer>();

            foreach (Player p in gameData.PlayerManager.GetPlayers())
            {
                players.Add(new SecretPlayer
                {
                    Color = p.Color,
                    ConnID = p.ConnID,
                    isAlive = p.isAlive,
                    Name = p.Name
                });
            }

            return players;
        }
    }
}
