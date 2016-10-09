using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace Rhythms.Shared.Business
{
	public class RangeEnabledObservableCollection<T> : ObservableCollection<T>
	{
		public RangeEnabledObservableCollection(IEnumerable<T> collection) : base(collection) { }

		public RangeEnabledObservableCollection() : base() { }

		public void InsertRange(IEnumerable<T> items)
		{
			CheckReentrancy();

			foreach (var item in items)
			{
				Items.Add(item);
			}

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}
}
