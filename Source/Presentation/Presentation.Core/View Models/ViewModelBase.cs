using System;

namespace Auxilia.Presentation.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, IDisposable
    {
	    private bool _isDisposed;
		
		~ViewModelBase()
		{
			Dispose(false);
		}

		internal ViewModelBase Owner { get; set; }
		
	    public void Dispose()
	    {
			Dispose(true);
			GC.SuppressFinalize(this);
	    }
	    private void Dispose(bool isDisposing)
	    {
			if(_isDisposed)
				return;

			ProtectedDispose(isDisposing);

			_isDisposed = true;
	    }
	    protected virtual void ProtectedDispose(bool isDisposing)
	    {
	    }
    }
}
