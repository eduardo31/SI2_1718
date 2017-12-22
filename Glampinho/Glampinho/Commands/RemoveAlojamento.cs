using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class RemoveAlojamento : ICmd
    {
        public readonly string Description;
        public RemoveAlojamento(string desc)
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
            string nomeAlojamento;
            try
            {
                nomeAlojamento = prms[0];
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
                        cmd.CommandText = "DeleteAlojamento";

                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeAlojamento;

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Alojamento removido com sucesso!");
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
            string nome;
            try
            {
                nome = prms[0];
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
                    ctx.DeleteAlojamento(nome);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Alojamento eliminada com sucesso!");
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Remover um Alojamento.");
            Console.WriteLine("Introduza o nome Alojamento.");
            list.Add(Console.ReadLine());
            return list;
        }
    }
}
