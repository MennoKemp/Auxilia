using Auxilia.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Auxilia.Delegation.Commands
{
	/// <summary>
	/// Controls the execution of an action and checking its condition.
	/// </summary>
	public abstract class CommandBase : ICommand
	{
	    protected CommandBase()
		{
			CommandName = GetType().Name.RemoveTail("Command");
		}

		/// <summary>
		/// Occurs when <see cref="CanExecute(object)"/> changed.
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		
		public string CommandName { get; }
		public bool RunInBackground { get; set; }
		
		public void Execute(object parameter = null)
        {
            if (RunInBackground)
	            Task.Run(() => ExecuteCommand(parameter));
			else
				ExecuteCommand(parameter);
        }
		protected abstract void ExecuteCommand(object parameter = null);

		public abstract bool CanExecute(object parameter = null);

		public static void UpdateCommandConditions()
		{
			Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
		}

	}
}
