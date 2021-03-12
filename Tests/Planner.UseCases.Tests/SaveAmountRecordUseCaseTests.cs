using Moq;
using Planner.Application.Commands.SaveAmountRecord;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class SaveAmountRecordUseCaseTests
    {
        private Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;
        private Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private ISaveAmountRecordUseCase _saveAmountRecordUseCase;

        public SaveAmountRecordUseCaseTests()
        {
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _saveAmountRecordUseCase = new SaveAmountRecordUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_Add_Amount_Record_To_Expenses()
        {
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();
            AmountRecord[] amountRecords = new AmountRecord[] {

                new AmountRecord(700),
                new AmountRecord(300)
            };
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


            var result = await _saveAmountRecordUseCase.Execute<Expense>(accountId, expenseId, amountRecords);

            decimal expectedTotal = 1_000m;
            decimal expectedExpenseTotal = 2_100m;
            double expectedPercentage = 47.62;
            double expectedExpensePercentage = 42;


            Assert.Equal(expenseId, result.Id);
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedExpenseTotal, result.Expense.Total);
            Assert.Equal(expectedExpensePercentage, Math.Round(result.Expense.Percentage * 100, 2));
            Assert.NotEmpty(result.AmountRecords);

            _accountReadOnlyRepository.Verify(x => x.Get(accountId), Times.Once);
            _accountWriteOnlyRepository.Verify(x => x.Update(account, It.IsAny<IFinanceStatement>()), Times.Once);
        }

        [Fact]
        public async Task Should_Add_Amount_Record_To_Income()
        {
            Guid accountId = Guid.NewGuid();
            Guid incomeId = Guid.NewGuid();
            AmountRecord[] amountRecords = new AmountRecord[] { 
                new AmountRecord(500),
                new AmountRecord(300) 
            };
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


            var result = await _saveAmountRecordUseCase.Execute<Income>(accountId, incomeId, amountRecords);

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
            Guid accountId = Guid.NewGuid();
            Guid investmentId = Guid.NewGuid();
            AmountRecord[] amountRecords = new AmountRecord[] { 
                new AmountRecord(500),
                new AmountRecord(300)
            };
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


            var result = await _saveAmountRecordUseCase.Execute<Investment>(accountId, investmentId, amountRecords);

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
            Guid accountId = Guid.NewGuid();
            Guid expenseId = Guid.NewGuid();
            AmountRecord[] amountRecords = new AmountRecord[] { new AmountRecord(124.99m) };
            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account))
                .Verifiable();

            Assert.ThrowsAsync<AccountNotFoundException>(() => _saveAmountRecordUseCase.Execute<Expense>(accountId, expenseId, amountRecords));

            _accountReadOnlyRepository.VerifyAll();
        }
    }
}
