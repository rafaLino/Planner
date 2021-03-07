namespace Planner.Domain.Exceptions
{
    public class FinanceStatementCollectionNotFoundException : DomainException
    {
        public FinanceStatementCollectionNotFoundException(string message) : base(message)
        {
        }
    }
}
