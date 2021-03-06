using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Remove
{
    public class RemoveExpenseUseCase : IRemoveExpenseUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

        public RemoveExpenseUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RemoveExpenseResult> Execute(string accountId, string expenseId)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            Expense expense = (Expense)account
                                         .Expenses
                                         .GetFinanceStatements()
                                         .SingleOrDefault(x => x.Id == expenseId);

            await _accountWriteOnlyRepository.Remove(account, expense);

            RemoveExpenseResult result = new RemoveExpenseResult
            {
                Total = account.Expenses.Total(),
                Percentage = expense.AmountRecords.Percentage(account.Expenses.Total()),
                TotalPercentage = account.Expenses.Percentage(account.Incomes.Total())
            };

            return result;
        }
    }
}
