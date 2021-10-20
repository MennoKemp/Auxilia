using System.Collections.Generic;

namespace Auxilia.Utilities
{
    public class CommandExecutionSettings
    {
        public Dictionary<string, string> EnvironmentVariables { get; set; }
        public string WorkingDirectory { get; set; }
        public bool StopAfterExecution { get; set; }

        public CommandPromptSettings CommandPromptSettings
        {
            get => new CommandPromptSettings
            {
                EnvironmentVariables = EnvironmentVariables,
                WorkingDirectory = WorkingDirectory
            };
        }
    }
}
