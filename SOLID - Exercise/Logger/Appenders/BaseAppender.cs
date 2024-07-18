using Logging.Enums;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Appenders
{
    public abstract class BaseAppender : IAppender
    {
        private readonly ILayout layout;
        private readonly Func<ILogMessage, bool>? filter;

        protected BaseAppender(ILayout layout, Func<ILogMessage, bool>? filter = null)
        {
            this.layout = layout;
            this.filter = filter;
        }

        public int AppendedMessagesCount { get; private set; }

        public void Append(ILogMessage logMessage)
        {
            if(filter is not null && !filter(logMessage))
            {
                return;
            }

            string formattedMessage = layout.Format(logMessage);

            Append(formattedMessage);

            AppendedMessagesCount++;
        }

        protected abstract void Append(string formattedMessage);
    }
}
