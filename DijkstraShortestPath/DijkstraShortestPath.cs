
using Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraShortestPath
{
	class DijkstraShortestPath
	{
		static Dictionary<int, int> A;
		static Dictionary<int, List<Node<int>>> B;
		static HashSet<Node<int>> X;
		static Node<int> source;

		static void Main(string[] args)
		{
			A = new Dictionary<int, int>();
			B = new Dictionary<int, List<Node<int>>>();
			X = new HashSet<Node<int>>();

			//Graph<int> graph = new Graph<int>(true); for the commented sample 
			Graph<int> graph = new Graph<int>(true);

			string line;

			for (int i = 1; i < 201; i++) //875715
			{
				graph.AddNode(i);
			}

			//The graph should have nodes 875714 and 5105043 edges
			StreamReader file = new StreamReader(@"dijkstraData.txt");
			while ((line = file.ReadLine()) != null)
			{
				string[] splits = line.Split('\t');
				int mainNode = Int32.Parse(splits[0]);

				for (int i = 1; i < splits.Length - 1; i++) // -1 is for the empty space in the end of every row
				{
					string[] nodeAndLength = splits[i].Split(',');
					int toNode = Int32.Parse(nodeAndLength[0]);
					int length = Int32.Parse(nodeAndLength[1]);

					graph.AddEdge(graph.Nodes[mainNode], graph.Nodes[toNode], true, length);
				}
			}

			//graph.AddNode('s');
			//graph.AddNode('v');
			//graph.AddNode('w');
			//graph.AddNode('t');

			//graph.AddEdge(graph.Nodes['s'], graph.Nodes['v'], true, 1);
			//graph.AddEdge(graph.Nodes['s'], graph.Nodes['w'], true, 4);
			//graph.AddEdge(graph.Nodes['v'], graph.Nodes['w'], true, 2);
			//graph.AddEdge(graph.Nodes['v'], graph.Nodes['t'], true, 6);
			//graph.AddEdge(graph.Nodes['w'], graph.Nodes['t'], true, 3);

			source = graph.Nodes[1];

			DijkstraHeap(graph, source);
			//Dijkstra(graph, source);

			//CORRECT ANSWER 2599,2610,2947,2052,2367,2399,2029,2442,2505,3068
			Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", A[7], A[37], A[59], A[82], A[99], A[115], A[133], A[165], A[188], A[197]);
		}

		static void DijkstraHeap(Graph<int> graph, Node<int> sourceNode)
		{
			A.Add(source.value, 0);
			X.Add(sourceNode);

			var heap = new C5.IntervalHeap<Node<int>>();

			foreach (var item in graph.Nodes)
			{
				if (item.Value != source)
				{
					heap.Add(item.Value);
				}
			}

			for (int i = 1; i < graph.Count; i++)
			{
				foreach (var unexploredNode in heap)
				{
					int minLength = 1000000;

					foreach (var edgeIn in unexploredNode.EdgesIn)
					{
						if (X.Contains(edgeIn.From))
						{
							if (minLength > edgeIn.From.distance + edgeIn.length)
							{
								minLength = edgeIn.From.distance + edgeIn.length;
							}
						}
					}

					unexploredNode.distance = minLength;
					setShortestPath(unexploredNode.value, minLength);
				}

				X.Add(heap.Min());

				foreach (var edgeOut in heap.Min().EdgesOut)
				{
					if (heap.Contains(edgeOut.To))
					{
						edgeOut.To.distance = Math.Min(edgeOut.To.distance, A[edgeOut.From.value] + edgeOut.length);
						setShortestPath(edgeOut.To.value, Math.Min(A[edgeOut.To.value], A[edgeOut.From.value] + edgeOut.length));
					}
				}

				heap.DeleteMin();
			}
		}

		static void setShortestPath(int s, int value)
		{
			if (A.ContainsKey(s))
			{
				A[s] = value;
			}
			else
			{
				A.Add(s, value);
			}
		}

		static void Dijkstra(Graph<int> graph, Node<int> sourceNode)
		{

			A.Add(source.value, 0);
			X.Add(sourceNode);

			for (int i = 1; i < graph.Count; i++)
			{
				int minLength = Int32.MaxValue;
				Edge<int> minEdge = null;

				foreach (var exploredNode in X)
				{

					foreach (var edgeOut in exploredNode.EdgesOut)
					{
						if (!X.Contains(edgeOut.To))
						{
							if (minLength > A[exploredNode.value] + edgeOut.length)
							{
								minLength = A[exploredNode.value] + edgeOut.length;
								minEdge = edgeOut;
							}
						}
					}
				}

				X.Add(minEdge.To);
				A.Add(minEdge.To.value, minLength);
			}
		}
	}
}
