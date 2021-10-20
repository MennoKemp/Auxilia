using Auxilia.Extensions;
using System.Collections.Generic;

namespace Auxilia.Delegation.Commands
{
	public class MissingParametersException : CommandParsingException
	{
		public MissingParametersException(string input, string commandName, IList<string> missingParameters)
			: base(input, commandName, $"Missing parameters: {missingParameters?.Combine(", ")}.")
		{
			MissingParameters = missingParameters ?? new List<string>();
		}

		public IList<string> MissingParameters { get; }
	}
}
