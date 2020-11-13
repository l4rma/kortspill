using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //bug: If you got 3 spades, one of which being "the Joker", you will still win!
        //Should players receive/discards cards after winner is declared?
        //Should players be able to draw before previous player discarded card?
        //Add more/less ascii "art"?
        //should player wins before discarding? now discards after win.. ..now not..
        //TODO: Fix discard algorithm!
        //TODO: Change name CardType to Suit
        //TODO: Override operator?
        //TODO: Fix lock and sleep placement. Fixed?
        //TODO: Implement at least one more Pattern and/or confirm singleton and flyweight
    }
}
