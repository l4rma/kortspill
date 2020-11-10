using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace kortspill
{
    class Player : IPlayer
    {
        private ArrayList hand = new ArrayList();

        public void requestCard()
        {
            throw new NotImplementedException();
        }

        public void discardCard()
        {
            throw new NotImplementedException();
        }

        public ArrayList getHand()
        {
            return hand;
        }
    }
}
