using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Rhythms.Shared.Entities;

namespace Rhythms.Shared.Xaml.Converters
{
	public class GraphStateTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Single { get; set; }
		public DataTemplate Double { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var state = item as GraphState;

			if (state.SecondStates != null)
			{
				return Double;
			}

			return Single;
		}
	}
}
