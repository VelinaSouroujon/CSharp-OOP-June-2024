using System;
using System.Globalization;
using Logging.Appenders;
using Logging.Enums;
using Logging.Factories.Appenders;
using Logging.Factories.Layouts;
using Logging.Interfaces;
using Logging.Interfaces.Factories;
using Logging.Layouts;
using Logging.Loggers;

namespace Logging
{
    public class Program
    {
        private static Dictionary<string, ILayoutFactory> layoutFactories =
            CreateLayoutFactories();

        private static Dictionary<string, IAppenderFactory> appenderFactories =
            CreateAppenderFactories();
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            IAppender[] appenders = new IAppender[n];

            for (int i = 0; i < n; i++)
            {
                string[] appenderInfo = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                appenders[i] = CreateAppender(appenderInfo);
            }

            ILogger logger = new Logger(appenders);

            string logMessageInput = "";
            while((logMessageInput = Console.ReadLine()).ToLower() != "end")
            {
                string[] logMessageData = logMessageInput.Split('|');

                ReportLevel reportLevel = Enum.Parse<ReportLevel>(logMessageData[0], true);
                DateTime time = DateTime.Parse(logMessageData[1], CultureInfo.InvariantCulture);
                string message = logMessageData[2];

                logger.Log(time, reportLevel, message);
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Appender #{i + 1} -> Messages count: {appenders[i].AppendedMessagesCount}");
            }
        }
        private static IAppender CreateAppender(string[] data)
        {
            string appenderType = data[0];
            string layoutType = data[1];

            Func<ILogMessage, bool>? filter = data.Length > 2
                ? lm => lm.ReportLevel >= Enum.Parse<ReportLevel>(data[2], true)
                : null;

            ILayout layout = layoutFactories[layoutType].CreateLayout();

            return appenderFactories[appenderType].CreateAppender(layout, filter);
        }
        private static Dictionary<string, ILayoutFactory> CreateLayoutFactories()
        {
            return new Dictionary<string, ILayoutFactory>()
            {
                [nameof(SimpleLayout)] = new SimpleLayoutFactory(),
                [nameof(XmlLayout)] = new XmlLayoutFactory()
            };
        }
        private static Dictionary<string, IAppenderFactory> CreateAppenderFactories()
        {
            return new Dictionary<string, IAppenderFactory>()
            {
                [nameof(ConsoleAppender)] = new ConsoleAppenderFactory(),
                [nameof(FileAppender)] = new FileAppenderFactory()
            };
        }
    }
}
