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
        private const int DELAY_TIME = 2000;
        private const int DELAY_TIME_WITHOUT_SLEEP = 10000;


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
                await SignalNewStateIncoming(gameCode);
            }
            else
            {
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnOnePlayerReady", games[gameCode].getPlayerReadyVotes());
            }

        }

        public async Task OnAgentFinished(int gameCode)
        {
            await SignalNewStateIncoming(gameCode);
        }



        public async Task GetPlayers(int gameCode, bool includingMe, bool withMafia)
        {
            if (includingMe)
                await Clients.Client(Context.ConnectionId).SendAsync("OnReceivePlayers", games[gameCode].GetPartOfPlayers(withMafia));
            else
                await Clients.Client(Context.ConnectionId).SendAsync("OnReceivePlayers", games[gameCode].GetPartOfPlayers(withMafia, Context.ConnectionId));

        }

        public async Task CheckIfMafia(int gameCode, string connId)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("OnCheckedIfMafia", games[gameCode].CheckIfMafia(Context.ConnectionId, connId));
        }

        public async Task ProtectPlayer(int gameCode, string connId)
        {
            games[gameCode].ProtectPlayer(Context.ConnectionId, connId);
            await Clients.Client(Context.ConnectionId).SendAsync("OnPlayerProtected");
        }

        public async Task MafiaEliminate(int gameCode, string connId)
        {
            games[gameCode].VoteMafiaKills(Context.ConnectionId, connId);
            if (games[gameCode].IsVotingKillingFinished())
            {
                foreach (var p in games[gameCode].GetAliveMafia())
                {
                    await Clients.Client(p.ConnID).SendAsync("OnMafiaVotingFinished", games[gameCode].GetAlmostExecuted());
                }
                var killed = games[gameCode].GetWhoWasKilled();
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnMafiaVotingFinished", killed);
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnUpdatePlayersList", games[gameCode].GetSecretPlayers());

                await Task.Delay(4000);
                await SignalNewStateIncoming(gameCode);
                if (killed != null)
                {
                    await Clients.Client(killed.ConnID).SendAsync("OnGetKilled");
                }
            }
        }

        public async Task OnPlayerDiscussionReadyUnready(int gameCode)
        {
            bool playerReady = games[gameCode].VoteDiscussionFinished(Context.ConnectionId);
            if (games[gameCode].IsVotingDiscussionFinished())
            {
                await sendToAllPlayers(gameCode, "OnVotingStarted");
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnVotingStarted");
            }
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync("OnDiscussionReadyChanged", playerReady);
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnOnePlayerReady", games[gameCode].getDiscussionFinishedVotes());
            }

        }

        public async Task OnVotingMainVotedUnvoted(int gameCode, string connId)
        {
            games[gameCode].VoteMain(Context.ConnectionId, connId);
            if (games[gameCode].IsVotingMainFinished())
            {
                var mainVotingResult = games[gameCode].getMainVotingResult();
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnHasResultsOfVoting", mainVotingResult);
                await SignalNewStateIncomingWithoutSleeping(gameCode);
            }
            else
            {
                await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnMainVoted", games[gameCode].getMainVotingVotes());
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

        private async Task SignalNewStateIncoming(int gameCode)
        {
            string nextState = games[gameCode].getState().GetType().Name;
            await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnPlayersReady", nextState);
            await sendToAllPlayers(gameCode, "OnPlayersReady", nextState);

            await Task.Delay(DELAY_TIME);
            await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnNextState");
            await sendToAllPlayers(gameCode, "OnNextState");
        }

        private async Task SignalNewStateIncomingWithoutSleeping(int gameCode)
        {
            string nextState = games[gameCode].getState().GetType().Name;
            await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnMainVotingFinished", nextState);
            await sendToAllPlayers(gameCode, "OnMainVotingFinished", nextState);

            await Task.Delay(DELAY_TIME_WITHOUT_SLEEP);
            await Clients.Client(games[gameCode].GetHostConnId()).SendAsync("OnNextState");
            await sendToAllPlayers(gameCode, "OnNextState");
        }
    }
}
