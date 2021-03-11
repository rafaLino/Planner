using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.RemoveExpense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IRemoveFinanceStatementUseCase _remove;

        public ExpenseController(IRemoveFinanceStatementUseCase remove)
        {
            _remove = remove;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemoveExpenseRequest request)
        {
            RemoveFinanceStatementResult result = await _remove.Execute<Expense>(request.AccountId, request.ExpenseId);

            return Ok(result);
        }
    }
}
