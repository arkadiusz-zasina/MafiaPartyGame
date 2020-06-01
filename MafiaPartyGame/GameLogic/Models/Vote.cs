using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Models
{
    public class Vote
    {
        public Player Voting { get; set; }
        public Player Voted { get; set; }

        public Vote(Player voting, Player voted)
        {
            this.Voting = voting;
            this.Voted = voted;
        }

        public Vote(Player readyPlayer)
        {
            this.Voting = readyPlayer;
        }
    }
}
