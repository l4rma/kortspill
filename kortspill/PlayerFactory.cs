using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    class PlayerFactory
    {
        private static int nameId; 
        public static Player CreatePlayer()
        {
            nameId++;
            return new Player(nameId);
        }
    }
}
