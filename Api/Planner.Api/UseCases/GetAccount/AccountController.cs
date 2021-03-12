using Microsoft.AspNetCore.Mvc;
using Planner.Application.Queries;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.GetAccount
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountQueries _accountQueries;

        public AccountController(IAccountQueries accountQueries)
        {
            _accountQueries = accountQueries;
        }

        /// <summary>
        /// Get an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>200</returns>
        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(System.Guid accountId)
        {
            var account = await _accountQueries.GetAccount(accountId);

            return Ok(account);
        }
    }
}
