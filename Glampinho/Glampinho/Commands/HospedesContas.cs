using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class HospedesContas : ICmd
    {

        public readonly string Description;
        public HospedesContas(string desc)
        {
            Description = desc;
        }
        public override string ToString()
        {
            return Description;
        }
        public List<string> prms = new List<string>();
        public void Execute(string con)
        {
            throw new NotImplementedException();
        }

        public void ExecuteEnt()
        {
            throw new NotImplementedException();
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Nome do Parque :");
            list.Add(Console.ReadLine());
            return list;
        }
    }
}
