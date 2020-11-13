using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal class SpadesDecorator : CardDecorator
    {
        public SpadesDecorator(ICard card) 
            : base(card)
        {
            Suit = Suit.Spades;
        }
    }
}
