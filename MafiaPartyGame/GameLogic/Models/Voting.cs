using GameLogic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.Models
{
    public class Voting
    {
        public List<Player> playersInVoting { get; set; }
        public List<Vote> votes { get; set; }

        public Voting (List<Player> players)
        {
            this.playersInVoting = players;
            votes = new List<Vote>();
        }


        public void AddVoteOnPlayer(Player voting, Player voted)
        {
            if (votes.SingleOrDefault(x => x.Voting == voting) != null) votes.Remove(votes.SingleOrDefault(x => x.Voting == voting));
            else votes.Add(VoteFactory.CreateVote(voting, voted));
        }

        public void VotePlayerReady(Player player)
        {
            votes.Add(VoteFactory.CreateVotePlayerReady(player));
        }

        public void VotePlayerReadyUnready(Player player)
        {
            if (votes.SingleOrDefault(x => x.Voting == player) != null) votes.Remove(votes.SingleOrDefault(x => x.Voting == player));
            else votes.Add(VoteFactory.CreateVotePlayerReady(player));
        }

        public int GetNumberOfVotesOnPlayer(Player player)
        {
            return votes.Count(x => x.Voted.ConnID == player.ConnID);
        }

        public Player GetOnWhomPlayerVoted(Player player)
        {
            return votes.SingleOrDefault(x => x.Voting.ConnID == player.ConnID).Voted;
        }

        public bool IsVotingFinished()
        {
            return votes.Count() == playersInVoting.Count();
        }

        public List<Vote> GetVotes() {
            return votes;
        }

        public List<Player> GetResultOfVoting()
        {
            if (!IsVotingFinished()) return null; //TODO: throw exception
            var result = votes.GroupBy(x => x.Voted)
                      .Select(group => new
                      {
                          player = group.Key,
                          Count = group.Count()
                      })
                        .OrderByDescending(x => x.Count);
            var max = result.First().Count;
            var maxPlayers = result.Where(x => x.Count == max).ToList();
            return maxPlayers.Select(x => 
                new Player(x.player.Name, x.player.ConnID, x.player.Color, x.player.isAlive, x.player.type)).ToList();
        }

        public bool IsPlayerReady(Player player)
        {
            return votes.SingleOrDefault(x => x.Voting == player) != null;
        }


    }
}
