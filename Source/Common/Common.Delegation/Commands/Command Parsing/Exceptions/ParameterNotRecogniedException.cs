namespace Auxilia.Delegation.Commands
{
	public class ParameterNotRecogniedException : CommandParsingException
	{
		public ParameterNotRecogniedException(string input, string commandName, string parameterName)
			: base(input, commandName, $"Parameter \"{parameterName}\" not recognized.")
		{
			ParameterName = parameterName;
		}

		public string ParameterName { get; }
	}
}
