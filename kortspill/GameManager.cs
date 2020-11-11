using System;
using System.Collections.Generic;
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

            Console.WriteLine("----------");
            Console.WriteLine("Game Over!");
            Console.WriteLine("----------");
            

            Console.WriteLine(" ");
            Console.WriteLine("Deck: "+ Deck.getDeckSize());

            foreach (Player player in Program.players)
            {
                if (player.winner)
                {
                    Console.WriteLine(player.getName() + " won the game!");
                }
            }

            foreach (Player player in Program.players)
            {
                Console.WriteLine(player.getName() + " " + player.getHandSize());
            }
        }



    }
}
