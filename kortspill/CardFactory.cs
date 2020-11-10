using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    class CardFactory
    {
        public static Card CreateCard(CardValue value, CardType type)
        {
            return new Card(value, type);
        }
    }
}
