using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Assignment2
{
    class DuplicateNodeException : Exception
    {
        public DuplicateNodeException(string message) : base(message)
        {

        }
    }
}
