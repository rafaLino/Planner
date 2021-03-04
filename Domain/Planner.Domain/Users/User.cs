using Planner.Domain.ValueObjects;

namespace Planner.Domain.Users
{
    public sealed class User : IEntity
    {

        public string AccountId { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }

        private User() { }

        public User(Email email, string name, string password)
        {
            Email = email;
            Name = name;
            Password = password;
        }


        public void Register(string accountId)
        {
            AccountId = accountId;
        }

        public static User Load(string accountId, string id, string name, Email email, string password)
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
