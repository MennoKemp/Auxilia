using System;
using System.ComponentModel;

namespace Auxilia.Presentation.ViewModels
{
	public abstract class WindowViewModelBase : ViewModelBase
	{
		public event EventHandler RequestClose;

		public CancelEventHandler ClosingHandler { get; }

		protected WindowViewModelBase()
		{
			ClosingHandler = OnClosing;
		}

		protected void Close()
		{
			RequestClose?.Invoke(this, new EventArgs());
		}

		protected virtual void OnClosing(object sender, CancelEventArgs e)
		{
		}
	}
}
