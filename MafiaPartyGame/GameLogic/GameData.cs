using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class GameData
    {
        public PlayerManager PlayerManager { get; set; }
        public int GameCode { get; set; }
        public Master Master { get; set; }

        public Voting VotingReady { get; set; }
        public Voting VotingKilling { get; set; }
        public Voting VotingDiscussionFinished { get; set; }
        public Voting VotingMain { get; set; }

        public GameData(int gameCode, string masterConnId)
        {
            GameCode = gameCode;
            Master = new Master { ConnID = masterConnId };
            PlayerManager = new PlayerManager();
            VotingReady = null;
            VotingKilling = null;
            VotingDiscussionFinished = null;
            VotingMain = null;
        }
    }
}
