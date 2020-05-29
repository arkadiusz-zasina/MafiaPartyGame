using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MafiaPartyGame.Services
{
    public class GameService
    {
        private Dictionary<int, Game> games;

        public GameService()
        {
            games = new Dictionary<int, Game>();
        }

        public void Add(int key, Game game)
        {
            games.Add(key, game);
        }

        public Game Get(int key)
        {
            return games[key];
        }

        public bool ContainsKey(int key)
        {
            return games.ContainsKey(key);
        }
    }
}
