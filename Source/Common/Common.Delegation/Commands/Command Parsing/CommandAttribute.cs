using Auxilia.Extensions;
using System;
using System.Windows.Input;

namespace Auxilia.Delegation.Commands
{
	[AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(Type commandType)
        {
            commandType.ThrowIfNull(nameof(commandType));

            if (!typeof(ICommand).IsAssignableFrom(commandType))
                throw new NotSupportedException($"{nameof(CommandAttribute)} is only supported for commands implementing {nameof(ICommand)}.");

            Name = commandType.Name.RemoveTail("Command");
        }

        public string Name { get; }
    }
}