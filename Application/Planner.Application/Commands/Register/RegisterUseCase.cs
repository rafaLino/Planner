using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Users;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public RegisterUseCase(IAccountWriteOnlyRepository accountWriteOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<RegisterResult> Execute(RegisterCommand command)
        {
            Account account = new Account();

            User user = new User(
                account.Id,
                command.Email,
                Password.Create(command.Password),
                command.Name);

            
            await _accountWriteOnlyRepository.Create(account);
            await _userWriteOnlyRepository.Create(user);

            RegisterResult result = new RegisterResult
            {
                Id = account.Id
            };

            return result;
        }
    }
}
