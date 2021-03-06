using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Incomes.Remove
{
    public class RemoveIncomeUseCase : IRemoveIncomeUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

        public RemoveIncomeUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RemoveFinanceStatementResult> Execute(string accountId, string incomeId)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            Income income = (Income)account
                                         .Incomes
                                         .Get(incomeId);

            account.Incomes.Remove(income);

            await _accountWriteOnlyRepository.Remove(account, income);

            RemoveFinanceStatementResult  result = new RemoveFinanceStatementResult
            {
                Total = account.Incomes.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;
        }
    }
}
