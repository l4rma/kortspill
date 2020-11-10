using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace kortspill
{
    class Program
    {
        static void Main(string[] args )
        {
            Init();
        }

        private static void Init()
        {
            Deck deck = new Deck();
            
            Player player1 = new Player();

            player1.requestCard(deck);
            player1.requestCard(deck);
            player1.requestCard(deck);
            player1.requestCard(deck);

            Console.WriteLine("player1 hand:");

            foreach (Card card in player1.getHand())
            {
                Console.WriteLine(card.getCardName());
            }

            Console.WriteLine("The deck:");
            deck.List();
        }
    }
}
