using System.Windows;
using System.Windows.Media;

namespace Auxilia.Presentation.Extensions
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
				Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
				child = v as T ?? GetVisualChild<T>(v);
				if (child != null)
					break;
			}
			return child;
		}
    }
}
