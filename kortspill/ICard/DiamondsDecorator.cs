using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Decorator Design Pattern
    public class DiamondsDecorator : CardDecorator
    {
        public DiamondsDecorator(ICard card)
            : base(card)
        {
            Suit = Suit.Diamonds;
        }
    }
}