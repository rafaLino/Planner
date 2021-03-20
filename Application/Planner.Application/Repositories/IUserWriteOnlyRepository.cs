using Planner.Domain.Users;
using System;
using System.Threading.Tasks;

namespace Planner.Application.Repositories
{
    public interface IUserWriteOnlyRepository
    {
        Task Create(User user);

        Task Remove(Guid userId);
    }
}
