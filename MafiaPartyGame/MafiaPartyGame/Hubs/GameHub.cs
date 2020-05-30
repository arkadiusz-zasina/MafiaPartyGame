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
        private static Dictionary<int, Game> games = new Dictionary<int, Game>();


        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            foreach(var game in games) {
                if (game.Value.GetHostConnId() == Context.ConnectionId) games.Remove(game.Key);
            }   
            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateRoom()
        {
            int gameCode;

            do
            {
                gameCode = new Random().Next(1000, 9999);
            } while (games.ContainsKey(gameCode));

            games.Add(gameCode, GameFactory.CreateGame(gameCode, Context.ConnectionId));
            Console.WriteLine("connId: " + Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("OnRoomCreated", gameCode);
            
        }

        public async Task ConnectToGame(int gameCode, string name)
        {
            Console.WriteLine("Mobile connected: " + Context.ConnectionId);
            games[gameCode].AddPlayer(PlayerFactory.CreatePlayer(name, Context.ConnectionId));
            await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnPlayerConnected", games[gameCode].GetSecretPlayers());
        }

        public async Task BeginGame(int gameCode)
        {
            games[gameCode].StartGame();
            foreach(var player in games[gameCode].GetPlayers())
            {
                Console.WriteLine("Sending to " + player.ConnID);
                await Clients.Client(player.ConnID).SendAsync("OnGameStarted", player.type);
            }
            Console.WriteLine("Clients assigned");

            await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnGameStarted");


        }
    }
}
