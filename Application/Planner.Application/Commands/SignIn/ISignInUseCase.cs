using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.SignIn
{
    public interface ISignInUseCase
    {
        Task<SignInResult> Execute(Email email, string password);
    }
}
