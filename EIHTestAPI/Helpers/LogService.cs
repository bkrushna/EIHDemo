using EIHTest.Interfaces;
using EIHTest.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace EIHTest.Helpers
{
    public class LogService : ILogService
    {
        private readonly AppSettings _appSettings;
        private readonly IMongoContext _mongoContext;

        public LogService(IOptions<AppSettings> appSettings, IMongoContext mongoContext)
        {
            _appSettings = appSettings.Value;
            _mongoContext = mongoContext;
        }

        public bool AddLog(LogModel log)
        {
            try
            {
                _mongoContext.Log.InsertOne(log);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<LogModel> GetAll()
        {
            var lst = _mongoContext.Log.Find(_ => true).ToList<LogModel>();
            return lst;
        }

        public IEnumerable<LogModel> GetAllLogsByLevel(int level)
        {
            var lst = _mongoContext.Log.Find(x=>x.LogLevel==level).ToList<LogModel>();
            return lst;
        }
    }
}
