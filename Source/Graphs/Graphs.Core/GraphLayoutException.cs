using Auxilia.Extensions;
using System;
using System.Collections.Generic;

namespace Auxilia.Graphs
{
	public class GraphLayoutException : Exception
	{
		public GraphLayoutException(Graph graph, IEnumerable<string> errors)
		{
			Graph = graph.ThrowIfNull(nameof(graph));
			Errors = errors.ThrowIfNullEmptyOrContainsNull(nameof(errors));
		}

		public Graph Graph { get; }
		public IEnumerable<string> Errors { get; }
	}
}
