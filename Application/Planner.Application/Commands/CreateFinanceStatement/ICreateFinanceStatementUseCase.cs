using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Planner.Application.Commands.CreateFinanceStatement
{
    public interface ICreateFinanceStatementUseCase
    {

        Task<CreateFinanceStatementResult> Execute<T>(Guid accountId, Title title, Amount amount = null) where T : class, IFinanceStatement;
    }
}
