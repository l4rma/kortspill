using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    internal class PlayerFactory
    {
        private static int _nameIndex = 0;

        public static IPlayer CreatePlayer()
        {
            _nameIndex++;
            IPlayer player = new Player(GameManager.PlayerNames[_nameIndex - 1]);
            return player;
        }
    }
}
