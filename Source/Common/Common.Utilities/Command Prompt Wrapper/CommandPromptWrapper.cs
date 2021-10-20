using Auxilia.Extensions;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auxilia.Utilities
{
	public class CommandPromptWrapper : IDisposable
    {
        private const string CmdName = "cmd.exe";

        private static readonly string[] CommandFileExtensions = {".bat", ".cmd", ".btm"};
        
        private readonly string _commandFileName;
        private readonly Process _process;

        private readonly EventHandler _exitedEventHandler;

        private readonly DataReceivedEventHandler _outputReceivedEventHandler;
        private readonly ConcurrentQueue<DataReceivedEventArgs> _outputQueue = new ConcurrentQueue<DataReceivedEventArgs>();
        private readonly AutoResetEvent _outputReceivedEvent = new AutoResetEvent(false);

        private readonly DataReceivedEventHandler _errorReceivedEventHandler;
        private readonly ConcurrentQueue<DataReceivedEventArgs> _errorQueue = new ConcurrentQueue<DataReceivedEventArgs>();
        private readonly AutoResetEvent _errorReceivedEvent = new AutoResetEvent(false);

        private readonly ConcurrentQueue<string> _commandQueue = new ConcurrentQueue<string>();
        private readonly AutoResetEvent _commandReceivedEvent = new AutoResetEvent(false);

        private bool _isDisposed;

        public CommandPromptWrapper(CommandPromptSettings commandPromptSettings)
        {
	        commandPromptSettings = commandPromptSettings.ThrowIfNull(nameof(commandPromptSettings));

	        ProcessStartInfo processStartInfo = new ProcessStartInfo
	        {
		        RedirectStandardInput = true,
		        RedirectStandardError = true,
		        RedirectStandardOutput = true,
		        UseShellExecute = false,
		        CreateNoWindow = true,
		        FileName = CmdName
	        };

	        commandPromptSettings.EnvironmentVariables?.Keys
		        .Execute(k => processStartInfo.EnvironmentVariables[k] = commandPromptSettings.EnvironmentVariables[k]);

	        if (commandPromptSettings.CommandFilePath != null)
	        {
		        PathInfo pathInfo = new PathInfo(commandPromptSettings.CommandFilePath);

		        if (!CommandFileExtensions.Contains(pathInfo.Extension, StringComparer.OrdinalIgnoreCase))
			        throw new ArgumentException("Command file is not a batch/command file.", nameof(commandPromptSettings));

		        _commandFileName = pathInfo.Name;
		        processStartInfo.WorkingDirectory = pathInfo.Directory;
	        }
	        else
	        {
		        processStartInfo.WorkingDirectory = commandPromptSettings.WorkingDirectory;
	        }

	        _process = new Process { StartInfo = processStartInfo };

	        _outputReceivedEventHandler = OnOutputReceived;
	        _errorReceivedEventHandler = OnErrorReceived;
	        _exitedEventHandler = OnExited;

	        _process.OutputDataReceived += _outputReceivedEventHandler;
	        _process.ErrorDataReceived += _errorReceivedEventHandler;
	        _process.Exited += _exitedEventHandler;
        }
        ~CommandPromptWrapper()
        {
            Dispose(false);
        }

		public event EventHandler Exited;

        public event EventHandler<DataReceivedEventArgs> OutputReceived;
        public event EventHandler<DataReceivedEventArgs> ErrorReceived;

		public event EventHandler<ExecutingCommandEventArgs> ExecutingCommand;

        public bool IsRunning { get; private set; }

        public void Start()
        {
            if(_isDisposed)
                throw new ObjectDisposedException(nameof(CommandPromptWrapper));

			try
			{
	            IsRunning = true;

	            Task.Run(CommandThread);
	            Task.Run(OutputThread);
	            Task.Run(ErrorThread);

	            _process.Start();
				_process.BeginOutputReadLine();
				_process.BeginErrorReadLine();

                if(!string.IsNullOrEmpty(_commandFileName))
					_process.StandardInput.WriteLine(_commandFileName);
			}
			catch
			{
                IsRunning = false;
				Dispose();
				throw;
			}
        }
        public void Stop(int timeout = int.MaxValue)
        {
            ExecuteCommand("exit");
            _process.WaitForExit(timeout);
            Dispose();
        }
        public void Abort()
        {
	        Dispose();
        }

        public void ExecuteCommand(string command)
        {
            if(!IsRunning)
                throw new InvalidOperationException("Cannot execute command before starting the process.");
            
            _commandQueue.Enqueue(command);
            _commandReceivedEvent.Set();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool isDisposing)
        {
            if(_isDisposed)
                return;

            if(_process != null)
            {
	            if (IsRunning)
	            {
	                _process.CancelOutputRead();
	                _process.CancelErrorRead();
                    _process.Kill();
	            }

	            _process.OutputDataReceived -= _outputReceivedEventHandler;
                _process.ErrorDataReceived -= _errorReceivedEventHandler;
				_process.Exited -= _exitedEventHandler;
			}

            IsRunning = false;
            _isDisposed = true;
        }
        
        private void OnOutputReceived(object sender, DataReceivedEventArgs e)
        {
            _outputQueue.Enqueue(e);
			_outputReceivedEvent.Set();
        }

        private void OnErrorReceived(object sender, DataReceivedEventArgs e)
        {
            _errorQueue.Enqueue(e);
			_errorReceivedEvent.Set();
        }

        private void OnExited(object sender, EventArgs e)
		{
			Exited?.Invoke(this, e);
            IsRunning = false;
        }

        private void CommandThread()
        {
            while (IsRunning)
            {
                _commandReceivedEvent.WaitOne();

                while (IsRunning && _commandQueue.TryDequeue(out string command))
				{
					ExecutingCommand?.Invoke(this, new ExecutingCommandEventArgs(command));
                    _process.StandardInput.WriteLine(command);
				}

                _commandReceivedEvent.Reset();
            }
        }

        private void OutputThread()
		{
			while (IsRunning)
			{
				_outputReceivedEvent.WaitOne();

				while (IsRunning && _outputQueue.TryDequeue(out DataReceivedEventArgs data))
					OutputReceived?.Invoke(this, data);

				_outputReceivedEvent.Reset();
			}
		}

		private void ErrorThread()
		{
			while (IsRunning)
			{
				_errorReceivedEvent.WaitOne();

				while (IsRunning && _errorQueue.TryDequeue(out DataReceivedEventArgs data))
					ErrorReceived?.Invoke(this, data);

				_errorReceivedEvent.Reset();
            }
		}
    }
}
