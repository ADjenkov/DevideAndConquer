using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFSShortestPath
{
	class BFSShortestPath
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
							edge.To.distance = currentNode.distance + 1;
							queue.Enqueue(edge.To);
						}
					}
					else if (edge.To == currentNode)
					{
						if (!edge.From.visited)
						{
							edge.From.visited = true;
							edge.From.distance = currentNode.distance + 1;
							queue.Enqueue(edge.From);
						}
					}
				}
			}
		}
	}
}
