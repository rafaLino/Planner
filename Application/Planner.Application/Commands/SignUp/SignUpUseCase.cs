using Microsoft.Extensions.Options;
using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain;
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
        private readonly JwtSettings jwtSettings;

        public SignUpUseCase(
            IAccountWriteOnlyRepository accountWriteOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IOptions<JwtSettings> jwtOptions)
        {
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            jwtSettings = jwtOptions.Value;
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

            string token = Token.Generate(user, jwtSettings);

            SignUpResult result = new SignUpResult
            {
                Token = token,
                UserId = user.Id,
                AccountId = account.Id
            };

            return result;
        }
    }
}
