using Logging.Enums;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Appenders
{
    public class ConsoleAppender : BaseAppender, IAppender
    {
        public ConsoleAppender(ILayout layout, Func<ILogMessage, bool>? filter = null)
            : base(layout, filter)
        {

        }

        protected override void Append(string formattedMessage)
        {
            Console.WriteLine(formattedMessage);
        }
    }
}
