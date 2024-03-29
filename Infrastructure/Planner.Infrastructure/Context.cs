﻿using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Planner.Infrastructure.Entities;
using System.Security.Authentication;

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
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                        new MongoUrl(_config.ConnectionString));
            settings.SslSettings =
            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            _mongoClient = new MongoClient(settings);
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

        public IMongoCollection<Picture> Pictures
        {
            get
            {
                return _dataBase.GetCollection<Picture>(nameof(Pictures));
            }
        }

        private static void Map()
        {
            var serializer = new GuidSerializer(MongoDB.Bson.BsonType.String);
            BsonClassMap.RegisterClassMap<Account>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                .SetSerializer(serializer)
                .SetIgnoreIfDefault(true);
            });

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                .SetSerializer(serializer);

                cm.GetMemberMap(x => x.AccountId)
                .SetSerializer(serializer);
            });

            BsonClassMap.RegisterClassMap<FinanceStatement>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                .SetSerializer(serializer);

                cm.GetMemberMap(x => x.AccountId).SetSerializer(serializer);
            });

            BsonClassMap.RegisterClassMap<AmountRecord>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                .SetSerializer(serializer);

                cm.GetMemberMap(x => x.FinanceStatementId).SetSerializer(serializer);
            });

            BsonClassMap.RegisterClassMap<Picture>(cm =>
            {
                cm.AutoMap();
                cm.MapIdField(x => x.Id)
                .SetSerializer(serializer);
            });
        }

    }
}
