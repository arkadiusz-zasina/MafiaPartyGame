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
        private const int DELAY_TIME = 10000;


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

        public async Task OnPlayerReady(int gameCode)
        {
            games[gameCode].VotePlayerReady(Context.ConnectionId);
            if (games[gameCode].IsVotingReadyFinished())
            {
                string nextState = games[gameCode].getState().GetType().Name;
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnPlayersReady", nextState);
                await sendToAllPlayers(gameCode, "OnPlayersReady", nextState);

                await Task.Delay(DELAY_TIME);
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnNextState");
                await sendToAllPlayers(gameCode, "OnNextState");
            } else
            {
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnOnePlayerReady", games[gameCode].getPlayerReadyVotes());
            }


        }

        private async Task sendToAllPlayers(int gameCode, string onMethod, Object obj)
        {
            foreach (var player in games[gameCode].GetPlayers())
            {
                Console.WriteLine("Sending to " + player.ConnID);
                await Clients.Client(player.ConnID).SendAsync(onMethod, obj);
            }
        }

        private async Task sendToAllPlayers(int gameCode, string onMethod)
        {
            foreach (var player in games[gameCode].GetPlayers())
            {
                Console.WriteLine("Sending to " + player.ConnID);
                await Clients.Client(player.ConnID).SendAsync(onMethod);
            }
        }
    }
}
