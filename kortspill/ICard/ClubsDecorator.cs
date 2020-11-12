using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal class ClubsDecorator : CardDecorator
    {
        public ClubsDecorator(ICard card)
            : base(card)
        {
            CardType = CardType.Clubs;
        }
    }
}