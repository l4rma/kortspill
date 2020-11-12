using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal class DiamondsDecorator : CardDecorator
    {
        public DiamondsDecorator(ICard card)
            : base(card)
        {
            CardType = CardType.Diamonds;
        }
    }
}