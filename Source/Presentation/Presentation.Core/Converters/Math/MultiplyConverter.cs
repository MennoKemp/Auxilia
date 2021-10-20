using System;
using System.Globalization;

namespace Auxilia.Presentation.Converters
{
	public class MultiplyConverter : ConverterBase
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return double.TryParse(value?.ToString(), out double a) && double.TryParse(parameter?.ToString(), out double b)
				? a * b
				: 0;
		}
	}
}
