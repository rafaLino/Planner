using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Planner.Infrastructure.Entities;

namespace Planner.Infrastructure
{
    public class Context
    {

        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _dataBase;
        private readonly PlannerAppConfig _config;

        public Context(IOptions<PlannerAppConfig> config)
        {
            _config = config.Value;
            _mongoClient = new MongoClient(_config.ConnectionString);
            _dataBase = _mongoClient.GetDatabase(_config.DataBase);
            Map();
        }

        public IMongoCollection<Account> Accounts
        {
            get
            {
                return _dataBase.GetCollection<Account>(nameof(Accounts));
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _dataBase.GetCollection<User>(nameof(Users));
            }
        }

        public IMongoCollection<AmountRecord> AmountRecords
        {
            get
            {
                return _dataBase.GetCollection<AmountRecord>(nameof(AmountRecords));
            }
        }


        public IMongoCollection<FinanceStatement> Incomes
        {
            get
            {
                return _dataBase.GetCollection<FinanceStatement>(nameof(Incomes));
            }
        }

        public IMongoCollection<FinanceStatement> Expenses
        {
            get
            {
                return _dataBase.GetCollection<FinanceStatement>(nameof(Expenses));
            }
        }

        public IMongoCollection<FinanceStatement> Investments
        {
            get
            {
                return _dataBase.GetCollection<FinanceStatement>(nameof(Investments));
            }
        }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<Account>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<FinanceStatement>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<AmountRecord>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
