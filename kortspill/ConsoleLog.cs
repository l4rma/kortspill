using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kortspill
{
    public class ConsoleLog
    {
        public static void PrintWinner()
        {
            Console.WriteLine();
            foreach (var player in GameManager.Players.Where(player => player.Winner))
            {
                Console.WriteLine("|--------------------|");
                Console.WriteLine("| " + player.Name + " won the game! |");
                Console.WriteLine("|--------------------|");
                Console.WriteLine();
                Console.WriteLine("Winning hand:");
                foreach (var card in player.Hand)
                {
                    Console.WriteLine("-" + card.GetCardName());
                }
            }
        }

        public static void TextBox(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n|-");
            foreach (var c in s)
            {
                sb.Append("-");
            }
            sb.Append("-|");
            string line = sb.ToString();
            sb.Append("\n| ").Append(s).Append(" |").Append(line);

            Console.WriteLine(sb.ToString());
        }
    }
}
