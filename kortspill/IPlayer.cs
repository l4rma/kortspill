using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    interface IPlayer
    {
        public void requestCard();
        public void discardCard();
        public ArrayList getHand();
    }
}
