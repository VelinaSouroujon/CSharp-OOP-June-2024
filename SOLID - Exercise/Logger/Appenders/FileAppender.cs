using Logging.Enums;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Appenders
{
    public class FileAppender : BaseAppender, IAppender
    {
        private readonly string fileName;

        public FileAppender(string fileName, ILayout layout, Func<ILogMessage, bool>? filter = null)
            : base(layout, filter)
        {
            this.fileName = fileName;
        }

        protected override void Append(string formattedMessage)
        {
            File.AppendAllLines(fileName, new string[] { formattedMessage });
        }
    }
}
