using System.Threading.Tasks;

namespace Planner.Application.Commands.Register
{
    public interface IRegisterUseCase
    {

        Task<RegisterResult> Execute();

    }
}
