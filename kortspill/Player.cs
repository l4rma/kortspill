using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Channels;


namespace Kortspill
{
    public class Player : IPlayer
    {
        public List<ICard> Hand { get; } = new List<ICard>();
        public string Name { get; }

        public bool Winner { get; set; } = false;
        public int MaxHandSize { get; set; } = HandSize.GetState().Max; // Singleton
        public bool IsQuarantined { get; set; } = false;
        public int ExtraCards { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public void Play() 
        {
            while (!GameManager.GameOver) 
            {
                RequestCard();
                DiscardUnwantedCard();
                GameManager.CheckIfWinner(this);
            }
        }

        private ICard WhatToGiveAway() 
        {
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

        public void DiscardUnwantedCard()
        {
            if (Hand.Count <= MaxHandSize + ExtraCards) return;
            ICard card = WhatToGiveAway();
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
