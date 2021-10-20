using System;

namespace Auxilia.Delegation.Commands
{
	public class CommandParsingException : Exception
	{
		public CommandParsingException(string input, string commandName, string message)
			: base(message)
		{
			Input = input;
			CommandName = commandName;
		}

		public string Input { get; }
		public string CommandName { get; }
	}
}
