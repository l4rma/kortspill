using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;


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
                Thread.Sleep(50);
            }
        }

        private ICard WhatToGiveAway() //TODO: ITS NOT WORKING! FIX IT!
        {
            var cardToReturn = _hand[0];
            var tempList = _hand
                .OrderBy(ICard => ICard.SpecialRule == "the Joker") // Sett Jokern sist
                .ThenByDescending(ICard => ICard.CardType.ToString());
            foreach (var card in _hand)
            {
                if (card.GetCardName() == tempList.First().GetCardName())
                {
                    cardToReturn = card;
                }

            }

            return cardToReturn;
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
