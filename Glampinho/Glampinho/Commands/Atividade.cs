using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Glampinho.Commands
{
    public class Atividade : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();
        public Atividade(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }

        public override void Execute(string con)
        {
            if (n.Equals("15")) ExecuteRemoveAtividade(con);  
            else ExecuteInsertUpdateAtividade(con);
        }

        private void ExecuteRemoveAtividade(string con)
        {
            int ano, num;
            try
            {
                num = Convert.ToInt32(list[0]);
                ano = Convert.ToInt32(list[1]);
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
        private void ExecuteInsertUpdateAtividade(string con)
        {
            int ano, num, lotacao;
            decimal preco;
            string nomeParque, nomeAtividade, descricaoAtividade;
            DateTime dataRealizacao;

            try
            {
                nomeParque = list[0];
                nomeAtividade = list[1];
                descricaoAtividade = list[2];
                lotacao = Convert.ToInt32(list[3]);
                dataRealizacao = Convert.ToDateTime(list[4]);
                preco = Convert.ToDecimal(list[5]);
                num = Convert.ToInt32(list[6]);
                ano = Convert.ToInt32(list[7]);
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
                    using(SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                      if(n.Equals("13"))  cmd.CommandText = "InsertAtividade";
                      else cmd.CommandText = "UpdateAtividade";

                        cmd.Parameters.Add("@num", SqlDbType.Int).Value = num;
                        cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano;
                        cmd.Parameters.Add("@parque", SqlDbType.NVarChar).Value = nomeParque;
                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeAtividade;
                        cmd.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = descricaoAtividade;
                        cmd.Parameters.Add("@lotacaoMaxima", SqlDbType.Int).Value = lotacao;
                        cmd.Parameters.Add("@dataRealizacao", SqlDbType.Date).Value = dataRealizacao;
                        cmd.Parameters.Add("@precoParticipante", SqlDbType.SmallMoney).Value = preco;
                        
                        cmd.ExecuteNonQuery();

                        if (n.Equals("13")) Console.WriteLine("Atividade inserida com sucesso!");
                        else Console.WriteLine("Atividade alterada com sucesso!");
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
            if (n.Equals("13") || n.Equals("14")) ExecuteEntInsertUpdateAtividade();
           
            else  ExecuteEntRemoveAtividade();  
        }
        private void ExecuteEntRemoveAtividade()
        {
           int num, ano;
           try
            {
                num = Convert.ToInt32(list[0]);
                ano = Convert.ToInt32(list[1]);
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
        private void ExecuteEntInsertUpdateAtividade()
        {
            int ano, num, lotacao;
            decimal preco;
            string nomeParque, nomeAtividade, descricaoAtividade;
            DateTime dataRealizacao;

            try
            {
                nomeParque = list[0];
                nomeAtividade = list[1];
                descricaoAtividade = list[2];
                lotacao = Convert.ToInt32(list[3]);
                dataRealizacao = Convert.ToDateTime(list[4]);
                preco = Convert.ToDecimal(list[5]);
                num = Convert.ToInt32(list[6]);
                ano = Convert.ToInt32(list[7]);

             /*  convert string to money
              *   string s = "000000000100";
                decimal iv = 0;
                decimal.TryParse(s, out iv);*/
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            using(var ctx = new GlampinhoEntities())
            {
                try
                {
                    if (n.Equals("13"))
                        ctx.InsertAtividade(num, ano, nomeParque, nomeAtividade, descricaoAtividade, lotacao, dataRealizacao, preco);
                    if(n.Equals("14"))
                        ctx.UpdateAtividade(num, ano, nomeParque, nomeAtividade, descricaoAtividade, lotacao, dataRealizacao, preco);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            if (n.Equals("13")) Console.WriteLine("Atividade introduzida com sucesso!");
            if (n.Equals("14")) Console.WriteLine("Atividade alterada com sucesso!");
        }
        private List<string> InfoGetter(List<string> list)
        {
            string option = n.ToString();

            switch (option)
            {
                case "13":
                    Console.WriteLine("Inserir uma Atividade.");

                    Console.WriteLine("Introduza o nome do parque.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o nome da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a descricao da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a lotacao maxima da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a data de realizacao da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o preco do participante.");
                    list.Add(Console.ReadLine());
                    break;

                case "14":
                    Console.WriteLine("Atualizar uma Atividade.");

                    Console.WriteLine("Introduza o nome do parque.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o nome da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a descricao da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a lotacao maxima da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a data de realizacao da atividade.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o preco do participante.");
                    list.Add(Console.ReadLine());

                    break;

                case "15":
                    Console.WriteLine("Remover uma Atividade.");
                    break;

       }
            Console.WriteLine("Introduza o numero da atividade.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza o ano da atividade.");
            list.Add(Console.ReadLine());

            return list;
        }
    }
}
