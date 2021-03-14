using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.SignUp;
using Planner.Domain.Users;
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
        /// <returns>return user account ids</returns>
        [HttpPost("SignUp")]
        public async Task<IActionResult> Post([FromBody] SignUpRequest request)
        {
            Picture picture = null;
            if (request.Picture != null)
            {
                picture = Picture.Create(
                      request.Picture.Bytes,
                      request.Picture.Size,
                      request.Picture.Type,
                      request.Picture.Name
                      );
            }
            SignUpCommand command = new SignUpCommand
            {
                Picture = picture,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            SignUpResult result = await _useCase.Execute(command);

            return Created(Request.Path, result);
        }
    }
}
