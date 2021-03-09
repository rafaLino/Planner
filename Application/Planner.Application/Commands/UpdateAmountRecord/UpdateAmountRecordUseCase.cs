using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.UpdateAmountRecord
{
    public class UpdateAmountRecordUseCase : IUpdateAmountRecordUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public UpdateAmountRecordUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<UpdateAmountRecordResult> Execute<T>(UpdateAmountRecordCommand command) where T : class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(command.AccountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists");

            T financeStatement = (T)account.Get<T>(x => x.Id == command.FinanceStatementId);

            AmountRecord amountRecord = financeStatement.AmountRecords.Get(command.AmountRecordId);

            amountRecord.Update(command.Amount, command.Description);

            await _accountWriteOnlyRepository.Update(account, financeStatement, amountRecord);

            decimal incomeTotal = account.Incomes.Total();
            decimal expenseTotal = account.Expenses.Total();
            decimal investmentTotal = account.Investments.Total();

            UpdateAmountRecordResult result = new UpdateAmountRecordResult
            {
                Total = financeStatement.AmountRecords.Total(),
                Percentage = financeStatement.AmountRecords.Percentage(account.GetCollecion<T>().Total()),
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
