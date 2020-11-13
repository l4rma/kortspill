using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal class HeartsDecorator : CardDecorator
    {
        public HeartsDecorator(ICard card)
            : base(card)
        {
            Suit = Suit.Hearts;
        }
    }
}
