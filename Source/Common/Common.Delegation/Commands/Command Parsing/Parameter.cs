using Auxilia.Extensions;
using System;
using System.Reflection;

namespace Auxilia.Delegation.Commands
{
	internal class Parameter : IEquatable<Parameter>, IComparable<Parameter>
    {
        public Parameter(Command command, PropertyInfo propertyInfo, ParameterAttribute parameterAttribute)
        {
            Command = command.ThrowIfNull(nameof(command));
            PropertyInfo = propertyInfo.ThrowIfNull(nameof(propertyInfo));
            Attribute = parameterAttribute.ThrowIfNull(nameof(parameterAttribute));
        }

        public Command Command { get; }
        public PropertyInfo PropertyInfo { get; }
        public ParameterAttribute Attribute { get; }
        public string Name
        {
            get => Attribute.Name;
        }
        public bool IsOptional
        {
            get => Attribute.IsOptional;
        }

        public int CompareTo(Parameter other)
        {
            if (other == null)
                return 1;

            int result = IsOptional.CompareTo(other.IsOptional);

            return result == 0 
                ? StringComparer.OrdinalIgnoreCase.Compare(Name, other.Name)
                : result;
        }

        public bool Equals(Parameter other)
        {
            return Name.Equals(other?.Name);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Parameter);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
