using System;

namespace Planner.Application.Commands.SignUp
{
    public class SignUpResult
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }
    }
}
