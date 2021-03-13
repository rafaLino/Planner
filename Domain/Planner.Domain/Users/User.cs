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

        public Picture Picture { get; set; }

        private User() { }

        public User(Guid accountId, Email email, Password password, string name, Picture picture = null)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Email = email;
            Name = name;
            Password = password;
            Picture = picture;

        }

        public static User Load(Guid accountId, Guid id, Email email, Password password, string name, Picture picture)
        {
            User user = new User();
            user.AccountId = accountId;
            user.Id = id;
            user.Name = name;
            user.Email = email;
            user.Password = password;
            user.Picture = picture;
            return user;
        }
    }
}
