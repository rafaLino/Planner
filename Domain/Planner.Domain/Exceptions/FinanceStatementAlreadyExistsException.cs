namespace Planner.Domain.Exceptions
{
    public class FinanceStatementAlreadyExistsException : DomainException
    {
        public FinanceStatementAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
