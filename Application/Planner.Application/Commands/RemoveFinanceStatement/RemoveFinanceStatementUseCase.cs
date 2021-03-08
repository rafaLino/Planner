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

            decimal totalIncomes = account.Incomes.Total();
            decimal totalExpenses = account.Expenses.Total();
            decimal totalInvestments = account.Investments.Total();

            RemoveFinanceStatementResult result = new RemoveFinanceStatementResult
            {
                Income = new Results.FinanceStatementResult
                {
                    Total = totalIncomes
                },
                Expense = new Results.FinanceStatementResult
                {
                    Total = totalExpenses,
                    Percentage = account.Expenses.Percentage(totalIncomes)
                },
                Investment = new Results.FinanceStatementResult
                {
                    Total = totalInvestments,
                    Percentage = account.Investments.Percentage(totalIncomes)
                },
            };

            return result;
        }
    }
}
