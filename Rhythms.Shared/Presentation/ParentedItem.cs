using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation
{
	/// <summary>
	/// Allows exposing a list of items with access to the parent view-model, without requiring each item
	/// to be view-models themselves.
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	/// <typeparam name="TParent"></typeparam>
	public class ParentedItem<TItem, TParent>
	{
		public ParentedItem(TItem item, TParent parent)
		{
			this.Item = item;
			this.Parent = parent;
		}

		public TItem Item { get; }

		public TParent Parent { get; }

		public override int GetHashCode() => this.Item?.GetHashCode() ?? 0;

		public override bool Equals(object obj)
		{
			var pItem = obj as ParentedItem<TItem, TParent>;
			if (pItem != null)
			{
				return pItem.Item.Equals(this.Item);
			}

			return false;
		}
	}
}
