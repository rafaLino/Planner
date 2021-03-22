using MongoDB.Driver;
using Planner.Application.Repositories;
using Planner.Domain.Users;
using Planner.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Planner.Infrastructure.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> Any(Email email)
        {
            return await _context
                .Users
                .Find(x => x.Email == email)
                .AnyAsync();
        }

        public async Task<User> Get(Email email)
        {
            Entities.User user = await _context
                .Users
                .Find(x => x.Email == email)
                .SingleOrDefaultAsync();

            Picture picture = await GetPicture(user.Id);
            Password password = Password.Load(user.Password, user.Salt);

            User result = User.Load(
                user.AccountId,
                user.Id,
                user.Name,
                user.Email,
                password,
                picture);

            return result;
        }

        public async Task Create(User user)
        {
            Entities.User userEntity = new Entities.User
            {
                AccountId = user.AccountId,
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password._hash,
                Salt = user.Password._salt
            };


            await _context.Users.InsertOneAsync(userEntity);
            await SavePicture(user.Id, user.Picture);
        }

        private async Task SavePicture(Guid id, Picture picture)
        {
            if (picture != null)
            {
                Entities.Picture entity = new Entities.Picture
                {
                    Id = id,
                    Bytes = picture._bytes,
                    Name = picture._name,
                    Size = picture._size,
                    Type = picture._type
                };
                await _context.Pictures.ReplaceOneAsync(x => x.Id == id, entity, new ReplaceOptions { IsUpsert = true });
            }
        }

        private async Task<Picture> GetPicture(Guid id)
        {

            Entities.Picture entity = await _context.Pictures.Find(x => x.Id == id).FirstOrDefaultAsync();

            return Picture.Load(entity.Bytes, entity.Size, entity.Type, entity.Name);
        }

        public async Task Remove(Guid userId)
        {
            var user = await _context.Users.FindOneAndDeleteAsync(x => x.Id == userId);

            await _context.Expenses.Find(x => x.AccountId == user.AccountId)
                .ForEachAsync(async x =>
                {
                    await _context.AmountRecords.DeleteManyAsync(y => y.FinanceStatementId == x.Id);
                });

            await _context.Incomes.Find(x => x.AccountId == user.AccountId)
                .ForEachAsync(async x =>
                {
                    await _context.AmountRecords.DeleteManyAsync(y => y.FinanceStatementId == x.Id);
                });

            await _context.Investments.Find(x => x.AccountId == user.AccountId)
                .ForEachAsync(async x =>
                {
                    await _context.AmountRecords.DeleteManyAsync(y => y.FinanceStatementId == x.Id);
                });

            await _context.Expenses.DeleteManyAsync(x => x.AccountId == user.AccountId);
            await _context.Incomes.DeleteManyAsync(x => x.AccountId == user.AccountId);
            await _context.Investments.DeleteManyAsync(x => x.AccountId == user.AccountId);
            await _context.Accounts.DeleteOneAsync(x => x.Id == user.AccountId);
            await _context.Pictures.DeleteOneAsync(x => x.Id == user.Id);
        }
    }
}
