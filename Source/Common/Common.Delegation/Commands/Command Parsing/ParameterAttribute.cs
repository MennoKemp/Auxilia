using Auxilia.Extensions;
using System;

namespace Auxilia.Delegation.Commands
{
	[AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute
    {
        public ParameterAttribute(string name, bool isOptional = false)
        {
            Name = name.ThrowIfNullOrEmpty(nameof(name));
            IsOptional = isOptional;
        }

        public string Name { get; }
        public bool IsOptional { get; }
    }
}
