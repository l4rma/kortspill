using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    public abstract class CardDecorator : ICard
    {
        private readonly ICard _card;
        protected CardType _cardType;
        private readonly CardValue _cardValue;

        protected CardDecorator(ICard card)
        {
            _card = card;
        }

        public string getCardName()
        {
            return _card.getValue() + " of " + _cardType;
        }

        public CardType getType()
        {
            return _cardType;
        }

        public virtual string SpecialRule()
        {
            return _card.SpecialRule();
        }

        public CardValue getValue()
        {
            return _card.getValue();
        }
    }
}
