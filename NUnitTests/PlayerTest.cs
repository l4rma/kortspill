using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using Kortspill;

namespace NUnitTests {
	public class Tests {
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void DiscardUnwantedCardTest()
		{
			Player p = new Player("Kjell");
			Player p2 = new Player("Kåre");
			Player p3 = new Player("Bjarne");
			ICard heart = new Card(Value.Four);
			ICard heart2 = new Card(Value.Five);
			ICard spades = new Card(Value.Four);
			ICard clubs = new Card(Value.Four);
			ICard diamonds = new Card(Value.Five);
			heart.Suit = Suit.Hearts;
			heart2.Suit = Suit.Hearts;
			spades.Suit = Suit.Spades;
			clubs.Suit = Suit.Clubs;
			diamonds.Suit = Suit.Clubs;
			p.Hand.Add(heart);
			p.Hand.Add(heart2);
			p.Hand.Add(spades);
			p.Hand.Add(clubs);
			p.Hand.Add(diamonds);
			
			p2.Hand.Add(CardFactory.CreateCard(Value.Ace, Suit.Clubs));
			p2.Hand.Add(CardFactory.CreateCard(Value.King, Suit.Clubs));
			p2.Hand.Add(CardFactory.CreateCard(Value.Jack, Suit.Diamonds));
			p2.Hand.Add(CardFactory.CreateCard(Value.Two, Suit.Spades));
			p2.Hand.Add(CardFactory.CreateCard(Value.Three, Suit.Spades));			
			
			p3.Hand.Add(CardFactory.CreateCard(Value.Ace, Suit.Clubs));
			p3.Hand.Add(CardFactory.CreateCard(Value.King, Suit.Clubs));
			p3.Hand.Add(CardFactory.CreateCard(Value.Jack, Suit.Spades));
			p3.Hand.Add(CardFactory.CreateCard(Value.Two, Suit.Spades));
			p3.Hand.Add(CardFactory.CreateCard(Value.Three, Suit.Spades));


			p.DiscardUnwantedCard();
			p2.DiscardUnwantedCard();
			p3.DiscardUnwantedCard();

			Assert.IsFalse(p.Hand.Contains(spades) && p.Hand.Contains(clubs) && p.Hand.Contains(diamonds));
			Assert.IsTrue(p2.Hand[0].Suit.ToString().Equals("Clubs") 
			              && p2.Hand[1].Suit.ToString().Equals("Clubs")
						  && p2.Hand[2].Suit.ToString().Equals("Spades")
						  && p2.Hand[3].Suit.ToString().Equals("Spades")
							);			
			Assert.IsTrue(p3.Hand[0].Suit.ToString().Equals("Clubs") 
			              && p3.Hand[1].Suit.ToString().Equals("Spades")
						  && p3.Hand[2].Suit.ToString().Equals("Spades")
						  && p3.Hand[3].Suit.ToString().Equals("Spades")
							);
			
		}

		[Test]
		public void CountTest()
		{
			Player p = new Player("Kjell");
			ICard heart = new Card(Value.Four);
			ICard heart2 = new Card(Value.Five);
			ICard spades = new Card(Value.Four);
			ICard clubs = new Card(Value.Four);
			ICard diamonds = new Card(Value.Five);
			heart.Suit = Suit.Hearts;
			heart2.Suit = Suit.Hearts;
			spades.Suit = Suit.Spades;
			clubs.Suit = Suit.Clubs;
			diamonds.Suit = Suit.Clubs;
			p.Hand.Add(heart);
			p.Hand.Add(heart2);
			p.Hand.Add(spades);
			p.Hand.Add(clubs);
			p.Hand.Add(diamonds);

			int value = p.Count(Suit.Hearts);
			Assert.IsTrue(value==2);
		}

		[Test]
		public void DiscardHandTest()
		{
			Player p = new Player("Kjell");
			ICard heart = new Card(Value.Four);
			ICard heart2 = new Card(Value.Five);
			ICard spades = new Card(Value.Four);
			ICard clubs = new Card(Value.Four);
			ICard diamonds = new Card(Value.Five);
			heart.Suit = Suit.Hearts;
			heart2.Suit = Suit.Hearts;
			spades.Suit = Suit.Spades;
			clubs.Suit = Suit.Clubs;
			diamonds.Suit = Suit.Clubs;
			p.Hand.Add(heart);
			p.Hand.Add(heart2);
			p.Hand.Add(spades);
			p.Hand.Add(clubs);
			p.Hand.Add(diamonds);

			p.DiscardHand();
			Assert.IsFalse(p.Hand.Any());
		}

		[Test, Order(0)]
		public void RequestCardTest()
		{
			//denne testen kjører grønt når den kjører alene, men fordi Dealer er static beholder
			//den variablene fra de forrige testene
			Player p74 = new Player("Kjell");
			Dealer d = new Dealer();
			p74.RequestCard();
			Assert.IsTrue(p74.Hand.Any());
			//Also tests Dealer.AcceptCardRequest, Dealer.DealTopCard, Dealer.Shuffle, CardFactory.CreateCard
		}

	}

}