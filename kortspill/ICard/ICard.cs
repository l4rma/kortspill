using System.Dynamic;

namespace Kortspill
{
    public interface ICard
    {
        Suit Suit { get; set; }
        Value Value { get; }
        string SpecialRule { get; set; }
        public string GetCardName();
    }
}