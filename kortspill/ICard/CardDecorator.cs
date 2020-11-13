using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    public abstract class CardDecorator : ICard
    {
        private readonly ICard _card;
        public CardType CardType { get; set; }

        public CardValue CardValue => _card.CardValue;

        public string SpecialRule { get; set; } = null;

        protected CardDecorator(ICard card)
        {
            _card = card;
        }

        public string GetCardName()
        {
            if (SpecialRule != null) return CardValue + " of " + CardType + " (" + SpecialRule + ")";
            return CardValue + " of " + CardType;
        }
    }
}
