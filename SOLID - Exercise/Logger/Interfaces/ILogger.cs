﻿using Logging.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Interfaces
{
    public interface ILogger
    {
        void Log(DateTime dateTime, ReportLevel reportLevel, string message);
        void Error(DateTime dateTime, string message)
            => Log(dateTime, ReportLevel.Error, message);
        void Info(DateTime dateTime, string message)
            => Log(dateTime, ReportLevel.Info, message);
        void Fatal(DateTime dateTime, string message)
            => Log(dateTime, ReportLevel.Fatal, message);
        void Critical(DateTime dateTime, string message)
            => Log(dateTime, ReportLevel.Critical, message);
        void Warning(DateTime dateTime, string message)
            => Log(dateTime, ReportLevel.Warning, message);
    }
}
