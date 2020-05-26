using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Factories
{
    public static class VoteFactory
    {
        public static Vote CreateVote(Player voting, Player voted)
        {
            return new Vote(voting, voted);
        }
    }
}
