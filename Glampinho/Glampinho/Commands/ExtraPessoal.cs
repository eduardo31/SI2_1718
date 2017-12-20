using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class ExtraPessoal : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();

        public ExtraPessoal(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }

        private List<string> InfoGetter(List<string> list)
        {
            string s = n.ToString();
            switch (s)
            {
                case "10":
                    Console.WriteLine("Insercao de um extra pessoal");

                    Console.WriteLine("Introduza o tipo do Extra (pa - pequeno almoco ou mp - meia pensao ou pc - pensao completa)");
                    list.Add(Console.ReadLine());

                    break;
                case "11":
                    Console.WriteLine("Atualizacao de um extra pessoal");

                    Console.WriteLine("Introduza o tipo do Extra (pa - pequeno almoco ou mp - meia pensao ou pc - pensao completa)");
                    list.Add(Console.ReadLine());
                    
                    break;
                case "12":
                    Console.WriteLine("Remocao de um extra pessoal");
                    break;
            }
            Console.WriteLine("Introduza o id do Extra)");
            list.Add(Console.ReadLine());

            return list;
        }
        public override void ExecuteEnt()
        {
            if (n.Equals("9")) ExecuteEntRemoveExPessoal();
            else ExecuteEntInsertUpdateExPessoal();
        }
        private void ExecuteEntInsertUpdateExPessoal()
        {
            int id;
            string tipo;
            try
            {
                tipo = list[0];
                id = Convert.ToInt32(list[1]);
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
                    if (n.Equals("10")) ctx.InsertExtraPessoal(id, tipo);
                    else ctx.UpdateExtraPessoal(id, tipo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }

            if (n.Equals("7")) Console.WriteLine("Extra Pessoal inserido com sucesso!");
            else Console.WriteLine("Extra Pessoal atualizado com sucesso!");
        }

        private void ExecuteEntRemoveExPessoal()
        {
            int id;
            try
            {
                id = Convert.ToInt32(list[0]);
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
                    ctx.DeleteExtraAloj(id);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Extra Pessoal eliminado com sucesso!");
        }

        public override void Execute(string con)
        {
            if (n.Equals("12")) ExecuteRemoveExPessoal(con);
            else ExecuteInsertUpdateExPessoal(con);
        }

        private void ExecuteInsertUpdateExPessoal(string con)
        {
            int id;
            string tipo;
            try
            {
                id = Convert.ToInt32(list[1]);
                tipo = list[0];
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

                        if (n.Equals("7")) cmd.CommandText = "InsertExtraPessoal";
                        else cmd.CommandText = "UpdateExtraPessoal";

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = tipo;

                        cmd.ExecuteNonQuery();
                        if (n.Equals("4")) Console.WriteLine("Extra Pessoal inserido com sucesso!");
                        else Console.WriteLine("Extra Pessoal atualizado com sucesso!");
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

        private void ExecuteRemoveExPessoal(string con)
        {
            int id;
            try
            {
                id = Convert.ToInt32(list[0]);
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
                        cmd.CommandText = "DeleteExtraPessoal";

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Extra Pessoal removida com sucesso!");
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
    }
}
