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
    }
}
