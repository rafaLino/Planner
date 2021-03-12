using Planner.Application.Results;
using System.Threading.Tasks;

namespace Planner.Application.Queries
{
    public interface IAccountQueries
    {
        Task<AccountQueryResult> GetAccount(System.Guid accountId);
    }
}
