using Logging.Enums;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Layouts
{
    public class SimpleLayout : ILayout
    {
        public string Format(ILogMessage logMessage)
        {
            return $"{logMessage.Time} - {logMessage.ReportLevel} - {logMessage.Message}";
        }
    }
}
