using System;
using System.Globalization;

namespace Auxilia.Presentation.Converters
{
	public class DivideConverter : ConverterBase
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return double.TryParse(value?.ToString(), out double a) && double.TryParse(parameter?.ToString(), out double b) && b != 0
				? a / b
				: 0;
		}
	}
}
