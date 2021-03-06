using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Incomes.Update
{
    public class UpdateIncomeUseCase : IUpdateIncomeUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public UpdateIncomeUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task Execute(string accountId, string incomeId, string name)
        {
            Account account = await _accountReadOnlyRepository.GetAccountIncomes(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            if (account.Incomes.GetFinanceStatements().Any(x => x.Title == name))
                throw new FinanceStatementExistsException($"Title {name} already exists!");


            Income income = (Income)account
                                       .Incomes
                                       .GetFinanceStatements()
                                       .SingleOrDefault(x => x.Id == incomeId);

            income.UpdateInfo(name);

            await _accountWriteOnlyRepository.Update(account, income);
        }
    }
}
