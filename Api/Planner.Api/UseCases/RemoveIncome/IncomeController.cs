using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.RemoveIncome
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IRemoveFinanceStatementUseCase _remove;

        public IncomeController(IRemoveFinanceStatementUseCase remove)
        {
            _remove = remove;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemoveIncomeRequest request)
        {
            RemoveFinanceStatementResult result = await _remove.Execute<Income>(request.AccountId, request.IncomeId);

            return Ok(result);
        }
    }
}
