namespace kortspill
{
    internal class Card : ICard

    {
        public CardValue CardValue { get; }
        public CardType CardType { get; set; }
        public string SpecialRule { get; set; } = null;

        public Card(CardValue cardValue)
        {
            CardValue = cardValue;
        }

        public string GetCardName()
        {
            if(SpecialRule != null) return CardValue + " of " + CardType + " (" + SpecialRule + ")";
            return CardValue + " of " + CardType;
        }
    }
}
