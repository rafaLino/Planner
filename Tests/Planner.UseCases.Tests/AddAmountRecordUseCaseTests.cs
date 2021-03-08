using Moq;
using Planner.Application.Commands.AddAmountRecord;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class AddAmountRecordUseCaseTests
    {
        private Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;
        private Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private IAddAmountRecordUseCase _addAmountRecordUseCase;

        public AddAmountRecordUseCaseTests()
        {
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _addAmountRecordUseCase = new AddAmountRecordUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_Add_Amount_Record_To_Expenses()
        {
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithExpenses(expenseId, 700, 600, 500)
                                    .WithIncomes(5000m)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));


            var result = await _addAmountRecordUseCase.Execute<Expense>(accountId, expenseId, 300);

            decimal expectedTotal = 1_000m;
            decimal expectedExpenseTotal = 2_100m;
            double expectedPercentage = 47.62;
            double expectedExpensePercentage = 42;


            Assert.Equal(expenseId, result.Id);
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedExpenseTotal, result.Expense.Total);
            Assert.Equal(expectedExpensePercentage, Math.Round(result.Expense.Percentage * 100, 2));


            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
            _accountWriteOnlyRepository.Verify(x => x.Update(account, It.IsAny<IFinanceStatement>()), Times.Once);
        }

        [Fact]
        public async Task Should_Add_Amount_Record_To_Income()
        {
            string accountId = Guid.NewGuid().ToString();
            string incomeId = Guid.NewGuid().ToString();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes(incomeId, 500m, 300m)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));


            var result = await _addAmountRecordUseCase.Execute<Income>(accountId, incomeId, 300);

            decimal expectedTotal = 800m;
            decimal expectedIncome = 1_100m;


            Assert.Equal(incomeId, result.Id);
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedIncome, result.Income.Total);


            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
            _accountWriteOnlyRepository.Verify(x => x.Update(account, It.IsAny<IFinanceStatement>()), Times.Once);
        }

        [Fact]
        public async Task Should_Add_Amount_Record_To_Investment()
        {
            string accountId = Guid.NewGuid().ToString();
            string investmentId = Guid.NewGuid().ToString();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithInvestments(investmentId, 500m, 300m)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));


            var result = await _addAmountRecordUseCase.Execute<Investment>(accountId, investmentId, 300);

            decimal expectedTotal = 800m;
            decimal expectedInvestmentTotal = 1_100m;


            Assert.Equal(investmentId, result.Id);
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedInvestmentTotal, result.Investment.Total);
            Assert.Equal(0, result.Investment.Percentage);


            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
            _accountWriteOnlyRepository.Verify(x => x.Update(account, It.IsAny<IFinanceStatement>()), Times.Once);
        }

        [Fact]
        public void Should_Throw_Exception_Given_Nonexistent_Account()
        {
            string accountId = Guid.NewGuid().ToString();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account))
                .Verifiable();

            Assert.ThrowsAsync<AccountNotFoundException>(() => _addAmountRecordUseCase.Execute<Expense>(accountId, "internet", 124.99m));

            _accountReadOnlyRepository.VerifyAll();
        }
    }
}
