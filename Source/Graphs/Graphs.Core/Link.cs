using Auxilia.Extensions;

namespace Auxilia.Graphs
{
    public class Link
    {
        public Link(Node source, Node target)
        {
            Source = source.ThrowIfNull(nameof(source));
            Target = target.ThrowIfNull(nameof(source));
        }

        public Node Source { get; }
        public Node Target { get; }
    }
}
