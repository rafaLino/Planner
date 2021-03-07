using Planner.Domain.ValueObjects;
using Planner.Domain.ValueObjects.Exceptions;
using Xunit;

namespace Planner.Domain.Tests
{
    public class TitleTests
    {

        [Fact]
        public void Should_Throw_Exception_Given_Empty_Title()
        {
            Assert.Throws<TitleShouldNotBeEmptyException>(() => new Title(string.Empty));
        }

        [Fact]
        public void Should_Title_Be_Created()
        {
            Title title = "Despesas váriaveis";

            Assert.Equal("Despesas váriaveis", title);
        }

        [Fact]
        public void Should_Titles_Be_Equals_With_Or_Withouts_Accents()
        {
            Title titleOne = "Despesas váriaveis";
            Title titleTwo = new Title("Despesãs variávêís");
            Title titleSameReference = titleOne;

            bool assertionOne = titleOne.Equals(titleTwo);
            bool assertionTwo = titleOne == titleTwo;
            bool assertionThree = titleOne.Equals(titleSameReference);
            bool assertionFour = titleOne.GetHashCode() == titleSameReference.GetHashCode();

            Assert.True(assertionOne);
            Assert.True(assertionTwo);
            Assert.True(assertionThree);
            Assert.True(assertionFour);
        }

        [Fact]
        public void Should_Title_Not_Be_Equals()
        {
            Title titleOne = "Despesas fixas";
            Title titleTwo = "Despesas váriaveis";
            Title titleThree = null;
            string titleString = titleTwo.ToString(); 
           

            bool assertionOne = titleOne != titleTwo;
            bool assertionTwo = titleOne.Equals(titleTwo);
            bool assertionThree = titleOne.Equals(titleThree);
            bool assertionFour = titleOne.Equals(titleString);
           

            Assert.True(assertionOne);

            Assert.False(assertionTwo);
            Assert.False(assertionThree);
            Assert.False(assertionFour);


        }
    }
}
