using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal interface IPlayer
    {
        public bool IsQuarantined { get; set; }
        public void RequestCard();
        public void DiscardUnwantedCard(ICard card);
        public List<ICard> Hand { get; }
    }
}
