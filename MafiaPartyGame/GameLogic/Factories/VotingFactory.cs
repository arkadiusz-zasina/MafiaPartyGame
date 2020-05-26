using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Factories
{
    public static class VotingFactory
    {
        public static Voting CreateVoting(List<Player> playersInVoting)
        {
            return new Voting(playersInVoting);
        }
    }
}
