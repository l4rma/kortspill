using System.Dynamic;

namespace kortspill
{
    public interface ICard
    {
        public string getCardName();

        public CardType getType();

        public string SpecialRule();

        public CardValue getValue();
    }
}