using Auxilia.Extensions;
using System;

namespace Auxilia.Data
{
	public class ValidationMessage : IEquatable<ValidationMessage>
    {
        internal ValidationMessage(string message, ValidationLevel level = ValidationLevel.Error)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Level = level.ThrowIfNotDefined(nameof(level));
        }

        public string Message { get; }
        public ValidationLevel Level { get; }
        public bool IsValid
        {
            get => Level != ValidationLevel.Error;
        }

        public override string ToString()
        {
            return Message;
        }

        public bool Equals(ValidationMessage other)
        {
            return Message.Equals(other?.Message, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ValidationMessage);
        }

        public override int GetHashCode()
        {
            return Message.GetHashCode();
        }
    }
}
