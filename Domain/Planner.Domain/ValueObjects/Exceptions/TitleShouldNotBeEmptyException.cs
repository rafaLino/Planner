using Planner.Domain.Exceptions;

namespace Planner.Domain.ValueObjects.Exceptions
{
    public class TitleShouldNotBeEmptyException : DomainException
    {
        public TitleShouldNotBeEmptyException(string message) : base(message)
        {
        }
    }
}
