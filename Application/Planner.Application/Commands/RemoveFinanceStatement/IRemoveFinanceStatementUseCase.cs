using Planner.Domain.Accounts;
using System;
using System.Threading.Tasks;

namespace Planner.Application.Commands.RemoveFinanceStatement
{
    public interface IRemoveFinanceStatementUseCase
    {

        Task<RemoveFinanceStatementResult> Execute<T>(Guid accountId, Guid financeStatementId) where T : class, IFinanceStatement;
    }
}
