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
            state = new AwaitingPlayersState(gameData);
        }

        public IState getState()
        {
            return state;
        }

        private void setState(IState state)
        {
            this.state = state;
        }

        public void AddPlayer(Player player)
        {
            this.setState(this.state.AddPlayer(player));
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

        public List<Player> GetPlayers()
            => gameData.PlayerManager.GetPlayers();

        public void StartGame()
        {
            this.setState(this.state.StartGame());
        }

        public void VotePlayerReady(string playerConnId)
        {
            this.setState(this.state.VotePlayerReady(playerConnId));
        }

        public List<Vote> getPlayerReadyVotes()
            => gameData.VotingReady.GetVotes();

        public bool IsVotingReadyFinished()
            => gameData.VotingReady.IsVotingFinished();

        public List<SecretPlayer> GetPartOfPlayers(bool withMafia, string withoutConnId = null)
            => gameData.PlayerManager.GetPartOfPlayers(withMafia, withoutConnId);

        public bool CheckIfMafia(string myConnID, string connID)
        {
            this.setState(this.state.CheckIfMafia(myConnID));
            return gameData.PlayerManager.CheckIfMafia(connID);
        }
            



    }
}
