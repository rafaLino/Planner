using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Register
{
    public interface IRegisterUseCase
    {

        Task<RegisterResult> Execute(RegisterCommand command);

    }
}
