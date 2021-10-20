using System.IO;

namespace Auxilia.Graphs
{
    public class LayoutSettings
    {
        public double HorizontalSpacing { get; set; } = 1;
        public double VerticalSpacing { get; set; } = 1;
        public bool EqualSpacing { get; set; } = true;

        public string OutputImagePath
        {
            get => _outputImagePath;
            set => _outputImagePath = Path.ChangeExtension(value, ".png");
        }
        private string _outputImagePath;
    }
}
