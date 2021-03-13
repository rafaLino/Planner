using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.UpdateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.UpdateExpense
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IUpdateFinanceStatementUseCase _update;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        public ExpenseController(IUpdateFinanceStatementUseCase update)
        {
            _update = update;
        }

        /// <summary>
        ///  Update title of expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns code="204"> </returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateExpenseRequest request)
        {
            await _update.Execute<Expense>(request.AccountId, request.ExpenseId, request.Title);

            return NoContent();
        }
    }
}
