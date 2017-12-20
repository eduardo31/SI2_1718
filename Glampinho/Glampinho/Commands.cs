using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho
{
    public abstract class ICmd
    {
        public abstract void ExecuteEnt();//Entity Framework
        public abstract void Execute(string con);// ADO.NET

        public StringBuilder convertToString(string con)
        {
            StringBuilder n = new StringBuilder();

            foreach(char c in con)
            {
                if (c == '.') break;
                else if (c >= '0' && c <= '9') n.Append(c);
                else return n;
            }
            return n;
        }
    }
}
