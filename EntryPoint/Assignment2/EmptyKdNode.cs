using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Assignment2
{
    class EmptyKdNode<T> : IKdNode<T>
    {
        public bool isEmpty
        {
            get
            {
                return true;
            }
        }
        public Tuple<T, T> key
        {
            get
            {
                throw new NodeIsEmptyException("Node is empty and has no value");
            }
        }
        public IKdNode<T> left
        {
            get
            {
                throw new NodeIsEmptyException("Node is empty and has no left");
            }
        }
        public IKdNode<T> right
        {
            get
            {
                throw new NodeIsEmptyException("Node is empty and has no right");
            }
        }
    }
}
