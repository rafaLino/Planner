using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.SaveAmountRecord;
using Planner.Domain.Accounts;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.SaveExpenseAmountRecords
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ISaveAmountRecordUseCase _useCase;

        public ExpenseController(ISaveAmountRecordUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Update amount records of expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns>200</returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] SaveExpenseAmountRecordRequest request)
        {
            var amountRecords = request.AmountRecords.Select(x => new AmountRecord(x.Amount, x.Description));
            SaveAmountRecordResult result = await _useCase.Execute<Expense>(request.AccountId, request.ExpenseId, amountRecords);
            return Ok(result);
        }
    }
}
