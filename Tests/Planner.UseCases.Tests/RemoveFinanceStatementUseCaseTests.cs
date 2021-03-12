using Moq;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using System.Threading.Tasks;
using System;
using Xunit;
using Planner.Domain.Accounts;
using Planner.Application.Commands.RemoveFinanceStatement;
using System.Linq;

namespace Planner.UseCases.Tests
{
    public class RemoveFinanceStatementUseCaseTests
    {

        private Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;

        private Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;

        private IRemoveFinanceStatementUseCase _removeUseCase;

        public RemoveFinanceStatementUseCaseTests()
        {
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();
            _removeUseCase = new RemoveFinanceStatementUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public void Should_Throws_Exception_Given_Nonexistent_Account()
        {
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account));

            Assert.ThrowsAsync<AccountNotFoundException>(() => _removeUseCase.Execute<Expense>(accountId, expenseId));

            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
        }

        [Fact]
        public async Task Should_Remove_Expense()
        {
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();
            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithExpenses(expenseId, 734.14m, 1284.98m)
                                    .WithIncomes(10119.63m)
                                    .WithInvestments(7015m)
                                    .Build();

            decimal expectedExpenseTotal = 1284.98m;
            double expectedTotalExpensesPercentage = 12.70;
            double expectedTotalInvestmentsPercentage = 69.32;

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(account, It.IsAny<IFinanceStatement>()))
                .Verifiable();

            var result = await _removeUseCase.Execute<Expense>(accountId, expenseId);

            Assert.Equal(expectedExpenseTotal, result.Expense.Total);
            Assert.Equal(expectedTotalExpensesPercentage, Math.Round(result.Expense.Percentage * 100, 2));
            Assert.Equal(expectedTotalInvestmentsPercentage, Math.Round(result.Investment.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Remove_Income()
        {
            Guid accountId = Guid.NewGuid();
            Guid IncomeId = Guid.NewGuid();
            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes()
                                    .WithIncomes(IncomeId, 300, 600, 800)
                                    .WithInvestments()
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(account, It.IsAny<IFinanceStatement>()))
                .Verifiable();

            decimal expectedTotal = 1_400m;

            var result = await _removeUseCase.Execute<Income>(accountId, IncomeId);

            Assert.Equal(2, account.Incomes.GetFinanceStatements().Count());
            Assert.Equal(expectedTotal, result.Income.Total);

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Remove_Investment()
        {
            Guid accountId = Guid.NewGuid();
            Guid InvestmentId = Guid.NewGuid();
            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes()
                                    .WithInvestments(InvestmentId, 5)
                                    .WithExpenses()
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(account, It.IsAny<IFinanceStatement>()))
                .Verifiable();

            await _removeUseCase.Execute<Investment>(accountId, InvestmentId);

            Assert.Equal(4, account.Investments.GetFinanceStatements().Count());

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }
    }
}
