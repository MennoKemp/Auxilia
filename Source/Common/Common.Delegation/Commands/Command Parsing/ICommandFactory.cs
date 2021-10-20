using System;
using System.Windows.Input;

namespace Auxilia.Delegation.Commands
{
	public interface ICommandFactory<out T> where T : ICommand
	{
		T CreateCommand(Type commandType);
	}
}
