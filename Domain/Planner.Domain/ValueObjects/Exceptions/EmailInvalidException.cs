using Planner.Domain.Exceptions;

namespace Planner.Domain.ValueObjects.Exceptions
{
    public class EmailInvalidException : DomainException
    {
        public EmailInvalidException(string message) : base(message)
        {
        }
    }
}
