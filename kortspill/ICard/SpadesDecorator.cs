using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Decorator Design Pattern
    public class SpadesDecorator : CardDecorator
    {
        public SpadesDecorator(ICard card) 
            : base(card)
        {
            Suit = Suit.Spades;
        }
    }
}
