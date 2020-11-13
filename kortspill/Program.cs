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
        //Bug/TODO:
        //Can have multiple winners. Fixed!
        //If you got 3 spades, one of which being "the Joker", you will still win! Fixed!
        //Should players receive/discards cards after winner is declared? No. Fixed!
        //Should players be able to draw before previous player discarded card? No. Fixed!
        //Add more/less ascii "art"?
        //should player wins before discarding? now discards after win.. ..now not.. No discarding after win. Fixed!
        // Fix discard algorithm! Fixed! :D
        // Change name CardType to Suit. Fixed
        //TODO: Override operator?
        // Fix lock and sleep placement. Fixed? Yes, i think so. Fixed!
        //TODO: Implement at least one more Pattern and/or confirm singleton and flyweight
        // Singleton: Object MaxHandSize;
        //Hvis player.hand er composit pattern, bruke det for å discarde kortet man har færrest av?
        //Nevn i oppgave: Visual Studio vil ha properties uten fields, "auto properties". 

        /* For testing discarding
         
            var list = new List<ICard>();
            list.Add(CardFactory.CreateCard(Value.Queen, Suit.Diamonds));
            list.Add(CardFactory.CreateCard(Value.Ten, Suit.Spades));
            list.Add(CardFactory.CreateCard(Value.Four, Suit.Spades));
            list.Add(CardFactory.CreateCard(Value.Five, Suit.Diamonds));
            list.Add(CardFactory.CreateCard(Value.Ace, Suit.Clubs));
            list.Add(CardFactory.CreateCard(Value.King, Suit.Diamonds));
            list[4].SpecialRule = "the Joker";
            foreach (var card in list)
            {
                Console.WriteLine(card.GetCardName());
            }
            //var sorted = list.GroupBy(c => c.Suit).OrderBy(g => g.Count());
            //Console.WriteLine(sorted.First().Key);
            //var sorted = list.GroupBy(c => c.Suit).OrderBy(g => g.Count());
            //Console.WriteLine(sorted.First().Key);
            var sorted = list
                .GroupBy(x => x.SpecialRule == "the Joker")
                .Select(x => new
                {
                    Cards = x.GroupBy(c => c.Suit).OrderBy(c => c.Count()),
                    Count = x.Count(),
                })
                .OrderByDescending(x => x.Count)
                .SelectMany(x => x.Cards);
            Console.WriteLine();
            Console.WriteLine(sorted.First().Key);//*/
    }
}
