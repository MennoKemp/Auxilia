using System;
using System.Globalization;

namespace Auxilia.Presentation.Converters
{
	public class EnumToIntConverter : ConverterBase
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value?.GetType().IsEnum ?? false
				? System.Convert.ToInt32(value)
				: -1;
		}
	}
}
