using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Investments.Create
{
    public class CreateInvestmentUseCase : ICreateInvestmentUseCase
    {

        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public CreateInvestmentUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<CreateFinanceStatementResult> Execute(string accountId, Title title, Amount amount = null)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists!");

            Investment investment = new Investment(title, amount);

            account
                .Investments
                .Add(investment);

            await _accountWriteOnlyRepository.Update(account);

            CreateFinanceStatementResult result = new CreateFinanceStatementResult
            {
                Id = investment.Id,
                Percentage = investment.AmountRecords.Percentage(account.Investments.Total()),
                Total = account.Investments.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;

        }
    }
}
