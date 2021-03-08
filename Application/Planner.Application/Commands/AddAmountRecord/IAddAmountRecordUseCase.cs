using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.AddAmountRecord
{
    public interface IAddAmountRecordUseCase
    {
        Task<AddAmountRecordResult> Execute<T>(string accountId, string financeStatementId, Amount amount, string description = null) where T : class, IFinanceStatement;
    }
}
