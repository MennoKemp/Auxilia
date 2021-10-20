using Auxilia.Extensions;
using System.Collections.Generic;

namespace Auxilia.Delegation.Commands
{
	public class DuplicateParametersException : CommandParsingException
	{
		public DuplicateParametersException(string input, string commandName, IEnumerable<string> duplicateParameters)
			: base(input, commandName, $"Duplicate parameters found: {duplicateParameters.Combine(", ")}")
		{
		}
	}
}
