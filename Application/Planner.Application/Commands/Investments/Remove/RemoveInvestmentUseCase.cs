using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Investments.Remove
{
    public class RemoveInvestmentUseCase : IRemoveInvestmentUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

        public RemoveInvestmentUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RemoveFinanceStatementResult> Execute(string accountId, string investmentId)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            Investment investment = (Investment)account
                                         .Investments
                                         .Get(investmentId);

            account.Investments.Remove(investment);

            await _accountWriteOnlyRepository.Remove(account, investment);

            RemoveFinanceStatementResult result = new RemoveFinanceStatementResult
            {
                Total = account.Investments.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;
        }
    }
}
