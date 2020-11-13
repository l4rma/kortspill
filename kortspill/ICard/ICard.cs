using System.Dynamic;

namespace kortspill
{
    public interface ICard
    {
        Suit Suit { get; set; }
        Value Value { get; }
        string SpecialRule { get; set; }
        public string GetCardName();

    }
}