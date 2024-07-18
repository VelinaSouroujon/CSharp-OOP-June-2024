using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging.Enums;

namespace Logging.Interfaces
{
    public interface IAppender
    {
        int AppendedMessagesCount { get; }
        void Append(ILogMessage logMessage);
    }
}
