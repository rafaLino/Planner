using System.Threading.Tasks;

namespace Planner.Application.Commands.SignUp
{
    public interface ISignUpUseCase
    {

        Task<SignUpResult> Execute(SignUpCommand command);

    }
}
