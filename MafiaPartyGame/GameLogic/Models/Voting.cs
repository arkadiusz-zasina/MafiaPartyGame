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
        }


        public void AddVoteOnPlayer(Player voting, Player voted)
        {
            if (votes.SingleOrDefault(x => x.Voting == voting) != null) votes.Remove(votes.SingleOrDefault(x => x.Voting == voting));

            votes.Add(VoteFactory.CreateVote(voting, voted));
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


    }
}
