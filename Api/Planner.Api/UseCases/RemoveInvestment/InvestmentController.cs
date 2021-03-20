using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.Api.Model;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.RemoveInvestment
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IRemoveFinanceStatementUseCase _remove;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remove"></param>
        public InvestmentController(IRemoveFinanceStatementUseCase remove)
        {
            _remove = remove;
        }

        /// <summary>
        ///  Remove an investment
        /// </summary>
        /// <param name="request"></param>
        /// <returns code="200">return remove result</returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] RemoveFinanceStatementRequest request)
        {
            RemoveFinanceStatementResult result = await _remove.Execute<Investment>(request.AccountId, request.Id);

            return Ok(result);
        }
    }
}
