using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.RemoveAmountRecord
{
    public interface IRemoveAmountRecordUseCase
    {
        Task<RemoveAmountRecordResult> Execute<T>(string accountId, string financeStatementId, string amountRecordId) where T : class, IFinanceStatement;
    }
}
