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
        private readonly List<ICard> _hand = new List<ICard>();
        private readonly string _name;
        public bool Winner = false;
        private int _maxHandSize = 4;
        private readonly object _lock = new object();
        public bool IsQuarantined { get; set; } = false;

        public Player(string name)
        {
            this._name = name;
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
            
            var sorted = _hand
                .GroupBy(x => x.SpecialRule == "the Joker")
                .Select(x => new
                {
                    Cards = x.GroupBy(c => c.CardType).OrderBy(c => c.Count()),
                    Count = x.Count(),
                })
                .OrderByDescending(x => x.Count)
                .SelectMany(x => x.Cards);
            foreach (var card in _hand)
            {
                if (card.CardType == sorted.First().Key)
                {
                    /* Checking what card to give away
                     * Console.WriteLine("\nwhat to give away: " + card.GetCardName());
                     */
                    return card;
                }
            }

            // Should never go here but Visual Studio don't believe me... 
            Console.WriteLine("Error: Can't find card to discard, discarding first card in hand..");
            return _hand[0];
        }


        public int Count(CardType cardType)
        {
            return _hand.Count(card => card.CardType == cardType);
        }

        public void RequestCard()
        {
            Dealer.AcceptCardRequest(this);
        }

        public void DiscardUnwantedCard(ICard card)
        {
            if (GetHandSize() <= _maxHandSize) return;
            Dealer.Deck.Add(card);
            Console.WriteLine(this.GetName() + " discarded " + card.GetCardName());
            _hand.Remove(card);
        }

        public void DiscardHand()
        {
            foreach (var card in _hand)
            {
                Dealer.Deck.Add(card);
            }
            _hand.Clear();
            Console.WriteLine(this.GetName() + " discards all cards from hand.");
        }

        public List<ICard> GetHand()
        {
            return _hand;
        }

        public string GetName()
        {
            return this._name;
        }

        public int GetHandSize()
        {
            return _hand.Count;
        }

        public void SetMaxHandSize(int maxHandSize)
        {
            _maxHandSize = maxHandSize;
        }

        public int GetMaxHandSize()
        {
            return _maxHandSize;
        }
    }
}
