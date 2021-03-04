using Planner.Domain.Exceptions;

namespace Planner.Domain.ValueObjects.Exceptions
{
    public class EmailShouldNotBeEmptyException : DomainException
    {
        public EmailShouldNotBeEmptyException(string message) : base(message)
        {
        }
    }
}
