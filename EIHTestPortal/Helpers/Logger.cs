using EIHTestPortal.Models.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EIHTestPortal.Helpers
{
    /// <summary>
    /// Logger class will store logs into database
    /// </summary>
    public class Logger
    {
        DBHelper dbObj;

        public Logger()
        {
            var conn_str = ConfigurationManager.AppSettings["connectionString"];
            dbObj = new DBHelper(conn_str);
        }


        public void LogInfo(string msg)
        {
            AppLog log = new AppLog();
            log.Message = "Info: " + msg;
            dbObj.LogException(log);

        }
        public void LogError(string msg)
        {
            AppLog log = new AppLog();
            log.Message = "Error: " + msg;
            dbObj.LogException(log);

        }
        public void LogException(string msg)
        {
            AppLog log = new AppLog();
            log.Message = "Exception: " + msg;
            dbObj.LogException(log);

        }
    }
}