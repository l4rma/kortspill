using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace kortspill
{
    internal class Program
    { 
        private static readonly GameManager GameManager = new GameManager();

        private static void Main(string[] args )
        {
            GameManager.Init();
        }
        //Notes:
        //bug: Can have multiple winners
        //Should players receive/discards cards after winner is declared?
        //Should players be able to draw before previous player discarded card?
        //Add more ascii "art"?
    }
}
