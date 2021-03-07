using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Exceptions;
using Planner.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.UpdateFinanceStatement
{
    public class UpdateFinanceStatementUseCase : IUpdateFinanceStatementUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public UpdateFinanceStatementUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }
        public async Task Execute<T>(string accountId, string financeStatementId, Title title) where T :  class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            FinanceStatementCollection collection = account.GetCollecion<T>();

            if (collection.Any(x => x.Title == title && x.Id != financeStatementId))
                throw new FinanceStatementAlreadyExistsException($"Title {title} already exists!");


            FinanceStatement financeStatement = (FinanceStatement)collection.Get(financeStatementId);

            financeStatement.UpdateInfo(title);

            await _accountWriteOnlyRepository.Update(account, financeStatement);
        }
    }
}
