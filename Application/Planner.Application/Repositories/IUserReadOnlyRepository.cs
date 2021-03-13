using Planner.Domain.Users;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Repositories
{
    public interface IUserReadOnlyRepository
    {
        Task<User> Get(Email email);
    }
}
