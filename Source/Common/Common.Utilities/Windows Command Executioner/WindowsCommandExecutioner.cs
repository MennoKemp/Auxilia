using Auxilia.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Auxilia.Utilities
{
	public class WindowsCommandExecutioner : IDisposable
    {
        private readonly CommandExecutionSettings _executionSettings;

        private readonly CommandPromptWrapper _commandPromptWrapper;
        private readonly EventHandler<DataReceivedEventArgs> _outputReceivedHandler;
        private readonly EventHandler<DataReceivedEventArgs> _errorReceivedHandler;

        private readonly List<string> _output = new List<string>();
        private readonly List<string> _errors = new List<string>();

        private bool _isDisposed;

        public WindowsCommandExecutioner(CommandExecutionSettings executionSettings)
        {
            _executionSettings = executionSettings.ThrowIfNull(nameof(executionSettings));

            _commandPromptWrapper = new CommandPromptWrapper(executionSettings.CommandPromptSettings);

            _outputReceivedHandler = OnOutputReceived;
            _commandPromptWrapper.OutputReceived += _outputReceivedHandler;

            _errorReceivedHandler = OnErrorReceived;
            _commandPromptWrapper.ErrorReceived += _errorReceivedHandler;
        }
        ~WindowsCommandExecutioner()
        {
            Dispose(false);
        }

        public IReadOnlyList<string> Output
        {
            get => _output.AsReadOnly();
        }
        public IReadOnlyList<string> Errors
        {
            get => _errors.AsReadOnly();
        }

        public bool IsRunning
        {
	        get => _commandPromptWrapper.IsRunning;
        }

        public void ExecuteCommand(string command)
        {
	        if (_isDisposed)
		        throw new ObjectDisposedException(nameof(WindowsCommandExecutioner));

            if (!_commandPromptWrapper.IsRunning)
                _commandPromptWrapper.Start();

            _commandPromptWrapper.ExecuteCommand(command);

            if (_executionSettings.StopAfterExecution)
                Stop();
        }
        public void ExecuteCommands(IEnumerable<string> commands)
        {
	        if (_isDisposed)
		        throw new ObjectDisposedException(nameof(WindowsCommandExecutioner));

            if (!_commandPromptWrapper.IsRunning)
                _commandPromptWrapper.Start();

            commands.Execute(_commandPromptWrapper.ExecuteCommand);
            
            if (_executionSettings.StopAfterExecution)
                Stop();
        }

        public void Stop()
        {
            _commandPromptWrapper.Stop();
            Dispose();
        }
        public void Abort()
        {
            _commandPromptWrapper.Abort();
            Dispose();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if(_commandPromptWrapper != null)
            {
                _commandPromptWrapper.OutputReceived -= _outputReceivedHandler;
                _commandPromptWrapper.ErrorReceived -= _errorReceivedHandler;
                _commandPromptWrapper.Dispose();
            }

            _isDisposed = true;
        }

        private void OnOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Data))
                _output.Add(e.Data);
        }

        private void OnErrorReceived(object sender, DataReceivedEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Data))
                _errors.Add(e.Data);
        }
    }
}
