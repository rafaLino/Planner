using Planner.Domain.ValueObjects;
using Xunit;

namespace Planner.Domain.Tests
{
    public class AmountTests
    {
        [Fact]
        public void Should_Amount_Be_Created()
        {
            Amount amount = new Amount(3_500m);
            Amount amountTwo = 3_600;
            decimal amountThree = amount;

            Assert.NotNull(amount);
            Assert.NotNull(amountTwo);
            Assert.Equal(amount.ToString(), amountThree.ToString());
        }

        [Fact]
        public void Should_Amount_Be_Negativated()
        {
            decimal expected = -3_500m;
            Amount amount = 3_500;
            Amount amountNegative = -amount;

            Assert.Equal<decimal>(expected, amountNegative);
        }

        [Fact]
        public void Should_Amount_Be_Added()
        {
            decimal expected = 2_000;
            Amount amountOne = 1_000;
            Amount amountTwo = 1_000;

            var result = amountOne + amountTwo;

            Assert.Equal<decimal>(expected, result);
        }

        [Fact]
        public void Should_Amount_Be_Subtracted()
        {
            decimal expected = 2_000;
            Amount amountOne = 6_000;
            Amount amountTwo = 4_000;

            var result = amountOne - amountTwo;

            Assert.Equal<decimal>(expected, result);
        }

        [Fact]
        public void Should_Amount_Be_Multiplicated()
        {
            decimal expected = 400;
            Amount amountOne = 20;
            decimal amountTwo = 20;

            Amount result = amountOne * amountTwo;

            Assert.Equal<decimal>(expected, result);
        }
        [Fact]
        public void Should_Amount_Be_Greater_Or_Equal()
        {
            Amount amountOne = 2_000;
            Amount amountTwo = 1_000;
            Amount amountThree = 2_000;

            bool assertion = amountOne > amountTwo;
            bool assertionTwo = amountOne >= amountThree;

            Assert.True(assertion);
            Assert.True(assertionTwo);
        }

        [Fact]
        public void Should_Amount_Be_Lesser_Or_Equal()
        {
            Amount amountOne = 1_000;
            Amount amountTwo = 2_000;
            Amount amountThree = 1_000;

            bool assertion = amountOne < amountTwo;
            bool assertionTwo = amountOne <= amountThree;

            Assert.True(assertion);
            Assert.True(assertionTwo);
        }

        [Fact]
        public void Should_Amount_Be_Equals()
        {
            Amount amountOne = new Amount(1_000);
            Amount amountTwo = 1_000;
            Amount amountThree = amountOne;
            decimal amountFour = 1_000;


            Assert.True(amountOne.Equals(amountTwo));
            Assert.True(amountOne == amountTwo);
            Assert.True(amountOne.Equals(amountThree));
            Assert.True(amountOne.Equals(amountFour));
            Assert.Equal(amountOne.GetHashCode(), amountThree.GetHashCode());
        }

        [Fact]
        public void Should_Amount_Not_Be_Equals()
        {
            Amount amountOne = 1_000;
            Amount amountTwo = 2_000;
            Amount amountThree = null;
            decimal amountFour = 2_000;

            Assert.True(amountOne != amountTwo);

            Assert.False(amountOne.Equals(amountTwo));
            Assert.False(amountOne.Equals(amountThree));
            Assert.False(amountOne.Equals(amountFour));

        }


    }
}
