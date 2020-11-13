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
        public int MaxHandSize = 4;
        public bool IsQuarantined { get; set; } = false;

        public Player(string name)
        {
            Name = name;
        }

        public void Play()
        {
            while (!GameManager.GameOver)
            {
                RequestCard();
                DiscardUnwantedCard(WhatToGiveAway());
                GameManager.CheckIfWinner(this); //TODO: ITS NOT WORKING! FIX IT!
                //Thread.Sleep(1000); Wait before drawing again
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
            //return _hand.Count(card => card.Suit == cardType);
            return num;
        }

        public void RequestCard()
        {
            Dealer.AcceptCardRequest(this);
        }

        public void DiscardUnwantedCard(ICard card)
        {
            if (Hand.Count <= MaxHandSize) return;
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
