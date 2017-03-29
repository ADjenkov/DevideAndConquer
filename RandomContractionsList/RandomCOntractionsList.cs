using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RandomContraction
{
    class Edge
    {
        public Vertex node1;
        public Vertex node2;

        public Edge(Vertex node1, Vertex node2)
        {
            this.node1 = node1;
            this.node2 = node2;
        }
    }

    class Vertex
    {
        public int value;
        public List<Edge> edges;

        public Vertex(int value)
        {
            this.value = value;
            this.edges = new List<Edge>();
        }
    }

    class RandomContraction
    {
        static void Main(string[] args)
        {
            //List<List<int>> graph = new List<List<int>> { new List<int> { 2, 4 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4 }, new List<int> { 1, 2, 3 } };
            string line;

            Dictionary<int, Vertex> vertices = new Dictionary<int, Vertex>();
            List<Edge> edges = new List<Edge>();

            //vertices.Add(1, new Vertex(1));
            //vertices.Add(2, new Vertex(2));
            //vertices.Add(3, new Vertex(3));
            //vertices.Add(4, new Vertex(4));

            //edges.Add(new Edge(vertices[1], vertices[2])); //1,2
            //edges.Add(new Edge(vertices[1], vertices[4])); //1,4
            //edges.Add(new Edge(vertices[2], vertices[3]));//2,3
            //edges.Add(new Edge(vertices[2], vertices[4]));//2,4
            //edges.Add(new Edge(vertices[3], vertices[4]));//3,4

            //vertices[1].edges = new List<Edge>() { edges[0], edges[1] };
            //vertices[2].edges = new List<Edge>() { edges[0], edges[2], edges[3] };
            //vertices[3].edges = new List<Edge>() { edges[2], edges[4] };
            //vertices[4].edges = new List<Edge>() { edges[1], edges[4], edges[3] };

            //edges = mergeNodes(edges, vertices);

            //Console.WriteLine("{0}-{1} ", vertices.ElementAt(0).Key, vertices.ElementAt(1).Key);

  
                StreamReader file = new StreamReader(@"kargerMinCut.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] splits = line.Split('\t');

                    Vertex currentVertex = getVertex(vertices, Int32.Parse(splits[0]));

                    for (int i = 1; i < splits.Length - 1; i++)
                    {
                        int currentKey = Int32.Parse(splits[i]);

                        Vertex innerVertex = getVertex(vertices, currentKey);
                        Edge currentEdge = getEdge(edges, currentVertex, innerVertex);

                        currentVertex.edges.Add(currentEdge);
                    }
                }

                edges = mergeNodes(edges, vertices);

                Console.WriteLine("{0} - {1} ", vertices.ElementAt(0).Value.edges.Count, vertices.ElementAt(1).Value.edges.Count);
 
        }

        static Vertex getVertex(Dictionary<int, Vertex> vertices, int key)
        {

            if (vertices.ContainsKey(key))
            {
                return vertices[key];
            }
            else
            {
                Vertex currentVertex = new Vertex(key);
                vertices.Add(key, currentVertex);

                return currentVertex;
            }
        }
        
        //We allow paraller cretion of nodes (1-2 equal 2-1) only after we init the graph.WE do not want to 
        //init the graph with both sides (parallel) edges.
        static Edge getEdge(List<Edge> edges, Vertex node1, Vertex node2, bool allowParallael = false)
        {
            Edge edgeNormal = edges.Where(edge => edge.node1.value == node1.value && edge.node2.value == node2.value).FirstOrDefault();
            Edge edgeReversed = edges.Where(edge => edge.node1.value == node2.value && edge.node2.value == node1.value).FirstOrDefault();

            if (edgeNormal != null || edgeReversed != null) 
            {
                if (allowParallael)
                {
                    Edge currentEdge = new Edge(node2, node1);
                    edges.Add(currentEdge);

                    return currentEdge;
                }
                else
                {
                    return edgeNormal != null ? edgeNormal : edgeReversed;
                }
            }
            else
            {
                Edge currentEdge = new Edge(node2, node1);
                edges.Add(currentEdge);

                return currentEdge;
            }
        }

        static List<Edge> mergeNodes(List<Edge> edges, Dictionary<int, Vertex> vertices)
        {
            var randomEdge = edges.ElementAt(new Random().Next(0, edges.Count));

            randomEdge.node1.edges.Remove(randomEdge);

            foreach (Edge edge in randomEdge.node2.edges)
            {
                edges.Remove(edge);

                //Selftloop
                if ((edge.node1.value == randomEdge.node1.value && edge.node2.value == randomEdge.node2.value)
                    || (edge.node1.value == randomEdge.node2.value && edge.node2.value == randomEdge.node1.value))
                {
                    randomEdge.node1.edges.Remove(edge);
                    continue;
                }

                if (edge.node1 == randomEdge.node2)
                {
                    Edge newEdge = getEdge(edges, randomEdge.node1, edge.node2, true);

                    edge.node2.edges.Remove(edge);
                    edge.node2.edges.Add(newEdge);

                    randomEdge.node1.edges.Add(newEdge);
                    randomEdge.node1.edges.Remove(edge);
                }
                else if (edge.node2 == randomEdge.node2)
                {
                    Edge newEdge = getEdge(edges, randomEdge.node1, edge.node1, true);

                    edge.node1.edges.Remove(edge);
                    edge.node1.edges.Add(newEdge);

                    randomEdge.node1.edges.Add(newEdge);
                    randomEdge.node1.edges.Remove(edge);
                }
            }

            edges.Remove(randomEdge);
            vertices.Remove(randomEdge.node2.value);

            if (vertices.Count > 2)
            {
                return mergeNodes(edges, vertices);
            }

            return edges;
        }
    }
}
