using Planner.Application.Results;
using System;

namespace Planner.Application.Commands.SignIn
{
    public class SignInResult
    {
        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public PictureResult Picture { get; set; }
        public string Token { get; set; }
    }
}
