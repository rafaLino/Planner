﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.Api.Model;
using Planner.Application.Commands.UpdateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.UpdateInvestment
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IUpdateFinanceStatementUseCase _update;

        /// <summary>
        /// 
        /// </summary>
        public InvestmentController(IUpdateFinanceStatementUseCase update)
        {
            _update = update;
        }

        /// <summary>
        ///  update title of investment
        /// </summary>
        /// <param name="request"></param>
        /// <returns code="204"></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UpdateFinanceStatementRequest request)
        {
            await _update.Execute<Investment>(request.AccountId, request.Id, request.Title);

            return NoContent();
        }
    }
}
