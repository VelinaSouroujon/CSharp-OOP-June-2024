using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    public class ModifyPrice
    {
        private readonly List<ICommand> commands;
        private ICommand command;

        public ModifyPrice()
        {
            commands = new List<ICommand>();
        }
        public void SetCommand(ICommand command)
        {
            this.command = command;
        }
        public void Invoke()
        {
            commands.Add(command);
            command.ExecuteAction();
        }
    }
}
