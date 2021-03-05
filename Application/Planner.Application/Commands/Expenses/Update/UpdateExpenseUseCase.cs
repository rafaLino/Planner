using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Update
{
    public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public UpdateExpenseUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task Execute(string accountId, string expenseId, string name)
        {
            Account account = await _accountReadOnlyRepository.GetAccountExpenses(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            if (account.Expenses.GetFinanceStatements().Any(x => x.Title == name))
                throw new FinanceStatementExistsException($"Title {name} already exists!");


            Expense expense = (Expense)account
                                       .Expenses
                                       .GetFinanceStatements()
                                       .SingleOrDefault(x => x.Id == expenseId);

            expense.UpdateInfo(name);

            await _accountWriteOnlyRepository.Update(account, expense);
        }
    }
}
