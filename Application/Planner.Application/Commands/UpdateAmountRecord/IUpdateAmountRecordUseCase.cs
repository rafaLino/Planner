using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.UpdateAmountRecord
{
    public interface IUpdateAmountRecordUseCase
    {
        Task<UpdateAmountRecordResult> Execute<T>(UpdateAmountRecordCommand command) where T : class, IFinanceStatement;
    }
}
