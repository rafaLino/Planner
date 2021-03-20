using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.DeleteAccount;
using System;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.DeleteAccount
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IDeleteAccountUseCase _deleteAccountUseCase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteAccountUseCase"></param>
        public UserController(IDeleteAccountUseCase deleteAccountUseCase)
        {
            _deleteAccountUseCase = deleteAccountUseCase;
        }

        /// <summary>
        /// Delete user account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> signOut</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _deleteAccountUseCase.Execute(userId);
            return Ok();
        }
    }
}
