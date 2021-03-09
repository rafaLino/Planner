using Moq;
using Planner.Application.Commands.RemoveAmountRecord;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class RemoveAmountRecordUseCaseTests
    {
        private readonly Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;
        private readonly Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private readonly IRemoveAmountRecordUseCase _removeAmountRecordUseCase;

        public RemoveAmountRecordUseCaseTests()
        {
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();

            _removeAmountRecordUseCase = new RemoveAmountRecordUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public void Should_Throw_Exception_Given_Nonexistent_Account()
        {
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account))
                .Verifiable();

            Assert.ThrowsAsync<AccountNotFoundException>(() => _removeAmountRecordUseCase.Execute<Expense>(accountId, expenseId, amountRecordId));

            _accountReadOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Remove_Amount_Record_From_Expense()
        {
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes(3000m)
                                    .WithExpenses(100m)
                                    .WithExpense(expenseId, amountRecordId, 30, 20)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(It.IsAny<IFinanceStatement>(), It.IsAny<AmountRecord>()))
                .Verifiable();

            decimal expectedExpenseTotal = 120m;
            double expectedExpensePercentage = 4;

            RemoveAmountRecordResult result = await _removeAmountRecordUseCase.Execute<Expense>(accountId, expenseId, amountRecordId);

            Assert.Equal(expectedExpenseTotal, result.Expense.Total);
            Assert.Equal(expectedExpensePercentage, Math.Round(result.Expense.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Remove_Amount_Record_From_Investment()
        {
            string accountId = Guid.NewGuid().ToString();
            string investmentId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes(3000m)
                                    .WithInvestments(100m)
                                    .WithInvestment(investmentId, amountRecordId, 30, 20)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(It.IsAny<IFinanceStatement>(), It.IsAny<AmountRecord>()))
                .Verifiable();

            decimal expectedExpenseTotal = 120m;
            double expectedExpensePercentage = 4;

            RemoveAmountRecordResult result = await _removeAmountRecordUseCase.Execute<Investment>(accountId, investmentId, amountRecordId);

            Assert.Equal(expectedExpenseTotal, result.Investment.Total);
            Assert.Equal(expectedExpensePercentage, Math.Round(result.Investment.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }


        [Fact]
        public async Task Should_Remove_Amount_Record_From_Income()
        {
            string accountId = Guid.NewGuid().ToString();
            string incomeId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithIncomes(3000m)
                                    .WithIncome(incomeId, amountRecordId, 50, 20)
                                    .WithExpenses(100m)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Remove(It.IsAny<IFinanceStatement>(), It.IsAny<AmountRecord>()))
                .Verifiable();

            decimal expectedExpenseTotal = 3_020;

            RemoveAmountRecordResult result = await _removeAmountRecordUseCase.Execute<Income>(accountId, incomeId, amountRecordId);

            Assert.Equal(expectedExpenseTotal, result.Income.Total);

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }
    }
}
