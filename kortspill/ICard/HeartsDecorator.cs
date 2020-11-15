using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Decorator Design Pattern
    internal class HeartsDecorator : CardDecorator
    {
        public HeartsDecorator(ICard card)
            : base(card)
        {
            Suit = Suit.Hearts;
        }
    }
}
