using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SignIn
{
    /// <summary>
    /// 
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
