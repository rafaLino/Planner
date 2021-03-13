namespace Planner.Application.Exceptions
{
    public class PasswordNotMatchException : ApplicationException
    {
        public PasswordNotMatchException(string message) : base(message)
        {
        }
    }
}
