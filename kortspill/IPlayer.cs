using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    interface IPlayer
    {
        public void requestCard();
        public void discardCard(Card card);
        public List<Card> getHand();
    }
}
