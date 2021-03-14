using System;

namespace Planner.Infrastructure.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public Guid AccountId { get; set; }

    }
}
