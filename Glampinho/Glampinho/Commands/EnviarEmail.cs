using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class EnviarEmail : ICmd
    {
        public readonly string Description;
        public EnviarEmail(string desc)
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
            prms = InfoGetter(prms);
            int intervalo;

            try
            {
                intervalo = Convert.ToInt32(prms[0]);
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
                        cmd.CommandText = "EnviarMailIntervalo";

                        cmd.Parameters.Add("@dias", SqlDbType.Int).Value = intervalo;
                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Emails Enviados.");
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

        public void ExecuteEnt()
        {
            prms = InfoGetter(prms);
            int intervalo;

            try
            {
                intervalo = Convert.ToInt32(prms[0]);
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
                    ctx.EnviarMailResponsaveis(intervalo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }

            }

            Console.WriteLine("Emails Enviados.");
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Avisar clientes.");

            Console.WriteLine("Intervalo em dias:");
            list.Add(Console.ReadLine());
            return list;
        }
    }
}
