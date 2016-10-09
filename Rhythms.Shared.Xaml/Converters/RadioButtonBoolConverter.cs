using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Rhythms.Shared.Xaml.Converters
{
	public class RadioButtonBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool param;
			bool obj;

			if (bool.TryParse(parameter.ToString(), out param) && bool.TryParse(value.ToString(), out obj))
			{
				return param == obj;
			}

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool param;

			if (bool.TryParse(parameter.ToString(), out param))
			{
				return param;
			}

			return null;
		}
	}
}
