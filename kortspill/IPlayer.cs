using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    internal interface IPlayer
    {
        //Properties:
        public List<ICard> Hand { get; }
        public string Name { get; }
        public bool Winner { get; set; }
        public bool IsQuarantined { get; set; }
        public int MaxHandSize { get; set; }

        //Methods:
        public void RequestCard();
        public void DiscardUnwantedCard(ICard card);
        public void Play();
        public void DiscardHand();
        public int Count(Suit suit);
    }
}
