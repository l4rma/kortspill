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
                discardCard(whatToGiveAway());
                checkForWin();
                Thread.Sleep(100);

            }

        }

        private Card whatToGiveAway()
        {
            foreach (Card card in hand)
            {
                if (count(card.getType()) < 2)
                {
                    return card;
                }
            }
            return hand[0];
        }

        private void checkForWin()
        {
            if (count(CardType.Spades) > 3 || count(CardType.Diamonds) > 3 || count(CardType.Hearts) > 3 || count(CardType.Clubs) > 3)
            {
                winner = true;
                GameManager.endGame();
            }
            
        }

        private int count(CardType cardtype)
        {
            int amount = 0;
            foreach (Card card in hand)
            {
                if (card.getType() == cardtype)
                {
                    amount++;
                }
                
            }

            return amount;
        }

        private void checkForSpecialCards()
        {
            throw new NotImplementedException();
        }


        public void requestCard()
        {
            Deck.DealTopCard(this);
        }

        public void discardCard(Card card)
        {
            if (getHandSize() > 4)
            {
                Deck.deck.Add(card);
                Console.WriteLine(this.getName() + " discarded " + card.getCardName());
                hand.Remove(card);
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
