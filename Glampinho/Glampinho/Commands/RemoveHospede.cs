using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class RemoveHospede : ICmd
    {
        public readonly string Description;
        public RemoveHospede(string desc)
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
            int nIdentificacao;
            
            prms = InfoGetter(prms);

            try
            {
                nIdentificacao = Convert.ToInt32(prms[0]);
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
                        cmd.CommandText = "DeleteHospede";

                        cmd.Parameters.Add("@nIdentificacao", SqlDbType.NVarChar).Value = nIdentificacao;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Hospede removido com sucesso!");
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
            int nIdentificacao;

            prms = InfoGetter(prms);

            try
            {
                nIdentificacao = Convert.ToInt32(prms[0]);
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
                    ctx.DeleteHospede(nIdentificacao.ToString());
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Hospede removido com sucesso!");
        }
        private List<string> InfoGetter(List<string> prms)
        {
            Console.WriteLine("Apagar um hospede da basedados.");

            Console.WriteLine("Seleccionar id do hospede:");
            prms.Add(Console.ReadLine());

            return prms;
        }
    }
}
