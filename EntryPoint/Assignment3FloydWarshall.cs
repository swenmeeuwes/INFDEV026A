using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class Assignment3FloydWarshall
    {
        // Adjacency matrix
        private bool[,] adjacencyMatrix;
        private float[,] distanceMatrix;
        private Tuple<Vector2, Vector2>[,] predecessorMatrix;

        public Assignment3FloydWarshall(Tuple<Vector2, Vector2>[] edges) //IEnumerable<Vector2> vertices, 
        {
            adjacencyMatrix = new bool[edges.Length, edges.Length];

            for (int i = 0; i < edges.Length; i++)
            {
                for (int j = 0; j < edges.Length; j++)
                {
                    adjacencyMatrix[i, j] = (edges[i].Item1.Equals(edges[j].Item2) || edges[i].Item2.Equals(edges[j].Item1));
                }
            }
        }

        public void SaveAdjacencyMatrix()
        {
            Directory.CreateDirectory(ConfigurationManager.AppSettings["saveDirectory"]);
            using (StreamWriter file = new StreamWriter(ConfigurationManager.AppSettings["saveDirectory"] + @"\Assignment 3 - Floyd Warshall adjacency matrix.txt"))
            {
                for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                    {
                        if (adjacencyMatrix[i, j])
                            file.Write("1 ");
                        else
                            file.Write("0 ");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }
    }
}
