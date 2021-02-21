using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIHTestPortal.Models.DB
{
    public class User
    {
        public User()
        {
            CreatedOn= DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fff");
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public string CreatedOn { get; set; }

    }

    public class AppLog
    {
        public AppLog()
        {
            CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fff");
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string Message { get; set; }
        public string CreatedOn { get; set; }
    }
}