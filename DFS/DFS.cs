using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
	class DFS
	{
		static int currentLabel;
		static Dictionary<Node<char>, int> topologicalOrder;

		static void Main(string[] args)
		{
			TopologicalSortDFS();
			//DFSMain();
		}

		static void DFSMain()
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

			DFSRecurse(graph, graph.Nodes['s']);
		}

		static void TopologicalSortDFS()
		{
			Graph<char> graph = new Graph<char>(true);

			graph.AddNode('s');
			graph.AddNode('v');
			graph.AddNode('w');
			graph.AddNode('t');

			graph.AddEdge(graph.Nodes['s'], graph.Nodes['v']);
			graph.AddEdge(graph.Nodes['s'], graph.Nodes['w']);
			graph.AddEdge(graph.Nodes['v'], graph.Nodes['t']);
			graph.AddEdge(graph.Nodes['w'], graph.Nodes['t']);

			currentLabel = graph.Nodes.Count;
			topologicalOrder = new Dictionary<Node<char>, int>();

			foreach (var node in graph.Nodes)
			{
				if (!node.Value.visited)
				{
					DFSRecurse(graph, node.Value);
				}
			}

			string s = "asd";
		}

		static void DFSRecurse(Graph<char> graph, Node<char> startNode)
		{
			graph.Nodes[startNode.value].visited = true;

			foreach (Edge<char> edge in startNode.EdgesOut)
			{
				if (edge.From == startNode)
				{
					if (!edge.To.visited)
					{
						edge.To.distance = startNode.distance + 1;
						DFSRecurse(graph, edge.To);
					}
				}
				else if (edge.To == startNode)
				{
					if (!edge.From.visited)
					{
						edge.From.distance = startNode.distance + 1;
						DFSRecurse(graph, edge.From);
					}
				}
			}

			topologicalOrder.Add(startNode, currentLabel);
			currentLabel--;
		}

		static void DFSNonRecursive(Graph<char> graph, Node<char> startNode)
		{
			graph.Nodes[startNode.value].visited = true;

			Stack<Node<char>> stack = new Stack<Node<char>>();
			stack.Push(startNode);

			while (stack.Any())
			{
				Node<char> currentNode = stack.Pop();

				//FOR UNDIRECTED GRAPH EDGESIN AND EDGESOUT ARE THE SAME
				foreach (Edge<char> edge in currentNode.EdgesOut)
				{
					if (edge.From == currentNode)
					{
						if (!edge.To.visited)
						{
							edge.To.visited = true;
							edge.To.distance = currentNode.distance + 1;
							stack.Push(edge.To);
						}
					}
					else if (edge.To == currentNode)
					{
						if (!edge.From.visited)
						{
							edge.From.visited = true;
							edge.From.distance = currentNode.distance + 1;
							stack.Push(edge.From);
						}
					}
				}
			}
		}
	}
}
