using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.UpdateFinanceStatement
{
    public interface IUpdateFinanceStatementUseCase
    {
        Task Execute<T>(string accountId, string financeStatementId, Title title) where T : class, IFinanceStatement;
    }
}
