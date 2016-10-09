using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Rhythms.Shared.Xaml.Converters
{
	public class BoolToCustomValueConverter : IValueConverter
	{
		public object TrueValue { get; set; }
		public object FalseValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool result = false;
			if (bool.TryParse(value.ToString(), out result))
			{
				if (result)
				{
					return TrueValue;
				}

				return FalseValue;
			}

			return FalseValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
