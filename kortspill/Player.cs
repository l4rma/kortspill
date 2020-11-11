using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading;

namespace kortspill
{
    class Player : IPlayer
    {
        public List<Card> hand = new List<Card>();
        private int name;
        public bool winner = false;

        public Player(int name)
        {
            this.name = name;
        }

        public void play()
        {
            while (!GameManager.gameOver)
            //for(int i = 0; i<5; i++)
            {
                requestCard();
                //checkForSpecialCards();
                discardCard();
                checkForWin();
                Thread.Sleep(100);

            }

        }

        private void checkForWin()
        {
            int spades = 0;
            foreach (Card card in hand)
            {
                if (card.getType() == CardType.Spades.ToString())
                {
                    spades++;
                }
            }

            if (spades > 2)
            {
                Console.WriteLine(this.getName() + " has 4 spades");
                winner = true;
                GameManager.endGame();
            }
            
        }

        private void checkForSpecialCards()
        {
            throw new NotImplementedException();
        }


        public void requestCard()
        {
            Deck.DealTopCard(this);
        }

        public void discardCard()
        {
            if (getHandSize() > 4)
            {
                Deck.deck.Add(hand[0]);
                Console.WriteLine(this.getName() + " discarded " + hand[0].getCardName());
                hand.RemoveAt(0);
            }
        }

        public List<Card> getHand()
        {
            return hand;
        }

        public int getName()
        {
            return this.name;
        }

        public int getHandSize()
        {
            return hand.Count;
        }
    }
}
