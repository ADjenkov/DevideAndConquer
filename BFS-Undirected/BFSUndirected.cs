using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_Undirected
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
		public List<Edge<T>> EdgesIn;
		public List<Edge<T>> EdgesOut;

		public Node(T value)
		{
			this.value = value;
			this.EdgesIn = new List<Edge<T>>();
			this.EdgesOut = new List<Edge<T>>();
			this.visited = false;
		}
	}

	public class Graph<T>
	{
		private Dictionary<T, Node<T>> nodes;
		private List<Edge<T>> edges;

		private bool isDirected;

		public Dictionary<T, Node<T>> Nodes
		{
			get { return this.nodes; }
			set { this.nodes = value; }
		}

		public List<Edge<T>> Edges
		{
			get { return this.edges; }
			set { this.edges = value; }
		}

		public Graph(bool isDirected)
		{
			this.isDirected = isDirected;
			this.nodes = new Dictionary<T, Node<T>>();
			this.edges = new List<Edge<T>>();
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
			bool isDuplicateEdge = this.edges.Where(edge => (edge.From.value.Equals(from.value) && edge.To.value.Equals(to.value)) ||
				(edge.From.value.Equals(to.value) && edge.To.value.Equals(from.value))).Any();

			if (allowParallel || !isDuplicateEdge)
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

				this.edges.Add(newEdge);
			}
		}

		public int Count
		{
			get { return nodes.Count; }
		}
	}

	class BFSUndirected
	{
		static void Main(string[] args)
		{
			Graph<char> graph = new Graph<char>(false);

			graph.AddNode('s');
			graph.AddNode('a');
			graph.AddNode('b');
			graph.AddNode('c');
			graph.AddNode('d');
			graph.AddNode('e');

			graph.AddEdge(graph.Nodes['s'], graph.Nodes['a']);
			graph.AddEdge(graph.Nodes['s'], graph.Nodes['b']);
			graph.AddEdge(graph.Nodes['a'], graph.Nodes['c']);
			graph.AddEdge(graph.Nodes['b'], graph.Nodes['c']);
			graph.AddEdge(graph.Nodes['b'], graph.Nodes['d']);
			graph.AddEdge(graph.Nodes['c'], graph.Nodes['d']);
			graph.AddEdge(graph.Nodes['c'], graph.Nodes['e']);
			graph.AddEdge(graph.Nodes['d'], graph.Nodes['e']);

			BFS(graph, graph.Nodes['s']);
		}

		static void BFS(Graph<char> graph, Node<char> startNode)
		{
			graph.Nodes[startNode.value].visited = true;

			Queue<Node<char>> queue = new Queue<Node<char>>();
			queue.Enqueue(startNode);

			while (queue.Any())
			{
				Node<char> currentNode = queue.Dequeue();

				//FOR UNDIRECTED GRAPH EDGESIN AND EDGESOUT ARE THE SAME
				foreach (Edge<char> edge in currentNode.EdgesOut)
				{
					if (edge.From == currentNode)
					{
						if (!edge.To.visited)
						{
							edge.To.visited = true;
							queue.Enqueue(edge.To);
						}
					}
					else if (edge.To == currentNode)
					{
						if (!edge.From.visited)
						{
							edge.From.visited = true;
							queue.Enqueue(edge.From);
						}
					}
				}
			}
		}
	}
}
