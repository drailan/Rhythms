using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Rhythms.Shared.Xaml.Converters
{
	public class DateToPrettyStringConverter : IValueConverter
	{
		public string Format { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var date = (DateTime)value;

			return date.ToString(Format);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
