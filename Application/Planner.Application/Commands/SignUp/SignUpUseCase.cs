using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.Users;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.SignUp
{
    public class SignUpUseCase : ISignUpUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public SignUpUseCase(IAccountWriteOnlyRepository accountWriteOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<SignUpResult> Execute(SignUpCommand command)
        {
            bool emailAlreadyExists = await _userReadOnlyRepository.Any(command.Email);

            if (emailAlreadyExists)
                throw new UserEmailAlreadyExistsException("An account already exists with this email");

            Account account = new Account();

            User user = new User(
                account.Id,
                command.Email,
                Password.Create(command.Password),
                command.Name,
                command.Picture);


            await _accountWriteOnlyRepository.Create(account);
            await _userWriteOnlyRepository.Create(user);

            SignUpResult result = new SignUpResult
            {
                UserId = user.Id,
                AccountId = account.Id
            };

            return result;
        }
    }
}
