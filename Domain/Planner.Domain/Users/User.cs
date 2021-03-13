using Planner.Domain.ValueObjects;
using System;

namespace Planner.Domain.Users
{
    public sealed class User : IEntity
    {

        public Guid AccountId { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }

        private User() { }

        public User(Guid accountId, Email email, Password password, string name)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Email = email;
            Name = name;
            Password = password;

        }


        public void Register(Guid accountId)
        {
            AccountId = accountId;
        }

        public static User Load(Guid accountId, Guid id, string name, Email email, Password password)
        {
            User user = new User();
            user.AccountId = accountId;
            user.Id = id;
            user.Name = name;
            user.Email = email;
            user.Password = password;
            return user;
        }
    }
}
