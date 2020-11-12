using System.Dynamic;

namespace kortspill
{
    public interface ICard
    {
        CardType CardType { get; set; }
        CardValue CardValue { get; }
        string SpecialRule { get; set; }
        public string GetCardName();

    }
}