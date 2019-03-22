using System;
using QuickGraph;

namespace DataStructures_Algorithms
{
	public class Edge<T> : QuickGraph.IUndirectedEdge<T>
	{
		public T Source { get; set; }
		public T Target { get; set; }
		public string Description { get; set; }
		public double Distance { get; set; }

		public Edge()
		{
		}
	}
}
