using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.States
{
    public class AwaitingPlayersState : StateAdapter
    {
        public AwaitingPlayersState(GameData gameData) : base(gameData)
        {
        }

        public override IState AddPlayer(Player player)
        {
            gameData.PlayerManager.AddPlayer(player);
            return this;
        }

        public override IState StartGame()
        {
            if (gameData.PlayerManager.HasEnoughPlayers())
            {
                //TODO: throw error   
            }
            gameData.PlayerManager.AssingRoles();
            return new AwaitingPlayersReadyState(gameData);
        }
    }
}
