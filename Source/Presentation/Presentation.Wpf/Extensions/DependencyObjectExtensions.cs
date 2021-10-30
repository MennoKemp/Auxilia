using System.Windows;
using System.Windows.Media;

namespace Auxilia.Presentation.Wpf.Extensions
{
	public static class DependencyObjectExtensions
	{
		public static T GetVisualChild<T>(this DependencyObject parent) 
			where T : Visual
		{
			T child = default;

			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				Visual visual = (Visual)VisualTreeHelper.GetChild(parent, i);
				child = visual as T ?? GetVisualChild<T>(visual);
				if (child != null)
					break;
			}
			return child;
		}

		public static T GetVisualParent<T>(this DependencyObject child)
			where T : Visual
		{
			Visual visual = (Visual)VisualTreeHelper.GetParent(child);
			return visual as T ?? GetVisualParent<T>(visual);
		}
	}
}
