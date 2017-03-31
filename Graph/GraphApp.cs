using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
	public class Edge<T>
	{
		public Node<T> From;
		public Node<T> To;

		public Edge(Node<T> from, Node<T> to)
		{
			this.From = from;
			this.To = to;
		}
	}

	public class Node<T>
	{
		public T value;
		public bool visited;
		public Node<T> leader;
		public HashSet<Edge<T>> EdgesIn;
		public HashSet<Edge<T>> EdgesOut;
		public int distance;
		public int label;

		public Node(T value)
		{
			this.value = value;
			this.EdgesIn = new HashSet<Edge<T>>();
			this.EdgesOut = new HashSet<Edge<T>>();
			this.visited = false;
			this.distance = 0;
		}

		public Node(T value, int label)
		{
			this.value = value;
			this.EdgesIn = new HashSet<Edge<T>>();
			this.EdgesOut = new HashSet<Edge<T>>();
			this.visited = false;
			this.distance = 0;
			this.label = label;
		}
	}

	public class Graph<T>
	{
		private Dictionary<T, Node<T>> nodes;
		private Dictionary<Tuple<T, T>, Edge<T>> edges;

		private bool isDirected;

		public Dictionary<T, Node<T>> Nodes
		{
			get { return this.nodes; }
			set { this.nodes = value; }
		}

		public Dictionary<Tuple<T, T>, Edge<T>> Edges
		{
			get { return this.edges; }
			set { this.edges = value; }
		}

		public Graph(bool isDirected)
		{
			this.isDirected = isDirected;
			this.nodes = new Dictionary<T, Node<T>>();
			this.edges = new Dictionary<Tuple<T, T>, Edge<T>>();
		}

		public void AddNode(T value, int label = 0)
		{
			if (!this.nodes.ContainsKey(value))
			{
				this.nodes.Add(value, new Node<T>(value, label));
			}
		}

		public void AddNode(T value)
		{
			if (!this.nodes.ContainsKey(value))
			{
				this.nodes.Add(value, new Node<T>(value));
			}
		}

		public void AddNode(Node<T> node)
		{
			if (!this.nodes.ContainsValue(node))
			{
				this.nodes.Add(node.value, node);
			}
		}

		public void AddEdge(Node<T> from, Node<T> to, bool allowParallel = false)
		{
			//bool isDuplicateEdge = this.edges.Where(edge => (edge.From.value.Equals(from.value) && edge.To.value.Equals(to.value)) ||
			//	(edge.From.value.Equals(to.value) && edge.To.value.Equals(from.value))).Any();

			bool hasKey = this.edges.ContainsKey(new Tuple<T, T>(from.value, to.value));
			bool hasReversedKey = this.edges.ContainsKey(new Tuple<T, T>(to.value, from.value));

			if (allowParallel || (!hasKey && !hasReversedKey))
			{
				Edge<T> newEdge = new Edge<T>(from, to);

				if (this.isDirected)
				{
					from.EdgesOut.Add(newEdge);
					to.EdgesIn.Add(newEdge);
				}
				else
				{
					from.EdgesOut.Add(newEdge);
					from.EdgesIn.Add(newEdge);

					to.EdgesOut.Add(newEdge);
					to.EdgesIn.Add(newEdge);
				}

				this.edges.Add(new Tuple<T, T>(from.value, to.value), newEdge);
			}
		}

		public void ResetVisited()
		{
			foreach (var node in this.nodes)
			{
				node.Value.visited = false;
			}
		}

		public int Count
		{
			get { return nodes.Count; }
		}
	}

	class GraphApp
	{
		static void Main(string[] args)
		{
		}
	}
}
