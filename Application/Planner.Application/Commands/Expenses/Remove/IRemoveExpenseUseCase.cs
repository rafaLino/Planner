using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Remove
{
    public interface IRemoveExpenseUseCase
    {
        Task<RemoveFinanceStatementResult> Execute(string accountId, string expenseId);
    }
}
