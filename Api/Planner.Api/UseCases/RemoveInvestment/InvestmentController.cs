using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.RemoveInvestment
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IRemoveFinanceStatementUseCase _remove;

        public InvestmentController(IRemoveFinanceStatementUseCase remove)
        {
            _remove = remove;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemoveInvestmentRequest request)
        {
            RemoveFinanceStatementResult result = await _remove.Execute<Investment>(request.AccountId, request.InvestmentId);

            return Ok(result);
        }
    }
}
