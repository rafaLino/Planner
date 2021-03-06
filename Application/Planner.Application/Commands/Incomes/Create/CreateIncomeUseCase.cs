using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Incomes.Create
{
    public class CreateIncomeUseCase : ICreateIncomeUseCase
    {

        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public CreateIncomeUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<CreateFinanceStatementResult> Execute(string accountId, Title title, Amount amount = null)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists!");

            Income income = new Income(title, amount);

            account
                .Incomes
                .Add(income);

            await _accountWriteOnlyRepository.Update(account);

            CreateFinanceStatementResult result = new CreateFinanceStatementResult
            {
                Id = income.Id,
                Percentage = income.AmountRecords.Percentage(account.Incomes.Total()),
                Total = account.Incomes.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;

        }
    }
}
