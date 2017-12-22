using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho
{
    public interface ICmd
    {
        void ExecuteEnt();//Entity Framework
        void Execute(string con);// ADO.NET

        /*public StringBuilder convertToString(string con)
        {
            StringBuilder n = new StringBuilder();

            foreach(char c in con)
            {
                if (c == '.') break;
                else if (c >= '0' && c <= '9') n.Append(c);
                else return n;
            }
            return n;
        }*/
    }
}
