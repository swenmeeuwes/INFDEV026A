using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Assignment2
{
    class KdTree
    {
        private IKdNode<float> root;
        public KdTree()
        {
            root = new EmptyKdNode<float>() as IKdNode<float>;
        }
        public void Insert(Tuple<float,float> key)
        {
            //root = InsertRec(root, key, -1);
            root = InsertXRec(root, key);
        }
        public IEnumerable<Vector2> GetAllNodesWithinDistance(Vector2 startPosition, float distance)
        {
            return GetAllNodesWithinDistanceRec(new List<Vector2>(), root, startPosition, distance);
        }
        public void PrintPreOrder()
        {
            PrintPreOrderRec(root);
        }
        public void PrintInOrder()
        {
            PrintInOrderRec(root);
        }
        public void PrintPostOrder()
        {
            PrintPostOrderRec(root);
        }
        // Option 1
        private IKdNode<float> InsertRec(IKdNode<float> root, Tuple<float, float> key, int dimension)
        {
            dimension++;
            if (root.isEmpty)
                return new KdNode<float>(key, new EmptyKdNode<float>(), new EmptyKdNode<float>());

            if (root.key == key)
                return root;

            if (dimension % 2 == 0) {
                if (root.key.Item1 > key.Item1)
                    return new KdNode<float>(root.key, InsertRec(root.left, key, dimension), root.right);
                else
                    return new KdNode<float>(root.key, root.left, InsertRec(root.right, key, dimension));
            }
            else
            {
                if (root.key.Item2 > key.Item2)
                    return new KdNode<float>(root.key, InsertRec(root.left, key, dimension), root.right);
                else
                    return new KdNode<float>(root.key, root.left, InsertRec(root.right, key, dimension));
            }
        }
        //Option2
        private IKdNode<float> InsertXRec(IKdNode<float> root, Tuple<float, float> key)
        {
            if (root.isEmpty)
                return new KdNode<float>(key, new EmptyKdNode<float>(), new EmptyKdNode<float>());

            if (root.key == key)
                return root;

            if (root.key.Item1 > key.Item1)
                return new KdNode<float>(root.key, InsertYRec(root.left, key), root.right);
            else
                return new KdNode<float>(root.key, root.left, InsertYRec(root.right, key));
        }
        private IKdNode<float> InsertYRec(IKdNode<float> root, Tuple<float, float> key)
        {
            if (root.isEmpty)
                return new KdNode<float>(key, new EmptyKdNode<float>(), new EmptyKdNode<float>());

            if (root.key == key)
                return root;
            if (root.key.Item2 > key.Item2)
                return new KdNode<float>(root.key, InsertXRec(root.left, key), root.right);
            else
                return new KdNode<float>(root.key, root.left, InsertXRec(root.right, key));
        }
        private IEnumerable<Vector2> GetAllNodesWithinDistanceRec(List<Vector2> foundNodes, IKdNode<float> root, Vector2 startPosition, float distance)
        {
            if (root.isEmpty)
                return foundNodes;
            if (Vector2.Distance(startPosition, new Vector2(root.key.Item1, root.key.Item2)) <= distance)
                foundNodes.Add(new Vector2(root.key.Item1, root.key.Item2));
            GetAllNodesWithinDistanceRec(foundNodes, root.left, startPosition, distance);
            GetAllNodesWithinDistanceRec(foundNodes, root.right, startPosition, distance);
            return foundNodes;
        }
        private void PrintPreOrderRec(IKdNode<float> root)
        {
            if (root.isEmpty)
                return;
            Console.WriteLine(root.key);
            PrintInOrderRec(root.left);
            PrintInOrderRec(root.right);
        }
        private void PrintInOrderRec(IKdNode<float> root)
        {
            if (root.isEmpty)
                return;
            PrintInOrderRec(root.left);
            Console.WriteLine(root.key);
            PrintInOrderRec(root.right);
        }
        private void PrintPostOrderRec(IKdNode<float> root)
        {
            if (root.isEmpty)
                return;
            PrintInOrderRec(root.left);
            PrintInOrderRec(root.right);
            Console.WriteLine(root.key);
        }
    }
}
