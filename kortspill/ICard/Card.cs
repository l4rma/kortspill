namespace Kortspill
{
    public class Card : ICard

    {
        public Value Value { get; }
        public Suit Suit { get; set; }
        public string SpecialRule { get; set; } = null;

        public Card(Value value)
        {
            Value = value;
        }

        public static bool operator ==(Card card1, Card card2)
        {
            return card1.Suit == card2.Suit;
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return card1.Suit != card2.Suit;
        }

        public string GetCardName()
        {
            if(SpecialRule != null) return Value + " of " + Suit + " (" + SpecialRule + ")";
            return Value + " of " + Suit;
        }
    }
}
