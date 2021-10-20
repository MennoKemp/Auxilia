namespace Auxilia.Graphs.Graphviz
{
    public interface ILayoutAlgorithm
    {
        void GenerateLayout(Graph graph, LayoutSettings layoutSettings);
    }
}