using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Planner.Application.Commands.UpdateFinanceStatement
{
    public interface IUpdateFinanceStatementUseCase
    {
        Task Execute<T>(Guid accountId, Guid financeStatementId, Title title) where T : class, IFinanceStatement;
    }
}
