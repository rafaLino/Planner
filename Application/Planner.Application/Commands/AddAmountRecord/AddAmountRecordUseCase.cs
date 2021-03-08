using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.AddAmountRecord
{
    public class AddAmountRecordUseCase : IAddAmountRecordUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        public AddAmountRecordUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<AddAmountRecordResult> Execute<T>(string accountId, string financeStatementId, Amount amount, string description) where T : class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            T financeStatement = (T)account.Get<T>(x => x.Id == financeStatementId);

            financeStatement
                .AmountRecords
                .Add(amount, description);

            await _accountWriteOnlyRepository.Update(account, financeStatement);

            decimal incomeTotal = account.Incomes.Total();
            decimal expenseTotal = account.Expenses.Total();
            decimal investmentTotal = account.Investments.Total();

            AddAmountRecordResult result = new AddAmountRecordResult
            {
                Id = financeStatement.Id,
                Total = financeStatement.AmountRecords.Total(),
                Percentage = financeStatement.AmountRecords.Percentage(account.GetCollecion<T>().Total()),
                Income = new Results.FinanceStatementResult
                {
                    Total = incomeTotal,
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
