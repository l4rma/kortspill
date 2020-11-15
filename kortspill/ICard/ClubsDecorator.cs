using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Decorator Design Pattern
    public class ClubsDecorator : CardDecorator
    {
        public ClubsDecorator(ICard card)
            : base(card)
        {
            Suit = Suit.Clubs;
        }
    }
}