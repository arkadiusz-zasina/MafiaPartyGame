﻿using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public class PlayerManager
    {
        private List<Player> players;

        public PlayerManager()
        {
            this.players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void ExecutePlayer(Player player)
        {
            players.SingleOrDefault(x => x.ConnID == player.ConnID).isAlive = false;
        }

        public List<Player> GetAlivePlayers()
        {
            return players.Where(x => x.isAlive == true).ToList();
        }

        public void AssingRoles()
        {
            int mafiaMembers = 1;
            int membersCount = players.Count;
            if (membersCount == 6 || membersCount == 7) mafiaMembers = 2;
            else if (membersCount > 7 && membersCount <= 10) mafiaMembers = 3;
            else if (membersCount > 10 && membersCount <= 13) mafiaMembers = 3;
            else if (membersCount > 13) mafiaMembers = 3;

            Random r = new Random();
            int rand;

            while(mafiaMembers != 0)
            {
                rand = r.Next(0, players.Count - 1);
                if (players.ElementAt(rand).type != PlayerTypes.UNDEFINED) continue;
                players.ElementAt(rand).type = PlayerTypes.MAFIA;
                mafiaMembers--;
            }

            while (true)
            {
                rand = r.Next(0, players.Count - 1);
                if (players.ElementAt(rand).type == PlayerTypes.UNDEFINED)
                {
                    players.ElementAt(rand).type = PlayerTypes.AGENT;
                    break;
                }
                
            } 

            foreach(var p in players.Where(x => x.type == PlayerTypes.UNDEFINED))
            {
                p.type = PlayerTypes.CITIZEN;
            }

        }
        public bool HasEnoughPlayers()
        {
            return players.Count >= 3;
        }

        public List<Player> GetPlayers()
            => players;
    }
}
