namespace kortspill
{
    internal class Card : ICard

    {
        private readonly CardValue _cardValue;
        private readonly CardType _cardType; 

        public Card(CardValue cardValue)
        {
            _cardValue = cardValue;
        }

        public string getCardName()
        {
            return _cardValue + " of " + getType();
        }


        public CardType getType()
        {
            return _cardType;
        }

        public string SpecialRule()
        {
            return "No special rule";
        }

        public CardValue getValue()
        {
            return _cardValue;
        }
    }
}
