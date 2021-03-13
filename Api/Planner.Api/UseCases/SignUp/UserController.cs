using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.SignUp;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.SignUp
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISignUpUseCase _useCase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useCase"></param>
        public UserController(ISignUpUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        ///  Sign up for a new account
        /// </summary>
        /// <returns>return account id</returns>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            SignUpResult result = await _useCase.Execute(null);

            return Created(Request.Path, result);
        }
    }
}
