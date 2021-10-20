using Auxilia.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auxilia.Graphs
{
	public class Graph
    {
        private readonly SortedDictionary<int, Node> _nodes = new SortedDictionary<int, Node>();
        private readonly List<Link> _links = new List<Link>();

        private readonly SortedDictionary<int, IList<Node>> _sources = new SortedDictionary<int, IList<Node>>();
        private readonly SortedDictionary<int, IList<Node>> _targets = new SortedDictionary<int, IList<Node>>();

        public IReadOnlyList<Node> Nodes
        {
            get => _nodes.Values.ToList().AsReadOnly();
        }
        public IReadOnlyList<Link> Links
        {
            get => _links.AsReadOnly();
        }

        public Node this[int id]
        {
            get => _nodes.TryGetValue(id, out Node node)
                ? node
                : null;
        }

        public void AddNode(Node node)
        {
            node.ThrowIfNull(nameof(node));
            if (_nodes.ContainsKey(node.Id))
                throw new ArgumentException($"Already contains a node with id '{node.Id}'.", nameof(node));

            _nodes.Add(node.Id, node);
            _sources.Add(node.Id, new List<Node>());
            _targets.Add(node.Id, new List<Node>());
        }

        public void AddLink(Link link)
        {
            link.ThrowIfNull(nameof(link));

            if (!_nodes.ContainsKey(link.Source.Id))
                throw new ArgumentException($"Cannot find source node with id '{link.Source.Id}'.", nameof(link));

            if (!_nodes.ContainsKey(link.Target.Id))
                throw new ArgumentException($"Cannot find target node with id '{link.Target.Id}'.", nameof(link));

            _links.Add(link);
            _sources[link.Target.Id].Add(link.Source);
            _targets[link.Source.Id].Add(link.Target);
        }

        public IEnumerable<Node> GetSources(Node node)
        {
            return _sources[node.Id];
        }

        public IEnumerable<Node> GetTargets(Node node)
        {
            return _targets[node.Id];
        }
    }
}
