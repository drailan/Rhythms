using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Presentation;

namespace Rhythms.Shared.Extensions
{
	public static class ViewModelBaseExtentions
	{
		/// <summary>
		/// Wraps the given item in a <see cref="ParentedItem{TItem,TParent}"/> with the current ViewModel set
		/// as the <see cref="ParentedItem{TItem,TParent}.Parent"/>
		/// </summary>
		/// <param name="vm">The current ViewModel</param>
		/// <param name="item">The item that will be paired with the current ViewModel as its parent</param>
		/// <returns>A <see cref="ParentedItem{TItem,TParent}"/> with the current ViewModel as the parent</returns>
		public static ParentedItem<TItem, TViewModel> CreateParented<TItem, TViewModel>(
			this TViewModel vm,
			TItem item)
			where TViewModel : ViewModelBase
		{
			return new ParentedItem<TItem, TViewModel>(item, vm);
		}
	}
}
