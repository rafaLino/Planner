using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.CreateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.CreateExpense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ICreateFinanceStatementUseCase _create;

        public ExpenseController(ICreateFinanceStatementUseCase create)
        {
            _create = create;
        }

        /// <summary>
        /// Create an expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateExpenseRequest request)
        {
            CreateFinanceStatementResult result = await _create.Execute<Expense>(request.AccountId, request.Title, request.Amount);

            return Created(Request.Path, result);
        }
    }
}
