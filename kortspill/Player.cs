using System;
using System.Collections;
using System.Collections.Generic;
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
                //checkForSpecialCards();
                DiscardCard(WhatToGiveAway());
                CheckForWin();
                Thread.Sleep(100);

            }

        }

        private ICard WhatToGiveAway()
        {
            foreach (var card in _hand.Where(card => Count(card.getType()) < 2))
                return card;

            return _hand[0];
        }

        private void CheckForWin()
        {
            //change to switch statement?
            if (Count(CardType.Spades) <= 3 && Count(CardType.Diamonds) <= 3 && Count(CardType.Hearts) <= 3 &&
                Count(CardType.Clubs) <= 3) return;
            Winner = true;
            GameManager.EndGame();

        }

        private int Count(CardType cardType)
        {
            return _hand.Count(card => card.getType() == cardType);
        }

        private void CheckForSpecialCards()
        {
            throw new NotImplementedException();
        }


        public void RequestCard()
        {
            Dealer.DealTopCard(this);
        }

        public void DiscardCard(ICard card)
        {
            if (GetHandSize() <= 4) return;
            Dealer.Deck.Add(card);
            Console.WriteLine(this.GetName() + " discarded " + card.getCardName());
            _hand.Remove(card);
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
    }
}
