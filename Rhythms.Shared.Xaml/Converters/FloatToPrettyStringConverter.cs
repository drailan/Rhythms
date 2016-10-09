using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Rhythms.Shared.Xaml.Converters
{
	public class FloatToPrettyStringConverter : IValueConverter
	{
		public string Format { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var str = (float)value;

			return str.ToString(Format);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
