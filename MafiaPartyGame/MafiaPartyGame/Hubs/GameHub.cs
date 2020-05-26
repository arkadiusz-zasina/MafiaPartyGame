using GameLogic;
using GameLogic.Factories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MafiaPartyGame.Hubs
{
    public class GameHub : Hub
    {
        private Dictionary<int, Game> games = new Dictionary<int, Game>();

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task CreateRoom()
        {
            int gameCode;

            do
            {
                gameCode = new Random().Next(1000, 9999);
            } while (games.ContainsKey(gameCode));

            games.Add(gameCode, GameFactory.CreateGame(gameCode));

            await Clients.Client(Context.ConnectionId).SendAsync("OnRoomCreated", gameCode);
        }

        public async Task ConnectToGame(int gameCode, string name)
        {
            games[gameCode].AddPlayer(PlayerFactory.CreatePlayer(name, Context.ConnectionId));
            //TODO: complete
        }
    }
}
