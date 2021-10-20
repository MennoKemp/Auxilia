using System;
using System.Globalization;

namespace Auxilia.Presentation.Converters
{
	public class TimeSpanToStringConverter : ConverterBase
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string result = string.Empty;

			if (value is TimeSpan timeSpan)
			{
				if (timeSpan.TotalSeconds < 0)
				{
					timeSpan = timeSpan.Negate();
					result += "-";
				}

				result += $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
			}

			return result;
		}
	}
}
