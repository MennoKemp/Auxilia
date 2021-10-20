using System;
using System.Globalization;

namespace Auxilia.Presentation.Converters
{
	public class IsGreaterThanConverter : ConverterBase
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return double.TryParse(value?.ToString(), out double number) && double.TryParse(parameter?.ToString(), out double argument) && number > argument;
		}
	}
}
