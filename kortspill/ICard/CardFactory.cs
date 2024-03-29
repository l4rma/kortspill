﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kortspill
{
    //INFO: Factory Design Pattern
    public class CardFactory
    {
        public static ICard CreateCard(Value value, Suit type)
        {
            ICard card;
            switch (type)
            {
                case Suit.Spades:
                    card = new SpadesDecorator(new Card(value));
                    break;
                case Suit.Clubs:
                    card = new ClubsDecorator(new Card(value));
                    break;
                case Suit.Hearts:
                    card = new HeartsDecorator(new Card(value));
                    break;
                case Suit.Diamonds:
                    card = new DiamondsDecorator(new Card(value));
                    break;
                default: //Should never happen
                    card = new Card(value);
                    Console.WriteLine("ERROR: Card has not set type and will be clubs...");
                    break;
            }
            return card;
        }
    }
}
