using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.SaveAmountRecord;
using Planner.Domain.Accounts;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.SaveIncomeAmountRecords
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly ISaveAmountRecordUseCase _useCase;

        public IncomeController(ISaveAmountRecordUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Update amount records of income
        /// </summary>
        /// <param name="request"></param>
        /// <returns>200</returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] SaveIncomeAmountRecordRequest request)
        {
            var amountRecords = request.AmountRecords.Select(x => new AmountRecord(x.Amount, x.Description));
            SaveAmountRecordResult result = await _useCase.Execute<Income>(request.AccountId, request.IncomeId, amountRecords);
            return Ok(result);
        }
    }
}
