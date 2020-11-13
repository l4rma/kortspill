using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    //INFO: Decorator Design Pattern
    internal class ClubsDecorator : CardDecorator
    {
        public ClubsDecorator(ICard card)
            : base(card)
        {
            Suit = Suit.Clubs;
        }
    }
}