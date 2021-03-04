using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expense.Create
{
    public interface ICreateExpenseUseCase
    {
        Task<CreateExpenseResult> Execute(string accountId, Title title, Amount amount = null);
    }
}
