using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    class Card : ICard
    {
        private CardType type;
        private CardValue value;

        public Card(CardValue value, CardType type)
        {
            this.value = value;
            this.type = type;
        }

        public string getCardName()
        {
            return value + " of " + type;
        }

        public string getValue()
        {
            return value.ToString();
        }

        public CardType getType()
        {
            return type;
        }
    }
}
