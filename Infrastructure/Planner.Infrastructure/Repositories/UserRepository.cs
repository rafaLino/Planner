using MongoDB.Driver;
using MongoDB.Driver.GridFS;
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
                await _context
                     .Bucket
                     .UploadFromBytesAsync(id, picture._name, picture._bytes, new GridFSUploadOptions
                     {
                         Metadata = new MongoDB.Bson.BsonDocument
                         {
                            {"type", picture._type }
                         }
                     }); ;
            }
        }

        private async Task<Picture> GetPicture(Guid id)
        {
            GridFSFileInfo<Guid> info = await _context
                .Bucket
                .Find(Builders<GridFSFileInfo<Guid>>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();

            if (info == null)
                return default;

            byte[] bytes = await _context.Bucket.DownloadAsBytesAsync(id);

            return Picture.Load(bytes, info.Length, info.Metadata["type"].AsString, info.Filename);
        }
    }
}
