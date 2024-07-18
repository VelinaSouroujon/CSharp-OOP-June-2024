using Logging.Appenders;
using Logging.Interfaces;
using Logging.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Factories.Appenders
{
    public class ConsoleAppenderFactory : IAppenderFactory
    {
        public IAppender CreateAppender(ILayout layout, Func<ILogMessage, bool>? filter = null)
        {
            return new ConsoleAppender(layout, filter);
        }
    }
}
