namespace Planner.Domain.Exceptions
{
    public class FinanceStatementExistsException : DomainException
    {
        public FinanceStatementExistsException(string message) : base(message)
        {
        }
    }
}
