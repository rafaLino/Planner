using System.Threading.Tasks;

namespace Planner.Application.Commands.Incomes.Remove
{
    public interface IRemoveIncomeUseCase
    {
        Task<RemoveFinanceStatementResult> Execute(string accountId, string incomeId);
    }
}
