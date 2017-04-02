using Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KosarajuDFS
{
    class KosarajuDFS
    {
        static int recursions;

        static int exploredNodes;
        static Node<int> latestDFSNode;
        static Dictionary<int, Node<int>> finishingTimes;
        static Dictionary<int, int> strongComponents;

        static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>(true);

            string line;

            for (int i = 1; i < 875715; i++) //875715
            {
                graph.AddNode(i);
            }

            //The graph should have nodes 875714 and 5105043 edges
            StreamReader file = new StreamReader(@"SCC.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] splits = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                int from = Int32.Parse(splits[0]);
                int to = Int32.Parse(splits[1]);

                graph.AddEdge(graph.Nodes[from], graph.Nodes[to], true);

            }


            //graph.AddNode(1);
            //graph.AddNode(2);
            //graph.AddNode(3);
            //graph.AddNode(4);
            //graph.AddNode(5);
            //graph.AddNode(6);
            //graph.AddNode(7);
            //graph.AddNode(8);
            //graph.AddNode(9);

            //graph.AddEdge(graph.Nodes[1], graph.Nodes[4]);
            //graph.AddEdge(graph.Nodes[4], graph.Nodes[7]);
            //graph.AddEdge(graph.Nodes[7], graph.Nodes[1]);
            //graph.AddEdge(graph.Nodes[9], graph.Nodes[7]);
            //graph.AddEdge(graph.Nodes[6], graph.Nodes[9]);
            //graph.AddEdge(graph.Nodes[9], graph.Nodes[3]);
            //graph.AddEdge(graph.Nodes[3], graph.Nodes[6]);
            //graph.AddEdge(graph.Nodes[8], graph.Nodes[6]);
            //graph.AddEdge(graph.Nodes[2], graph.Nodes[8]);
            //graph.AddEdge(graph.Nodes[5], graph.Nodes[2]);
            //graph.AddEdge(graph.Nodes[8], graph.Nodes[5]);

            exploredNodes = 0;
            finishingTimes = new Dictionary<int, Node<int>>();
            strongComponents = new Dictionary<int, int>();


            Thread t = new Thread(() =>
            {
                for (int i = graph.Count; i > 0; i--)
                {
                    var node = graph.Nodes[i];

                    if (!node.visited)
                    {
                        DFSRecurseReverse(graph, node);
                    }
                }

                graph.ResetVisited();

                for (int i = graph.Count; i > 0; i--)
                {

                    var node = finishingTimes[i];

                    if (!node.visited)
                    {
                        latestDFSNode = node;
                        DFSRecurse(graph, node);
                    }
                }
            }, 10000000);
            t.Start();
            t.Join();

            //GET THE BIGGEST 5 
            var strongComponentsList = strongComponents.ToList();
            strongComponentsList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }

        static void DFSRecurse(Graph<int> graph, Node<int> startNode, bool inReverse = false)
        {

            startNode.visited = true;

            if (strongComponents.ContainsKey(latestDFSNode.value))
            {
                strongComponents[latestDFSNode.value] += 1;
            }
            else
            {
                strongComponents.Add(latestDFSNode.value, 1);
            }

            foreach (Edge<int> edge in startNode.EdgesOut)
            {
                if (edge.From == startNode)
                {
                    if (!edge.To.visited)
                    {
                        DFSRecurse(graph, edge.To);
                    }
                }
                else if (edge.To == startNode)
                {
                    if (!edge.From.visited)
                    {
                        DFSRecurse(graph, edge.From);
                    }
                }
            }
        }

        static void DFSRecurseReverse(Graph<int> graph, Node<int> startNode)
        {
            recursions++;

            startNode.visited = true;

            foreach (Edge<int> edge in startNode.EdgesIn)
            {
                if (edge.From == startNode)
                {
                    if (!edge.To.visited)
                    {
                        //edge.To.distance = startNode.distance + 1;
                        DFSRecurseReverse(graph, edge.To);
                    }
                }
                else if (edge.To == startNode)
                {
                    if (!edge.From.visited)
                    {
                        //edge.From.distance = startNode.distance + 1;
                        DFSRecurseReverse(graph, edge.From);
                    }
                }
            }

            exploredNodes++;
            finishingTimes.Add(exploredNodes, startNode);
            startNode.label = exploredNodes;
        }
    }
}
