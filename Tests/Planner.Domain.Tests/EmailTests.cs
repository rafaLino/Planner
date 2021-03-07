using Planner.Domain.ValueObjects;
using Planner.Domain.ValueObjects.Exceptions;
using Xunit;

namespace Planner.Domain.Tests
{
    public class EmailTests
    {

        [Fact]
        public void Should_Throw_Exception_Given_Empty_Email()
        {
            Assert.Throws<EmailShouldNotBeEmptyException>(() => new Email(string.Empty));
        }

        [Fact]
        public void Should_Throw_Exception_Given_Invalid_Email()
        {
            Assert.Throws<EmailInvalidException>(() => new Email("email super invalid!"));

        }

        [Fact]
        public void Should_Email_Be_Created()
        {
            string expected = "emailWithValidPattern@gmail.com";
            Email email = expected;
            string emailAsString = email;

            Assert.NotNull(email);
            Assert.Equal(expected, email.ToString());
            Assert.NotEmpty(emailAsString);
        }

        [Fact]
        public void Should_Email_Be_Equals()
        {
            string email = "emailWithValidPattern@outlook.com.br";
            Email emailOne = email;
            Email emailTwo = new Email("emailWithValidPattern@outlook.com.br");
            Email emailThree = emailOne;

            Assert.True(emailOne.Equals(emailTwo));
            Assert.True(emailOne.Equals(emailThree));
            Assert.True(emailOne.Equals(email));
            Assert.True(emailOne.GetHashCode() == emailThree.GetHashCode());
        }

        [Fact]
        public void Should_Email_Not_Be_Equals()
        {
            Email emailOne = "emailWithValidPattern@gmail.com";
            Email emailTwo = "AnotherEmailValid@gmail.com";
            string emailThree = null;


            Assert.False(emailOne.Equals(emailTwo));
            Assert.False(emailOne.Equals(emailThree));
        }


    }
}
