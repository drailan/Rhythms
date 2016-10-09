using Rhythms.Shared.Extensions;
using Rhythms.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using ParentedEntryItem = Presentation.ParentedItem<Rhythms.Shared.Entities.Entry, Rhythms.Shared.Presentation.PeopleViewModel>;

namespace Rhythms.Shared.Xaml.Converters
{
	public class PeopleTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Single { get; set; }
		public DataTemplate Double { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var entry = (item as ParentedEntryItem)?.Item;

			if (entry.SecondBirthDate != null && !entry.SecondName.IsNullOrEmpty())
			{
				return Double;
			}

			return Single;
		}
	}
}
