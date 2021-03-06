using System.Threading.Tasks;

namespace Planner.Application.Commands.Investments.Remove
{
    public interface IRemoveInvestmentUseCase
    {
        Task<RemoveFinanceStatementResult> Execute(string accountId, string investmentId);
    }
}
