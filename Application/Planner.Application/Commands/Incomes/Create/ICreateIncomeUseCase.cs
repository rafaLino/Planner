using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Incomes.Create
{
    public interface ICreateIncomeUseCase
    {
        Task<CreateFinanceStatementResult> Execute(string accountId, Title title, Amount amount = null);
    }
}
