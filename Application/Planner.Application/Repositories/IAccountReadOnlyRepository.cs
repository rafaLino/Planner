using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Repositories
{
    public interface IAccountReadOnlyRepository
    {
        Task<Account> Get(string accountId);
    }
}
