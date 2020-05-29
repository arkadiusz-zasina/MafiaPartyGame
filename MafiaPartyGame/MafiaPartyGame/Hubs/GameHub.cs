using GameLogic;
using GameLogic.Factories;
using MafiaPartyGame.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MafiaPartyGame.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService gameService;

        public GameHub(GameService gameService)
        {
            this.gameService = gameService;
        }

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
            } while (gameService.ContainsKey(gameCode));

            gameService.Add(gameCode, GameFactory.CreateGame(gameCode, Context.ConnectionId));
            Console.WriteLine("connId: " + Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("OnRoomCreated", gameCode);
            
        }

        public async Task ConnectToGame(int gameCode, string name)
        {
            var game = gameService.Get(gameCode);
            Console.WriteLine("Mobile connected: " + Context.ConnectionId);
            game.AddPlayer(PlayerFactory.CreatePlayer(name, Context.ConnectionId));
            await Clients.Client(game.GetHostConnId()).SendAsync("OnPlayerConnected", game.GetSecretPlayers());
        }
    }
}
