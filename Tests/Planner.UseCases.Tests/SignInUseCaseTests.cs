using Moq;
using Planner.Application.Commands.SignIn;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Users;
using Planner.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class SignInUseCaseTests
    {
        private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;
        private readonly ISignInUseCase _signInUseCase;
        public SignInUseCaseTests()
        {
            _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
            _signInUseCase = new SignInUseCase(_userReadOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_SignIn_Into_Account()
        {
            Email email = "jubileu@gmail.com";
            string password = "lpw12354";

            Guid accountId = Guid.NewGuid();
            Guid id = Guid.NewGuid();


            User user = User.Load(accountId, id, "jubileu", email, Password.Create(password), null);

            _userReadOnlyRepository
                .Setup(x => x.Get(email))
                .ReturnsAsync(user);

            SignInResult result = await _signInUseCase.Execute(email, password);

            Assert.Equal(accountId, result.AccountId);
            Assert.Equal(id, result.UserId);
            Assert.Equal(user.Name, result.Name);
            Assert.NotNull(result.Token);

            _userReadOnlyRepository.Verify(x => x.Get(email), Times.Once);
        }

        [Fact]
        public async Task Should_Throws_Exception_Given_Not_Matched_Password()
        {
            Email email = "jubileu@gmail.com";
            string password = "lpw12354";

            Guid accountId = Guid.NewGuid();
            Guid id = Guid.NewGuid();


            User user = User.Load(accountId, id, "jubileu", email, Password.Create("anotherPassword"), null);

            _userReadOnlyRepository
                .Setup(x => x.Get(email))
                .ReturnsAsync(user);

            await Assert.ThrowsAsync<PasswordNotMatchException>(() => _signInUseCase.Execute(email, password));


            _userReadOnlyRepository.Verify(x => x.Get(email), Times.Once);
        }

        [Fact]
        public async Task Should_Throws_Exception_Given_NoExistent_Email()
        {
            Email email = "jubileu@gmail.com";
            string password = "lpw12354";

            _userReadOnlyRepository
                .Setup(x => x.Get(email))
                .ReturnsAsync(default(User));

            await Assert.ThrowsAsync<UserNotFoundException>(() => _signInUseCase.Execute(email, password));


            _userReadOnlyRepository.Verify(x => x.Get(email), Times.Once);
        }
    }
}
