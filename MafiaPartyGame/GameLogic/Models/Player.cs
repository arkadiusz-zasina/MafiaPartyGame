using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string ConnID { get; set; }
        public string Color { get; set; }
        public bool isAlive { get; set; }
        public PlayerTypes type { get; set; }

        public Player(string name, string connID, string color, bool isAlive, PlayerTypes type)
        {
            Name = name;
            ConnID = connID;
            Color = color;
            this.isAlive = isAlive;
            this.type = type;
        }
    }
}
