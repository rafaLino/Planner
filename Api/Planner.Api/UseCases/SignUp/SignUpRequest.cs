using Planner.Api.Model;

namespace Planner.Api.UseCases.SignUp
{
    /// <summary>
    /// 
    /// </summary>
    public class SignUpRequest
    {
        /// <summary>
        /// profile picture
        /// </summary>
        public PictureModel Picture { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// user email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        public string Password { get; set; }


    }
}
