using EIHTestPortal.Models.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIHTestPortal.Helpers
{
    public class DBHelper
    {
        IMongoCollection<User> _userCollection;

        IMongoCollection<AppLog> _logCollection;

        public DBHelper(string ConnectionString)
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("CamelCaseConvensions", pack, t => true);

            var mongoUrlBuilder = new MongoUrlBuilder(ConnectionString);
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            var Database = mongoClient.GetDatabase(mongoUrlBuilder.DatabaseName);

            _userCollection = Database.GetCollection<User>("User");

            _logCollection = Database.GetCollection<AppLog>("AppLog");
        }
        public void AddUser(User obj)
        {
            _userCollection.InsertOne(obj);
       }

        public User GetUser(string emailaddress)
        {
            return _userCollection.Find<User>(x => x.EmailAddress.ToLower() == emailaddress.ToLower()).SingleOrDefault();
        }

        public void LogException(AppLog msg)
        {
            _logCollection.InsertOne(msg);
        }
    }
}