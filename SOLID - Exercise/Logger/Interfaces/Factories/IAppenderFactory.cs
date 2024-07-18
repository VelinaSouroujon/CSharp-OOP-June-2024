using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Interfaces.Factories
{
    public interface IAppenderFactory
    {
        IAppender CreateAppender(ILayout layout, Func<ILogMessage, bool>? filter = null);
    }
}
