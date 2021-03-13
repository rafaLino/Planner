using Planner.Domain.ValueObjects;
using Xunit;

namespace Planner.Domain.Tests
{
    public class PasswordTests
    {

        [Fact]
        public void Should_Create_New_Password()
        {
            string word = "abacuque";
            Password password = Password.Create(word);

            Assert.NotNull(password);
        }

        [Fact]
        public void Should_Password_Valid()
        {
            string word = "abacuque";
            Password password = Password.Create(word);

            Assert.True(password.Verify(word));
        }
    }
}
