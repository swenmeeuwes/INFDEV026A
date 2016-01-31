using EntryPoint.Assignment2;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EntryPoint
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var fullscreen = false;
            read_input:
            switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
            {
                case "1":
                    using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
                        game.Run();
                    break;
                case "2":
                    using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
                        game.Run();
                    break;
                case "3":
                    using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
                        game.Run();
                    break;
                case "4":
                    using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
                        game.Run();
                    break;
                case "q":
                    return;
            }
            goto read_input;
        }

        private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
        {
            Assignment1 specialBuildingsList = new Assignment1(specialBuildings.ToArray());
            specialBuildingsList.MergeSort(house);
            return specialBuildingsList.listContent;

            //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
          IEnumerable<Vector2> specialBuildings,
          IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            KdTree tree = new KdTree();
            foreach (var building in specialBuildings)
            {
                tree.Insert(new Tuple<float, float>(building.X, building.Y));
            }

            List<List<Vector2>> specialBuildingWithinDistanceFromHouses = new List<List<Vector2>>();
            foreach (var house in housesAndDistances)
            {
                specialBuildingWithinDistanceFromHouses.Add(tree.GetAllNodesWithinDistance(house.Item1, house.Item2).ToList());
            }

            return specialBuildingWithinDistanceFromHouses;

            //return
            //    from h in housesAndDistances
            //    select
            //      from s in specialBuildings
            //      where Vector2.Distance(h.Item1, s) <= h.Item2
            //      select s;
        }

        private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding,
          Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
            List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
            var prevRoad = startingRoad;
            for (int i = 0; i < 30; i++)
            {
                prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
                fakeBestPath.Add(prevRoad);
            }
            return fakeBestPath;
        }

        private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding,
          IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            //Vector2[] specialBuildings = new Vector2[destinationBuildings.Count() + 1];
            //specialBuildings[0] = startingBuilding;
            //IEnumerator destinationBuildingsEnumerator = destinationBuildings.GetEnumerator();
            //int i = 1;
            //while (destinationBuildingsEnumerator.MoveNext())
            //{
            //    specialBuildings[i++] = (Vector2)destinationBuildingsEnumerator.Current;
            //}

            Assignment3FloydWarshall floydwarshall = new Assignment3FloydWarshall(roads.ToArray());
            floydwarshall.SaveAdjacencyMatrixToFile("Assignment 3 - Floyd Warshall adjacency matrix");
            floydwarshall.SaveDistanceMatrixToFile("Assignment 3 - Floyd Warshall distance matrix");
            floydwarshall.SavePredecessorMatrixToFile("Assignment 3 - Floyd Warshall predecessor matrix");

            List<List<Tuple<Vector2, Vector2>>> routes = new List<List<Tuple<Vector2, Vector2>>>();

            IEnumerator roadsEnumerator = roads.GetEnumerator();
            int i = 0;
            while (roadsEnumerator.MoveNext())
            {
                if (((Tuple<Vector2, Vector2>)roadsEnumerator.Current).Item1 == startingBuilding)
                    break;
                i++;
            }

            IEnumerator destinationBuildingsEnumerator = destinationBuildings.GetEnumerator();
            while (destinationBuildingsEnumerator.MoveNext())
            {
                for (int j = 0; j < floydwarshall.predecessorMatrix.GetLength(1); j++)
                {
                    if (floydwarshall.predecessorMatrix[i, j].Item2 == (Vector2)destinationBuildingsEnumerator.Current)
                    {
                        Console.WriteLine("Distance from {0} to {1} is {2}.", startingBuilding, destinationBuildingsEnumerator.Current, floydwarshall.distanceMatrix[i, j]);
                        List<Tuple<Vector2, Vector2>> temp = new List<Tuple<Vector2, Vector2>>();
                        temp.Add((Tuple<Vector2, Vector2>)destinationBuildingsEnumerator.Current);
                        routes.Add(temp);
                    }
                }
            }

            return new List<List<Tuple<Vector2, Vector2>>>();

            //List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
            //foreach (var d in destinationBuildings)
            //{
            //    var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
            //    List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
            //    var prevRoad = startingRoad;
            //    for (int i = 0; i < 30; i++)
            //    {
            //        prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
            //        fakeBestPath.Add(prevRoad);
            //    }
            //    result.Add(fakeBestPath);
            //}

            //return result;
        }
}
#endif
}
