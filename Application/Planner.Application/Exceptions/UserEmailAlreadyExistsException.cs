namespace Planner.Application.Exceptions
{
    public class UserEmailAlreadyExistsException : ApplicationException
    {
        public UserEmailAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
