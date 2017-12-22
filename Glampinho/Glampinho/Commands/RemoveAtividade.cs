using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class RemoveAtividade : ICmd
    {
        public readonly string Description;
        public RemoveAtividade(string desc)
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
            int ano, num;
            try
            {
                num = Convert.ToInt32(prms[0]);
                ano = Convert.ToInt32(prms[1]);
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
                        cmd.CommandText = "DeleteAtividade";

                        cmd.Parameters.Add("@num", SqlDbType.Int).Value = num;
                        cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Atividade removida com sucesso!");
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
            int num, ano;
            try
            {
                num = Convert.ToInt32(prms[0]);
                ano = Convert.ToInt32(prms[1]);
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
                    ctx.DeleteAtividade(num, ano);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Atividade eliminada com sucesso!");
        }
        private List<string> InfoGetter(List<string> list)
        {
            
            Console.WriteLine("Remover uma Atividade.");
                    
            Console.WriteLine("Introduza o numero da atividade.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza o ano da atividade.");
            list.Add(Console.ReadLine());

            return list;
        }

    }
}
