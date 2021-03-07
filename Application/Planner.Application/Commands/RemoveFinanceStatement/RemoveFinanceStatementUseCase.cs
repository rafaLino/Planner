using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.RemoveFinanceStatement
{
    public class RemoveFinanceStatementUseCase : IRemoveFinanceStatementUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public RemoveFinanceStatementUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RemoveFinanceStatementResult> Execute<T>(string accountId, string financeStatementId) where T : class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            FinanceStatementCollection collection = account.GetCollecion<T>();

            T financeStatement = (T)collection.Get(financeStatementId);

            collection.Remove(financeStatement);

            await _accountWriteOnlyRepository.Remove(account, financeStatement);

            RemoveFinanceStatementResult result = new RemoveFinanceStatementResult
            {
                Total = collection.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;
        }
    }
}
