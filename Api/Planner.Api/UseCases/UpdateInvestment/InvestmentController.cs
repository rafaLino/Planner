using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.UpdateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.UpdateInvestment
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IUpdateFinanceStatementUseCase _update;

        public InvestmentController(IUpdateFinanceStatementUseCase update)
        {
            _update = update;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>204</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateInvestmentRequest request)
        {
            await _update.Execute<Investment>(request.AccountId, request.InvestmentId, request.Title);

            return NoContent();
        }
    }
}
