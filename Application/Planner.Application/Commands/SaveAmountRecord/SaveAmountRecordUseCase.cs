using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.SaveAmountRecord
{
    public class SaveAmountRecordUseCase : ISaveAmountRecordUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        public SaveAmountRecordUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<SaveAmountRecordResult> Execute<T>(Guid accountId, Guid financeStatementId, IEnumerable<AmountRecord> amountRecords) where T : class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            T financeStatement = (T)account.Get<T>(x => x.Id == financeStatementId);

            financeStatement
                .AmountRecords
                .Replace(amountRecords);

            await _accountWriteOnlyRepository.Update(account, financeStatement);

            decimal incomeTotal = account.Incomes.Total();
            decimal expenseTotal = account.Expenses.Total();
            decimal investmentTotal = account.Investments.Total();

            SaveAmountRecordResult result = new SaveAmountRecordResult
            {
                Id = financeStatement.Id,
                Total = financeStatement.AmountRecords.Total(),
                Percentage = financeStatement.AmountRecords.Percentage(account.GetCollecion<T>().Total()),
                AmountRecords = financeStatement.AmountRecords
                .GetAmountRecords()
                .Select(x => new Results.AmountRecordResult { Id = x.Id, Amount = x.Amount, Description = x.Description }),

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
