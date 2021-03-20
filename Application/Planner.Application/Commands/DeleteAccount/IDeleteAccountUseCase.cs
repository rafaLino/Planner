using System;
using System.Threading.Tasks;

namespace Planner.Application.Commands.DeleteAccount
{
    public interface IDeleteAccountUseCase
    {
        public Task Execute(Guid userId);
    }
}
