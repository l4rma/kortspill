using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    public abstract class CardDecorator : ICard
    {
        private readonly ICard _card;
        public Suit Suit { get; set; }

        public Value Value => _card.Value;

        public string SpecialRule { get; set; } = null;

        protected CardDecorator(ICard card)
        {
            _card = card;
        }

        public string GetCardName()
        {
            if (SpecialRule != null) return Value + " of " + Suit + " (" + SpecialRule + ")";
            return Value + " of " + Suit;
        }
    }
}
