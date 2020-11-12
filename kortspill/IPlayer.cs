using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal interface IPlayer
    {
        public void RequestCard();
        public void DiscardCard(ICard card);
        public List<ICard> GetHand();
    }
}
