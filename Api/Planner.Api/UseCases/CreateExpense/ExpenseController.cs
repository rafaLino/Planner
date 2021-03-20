using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.Api.Model;
using Planner.Application.Commands.CreateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.CreateExpense
{
    /// <summary>
    /// Expense
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ICreateFinanceStatementUseCase _create;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="create"></param>
        public ExpenseController(ICreateFinanceStatementUseCase create)
        {
            _create = create;
        }

        /// <summary>
        /// Create an expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return expense result</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateFinanceStatementRequest request)
        {
            CreateFinanceStatementResult result = await _create.Execute<Expense>(request.AccountId, request.Title, request.Amount);

            return Created(Request.Path, result);
        }
    }
}
