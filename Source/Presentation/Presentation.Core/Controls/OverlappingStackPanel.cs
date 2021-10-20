using System;
using System.Windows.Controls;

namespace Auxilia.Presentation.Controls
{
	public class OverlappingStackPanel : StackPanel
	{
		public OverlappingStackPanel()
		{
			LayoutUpdated += OnLayoutUpdated;
		}

		private void OnLayoutUpdated(object sender, EventArgs e)
		{
		}
	}
}
