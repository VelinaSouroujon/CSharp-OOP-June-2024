using Logging.Enums;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Layouts
{
    public class XmlLayout : ILayout
    {
        public string Format(ILogMessage logMessage)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<log>");

            sb.AppendLine($"\t<date>{logMessage.Time}</date>");
            sb.AppendLine($"\t<level>{logMessage.ReportLevel}</level>");
            sb.AppendLine($"\t<message>{logMessage.Message}</message>");

            sb.AppendLine("</log>");

            return sb.ToString().TrimEnd();
        }
    }
}
