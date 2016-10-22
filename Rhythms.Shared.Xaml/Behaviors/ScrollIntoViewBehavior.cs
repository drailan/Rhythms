using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Interactivity;
using Rhythms.Shared.Entities;
using System.Windows.Media;

namespace Rhythms.Shared.Xaml.Behaviors
{
	public class ScrollIntoViewBehavior : Behavior<ListView>
	{
		public Guid Trigger
		{
			get { return (Guid)GetValue(TriggerProperty); }
			set { SetValue(TriggerProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Trigger.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TriggerProperty =
			DependencyProperty.Register("Trigger", typeof(Guid), typeof(ScrollIntoViewBehavior), new PropertyMetadata(default(Guid), OnDateChanged));


		public int Scale
		{
			get { return (int)GetValue(ScaleProperty); }
			set { SetValue(ScaleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ScaleProperty =
			DependencyProperty.Register("Scale", typeof(int), typeof(ScrollIntoViewBehavior), new PropertyMetadata(0/*, OnDateChanged*/));


		public DateTime Today
		{
			get { return (DateTime)GetValue(TodayProperty); }
			set { SetValue(TodayProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Today.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TodayProperty =
			DependencyProperty.Register("Today", typeof(DateTime), typeof(ScrollIntoViewBehavior), new PropertyMetadata(default(DateTime), OnDateChanged));

		private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var b = d as ScrollIntoViewBehavior;
			if (b != null)
			{
				b.ScrollIntoView();
			}
		}

		private void ScrollIntoView()
		{
			var scrollViewer = FindChild<ScrollViewer>(AssociatedObject, "");

			var listItem = AssociatedObject.Items
				.Cast<GraphState>()
				.Where(o => o.Date.Date == Today.Date)
				.FirstOrDefault();

			var itemWidth = 92;

			if (listItem.SecondStates != null)
			{
				itemWidth = 170;
			}

			if (listItem != null)
			{
				var index = AssociatedObject.Items.IndexOf(listItem);
				scrollViewer.ScrollToHorizontalOffset(itemWidth * index);
			}
		}

		protected override void OnAttached()
		{
			base.OnAttached();
		}

		public static T FindChild<T>(DependencyObject parent, string childName)
		   where T : DependencyObject
		{
			// Confirm parent and childName are valid. 
			if (parent == null)
				return null;

			T foundChild = null;

			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				// If the child is not of the request child type child
				T childType = child as T;
				if (childType == null)
				{
					// recursively drill down the tree
					foundChild = FindChild<T>(child, childName);

					// If the child is found, break so we do not overwrite the found child. 
					if (foundChild != null)
						break;
				}
				else if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;
					// If the child's name is set for search
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						// if the child's name is of the request name
						foundChild = (T)child;
						break;
					}
				}
				else
				{
					// child element found.
					foundChild = (T)child;
					break;
				}
			}

			return foundChild;
		}
	}
}
