using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly Dictionary<string, Type> cache = new Dictionary<string, Type>();

        private const string CommandTypeNameSuffix = "Command";
        public string Read(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                throw new ArgumentException("Command input should not be empty");
            }

            string[] commandInfo = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string commandName = commandInfo[0];

            Assembly assembly = Assembly.GetCallingAssembly();

            Type commandType = FindCommandType(assembly, commandName);
            if (commandType is null)
            {
                throw new InvalidOperationException($"Command \"{commandName}\" was not found");
            }

            ICommand commandInstance = Activator.CreateInstance(commandType) as ICommand;

            return commandInstance.Execute(commandInfo.Skip(1).ToArray());
        }
        private Type FindCommandType(Assembly assembly, string commandName)
        {
            string cacheKey = $"{assembly.Location}|{commandName}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            Type[] types = assembly.GetTypes();
            Type commandInterfaceType = typeof(ICommand);

            string expectedCommandTypeName = $"{commandName}{CommandTypeNameSuffix}";

            foreach (Type type in types.Where(t => t.IsClass && !t.IsAbstract))
            {
                bool isCommand = type.IsAssignableTo(commandInterfaceType);

                if (isCommand && type.Name == expectedCommandTypeName)
                {
                    cache[cacheKey] = type;
                    return type;
                }
            }

            return null;
        }
    }
}
