using Auxilia.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Auxilia.Presentation.Wpf.Controls
{
	public class OverlappingPanel : Panel
	{
		public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
			nameof(Spacing),
			typeof(double),
			typeof(OverlappingPanel));
		public double Spacing
		{
			get => (double)GetValue(SpacingProperty);
			set => SetValue(SpacingProperty, value);
		}

		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
			nameof(Orientation),
			typeof(Orientation),
			typeof(OverlappingPanel));
		public Orientation Orientation
		{
			get => (Orientation)GetValue(OrientationProperty);
			set => SetValue(OrientationProperty, value);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			List<UIElement> children = InternalChildren.OfType<UIElement>().ToList();
			children.Execute(c => c.Measure(availableSize));

			if (!children.Any())
				return availableSize;

			double totalWidth = children.Sum(c => c.DesiredSize.Width);
			double totalHeight = children.Sum(c => c.DesiredSize.Height);
			double maxWidth = children.Max(c => c.DesiredSize.Width);
			double maxHeight = children.Max(c => c.DesiredSize.Height);

			double spacing = (children.Count - 1) * Spacing;

			return Orientation == Orientation.Horizontal
				? new Size(totalWidth + spacing, maxHeight)
				: new Size(maxWidth, totalHeight + spacing);
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			List<UIElement> children = InternalChildren.OfType<UIElement>().ToList();
			children.Execute(c => c.Measure(finalSize));

			double position = 0;

			if (Orientation == Orientation.Horizontal)
			{
				double desiredWidth = children.Sum(c => c.DesiredSize.Width);
				double margin = Math.Min(Spacing, (DesiredSize.Width - desiredWidth) / (children.Count - 1));
				foreach (UIElement child in children)
				{
					child.Arrange(new Rect(new Point(position, 0), child.DesiredSize));
					position += child.DesiredSize.Width + margin;
				}
			}
			else
			{
				double desiredHeight = children.Sum(c => c.DesiredSize.Height);
				double margin = Math.Min(Spacing, (DesiredSize.Height - desiredHeight) / (children.Count - 1));

				foreach (UIElement child in children)
				{
					child.Arrange(new Rect(new Point(0, position), child.DesiredSize));
					position += child.DesiredSize.Height + margin;
				}
			}

			return finalSize;
		}
	}
}
