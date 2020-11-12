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
        public bool IsQuarantined { get; set; } = false;

        public Player(string name)
        {
            this._name = name;
        }

        public void Play()
        {
            while (!GameManager.GameOver)
            //for(int i = 0; i<5; i++)
            {
                RequestCard();
                DiscardUnwantedCard(WhatToGiveAway());
                CheckForWin(); // TODO: FLytt til GameManager
                Thread.Sleep(500);

            }

        }

        private ICard WhatToGiveAway()
        {
            foreach (var card in _hand.Where(card => Count(card.CardType) < 2))
                return card;

            return _hand[0];
        }

        private void CheckForWin()
        {
            if (HasWinningHand())
            {
                Winner = true;
                GameManager.EndGame();
            }
        }

        public bool HasWinningHand()
        {
            return Count(CardType.Spades) > 3 || Count(CardType.Diamonds) > 3 || Count(CardType.Hearts) > 3 ||
                   Count(CardType.Clubs) > 3;
        }

        private int Count(CardType cardType)
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
