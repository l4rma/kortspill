using System;
using System.Collections.Generic;
using System.Text;
using Kortspill;
using NUnit.Framework;

namespace NUnitTests {
	class CardTests {
		[Test]
		public void OperatorTest()
		{
			Card card = new Card(Value.Eight);
			Card card2 = new Card(Value.Eight);
			Card card3 = new Card(Value.Eight);
			Card card4 = new Card(Value.Eight);
			card.Suit = Suit.Spades;
			card2.Suit = Suit.Spades;
			card3.Suit = Suit.Hearts;
			card4.Suit = Suit.Diamonds;
			Assert.IsTrue(card == card2);
			Assert.IsFalse(card3 == card4);
			Assert.IsFalse(card == card4);

		}
	}
}
