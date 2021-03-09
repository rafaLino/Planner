using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.RemoveAmountRecord
{
    public class RemoveAmountRecordUseCase : IRemoveAmountRecordUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public RemoveAmountRecordUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RemoveAmountRecordResult> Execute<T>(string accountId, string financeStatementId, string amountRecordId) where T : class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does exists");

            T financeStatement = (T)account.Get<T>(x => x.Id == financeStatementId);
            AmountRecord removed = financeStatement
                                  .AmountRecords
                                  .Remove(amountRecordId);

            if (removed != null)
                await _accountWriteOnlyRepository.Remove(financeStatement, removed);

            decimal incomeTotal = account.Incomes.Total();
            decimal expenseTotal = account.Expenses.Total();
            decimal investmentTotal = account.Investments.Total();

            RemoveAmountRecordResult result = new RemoveAmountRecordResult
            {
                Income = new Results.FinanceStatementResult
                {
                    Total = incomeTotal
                },
                Expense = new Results.FinanceStatementResult
                {
                    Total = expenseTotal,
                    Percentage = account.Expenses.Percentage(incomeTotal)
                },
                Investment = new Results.FinanceStatementResult
                {
                    Total = investmentTotal,
                    Percentage = account.Investments.Percentage(incomeTotal)
                }
            };
            return result;
        }
    }
}
