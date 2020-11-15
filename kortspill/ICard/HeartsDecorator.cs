using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Decorator Design Pattern
    public class HeartsDecorator : CardDecorator
    {
        public HeartsDecorator(ICard card)
            : base(card)
        {
            Suit = Suit.Hearts;
        }
    }
}
