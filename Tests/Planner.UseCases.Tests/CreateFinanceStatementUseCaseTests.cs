using Moq;
using Planner.Application.Commands.CreateFinanceStatement;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class CreateFinanceStatementUseCaseTests
    {


        private Mock<IAccountReadOnlyRepository> _accountReadOnlyRepository;
        private Mock<IAccountWriteOnlyRepository> _accountWriteOnlyRepository;
        private ICreateFinanceStatementUseCase _createUseCase;
        public CreateFinanceStatementUseCaseTests()
        {
            _accountReadOnlyRepository = new Mock<IAccountReadOnlyRepository>();
            _accountWriteOnlyRepository = new Mock<IAccountWriteOnlyRepository>();
            _createUseCase = new CreateFinanceStatementUseCase(_accountReadOnlyRepository.Object, _accountWriteOnlyRepository.Object);
        }

        [Fact]
        public void Should_Throw_Exception_Given_Nonexistent_Account()
        {
            Guid accountId = Guid.NewGuid();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(default(Account))
                .Verifiable();

            Assert.ThrowsAsync<AccountNotFoundException>(() => _createUseCase.Execute<Expense>(accountId, "internet", 124.99m));

            _accountReadOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Create_Finance_Statement_Without_Amount()
        {
            Guid accountId = Guid.NewGuid();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .Build();

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));


            var result = await _createUseCase.Execute<Expense>(accountId, "internet", null);

            Assert.NotNull(result.Id.ToString());
            Assert.Equal(0, result.Total);
            
            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Create_Expense()
        {
            Guid accountId = Guid.NewGuid();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithExpenses(734.14m, 1284.98m)
                                    .WithIncomes(10119.63m)
                                    .WithInvestments(7015)
                                    .Build();

            decimal expectedTotal = 124.99m;
            decimal expectedExpenseTotal = 2144.11m;
            double expectedPercentage = 5.83;
            double expectedTotalPercentage = 21.19;

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));
                

            var result = await _createUseCase.Execute<Expense>(accountId, "internet", 124.99m);

            Assert.NotNull(result.Id.ToString());
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedExpenseTotal, result.Expense.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedTotalPercentage, Math.Round(result.Expense.Percentage * 100, 2));
            Assert.NotEmpty(result.AmountRecords);

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Create_Income()
        {
            Guid accountId = Guid.NewGuid();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithExpenses(734.14m, 1284.98m)
                                    .WithIncomes(10119.63m)
                                    .WithInvestments(7015m)
                                    .Build();

            decimal expectedTotal = 124.99m;
            decimal expectedIncomeTotal = 10244.62m;
            double expectedPercentage = 1.22;

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));
                

            var result = await _createUseCase.Execute<Income>(accountId, "bonus", 124.99m);

            Assert.NotNull(result.Id.ToString());
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedIncomeTotal, result.Income.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_Create_Investment()
        {
            Guid accountId = Guid.NewGuid();

            Account account = AccountBuilder
                                    .New
                                    .WithId(accountId)
                                    .WithExpenses(734.14m, 1284.98m)
                                    .WithIncomes(10119.63m)
                                    .WithInvestments(7015m)
                                    .Build();

            decimal expectedTotal = 124.99m;
            decimal expectedInvestmentTotal = 7139.99m;
            double expectedPercentage = 1.75;
            double expectedTotalPercentage = 70.56;

            _accountReadOnlyRepository
                .Setup(x => x.Get(accountId))
                .ReturnsAsync(account);

            _accountWriteOnlyRepository
                .Setup(x => x.Update(account, It.IsAny<IFinanceStatement>()));
                

            var result = await _createUseCase.Execute<Investment>(accountId, "TSLA34", 124.99m);

            Assert.NotNull(result.Id.ToString());
            Assert.Equal(expectedTotal, result.Total);
            Assert.Equal(expectedInvestmentTotal, result.Investment.Total);
            Assert.Equal(expectedPercentage, Math.Round(result.Percentage * 100, 2));
            Assert.Equal(expectedTotalPercentage, Math.Round(result.Investment.Percentage * 100, 2));

            _accountReadOnlyRepository.VerifyAll();
            _accountWriteOnlyRepository.VerifyAll();
        }

    }
}
