using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Assignment2
{
    class NodeIsEmptyException : Exception
    {
        public NodeIsEmptyException(string message) : base(message)
        {
            
        }
    }
}
