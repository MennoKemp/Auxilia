using Auxilia.Extensions;
using System;

namespace Auxilia.Utilities
{
	public class ExecutingCommandEventArgs : EventArgs
	{
		public ExecutingCommandEventArgs(string command)
		{
			Command = command.ThrowIfNull(nameof(command));
		}

		public string Command { get; }
	}
}
