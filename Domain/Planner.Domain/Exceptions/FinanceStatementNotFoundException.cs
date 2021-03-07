namespace Planner.Domain.Exceptions
{
    public class FinanceStatementNotFoundException : DomainException
    {
        public FinanceStatementNotFoundException(string message) : base(message)
        {
        }
    }
}
