using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal class PlayerFactory
    {

        private static readonly string[] PlayerNames = { "Nils", "Kåre", "Geir", "Otto" };
        private static int _nameIndex = 0;

        public static Player CreatePlayer()
        {
            _nameIndex++;
            return new Player(PlayerNames[_nameIndex-1]);
        }
    }
}
