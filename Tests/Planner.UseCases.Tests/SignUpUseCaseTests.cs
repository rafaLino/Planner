using Moq;
using Planner.Application.Commands.SignUp;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Users;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class SignUpUseCaseTests
    {
        private readonly Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private readonly Mock<IUserWriteOnlyRepository> _userWriteOnlyRepository;
        private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;
        private readonly ISignUpUseCase signUpUseCase;

        public SignUpUseCaseTests()
        {
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();
            _userWriteOnlyRepository = new Mock<IUserWriteOnlyRepository>();
            _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();

            signUpUseCase = new SignUpUseCase(_accountWriteOnlyRepository.Object, _userWriteOnlyRepository.Object, _userReadOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_Create_New_Account()
        {
            SignUpCommand command = new SignUpCommand
            {
                Email = "jubileu@gmail.com",
                Name = "Jubileu antunes",
                Password = "123456",
                Picture = null
            };

            _userReadOnlyRepository
                .Setup(x => x.Any(command.Email))
                .ReturnsAsync(false);

            _accountWriteOnlyRepository
                .Setup(x => x.Create(It.IsAny<Account>()));

            _userWriteOnlyRepository
                .Setup(x => x.Create(It.IsAny<User>()));

            SignUpResult result = await signUpUseCase.Execute(command);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.UserId);
            Assert.NotEqual(Guid.Empty, result.AccountId);
        }

        [Fact]
        public async Task Should_Throws_Exception_Given_Existent_Email()
        {
            SignUpCommand command = new SignUpCommand
            {
                Email = "jubileu@gmail.com",
                Name = "Jubileu antunes",
                Password = "123456",
                Picture = null
            };

            _userReadOnlyRepository
                .Setup(x => x.Any(command.Email))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<UserEmailAlreadyExistsException>(() => signUpUseCase.Execute(command));
        }

    }
}
