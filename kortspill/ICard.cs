using System.Dynamic;

namespace kortspill
{
    internal interface ICard
    {
        public string getCardName();
        
        public string getValue();

        public string getType();
    }
}