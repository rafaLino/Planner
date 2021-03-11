namespace Planner.Infrastructure.Exceptions
{
    public class AccountNotFoundException : InfrastructureException
    {
        public AccountNotFoundException(string message) : base(message)
        {
        }
    }
}
