using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace kortspill
{
    class Player : IPlayer
    {
        public ArrayList hand = new ArrayList();

        public void requestCard(Deck deck)
        {
            deck.DealTopCard(this);
        }

        public void discardCard()
        {
            throw new NotImplementedException();
        }

        public ArrayList getHand()
        {
            return hand;
        }
    }
}
