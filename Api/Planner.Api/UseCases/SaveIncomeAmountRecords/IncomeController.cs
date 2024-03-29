﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.Api.Model;
using Planner.Application.Commands.SaveAmountRecord;
using Planner.Domain.Accounts;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.SaveIncomeAmountRecords
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly ISaveAmountRecordUseCase _useCase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useCase"></param>
        public IncomeController(ISaveAmountRecordUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Update amount records of income
        /// </summary>
        /// <param name="request"></param>
        /// <returns>return income result</returns>
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Patch([FromBody] SaveAmountRecordRequest request)
        {
            var amountRecords = request.AmountRecords.Select(x => AmountRecord.Load(x.Id, x.Description, x.Amount));
            SaveAmountRecordResult result = await _useCase.Execute<Income>(request.AccountId, request.Id, amountRecords);
            return Ok(result);
        }
    }
}
