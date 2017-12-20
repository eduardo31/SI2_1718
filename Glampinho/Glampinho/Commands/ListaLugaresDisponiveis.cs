using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class ListaLugaresDisponiveis : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();
        public ListaLugaresDisponiveis(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }

        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Listagem de todas as atividades com lugares disponiveis entre um dado intervalo de datas especificado");

            Console.WriteLine("Insira a data de inicio.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Insira a data de fim.");
            list.Add(Console.ReadLine());

            return list;
        }

        public override void Execute(string con)
        {
            DateTime dataInicio, dataFim;
            try
            {
                dataInicio = Convert.ToDateTime(list[0]);
                dataFim = Convert.ToDateTime(list[1]);
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                SqlTransaction tran = sql.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.TableDirect;
                        cmd.CommandText = "ListarAtividadesDisponiveis";

                        cmd.Parameters.Add("@dataInicio", SqlDbType.Date).Value = dataInicio;
                        cmd.Parameters.Add("@dataFim", SqlDbType.Date).Value = dataFim;

                        cmd.ExecuteNonQuery(); 
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    Console.WriteLine(e.Message);
                    return;
                }
                tran.Commit();
                sql.Close();
            }
        }

        public override void ExecuteEnt()
        {
            DateTime dataInicio, dataFim;
            try
            {
                dataInicio = Convert.ToDateTime(list[0]);
                dataFim = Convert.ToDateTime(list[1]);
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            using (var ctx = new GlampinhoEntities())
            {
                try
                {
                    ctx.ListarAtividadesDisponiveis(dataInicio,dataFim);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
        }
    }
}
