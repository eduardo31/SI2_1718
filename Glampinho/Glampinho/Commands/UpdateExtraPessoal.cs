using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class UpdateExtraPessoal : ICmd
    {
        public readonly string Description;
        public UpdateExtraPessoal(string desc)
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
            int id;
            string tipo;
            try
            {
                id = Convert.ToInt32(prms[1]);
                tipo = prms[0];
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

                        cmd.CommandText = "UpdateExtraPessoal";

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = tipo;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Extra Pessoal atualizado com sucesso!");
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
            int id;
            string tipo;
            try
            {
                tipo = prms[0];
                id = Convert.ToInt32(prms[1]);
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
                    ctx.UpdateExtraPessoal(id, tipo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }

            Console.WriteLine("Extra Pessoal atualizado com sucesso!");
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Atualizacao de um extra pessoal");

            Console.WriteLine("Introduza o tipo do Extra (pa - pequeno almoco ou mp - meia pensao ou pc - pensao completa)");
            list.Add(Console.ReadLine());
            Console.WriteLine("Introduza o id do Extra)");
            list.Add(Console.ReadLine());

            return list;
        }

    }
}
