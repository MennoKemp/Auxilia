using Auxilia.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Auxilia.Delegation.Commands
{
	public class CommandParser<T> where T : ICommand
    {
        private static readonly BindingFlags ParameterBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;

        private readonly List<Command> _registeredCommands = new List<Command>();

        private readonly ICommandFactory<T> _commandFactory;
        private readonly ResourceManager _resourceManager;
        
        public CommandParser(ICommandFactory<T> commandFactory, ResourceManager resourceManager)
        {
	        _commandFactory = commandFactory.ThrowIfNull(nameof(commandFactory));
            _resourceManager = resourceManager.ThrowIfNull(nameof(resourceManager));
        }
        public CommandParser(ICommandFactory<T> commandFactory, ResourceManager resourceManager, IEnumerable<Type> commandsToRegister)
			: this(commandFactory, resourceManager)
        {
	        commandsToRegister.ThrowIfNull(nameof(commandsToRegister)).Execute(Register);
        }

        public string ArgumentPattern { get; set; } = @"(\"".*?\""|\d*\.\d+|\d+)";
        public string ParameterPattern { get; set; } = @"(\w+)=(\"".*?\""|\d*\.\d+|\d+)";

        public IEnumerable<string> RegisteredCommands
        {
            get => _registeredCommands.Select(c => c.Name).Order();
        }

        private Command this[T command]
        {
            get => _registeredCommands.FirstOrDefault(c => c.Type == command.GetType());
        }
        private Command this[string commandName]
        {
	        get => _registeredCommands.FirstOrDefault(c => c.Name.Equals(commandName, true));
        }
        private Parameter this[string commandName, string parameter]
        {
            get => this[commandName]?.Parameters.FirstOrDefault(p => p.Name.Equals(parameter, true));
        }
        private Parameter this[T command, string parameter]
        {
	        get => this[command]?.Parameters.FirstOrDefault(p => p.Name.Equals(parameter));
        }

        public void Register<T>()
        {
            Register(typeof(T));
        }
        public void Register(Type commandType)
        {
	        if (commandType.GetCustomAttribute<CommandAttribute>() is not CommandAttribute commandAttribute)
		        throw new NotSupportedException($"\"{commandType.Name}\" does not support parsing.");

	        if (_registeredCommands.Any(c => c.Type == commandType))
		        throw new ArgumentException($"Command \"{commandAttribute.Name}\" has already been registered.");

	        Command command = new Command(commandType, commandAttribute);

	        foreach (PropertyInfo property in commandType.GetProperties(ParameterBindingFlags))
	        {
		        if (property.GetCustomAttribute<ParameterAttribute>() is ParameterAttribute parameterAttribute)
			        command.AddParameter(new Parameter(command, property, parameterAttribute));
	        }

	        _registeredCommands.Add(command);
        }

		public string ParseCommandName(string input)
		{
			string commandName = input?.Split(' ').First();

			if (string.IsNullOrEmpty(commandName))
				throw new CommandParsingException(input, commandName, "No command given.");

			if (_registeredCommands.FirstOrDefault(c => c.Name.Equals(commandName, true)) is not Command registeredCommand)
				throw new CommandParsingException(input, commandName, "Command not registered.");

			return commandName;
		}

		public T ParseCommand(string input)
        {
            string commandName = input?.Split(' ').First();

            if (string.IsNullOrEmpty(commandName))
	            throw new CommandParsingException(input, commandName, "No command given.");
            
            if (_registeredCommands.FirstOrDefault(c => c.Name.Equals(commandName, true)) is not Command registeredCommand)
                throw new CommandParsingException(input, commandName, "Command not registered.");

            T parsedCommand = _commandFactory.CreateCommand(registeredCommand.Type);
            commandName = registeredCommand.Name;

            List<Parameter> requiredParameters = registeredCommand.Parameters
                .Where(p => !p.IsOptional)
                .ToList();

            List<IGrouping<string, Match>> inputParameters = Regex.Matches(input.Right(input.Length - commandName.Length).Trim(), ParameterPattern)
                .GroupBy(p => p.Groups[0].Value, StringComparer.OrdinalIgnoreCase)
                .ToList();

            List<string> duplicateParameters = inputParameters
                .Where(p => p.Count() > 1)
                .Select(p => p.Key)
                .ToList();

            if (duplicateParameters.Any())
	            throw new DuplicateParametersException(input, commandName, duplicateParameters);

            foreach (Match inputParameter in inputParameters.Select(p => p.Single()))
            {
                string parameter = inputParameter.Groups[1].Value;
                string argument = inputParameter.Groups[2].Value.Trim('"');

                if (registeredCommand.Parameters.FirstOrDefault(p => p.Name.Equals(parameter, true)) is not Parameter registeredParameter)
	                throw new ParameterNotRecogniedException(input, commandName, parameter);

                object value = string.IsNullOrWhiteSpace(argument) 
	                ? null 
	                : Convert.ChangeType(argument, registeredParameter.PropertyInfo.PropertyType);

                registeredParameter.PropertyInfo.SetValue(parsedCommand, value);

                requiredParameters.Remove(registeredParameter);
            }

            if (requiredParameters.Any())
	            throw new MissingParametersException(input, commandName, requiredParameters.SelectStrings().ToList());

            return parsedCommand;
        }

        public string GetCommandInfo(string commandName)
        {
            return GetCommandInfo(this[commandName]);
        }
        public string GetCommandInfo(T command)
        {
            return GetCommandInfo(this[command]);
        }

        public string GetParameterInfo(string commandName, string parameterName)
        {
            return GetParameterInfo(this[commandName, parameterName]);
        }
        public string GetParameterInfo(T command, string parameterName)
        {
            return GetParameterInfo(this[command, parameterName]);
        }

        public string GetFormattedCommandInfo(T command)
        {
	        return GetFormattedCommandInfo(this[command]?.Name);
        }
        public string GetFormattedCommandInfo(string commandName)
        {
	        if (_resourceManager == null)
		        return null;

	        if (this[commandName] is not Command registeredCommand)
		        return null;

	        StringBuilder info = new StringBuilder();
	        info.AppendLine($"{registeredCommand.Name}: {_resourceManager.GetString(registeredCommand.Name)}");

	        foreach (Parameter parameter in registeredCommand.Parameters)
	        {
		        info.Append($"{parameter.Name,-30} - ");

		        if (parameter.IsOptional)
			        info.Append("(Optional) ");

		        string resourceName = $"{registeredCommand.Name}_{parameter.Name}";
		        info.AppendLine(_resourceManager.GetString(resourceName));
	        }

	        return info.ToString().RemoveTail(Environment.NewLine);
        }

        private Dictionary<string, string> ParseArguments(string input, Command command)
        {
	        //string argumentString = input.Right(input.Length - command.Name.Length).Trim();
            
	        //MatchCollection parameterMatches = Regex.Matches(argumentString, ParameterPattern).GroupBy(p => p.Groups[0].Value);



	        //MatchCollection argumentMatches = Regex.Matches(argumentString, ArgumentPattern);



	        //List<IGrouping<string, Match>> inputParameters = Regex.Matches(input.Right(input.Length - command.Name.Length).Trim(), ParameterPattern)
		       // .GroupBy(p => p.Groups[0].Value, StringComparer.OrdinalIgnoreCase)
		       // .ToList();

		       return null;
        }

        private string GetCommandInfo(Command command)
        {
	        if (command == null || _resourceManager == null)
		        return null;

	        return command.Name.ConcatenateTo(command.GetBaseTypes()
			        .Select(t => t.Name.RemoveTail("Command", true)))
		        .Select(n => _resourceManager.GetString(n))
		        .FirstOrDefault();
        }

        private string GetParameterInfo(Parameter parameter)
        {
	        if (parameter == null || _resourceManager == null)
		        return null;

	        return parameter.Command.Name.ConcatenateTo(parameter.Command.GetBaseTypes()
			        .Select(t => t.Name.RemoveTail("Command", true)))
		        .Select(n => _resourceManager.GetString($"{n}_{parameter.Name}"))
		        .FirstOrDefault();
        }
    }
}