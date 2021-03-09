using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public RegisterUseCase(IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RegisterResult> Execute()
        {
            Account account = new Account();

            await _accountWriteOnlyRepository.Create(account);

            RegisterResult result = new RegisterResult
            {
                Id = account.Id
            };

            return result;
        }
    }
}
