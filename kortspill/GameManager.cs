using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace kortspill
{
    class GameManager
    {
        private object _lock;
        public static bool gameOver = false;

        public static void endGame()
        {
            gameOver = true;

            Thread.Sleep(1000);
            Console.WriteLine(" ");

            foreach (Player player in Program.players)
            {
                if (player.winner)
                {
                    Console.WriteLine("|-----------------|");
                    Console.WriteLine("|-" + player.getName() + " won the game!-|");
                    Console.WriteLine("|-----------------|");
                    Console.WriteLine();
                    Console.WriteLine("Winning hand:");
                    foreach (Card card in player.getHand())
                    {
                        Console.WriteLine("-" + card.getCardName());
                    }

                }
            }
        }



    }
}
