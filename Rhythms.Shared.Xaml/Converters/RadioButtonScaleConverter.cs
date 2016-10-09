using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Rhythms.Shared.Xaml.Converters
{
	public class RadioButtonScaleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int param;
			int obj;

			if (int.TryParse(parameter.ToString(), out param) && int.TryParse(value.ToString(), out obj))
			{
				return param == obj;
			}

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int param;

			if (int.TryParse(parameter.ToString(), out param))
			{
				return param;
			}

			return null;
		}
	}
}
