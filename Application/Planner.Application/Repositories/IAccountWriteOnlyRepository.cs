using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Repositories
{
    public interface IAccountWriteOnlyRepository
    {
        Task Create(Account account);

        Task Update(Account account);

        Task Update(Account account, IFinanceStatement financeStatement);

        Task Delete(Account account);
    }
}
