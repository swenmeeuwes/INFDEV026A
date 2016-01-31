using Microsoft.Xna.Framework;
using System;
using System.Configuration;
using System.IO;

namespace EntryPoint
{
    class Assignment3FloydWarshall
    {
        private bool[,] roadAdjacencyMatrix;
        public int[,] distanceMatrix { get; private set; }
        public Tuple<Vector2, Vector2>[,] predecessorMatrix { get; private set; }

        // O(n^3)
        public Assignment3FloydWarshall(Tuple<Vector2, Vector2>[] roads)
        {
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

            roadAdjacencyMatrix = new bool[roads.Length, roads.Length];
            distanceMatrix = new int[roads.Length, roads.Length];
            predecessorMatrix = new Tuple<Vector2, Vector2>[roads.Length, roads.Length];

            for (int i = 0; i < roads.Length; i++)
            {
                for (int j = 0; j < roads.Length; j++)
                {
                    roadAdjacencyMatrix[i, j] = (roads[i].Item1.Equals(roads[j].Item2) || roads[i].Item2.Equals(roads[j].Item1));
                }
            }

            // Distance - & Predecessor matrix
            for (int i = 0; i < roads.Length; i++)
            {
                for (int j = 0; j < roads.Length; j++)
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
                        predecessorMatrix[i, j] = roads[i];
                        distanceMatrix[i, j] = 1;//Vector2.Distance(edges[i].Item1, edges[i].Item2);
                    }
                    else
                        distanceMatrix[i, j] = Int16.MaxValue;//float.PositiveInfinity;
                }
            }

            for (int k = 0; k < roads.Length; k++)
            {
                Console.WriteLine("Iteration k={0}", k);
                for (int i = 0; i < roads.Length; i++)
                {
                    for (int j = 0; j < roads.Length; j++)
                    {
                        if (distanceMatrix[i, j] > distanceMatrix[i, k] + distanceMatrix[k, j])
                        {
                            distanceMatrix[i, j] = distanceMatrix[i, k] + distanceMatrix[k, j];
                            predecessorMatrix[i, j] = roads[k];
                        }
                    }
                }
            }
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
