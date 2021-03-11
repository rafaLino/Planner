using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.UpdateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.UpdateIncome
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IUpdateFinanceStatementUseCase _update;

        public IncomeController(IUpdateFinanceStatementUseCase update)
        {
            _update = update;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>204</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateIncomeRequest request)
        {
            await _update.Execute<Income>(request.AccountId, request.IncomeId, request.Title);

            return NoContent();
        }
    }
}
