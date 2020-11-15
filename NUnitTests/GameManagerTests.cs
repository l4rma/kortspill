using System;
using System.Collections.Generic;
using System.Text;
using Kortspill;
using NUnit.Framework;

namespace NUnitTests {
	class GameManagerTests {
		[Test]
		public void CheckStarterHandTest()
		{
			//the point with this method is to check if a player starts the game with a winning hand
			Player p = new Player("Kåre");
			Player p2 = new Player("Sebastian");
			Dealer d = new Dealer();
			
			ICard heart = new Card(Value.Four);
			ICard heart2 = new Card(Value.Five);
			ICard heart3 = new Card(Value.Four);
			ICard heart4 = new Card(Value.Four);
			ICard spades = new Card(Value.Four);
			
			heart.Suit = Suit.Hearts;
			heart2.Suit = Suit.Hearts;
			heart3.Suit = Suit.Hearts;
			heart4.Suit = Suit.Hearts;
			spades.Suit = Suit.Spades;
			
			p.Hand.Add(heart);
			p.Hand.Add(heart2);
			p.Hand.Add(heart3);
			p.Hand.Add(heart4);
			p2.Hand.Add(heart);
			p2.Hand.Add(heart2);
			p2.Hand.Add(heart3);
			p2.Hand.Add(spades);
			GameManager.CheckStarterHand(p);
			GameManager.CheckStarterHand(p2);

			Assert.IsFalse(p.Hand[0].Suit.Equals(Suit.Hearts)
							&& p.Hand[1].Suit.Equals(Suit.Hearts)
							&& p.Hand[2].Suit.Equals(Suit.Hearts)
			                && p.Hand[3].Suit.Equals(Suit.Hearts)
							);
			Assert.IsTrue(p2.Hand[0].Suit.Equals(Suit.Hearts)
			              && p2.Hand[1].Suit.Equals(Suit.Hearts)
			              && p2.Hand[2].Suit.Equals(Suit.Hearts)
			              && p2.Hand[3].Suit.Equals(Suit.Spades)
							);
		}

		[Test]
		public void CheckIfWinnerTest()
		{
			Player p = new Player("Kåre");
			Dealer d = new Dealer();
			ICard heart = new Card(Value.Four);
			ICard heart2 = new Card(Value.Five);
			ICard heart3 = new Card(Value.Four);
			ICard heart4 = new Card(Value.Four);
			heart.Suit = Suit.Hearts;
			heart2.Suit = Suit.Hearts;
			heart3.Suit = Suit.Hearts;
			heart4.Suit = Suit.Hearts;
			p.Hand.Add(heart);
			p.Hand.Add(heart2);
			p.Hand.Add(heart3);
			p.Hand.Add(heart4);

			GameManager.CheckIfWinner(p);
			Assert.IsTrue(GameManager.GameOver);
			//also tests GameManager.EndGame()
		}

		[Test]
		public void HasWinningHand()
		{
			Player p = new Player("Kåre");
			Player p2 = new Player("Sebastian");
			Dealer d = new Dealer();
			ICard heart = new Card(Value.Four);
			ICard heart2 = new Card(Value.Five);
			ICard heart3 = new Card(Value.Four);
			ICard heart4 = new Card(Value.Four);
			ICard spades = new Card(Value.Four);
			heart.Suit = Suit.Hearts;
			heart2.Suit = Suit.Hearts;
			heart3.Suit = Suit.Hearts;
			heart4.Suit = Suit.Hearts;
			spades.Suit = Suit.Spades;
			p.Hand.Add(heart);
			p.Hand.Add(heart2);
			p.Hand.Add(heart3);
			p.Hand.Add(heart4);	
			p2.Hand.Add(heart);
			p2.Hand.Add(heart2);
			p2.Hand.Add(heart3);
			p2.Hand.Add(spades);
			Assert.IsTrue(GameManager.HasWinningHand(p));
			Assert.IsFalse(GameManager.HasWinningHand(p2));
		}
	}
}
