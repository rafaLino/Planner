using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Update
{
    public interface IUpdateExpenseUseCase
    {
        Task Execute(string accountId, string expenseId, string name);
    }
}
