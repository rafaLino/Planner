using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Investments.Create
{
    public interface ICreateInvestmentUseCase
    {
        Task<CreateFinanceStatementResult> Execute(string accountId, Title title, Amount amount = null);
    }
}
