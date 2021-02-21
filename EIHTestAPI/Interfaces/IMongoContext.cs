using EIHTest.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.Interfaces
{
    public interface IMongoContext
    {
         IMongoCollection<Contact> Contact { get; }

        IMongoCollection<LogModel> Log { get; }
    }
}
