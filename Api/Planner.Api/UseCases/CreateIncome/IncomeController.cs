using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.CreateFinanceStatement;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.CreateIncome
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class IncomeController : ControllerBase
    {
        private readonly ICreateFinanceStatementUseCase _create;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="create"></param>
        public IncomeController(ICreateFinanceStatementUseCase create)
        {
            _create = create;
        }

        /// <summary>
        ///  Create an income
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return income result</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateIncomeRequest request)
        {
            CreateFinanceStatementResult result = await _create.Execute<Income>(request.AccountId, request.Title, request.Amount);

            return Created(Request.Path, result);
        }
    }
}
