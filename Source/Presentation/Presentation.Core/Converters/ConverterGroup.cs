using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Auxilia.Presentation.Converters
{
	public class ConverterGroup : List<IValueConverter>, IValueConverter
	{
		public object[] ConverterParameters { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Dictionary<IValueConverter, object> parameters = ExtractParameters(parameter);
			return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameters[converter], culture));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Dictionary<IValueConverter, object> parameters = ExtractParameters(parameter);
			return this.AsEnumerable().Reverse().Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameters[converter], culture));
		}

		private Dictionary<IValueConverter, object> ExtractParameters(object parameter)
		{
			Dictionary<IValueConverter, object> parameters = this.AsEnumerable().ToDictionary(c => c, _ => (object)null);

			object[] arguments = ConverterParameters ?? parameter as object[] ?? Array.Empty<object>();

			for (int c = 0; c < Count; c++)
				parameters[this[c]] = arguments.ElementAtOrDefault(c);

			return parameters;
		}
	}
}
