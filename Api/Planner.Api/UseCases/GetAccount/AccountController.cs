using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.Application.Queries;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.GetAccount
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountQueries _accountQueries;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountQueries"></param>
        public AccountController(IAccountQueries accountQueries)
        {
            _accountQueries = accountQueries;
        }

        /// <summary>
        /// Get fully account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns code="200">Return account</returns>
        [HttpGet("{accountId}")]
        [Authorize]
        public async Task<IActionResult> Get(System.Guid accountId)
        {
            var account = await _accountQueries.GetAccount(accountId);

            return Ok(account);
        }
    }
}
