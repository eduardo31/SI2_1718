using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Exceptions
{
    class MismatchedCommand:Exception
    {
        public MismatchedCommand(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
