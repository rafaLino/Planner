using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Remove
{
    public interface IRemoveExpenseUseCase
    {
        Task<RemoveExpenseResult> Execute(string accountId, string expenseId);
    }
}
