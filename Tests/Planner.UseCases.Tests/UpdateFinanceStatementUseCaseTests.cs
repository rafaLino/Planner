using Moq;
using Planner.Application.Commands.UpdateFinanceStatement;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class UpdateFinanceStatementUseCaseTests
    {
        private Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;
        private Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private IUpdateFinanceStatementUseCase _updateFinanceStatementUseCase;
        public UpdateFinanceStatementUseCaseTests()
        {
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();
            _updateFinanceStatementUseCase = new UpdateFinanceStatementUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_Update_Expense()
        {
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();
            string name = "educação";

            Account account = AccountBuilder
                                        .New
                                        .WithId(accountId)
                                        .WithExpenses(expenseId)
                                        .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));

            await _updateFinanceStatementUseCase.Execute<Expense>(accountId, expenseId, name);

            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
            _accountWriteOnlyRepository.Verify(x => x.Update(account, It.IsAny<IFinanceStatement>()), Times.Once);
        }

        [Fact]
        public void Should_Throw_Exception_Given_Nonexistent_Account()
        {
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account))
                .Verifiable();

            Assert.ThrowsAsync<AccountNotFoundException>(() => _updateFinanceStatementUseCase.Execute<Expense>(accountId, expenseId, "educação"));

            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
        }

        [Fact]
        public void Should_Throws_Exception_Given_Existed_Title()
        {
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();
            string name = "aluguel";

            Account account = AccountBuilder
                                        .New
                                        .WithId(accountId)
                                        .WithExpenses(title: name)
                                        .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));

            Assert.ThrowsAsync<FinanceStatementAlreadyExistsException>(() => _updateFinanceStatementUseCase.Execute<Expense>(accountId, expenseId, name));

            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
            _accountWriteOnlyRepository.Verify(x => x.Update(account, It.IsAny<IFinanceStatement>()), Times.Never);
        }
    }
}
