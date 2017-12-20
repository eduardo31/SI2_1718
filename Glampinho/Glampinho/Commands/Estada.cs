using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class Estada : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();
        public Estada(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Criar uma Estada para um dado período de tempo.");

            Console.WriteLine("Insira o id da Estada pretendida.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza a data de inicio da Estada.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza a data de fim da Estada.");
            list.Add(Console.ReadLine());

            return list;

        }
        public override void Execute(string con)
        {
            int idEstada;
            DateTime dataInicio, dataFim;
            try
            {
                idEstada = Convert.ToInt32(list[0]);
                dataInicio = Convert.ToDateTime(list[1]);
                dataFim = Convert.ToDateTime(list[2]);
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
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CreateEstada";


                        cmd.Parameters.Add("@idEstada", SqlDbType.Int).Value = idEstada;
                        cmd.Parameters.Add("@dataInicio", SqlDbType.DateTime).Value = dataInicio;
                        cmd.Parameters.Add("@dataFim", SqlDbType.DateTime).Value = dataFim;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Criacao da Estada com sucesso!");
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
            int idEstada;
            DateTime dataInicio, dataFim;
            try
            {
                idEstada = Convert.ToInt32(list[0]);
                dataInicio = Convert.ToDateTime(list[1]);
                dataFim = Convert.ToDateTime(list[2]);
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
                    ctx.CreateEstada(idEstada, dataInicio, dataFim);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
                Console.WriteLine("Criacao da Estada com sucesso!");
            }
       }
    }
}
