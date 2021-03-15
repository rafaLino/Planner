using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Users;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.SignIn
{
    public class SignInUseCase : ISignInUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public SignInUseCase(IUserReadOnlyRepository userReadOnlyRepository)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<SignInResult> Execute(Email email, string password)
        {
            User user = await _userReadOnlyRepository.Get(email);

            if (user == null)
                throw new UserNotFoundException("The account was not found");

            if (!user.Password.Verify(password))
                throw new PasswordNotMatchException("The password does not match");

            SignInResult result = new SignInResult
            {
                AccountId = user.AccountId,
                UserId = user.Id,
                Name = user.Name,
                Bytes = user.Picture?._bytes
            };

            return result;

        }
    }
}
