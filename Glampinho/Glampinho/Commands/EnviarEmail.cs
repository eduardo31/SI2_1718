using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class EnviarEmail : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();
        public EnviarEmail(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Enviar emails a todos os hospedes responsaveis.");

            Console.WriteLine("nif do hospede responsavel.");
            list.Add(Console.ReadLine());

            Console.WriteLine("texto que deseja no email.");
            list.Add(Console.ReadLine());

            return list;
        }

        public override void Execute(string con)
        {
            int nif;
            string texto;
            try
            {
                nif = Convert.ToInt32(list[0]);
                texto = list[1];
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
                        cmd.CommandText = "SendMail";

                        cmd.Parameters.Add("@nif", SqlDbType.Decimal).Value = nif;
                        cmd.Parameters.Add("@texto", SqlDbType.NVarChar).Value = texto;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Envio de email bem sucedido!");
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
            int nif;
            string texto;
            try
            {
                nif = Convert.ToInt32(list[0]);
                texto = list[1];
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
                    ctx.SendMail(nif, texto);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Envio de email bem sucedido!");
        }
    }
}
