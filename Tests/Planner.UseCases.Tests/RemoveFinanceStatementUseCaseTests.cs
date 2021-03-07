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
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account));

            Assert.ThrowsAsync<AccountNotFoundException>(() => _removeUseCase.Execute<Expense>(accountId, expenseId));

            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
        }

        [Fact]
        public async Task Should_Remove_Expense()
        {
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();
            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithExpenses(expenseId, 734.14m, 1284.98m)
                                    .WithIncomes(10119.63m)
                                    .WithInvestments(7015m)
                                    .Build();

            decimal expectedTotal = 1284.98m;
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

            Assert.Single(account.Expenses.GetFinanceStatements());
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedTotalExpensesPercentage, Math.Round(result.ExpenseTotalPercentage, 2));
            Assert.Equal(expectedTotalInvestmentsPercentage, Math.Round(result.InvestmentTotalPercentage, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Remove_Income()
        {
            string accountId = Guid.NewGuid().ToString();
            string IncomeId = Guid.NewGuid().ToString();
            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes()
                                    .WithIncomes(IncomeId, 5)
                                    .WithInvestments()
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(account, It.IsAny<IFinanceStatement>()))
                .Verifiable();

            await _removeUseCase.Execute<Income>(accountId, IncomeId);

            Assert.Equal(4, account.Incomes.GetFinanceStatements().Count());

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Remove_Investment()
        {
            string accountId = Guid.NewGuid().ToString();
            string InvestmentId = Guid.NewGuid().ToString();
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
