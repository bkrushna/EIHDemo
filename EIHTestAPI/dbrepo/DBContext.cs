using EIHTest.Helpers;
using EIHTest.Interfaces;
using EIHTest.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.dbrepo
{
    public class MongoContext : IMongoContext
    {
        private IMongoCollection<Contact> _contactCollection;
        private IMongoCollection<LogModel> _logCollection;
        private readonly AppSettings _appSettings;

        public IMongoDatabase Database { get; private set; }
        public MongoContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("CamelCaseConvensions", pack, t => true);

            var mongoUrlBuilder = new MongoUrlBuilder(_appSettings.ConnectionString);
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            Database = mongoClient.GetDatabase(mongoUrlBuilder.DatabaseName);

            _contactCollection = Database.GetCollection<Contact>("Contact");

            _logCollection = Database.GetCollection<LogModel>("Log");
        }

        public IMongoCollection<Contact> Contact
        {
            get { return _contactCollection; }
        }

        public IMongoCollection<LogModel> Log
        {
            get { return _logCollection; }
        }
    }
}
