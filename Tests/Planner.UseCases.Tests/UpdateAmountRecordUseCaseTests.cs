using Moq;
using Planner.Application.Commands.UpdateAmountRecord;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class UpdateAmountRecordUseCaseTests
    {
        private readonly Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;
        private readonly Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private readonly IUpdateAmountRecordUseCase _updateAmountRecordUseCase;

        public UpdateAmountRecordUseCaseTests()
        {
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();

            _updateAmountRecordUseCase = new UpdateAmountRecordUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_Update_Expense_Amount_Record()
        {
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();
            Amount amount = 300;

            Account account = AccountBuilder
                                     .New
                                     .WithExpenses(3000m)
                                     .WithExpense(expenseId, amountRecordId, 50, 100)
                                     .WithIncomes(10000m)
                                     .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<AmountRecord>()))
                .Verifiable();

            decimal expectedTotal = 400m;
            double expectedPercentage = 11.76;
            decimal expectedExpenseTotal = 3_400m;
            double expectedExpensePercentage = 34;

            var command = new UpdateAmountRecordCommand
            {
                AccountId = accountId,
                FinanceStatementId = expenseId,
                AmountRecordId = amountRecordId,
                Amount = amount
            };

            UpdateAmountRecordResult result = await _updateAmountRecordUseCase.Execute<Expense>(command);

            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedExpenseTotal, result.Expense.Total);
            Assert.Equal(expectedExpensePercentage, Math.Round(result.Expense.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Update_Income_Amount_Record()
        {
            string accountId = Guid.NewGuid().ToString();
            string incomeId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();
            Amount amount = 300;

            Account account = AccountBuilder
                                     .New
                                     .WithExpenses(3000m)
                                     .WithIncomes(10000m)
                                     .WithIncome(incomeId, amountRecordId, 50, 100)
                                     .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<AmountRecord>()))
                .Verifiable();

            decimal expectedTotal = 400m;
            double expectedPercentage = 3.85;
            decimal expectedIncomeTotal = 10_400m;

            var command = new UpdateAmountRecordCommand
            {
                AccountId = accountId,
                FinanceStatementId = incomeId,
                AmountRecordId = amountRecordId,
                Amount = amount
            };

            UpdateAmountRecordResult result = await _updateAmountRecordUseCase.Execute<Income>(command);

            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedIncomeTotal, result.Income.Total);

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }


        [Fact]
        public async Task Should_Update_Investment_Amount_Record()
        {
            string accountId = Guid.NewGuid().ToString();
            string incomeId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();
            Amount amount = 300;

            Account account = AccountBuilder
                                     .New
                                     .WithInvestments(3000m)
                                     .WithIncomes(10000m)
                                     .WithInvestment(incomeId, amountRecordId, 50, 100)
                                     .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account)
                .Verifiable();

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<AmountRecord>()))
                .Verifiable();

            decimal expectedTotal = 400m;
            double expectedPercentage = 11.76;
            decimal expectedInvestmentTotal = 3_400m;
            double expectedExpensePercentage = 34;

            var command = new UpdateAmountRecordCommand
            {
                AccountId = accountId,
                FinanceStatementId = incomeId,
                AmountRecordId = amountRecordId,
                Amount = amount,
                Description = "stocks"
            };

            UpdateAmountRecordResult result = await _updateAmountRecordUseCase.Execute<Investment>(command);

            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedInvestmentTotal, result.Investment.Total);
            Assert.Equal(expectedExpensePercentage, Math.Round(result.Investment.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }
        [Fact]
        public void Should_Throw_Exception_Given_Nonexistent_Account()
        {
            string accountId = Guid.NewGuid().ToString();
            string expenseId = Guid.NewGuid().ToString();
            string amountRecordId = Guid.NewGuid().ToString();
            Amount amount = 300;

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account))
                .Verifiable();

            var command = new UpdateAmountRecordCommand
            {
                AccountId = accountId,
                FinanceStatementId = expenseId,
                AmountRecordId = amountRecordId,
                Amount = amount
            };

            Assert.ThrowsAsync<AccountNotFoundException>(() => _updateAmountRecordUseCase.Execute<Expense>(command));

            _accountReadOnlyRepository.VerifyAll();
        }
    }
}
