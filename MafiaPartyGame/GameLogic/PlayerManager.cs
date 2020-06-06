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

        public Player GetPlayerByConnId(string connId)
            => players.SingleOrDefault(x => x.ConnID == connId);

        public bool IsAgentAlive()
        {
            if (players.SingleOrDefault(x => x.type == PlayerTypes.AGENT) == null) return false;
            return players.SingleOrDefault(x => x.type == PlayerTypes.AGENT).isAlive;
        }

        public List<SecretPlayer> GetPartOfPlayers(bool withMafia, string withoutConnId = null)
        {
            IEnumerable<Player> list = new List<Player>();
            if (withoutConnId == null && withMafia) {
                list = players.Where(x => x.isAlive);
            }
            else if (withoutConnId != null && withMafia) {
                list = players.Where(x => x.isAlive && x.ConnID != withoutConnId);
            }
            else if (withoutConnId == null && !withMafia)
            {
                list = players.Where(x => x.isAlive && x.type != PlayerTypes.MAFIA);
            }
            else if (withoutConnId != null && !withMafia)
            {
                list = players.Where(x => x.isAlive && x.type != PlayerTypes.MAFIA && x.ConnID != withoutConnId);
            }


            return list.Select(x => new SecretPlayer
            {
                isAlive = x.isAlive,
                Color = x.Color,
                ConnID = x.ConnID,
                Name = x.Name
            }).ToList();
        }

        public bool CheckIfMafia(string connID)
        {
            return players.SingleOrDefault(x => x.ConnID == connID).type == PlayerTypes.MAFIA;
        }

        public bool CheckIfAgent(string connID)
        {
            return players.SingleOrDefault(x => x.ConnID == connID).type == PlayerTypes.AGENT;
        }
    }
}
