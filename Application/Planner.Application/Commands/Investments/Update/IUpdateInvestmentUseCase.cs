using System.Threading.Tasks;

namespace Planner.Application.Commands.Investments.Update
{
    public interface IUpdateInvestmentUseCase
    {
        Task Execute(string accountId, string investmentId, string name);
    }
}
