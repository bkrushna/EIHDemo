using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIHTest.Interfaces;
using EIHTest.Models;
using Microsoft.Extensions.Options;

namespace EIHTest.Helpers
{

    public enum AppLogLevel { Info, Warn, Error, Excep };
    //this will save logs to database
    public class DBLog : ILog
    {
        private readonly AppSettings _appSettings;

        private readonly ILogService _logService;

        public DBLog(IOptions<AppSettings> appSettings,ILogService logService)
        {
            _appSettings = appSettings.Value;
            _logService = logService;
        }
        public  void InfoLog(string msg) {
            if (_appSettings.LogLevel == (int)AppLogLevel.Info)
            {
                msg = "Info:    " + msg;

                LogModel log = new LogModel();
                log.LogLevel = _appSettings.LogLevel;
                log.Message = msg;
                _logService.AddLog(log);
            }
        }

        public void WarnLog(string msg) {

            if (_appSettings.LogLevel == (int)AppLogLevel.Warn)
            {
                msg = "Warn:    " + msg;

                LogModel log = new LogModel();
                log.LogLevel = _appSettings.LogLevel;
                log.Message = msg;
                _logService.AddLog(log);
            }
        }

        public void ErrorLog(string msg) {
            msg = "Error:    " + msg;
            LogModel log = new LogModel();
            log.LogLevel = _appSettings.LogLevel;
            log.Message = msg;
            _logService.AddLog(log);
        }

        public void ExceptionLog(string msg) {

            msg = "Exception:    " + msg;
            LogModel log = new LogModel();
            log.LogLevel = _appSettings.LogLevel;
            log.Message = msg;
            _logService.AddLog(log);
        }

       
    }
}
