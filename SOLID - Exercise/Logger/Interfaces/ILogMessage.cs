using Logging.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Interfaces
{
    public interface ILogMessage
    {
        DateTime Time { get; }
        ReportLevel ReportLevel { get; }
        string Message { get; }
    }
}
