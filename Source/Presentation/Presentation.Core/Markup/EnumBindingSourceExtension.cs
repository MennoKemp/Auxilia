using System;
using System.Windows.Markup;

namespace Auxilia.Presentation.Markup
{
	public class EnumBindingSourceExtension : MarkupExtension
	{
		private Type _enumType;

		public EnumBindingSourceExtension()
		{
		}

		public EnumBindingSourceExtension(Type enumType)
		{
			EnumType = enumType;
		}

		public Type EnumType
		{
			get => _enumType;
			set
			{
				if (value != _enumType)
				{
					if (!(Nullable.GetUnderlyingType(value) ?? value).IsEnum)
						throw new ArgumentException("Not an enum.", nameof(value));

					_enumType = value;
				}
			}
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (EnumType is null)
				throw new InvalidOperationException("Enum type is not specified.");

			Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
			Array enumValues = Enum.GetValues(actualEnumType);

			if (actualEnumType == _enumType)
				return enumValues;

			Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
			enumValues.CopyTo(tempArray, 1);
			return tempArray;
		}
	}

}
