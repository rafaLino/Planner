﻿using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.RemoveExpense
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IRemoveFinanceStatementUseCase _remove;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remove"></param>
        public ExpenseController(IRemoveFinanceStatementUseCase remove)
        {
            _remove = remove;
        }

        /// <summary>
        /// remove an expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns code="200">return remove results</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemoveExpenseRequest request)
        {
            RemoveFinanceStatementResult result = await _remove.Execute<Expense>(request.AccountId, request.ExpenseId);

            return Ok(result);
        }
    }
}
