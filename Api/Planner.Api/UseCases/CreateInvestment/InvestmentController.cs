﻿using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.CreateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.CreateInvestment
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly ICreateFinanceStatementUseCase _create;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="create"></param>
        public InvestmentController(ICreateFinanceStatementUseCase create)
        {
            _create = create;
        }

        /// <summary>
        /// Create an investment
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return investment result</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInvestmentRequest request)
        {
            CreateFinanceStatementResult result = await _create.Execute<Investment>(request.AccountId, request.Title, request.Amount);

            return Created(Request.Path, result);
        }
    }
}
