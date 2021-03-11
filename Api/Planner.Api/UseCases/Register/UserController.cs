using Microsoft.AspNetCore.Mvc;
using Planner.Application.Commands.Register;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUseCase _register;

        public UserController(IRegisterUseCase register)
        {
            _register = register;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            RegisterResult result = await _register.Execute();

            return Created(Request.Path, result);
        }
    }
}
