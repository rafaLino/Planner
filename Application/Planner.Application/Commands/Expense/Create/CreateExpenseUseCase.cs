using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expense.Create
{
    public class CreateExpenseUseCase : ICreateExpenseUseCase
    {

        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public CreateExpenseUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<CreateExpenseResult> Execute(string accountId, Title title, Amount amount = null)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists!");

            Domain.Accounts.Expense expense = new Domain.Accounts.Expense(title, amount);

            account
                .Expenses
                .Add(expense);

            await _accountWriteOnlyRepository.Update(account);

            CreateExpenseResult result = new CreateExpenseResult
            {
                Id = expense.Id,
                Total = account.Expenses.Total(),
                Percentage = expense.AmountRecords.Percentage(account.Expenses.Total()),
                TotalPercentage = account.Expenses.Percentage(account.Incomes.Total())
            };

            return result;

        }
    }
}
