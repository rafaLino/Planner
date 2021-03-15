using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planner.Api.Model;
using Planner.Application.Commands.SignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Api.UseCases.SignIn
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISignInUseCase _useCase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useCase"></param>
        public UserController(ISignInUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Sign In an account
        /// </summary>
        /// <param name="request"></param>
        /// <returns code="200">logged result</returns>
        [HttpPost("SignIn")]
        public async Task<IActionResult> Post([FromBody] SignInRequest request)
        {
            Application.Commands.SignIn.SignInResult result = await _useCase.Execute(request.Email, request.Password);
            return Ok(result);
        }
    }
}
