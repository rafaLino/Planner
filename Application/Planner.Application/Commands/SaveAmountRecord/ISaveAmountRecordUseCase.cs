using Planner.Domain.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planner.Application.Commands.SaveAmountRecord
{
    public interface ISaveAmountRecordUseCase
    {
        Task<SaveAmountRecordResult> Execute<T>(string accountId, string financeStatementId, IEnumerable<AmountRecord> amountRecords) where T : class, IFinanceStatement;
    }
}
