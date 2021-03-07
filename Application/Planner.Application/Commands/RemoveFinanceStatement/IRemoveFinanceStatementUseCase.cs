using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.RemoveFinanceStatement
{
    public interface IRemoveFinanceStatementUseCase
    {

        Task<RemoveFinanceStatementResult> Execute<T>(string accountId, string financeStatementId) where T : class, IFinanceStatement;
    }
}
