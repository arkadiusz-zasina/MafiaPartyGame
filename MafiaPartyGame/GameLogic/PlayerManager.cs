using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class PlayerManager
    {
        private List<Player> players;

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void ExecutePlayer(Player player)
        {
            players.SingleOrDefault(x => x.ConnID == player.ConnID).isAlive = false;
        }

        public List<Player> GetAlivePlayers()
        {
            return players.Where(x => x.isAlive == true).ToList();
        }

        public List<Player> GetPlayers()
            => players;
    }
}
