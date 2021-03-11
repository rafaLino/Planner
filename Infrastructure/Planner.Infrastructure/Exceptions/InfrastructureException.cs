using System;

namespace Planner.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message)
        {

        }
    }
}
