using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace Rhythms.Shared.Business
{
	public class RangeEnabledObservableCollection<T> : ObservableCollection<T>
	{
		public RangeEnabledObservableCollection(IEnumerable<T> collection) : base(collection) { }

		public RangeEnabledObservableCollection() : base() { }

		public void InsertRange(IEnumerable<T> items)
		{
			CheckReentrancy();

			if (items == null)
			{
				return;
			}

			foreach (var item in items)
			{
				Items.Add(item);
			}

			Application.Current.Dispatcher.BeginInvoke(
				System.Windows.Threading.DispatcherPriority.Background, 
				new Action(() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)))
			);
		}
	}
}
