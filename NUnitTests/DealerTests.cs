using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kortspill;
using NUnit.Framework;

namespace NUnitTests {
	public class DealerTests {
		//Dealer.DealTopCard, Dealer.Shuffle gets tested every time we instanciate a dealer
		//By running DealStartingHandsTest sevral times we can see that shuffle works
		[Test, Order(1)]
		public void InitDealerTest()
		{
			Dealer d = new Dealer();
			Assert.IsTrue(Dealer.Deck.Any());
			Assert.IsTrue(Dealer.Deck.Count == 52);
		}

		[Test]
		public void DealStartingHandsTest()
		{
			Player p = new Player("Kjell");
			Dealer d = new Dealer();
			GameManager.Players.Add(p);
			Dealer.DealStartingHands();
			Assert.IsTrue(p.Hand.Count==4);
		}

		[Test]
		public void AddStartingCardsToPlayerDeckTest()
		{
			Player p = new Player("Kjell");
			Dealer d = new Dealer();
			Dealer.AddStartingCardsToPlayerDeck(p,2);
			Assert.IsTrue(p.Hand.Count==2);
		}

		[Test]
		public void ReturnRandomCardFromDeck()
		{
			Dealer d = new Dealer();
			ICard c = Dealer.ReturnRandomCardFromDeck();
			Assert.IsTrue(c.Value.ToString().Length>0);
			Assert.IsTrue(c.Suit.ToString().Length>0);
		}
	}
}
