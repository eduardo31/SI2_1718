using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class ExtraAloj : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();

        public ExtraAloj(string s)
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
                case "7":
                    Console.WriteLine("Insercao de um extra de alojamento");

                    Console.WriteLine("Introduza o tipo do Extra (ac - animal de companhia ou pe - pessoa extra ou ea - estacionamento automovel)");
                    list.Add(Console.ReadLine());
                    break;
                case "8":
                    Console.WriteLine("Atualizacao de um extra de alojamento");

                    Console.WriteLine("Introduza o tipo do Extra (ac - animal de companhia ou pe - pessoa extra ou ea - estacionamento automovel)");
                    list.Add(Console.ReadLine());
                    break;
                case "9":
                    Console.WriteLine("Remocao de um extra de alojamento");
                    break;
            }     
            Console.WriteLine("Introduza o id do Extra)");
            list.Add(Console.ReadLine());

            return list;
        }
        public override void Execute(string con)
        {
            if (n.Equals("9")) ExecuteRemoveExAloj(con);
            else ExecuteInsertUpdateExAloj(con);
        }
        private void ExecuteInsertUpdateExAloj(string con)
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

                        if (n.Equals("7")) cmd.CommandText = "InsertExtraAloj";
                        else cmd.CommandText = "UpdateExtraAloj";

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = tipo;

                        cmd.ExecuteNonQuery();
                        if (n.Equals("4")) Console.WriteLine("Extra de Alojamento inserido com sucesso!");
                        else Console.WriteLine("Extra de Alojamento atualizado com sucesso!");
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

        private void ExecuteRemoveExAloj(string con)
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
                        cmd.CommandText = "DeleteExtraAloj";

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Extra de Alojamento removida com sucesso!");
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
            if (n.Equals("9")) ExecuteEntRemoveExAloj();
            else ExecuteEntInsertUpdateExAloj();
        }

        private void ExecuteEntInsertUpdateExAloj()
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
                    if (n.Equals("7")) ctx.InsertExtraAloj(id, tipo);
                    else ctx.UpdateExtraAloj(id, tipo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
          
            if (n.Equals("7")) Console.WriteLine("Extra de Alojamento inserido com sucesso!");
            else Console.WriteLine("Extra de Alojamento atualizado com sucesso!");
        }
        private void ExecuteEntRemoveExAloj()
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
            Console.WriteLine("Extra de Alojamento eliminado com sucesso!");
        }
    }
}
