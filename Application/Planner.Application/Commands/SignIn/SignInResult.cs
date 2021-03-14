using Planner.Domain.Users;
using System;

namespace Planner.Application.Commands.SignIn
{
    public class SignInResult
    {
        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }
        public Picture Picture { get; set; }
    }
}
