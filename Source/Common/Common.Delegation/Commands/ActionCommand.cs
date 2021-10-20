using Auxilia.Extensions;
using System;

namespace Auxilia.Delegation.Commands
{
	/// <summary>
	/// Controls the execution of an action and checking its condition.
	/// </summary>
	public sealed class ActionCommand : CommandBase
	{
		private readonly Action _action;
		private readonly Func<bool> _condition;

		/// <summary>
		/// Initializes new instance of <see cref="ActionCommand"/>.
		/// </summary>
		/// <param name="action">Action to execute.</param>
		public ActionCommand(Action action)
			: this(action, null)
		{
		}
		/// <summary>
		/// Initializes new instance of <see cref="ActionCommand"/>.
		/// </summary>
		/// <param name="action">Action to execute.</param>
		/// <param name="condition">Condition that must be met.</param>
		public ActionCommand(Action action, Func<bool> condition)
		{
			_action = action.ThrowIfNull(nameof(action));
			_condition = condition;
		}
		
        protected override void ExecuteCommand(object parameter = null)
        {
			_action.Invoke();
        }

		/// <summary>
		/// Checks if the condition passes.
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
        public override bool CanExecute(object parameter = null)
        {
			return _condition?.Invoke() ?? true;
        }
	}

	/// <summary>
	/// Bindable command used to handle actions invoked through the UI.
	/// </summary>
	public sealed class ActionCommand<T> : CommandBase
	{
		private readonly Action<T> _action;
		private readonly Func<T, bool> _condition;

		public ActionCommand(Action<T> action)
			: this(action, null)
		{
		}
		public ActionCommand(Action<T> action, Func<T, bool> condition)
		{
			_action = action.ThrowIfNull(nameof(action));
			_condition = condition;
		}

		protected override void ExecuteCommand(object parameter = null)
		{
			_action.Invoke((T) parameter);
		}
		
        public override bool CanExecute(object parameter = null)
        {
			return _condition?.Invoke((T)parameter) ?? true;
		}
	}
}
