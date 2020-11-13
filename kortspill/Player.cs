using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Channels;


namespace kortspill
{
    internal class Player : IPlayer
    {
        public List<ICard> Hand { get; } = new List<ICard>();
        public string Name { get; }

        public bool Winner = false;
        public int MaxHandSize = HandSize.GetState().Max;  // Singleton in action TODO: Ikker riktig implementert.
        public bool IsQuarantined { get; set; } = false;
        public int ExtraCards { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public void Play() //INFO: Facade Design Pattern
        {
            while (!GameManager.GameOver) 
            {
                RequestCard();
                DiscardUnwantedCard(WhatToGiveAway());
                GameManager.CheckIfWinner(this);
            }
        }

        private ICard WhatToGiveAway() //TODO: ITS NOT WORKING! FIX IT!
        {
            /* Console log hands before discarding
            Console.WriteLine("Hand:");
            foreach (var card in _hand)
            {
                Console.WriteLine(card.GetCardName());
            }
            */
            
            var sorted = Hand
                .GroupBy(x => x.SpecialRule == "the Joker")
                .Select(x => new
                {
                    Cards = x.GroupBy(c => c.Suit).OrderBy(c => c.Count()),
                    Count = x.Count(),
                })
                .OrderByDescending(x => x.Count)
                .SelectMany(x => x.Cards);
            foreach (var card in Hand)
            {
                if (card.Suit == sorted.First().Key)
                {
                    /* Checking what card to give away
                     * Console.WriteLine("\nwhat to give away: " + card.GetCardName());
                     */
                    return card;
                }
            }

            // Should never go here but Visual Studio don't believe me... 
            Console.WriteLine("Error: Can't find card to discard, discarding first card in hand..");
            return Hand[0];
        }


        public int Count(Suit cardType)
        {
            int num = 0;
            foreach (var card in Hand.Where(c => c.SpecialRule != "the Joker"))
            {
                if (card.Suit == cardType) num++;
            }
            return num;
        }

        public void RequestCard()
        {
            Dealer.AcceptCardRequest(this);
        }

        public void DiscardUnwantedCard(ICard card)
        {
            if (Hand.Count <= MaxHandSize + ExtraCards) return;
            Dealer.Deck.Add(card);
            Console.WriteLine(Name + " discarded " + card.GetCardName());
            Hand.Remove(card);
        }

        public void DiscardHand()
        {
            foreach (var card in Hand) Dealer.Deck.Add(card);
            Hand.Clear();
            Console.WriteLine(Name + " discards all cards from hand.");
        }
    }
}
