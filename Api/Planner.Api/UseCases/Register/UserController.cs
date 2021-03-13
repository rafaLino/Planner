using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.Register;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.Register
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUseCase _register;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        public UserController(IRegisterUseCase register)
        {
            _register = register;
        }

        /// <summary>
        ///  Sign up for a new account
        /// </summary>
        /// <returns>return account id</returns>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            RegisterResult result = await _register.Execute(null);

            return Created(Request.Path, result);
        }
    }
}
