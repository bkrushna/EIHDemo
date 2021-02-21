using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.Models
{
    public class LogModel
    {

        public LogModel()
        {
            AddedOn = DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fff");
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Message { get; set; }
        public int LogLevel { get; set; }
        public string AddedOn { get; set; }


    }
}
