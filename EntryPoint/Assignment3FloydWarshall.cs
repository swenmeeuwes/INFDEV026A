using Microsoft.Xna.Framework;
using System;
using System.Configuration;
using System.IO;

namespace EntryPoint
{
    class Assignment3FloydWarshall
    {
        //private Tuple<Vector2, Vector2>[] edges;
        private bool[,] roadAdjacencyMatrix;
        private float[,] distanceMatrix;
        private Tuple<Vector2, Vector2>[,] predecessorMatrix;

        //private bool[,] adjacencyMatrix;
        //private float[,] distanceMatrix;
        //private Tuple<Vector2, Vector2>[,] predecessorMatrix;

        // O(n^3)
        public Assignment3FloydWarshall(Tuple<Vector2, Vector2>[] edges) //Vector2[] vertices, 
        {
            //Test data
            //edges = new Tuple<Vector2, Vector2>[] { new Tuple<Vector2, Vector2>(new Vector2(0,0), new Vector2(0,1)), new Tuple<Vector2, Vector2>(new Vector2(0, 1), new Vector2(1, 1)), new Tuple<Vector2, Vector2>(new Vector2(0, 1), new Vector2(0, 2))};

            //float farestX = 0;
            //float farestY = 0;
            //foreach (var item in edges)
            //{
            //    if (item.Item1.X > farestX)
            //        farestX = item.Item1.X;
            //    if (item.Item2.X > farestX)
            //        farestX = item.Item2.X;
            //    if (item.Item1.Y > farestY)
            //        farestY = item.Item1.Y;
            //    if (item.Item2.Y > farestY)
            //        farestY = item.Item2.Y;
            //}

            //this.edges = edges;
            roadAdjacencyMatrix = new bool[edges.Length, edges.Length];
            distanceMatrix = new float[edges.Length, edges.Length];
            predecessorMatrix = new Tuple<Vector2, Vector2>[edges.Length, edges.Length];

            for (int i = 0; i < edges.Length; i++)
            {
                for (int j = 0; j < edges.Length; j++)
                {
                    roadAdjacencyMatrix[i, j] = (edges[i].Item1.Equals(edges[j].Item2) || edges[i].Item2.Equals(edges[j].Item1));
                }
            }

            // Distance - & Predecessor matrix
            for (int i = 0; i < edges.Length; i++)
            {
                for (int j = 0; j < edges.Length; j++)
                {
                    // 0 is the distance between a vertex and itself
                    if (i == j)
                    {
                        distanceMatrix[i, j] = 0;
                        continue;
                    }

                    // w(i,j) is the distance between adjacent vertices, ∞ is the distance between non-adjacent vertices
                    if (roadAdjacencyMatrix[i, j])
                    {
                        predecessorMatrix[i, j] = edges[i];
                        distanceMatrix[i, j] = Vector2.Distance(edges[i].Item1, edges[i].Item2);
                    }
                    else
                        distanceMatrix[i, j] = float.PositiveInfinity;
                }
            }

            //fork from1 to | V |
            //fori from1 to| V |
            //forj from1 to| V |
            //ifdist[i][j] > dist[i][k] + dist[k][j]
            //dist[i][j] ← dist[i][k] + dist[k][j]
            //end if

            //for (int k = 0; k < edges.Length; k++)
            //{
            //    Console.WriteLine("Iteration k={0}", k);
            //    for (int i = 0; i < edges.Length; i++)
            //    {
            //        for (int j = 0; j < edges.Length; j++)
            //        {
            //            if (roadDistanceMatrix[i, j] > roadDistanceMatrix[i, k] + roadDistanceMatrix[k, j])
            //            {
            //                roadDistanceMatrix[i, j] = roadDistanceMatrix[i, k] + roadDistanceMatrix[k, j];
            //                roadPredecessorMatrix[i, j] = edges[k];
            //            }
            //        }
            //    }
            //}
        }

        public void SaveAdjacencyMatrixToFile(string fileName)
        {
            Directory.CreateDirectory(ConfigurationManager.AppSettings["saveDirectory"]);
            using (StreamWriter file = new StreamWriter(ConfigurationManager.AppSettings["saveDirectory"] + @"\" + fileName + ".txt"))
            {
                for (int i = 0; i < roadAdjacencyMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < roadAdjacencyMatrix.GetLength(1); j++)
                    {
                        if (roadAdjacencyMatrix[i, j])
                            file.Write("1 ");
                        else
                            file.Write("0 ");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }

        public void SaveDistanceMatrixToFile(string fileName)
        {
            Directory.CreateDirectory(ConfigurationManager.AppSettings["saveDirectory"]);
            using (StreamWriter file = new StreamWriter(ConfigurationManager.AppSettings["saveDirectory"] + @"\" + fileName + ".txt"))
            {
                for (int i = 0; i < distanceMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < distanceMatrix.GetLength(1); j++)
                    {
                        file.Write(distanceMatrix[i, j] + " ");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }

        public void SavePredecessorMatrixToFile(string fileName)
        {
            Directory.CreateDirectory(ConfigurationManager.AppSettings["saveDirectory"]);
            using (StreamWriter file = new StreamWriter(ConfigurationManager.AppSettings["saveDirectory"] + @"\" + fileName + ".txt"))
            {
                for (int i = 0; i < predecessorMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < predecessorMatrix.GetLength(1); j++)
                    {
                        if (predecessorMatrix[i, j] != null)
                            file.Write(predecessorMatrix[i, j] + " ");
                        else
                            file.Write(" -  ");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }
    }
}
