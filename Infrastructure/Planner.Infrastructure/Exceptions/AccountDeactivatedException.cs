namespace Planner.Infrastructure.Exceptions
{
    public class AccountDeactivatedException : InfrastructureException
    {
        public AccountDeactivatedException(string message) : base(message)
        {
        }
    }
}
