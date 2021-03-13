using Planner.Domain.ValueObjects;

namespace Planner.Application.Commands.SignUp
{
    public class SignUpCommand
    {
        public Picture Picture { get; set; }
        public Email Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
