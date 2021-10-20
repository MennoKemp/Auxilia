namespace Auxilia.Delegation.Commands
{
	internal class Argument
	{
		public Argument(string value)
		{
			Value = value;
		}

		public Argument(string parameterName, string value)
		{
			ParameterName = parameterName;
			Value = value;
		}

		public string ParameterName { get; }
		public string Value { get; }
	}
}
