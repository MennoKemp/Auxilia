using System;
using System.Globalization;
using System.Windows.Media;

namespace Auxilia.Presentation.Converters
{
	public class ColorToBrushConverter : ConverterBase
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is Color color
				? new SolidColorBrush(color)
				: Brushes.White;
		}
	}
}
