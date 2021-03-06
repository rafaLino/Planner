using System.Threading.Tasks;

namespace Planner.Application.Commands.Incomes.Update
{
    public interface IUpdateIncomeUseCase
    {
        Task Execute(string accountId, string incomeId, string name);
    }
}
