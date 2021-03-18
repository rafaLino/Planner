using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.RemoveIncome
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IRemoveFinanceStatementUseCase _remove;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remove"></param>
        public IncomeController(IRemoveFinanceStatementUseCase remove)
        {
            _remove = remove;
        }

        /// <summary>
        /// Remove an income
        /// </summary>
        /// <param name="request"></param>
        /// <returns code="200">return an remove result</returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] RemoveIncomeRequest request)
        {
            RemoveFinanceStatementResult result = await _remove.Execute<Income>(request.AccountId, request.IncomeId);

            return Ok(result);
        }
    }
}
