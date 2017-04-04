
using Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace DijkstraShortestPath
{
	//INFO: Using Priority Queue https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Using-the-SimplePriorityQueue
	class DijkstraShortestPath
	{
		static Dictionary<int, int> A;
		static HashSet<Node<int>> X;
		static Node<int> source;

		static void Main(string[] args)
		{
			A = new Dictionary<int, int>();
			X = new HashSet<Node<int>>();

			Graph<int> graph = new Graph<int>(true);

			string line;
			for (int i = 1; i < 201; i++)
			{
				graph.AddNode(i);
			}

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

			source = graph.Nodes[1];

			DijkstraHeap(graph, source);
			//Dijkstra(graph, source);

			//CORRECT ANSWER 2599,2610,2947,2052,2367,2399,2029,2442,2505,3068
			Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", A[7], A[37], A[59], A[82], A[99], A[115], A[133], A[165], A[188], A[197]);
		}

		static void DijkstraHeap(Graph<int> graph, Node<int> sourceNode)
		{
			SimplePriorityQueue<Node<int>> heap = new SimplePriorityQueue<Node<int>>();

			foreach (var item in graph.Nodes)
			{
				if (item.Value != source)
				{
					heap.Enqueue(item.Value, Int32.MaxValue);
					setShortestPath(item.Value.value, Int32.MaxValue);

				}
				else
				{
					heap.Enqueue(item.Value, 0);
				}
			}

			setShortestPath(source.value, 0);

			while (heap.Any())
			{
				var minItem = heap.Dequeue();

				foreach (var edgeOut in minItem.EdgesOut)
				{
					if (heap.Contains(edgeOut.To))
					{
						int alt = A[minItem.value] + edgeOut.length;

						if (alt < A[edgeOut.To.value])
						{
							setShortestPath(edgeOut.To.value, alt);
							heap.UpdatePriority(edgeOut.To, alt);
						}
					}
				}
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
	}
}
