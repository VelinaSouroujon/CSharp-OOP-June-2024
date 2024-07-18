using Logging.Enums;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Models
{
    public class LogMessage : ILogMessage
    {
        public LogMessage(DateTime time, ReportLevel reportLevel, string message)
        {
            Time = time;
            ReportLevel = reportLevel;
            Message = message;
        }

        public DateTime Time { get; }

        public ReportLevel ReportLevel { get; }

        public string Message { get; }
    }
}
