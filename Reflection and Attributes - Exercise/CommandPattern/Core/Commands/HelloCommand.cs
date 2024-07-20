using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core.Commands
{
    public class HelloCommand : ICommand
    {
        public string Execute(string[] args)
        {
            if(args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            if(args.Length != 1)
            {
                throw new InvalidOperationException("Hello command requires a single argument");
            }

            return $"Hello, {args[0]}";
        }
    }
}
