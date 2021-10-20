using System.Collections.Generic;

namespace Auxilia.Utilities
{
    public class CommandPromptSettings
    {
        public string CommandFilePath { get; set; }
        public Dictionary<string, string> EnvironmentVariables { get; set; }
        public string WorkingDirectory { get; internal set; }
    }
}
