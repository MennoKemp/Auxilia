using Auxilia.Extensions;
using System;
using System.Collections.Generic;

namespace Auxilia.Delegation.Commands
{
	internal class Command : IEquatable<Command>
    {
        private readonly SortedList<Parameter, Parameter> _parameters = new SortedList<Parameter, Parameter>();

        public Command(Type commandType, CommandAttribute commandAttribute)
        {
            Type = commandType.ThrowIfNull(nameof(commandType));
            Attribute = commandAttribute.ThrowIfNull(nameof(commandAttribute));
        }

        public Type Type { get; }
        public CommandAttribute Attribute { get; }
        public string Name
        {
            get => Attribute.Name;
        }
        
        public IEnumerable<Parameter> Parameters
        {
            get => _parameters.Values;
        }

        public void AddParameter(Parameter parameter)
        {
            parameter.ThrowIfNull(nameof(parameter));
            _parameters.Add(parameter, parameter);
        }

        public bool Equals(Command other)
        {
            return Name.Equals(other?.Name);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Command);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public IList<Type> GetBaseTypes()
        {
	        List<Type> baseTypes = new List<Type>();

	        Type baseType = Type.BaseType;

	        while (baseType != null)
	        {
		        baseTypes.Add(baseType);
		        baseType = baseType.BaseType;
	        }

	        return baseTypes;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
