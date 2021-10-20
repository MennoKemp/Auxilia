using Auxilia.Extensions;
using Auxilia.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Auxilia.Graphs.Graphviz
{
	public class LayoutAlgorithm : ILayoutAlgorithm
    {
        private const string CreateImageCommand = "dot -Tpng graph.dot -o output.png";
        private const string CreateLayoutCommand = "dot -Tplain graph.dot -o output.txt";

        private const string DotFileName = "graph.dot";
        private const string ImageOutputName = "output.png";
        private const string TextOutputName = "output.txt";

        private readonly string _graphvizDirectory;

        public LayoutAlgorithm(string graphvizDirectory)
        {
            if (!Directory.Exists(graphvizDirectory))
                throw new DirectoryNotFoundException($"Cannot find graphviz directory {graphvizDirectory}.");

            _graphvizDirectory = graphvizDirectory;
        }

        public string DotFile { get; private set; }

        public void GenerateLayout(Graph graph, LayoutSettings layoutSettings)
        {
            graph.ThrowIfNull(nameof(graph));
            layoutSettings.ThrowIfNull(nameof(layoutSettings));

            CreateDotFile(graph, layoutSettings);

            CommandExecutionSettings commandExecutionSettings = new CommandExecutionSettings
            {
                EnvironmentVariables = new Dictionary<string, string>
                {
                    { "Path", _graphvizDirectory }
                },
                WorkingDirectory = _graphvizDirectory,
                StopAfterExecution = false
            };

            using WindowsCommandExecutioner commandExecutioner = new WindowsCommandExecutioner(commandExecutionSettings);

            commandExecutioner.ExecuteCommand(CreateLayoutCommand);

            PathInfo outputImagePath = new PathInfo(layoutSettings.OutputImagePath);

            if (outputImagePath.IsValid())
            {
                commandExecutioner.ExecuteCommand(CreateImageCommand);
                commandExecutioner.Stop();
                outputImagePath.Parent.Create();
                File.Copy(GetFilePath(ImageOutputName).FullPath, outputImagePath.FullPath, true);
            }

            if (commandExecutioner.Errors.Any())
	            throw new GraphLayoutException(graph, commandExecutioner.Errors);
            
            if(commandExecutioner.IsRunning)
				commandExecutioner.Stop();

            CleanUp();
        }

        private void CreateDotFile(Graph graph, LayoutSettings layoutSettings)
        {
            StringBuilder dotFile = new StringBuilder();
            dotFile.AppendLine("digraph G {");

            dotFile.Append("graph[");
            dotFile.Append($"nodesep={layoutSettings.HorizontalSpacing.Clamp(0.02)},");
            dotFile.Append($"ranksep={layoutSettings.VerticalSpacing.Clamp(0.02)}{(layoutSettings.EqualSpacing ? "equally" : string.Empty)}");
            dotFile.AppendLine("]");

            graph.Nodes.Execute(n => dotFile.AppendLine($"{n.Id} [shape=rectangle, width=2, height=0.5]"));
            graph.Links.Execute(l => dotFile.AppendLine($"{l.Source.Id}->{l.Target.Id}"));

            dotFile.AppendLine("}");

            DotFile = dotFile.ToString();
            File.WriteAllText(GetFilePath(DotFileName).FullPath, DotFile);
        }

        private void CleanUp()
        {
            GetFilePath(DotFileName).Delete();
            GetFilePath(ImageOutputName).Delete();
            GetFilePath(TextOutputName).Delete();
        }

        private PathInfo GetFilePath(string fileName)
        {
            return new PathInfo(_graphvizDirectory, fileName);
        }
    }
}
