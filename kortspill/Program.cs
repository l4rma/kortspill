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
        //bug: If you got 3 spades, one of which being "the Joker", you will still win!
        //Should players receive/discards cards after winner is declared?
        //Should players be able to draw before previous player discarded card?
        //Add more/less ascii "art"?
        //should player wins before discarding? now discards after win.. ..now not..
        // Fix discard algorithm! Fixed! :D
        //TODO: Change name CardType to Suit
        //TODO: Override operator?
        //TODO: Fix lock and sleep placement. Fixed? Yes, i think so.
        //TODO: Implement at least one more Pattern and/or confirm singleton and flyweight
        //Hvis player.hand er composit pattern, bruke det for å discarde kortet man har færrest av?

        /* For testing discarding
         
            var list = new List<ICard>();
            list.Add(CardFactory.CreateCard(CardValue.Queen, CardType.Diamonds));
            list.Add(CardFactory.CreateCard(CardValue.Ten, CardType.Spades));
            list.Add(CardFactory.CreateCard(CardValue.Four, CardType.Spades));
            list.Add(CardFactory.CreateCard(CardValue.Five, CardType.Diamonds));
            list.Add(CardFactory.CreateCard(CardValue.Ace, CardType.Clubs));
            list.Add(CardFactory.CreateCard(CardValue.King, CardType.Diamonds));
            list[4].SpecialRule = "the Joker";
            foreach (var card in list)
            {
                Console.WriteLine(card.GetCardName());
            }
            //var sorted = list.GroupBy(c => c.CardType).OrderBy(g => g.Count());
            //Console.WriteLine(sorted.First().Key);
            //var sorted = list.GroupBy(c => c.CardType).OrderBy(g => g.Count());
            //Console.WriteLine(sorted.First().Key);
            var sorted = list
                .GroupBy(x => x.SpecialRule == "the Joker")
                .Select(x => new
                {
                    Cards = x.GroupBy(c => c.CardType).OrderBy(c => c.Count()),
                    Count = x.Count(),
                })
                .OrderByDescending(x => x.Count)
                .SelectMany(x => x.Cards);
            Console.WriteLine();
            Console.WriteLine(sorted.First().Key);//*/
    }
}
