using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace GameLogic.Factories
{


    public static class PlayerFactory
    {
        [DllImport("shlwapi.dll")]
        public static extern int ColorHLSToRGB(int H, int L, int S);

        public static Player CreatePlayer(string name, string connId)
        {
            Color x = Color.FromArgb(ColorHLSToRGB(new Random().Next(1, 255), 86, 169));
            string hex = x.R.ToString("X2") + x.G.ToString("X2") + x.B.ToString("X2");
            return new Player(name, connId, hex, true, PlayerTypes.UNDEFINED);
        } 
    }


}

