using Logging.Enums;
using Logging.Interfaces;
using Logging.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Loggers
{
    public class Logger : ILogger
    {
        private readonly IAppender[] appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }
        public void Log(DateTime dateTime, ReportLevel reportLevel, string message)
        {
            ILogMessage logMessage = new LogMessage(dateTime, reportLevel, message);

            foreach (IAppender appender in appenders)
            {
                appender.Append(logMessage);
            }
        }
    }
}
