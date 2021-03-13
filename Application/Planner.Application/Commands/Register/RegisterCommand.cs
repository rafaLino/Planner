using Planner.Domain.ValueObjects;

namespace Planner.Application.Commands.Register
{
    public class RegisterCommand
    {
        public Email Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
