using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Investments.Update
{
    public class UpdateInvestmentUseCase : IUpdateInvestmentUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public UpdateInvestmentUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task Execute(string accountId, string investmentId, string name)
        {
            Account account = await _accountReadOnlyRepository.GetAccountInvestments(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            if (account.Investments.GetFinanceStatements().Any(x => x.Title == name))
                throw new FinanceStatementExistsException($"Title {name} already exists!");


            Investment investment = (Investment)account
                                       .Investments
                                       .GetFinanceStatements()
                                       .SingleOrDefault(x => x.Id == investmentId);

            investment.UpdateInfo(name);

            await _accountWriteOnlyRepository.Update(account, investment);
        }
    }
}
