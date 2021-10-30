using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Auxilia.Presentation.Wpf.Properties
{
	public static class ItemsControlProperties
	{
		private const string AutoScrollToBottomName = "AutoScrollToBottom";
		public static readonly DependencyProperty AutoScrollToBottomProperty = DependencyProperty.RegisterAttached(
			  AutoScrollToBottomName,
			  typeof(bool),
			  typeof(ItemsControl),
			  new FrameworkPropertyMetadata(AutoScrollToBottomLogic.OnAutoScrollToBottomChanged));
		public static bool GetAutoScrollToBottom(DependencyObject element)
		{
			return (bool)element.GetValue(AutoScrollToBottomProperty);
		}
		public static void SetAutoScrollToBottom(DependencyObject element, bool value)
		{
			element.SetValue(AutoScrollToBottomProperty, value);
		}

		private static class AutoScrollToBottomLogic
		{
			private static readonly Dictionary<ItemsControl, NotifyCollectionChangedEventHandler> _eventHandlers = new Dictionary<ItemsControl, NotifyCollectionChangedEventHandler>();

			public static void OnAutoScrollToBottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
			{
				if (d is ItemsControl itemsControl)
				{
					if(itemsControl.Parent is ScrollViewer scrollViewer)
					{
						if ((bool)e.NewValue)
							Subscribe(itemsControl, scrollViewer);
						else
							Unsubscribe(itemsControl);
					}
					else
					{
						throw new NotSupportedException($"{AutoScrollToBottomName} is only supported for ItemsControls inside a ScrollViewer.");
					}
				}
			}

			private static void Subscribe(ItemsControl itemsControl, ScrollViewer scrollViewer)
			{
				NotifyCollectionChangedEventHandler handler = (sender, args) => scrollViewer.ScrollToBottom();
				_eventHandlers.Add(itemsControl, handler);
				((INotifyCollectionChanged) itemsControl.Items).CollectionChanged += handler;
			}

			private static void Unsubscribe(ItemsControl itemsControl)
			{
				if (_eventHandlers.TryGetValue(itemsControl, out NotifyCollectionChangedEventHandler handler))
				{
					((INotifyCollectionChanged) itemsControl.Items).CollectionChanged -= handler;
					_eventHandlers.Remove(itemsControl);
				}
			}
		}
	}
}
