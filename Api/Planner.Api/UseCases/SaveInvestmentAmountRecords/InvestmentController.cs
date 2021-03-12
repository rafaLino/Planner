using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.SaveAmountRecord;
using Planner.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.SaveInvestmentAmountRecords
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly ISaveAmountRecordUseCase _useCase;

        public InvestmentController(ISaveAmountRecordUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Update amount records of expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns>200</returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] SaveInvestmentAmountRecordRequest request)
        {
            var amountRecords = request.AmountRecords.Select(x => AmountRecord.Load(x.Id, x.Description, x.Amount));
            SaveAmountRecordResult result = await _useCase.Execute<Investment>(request.AccountId, request.InvestmentId, amountRecords);
            return Ok(result);
        }
    }
}
