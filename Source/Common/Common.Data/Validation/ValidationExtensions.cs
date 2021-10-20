using Auxilia.Extensions;

namespace Auxilia.Data
{
    public static class ValidationExtensions
    {
		public static bool IsValid(this IValidatable validatableObject)
		{
			validatableObject.ThrowIfNull(nameof(validatableObject));
			return validatableObject.IsValid(out ValidationCollection _);
		}

		public static bool IsValid(this IValidatable validatableObject, ValidationCollection validations)
		{
			validatableObject.ThrowIfNull(nameof(validatableObject));
			validations.ThrowIfNull(nameof(validations));

			validatableObject.Validate(validations);
			return validations.IsValid;
		}

		public static bool IsValid(this IValidatable validatableObject, out ValidationCollection validations)
		{
			validatableObject.ThrowIfNull(nameof(validatableObject));

			validations = new ValidationCollection();
			validatableObject.Validate(validations);
			return validations.IsValid;
		}
	}
}
