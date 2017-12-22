using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class InsertAtividade : ICmd
    {
        public readonly string Description;
        public InsertAtividade(string desc)
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
            int ano, num, lotacao;
            decimal preco;
            string nomeParque, nomeAtividade, descricaoAtividade;
            DateTime dataRealizacao;

            try
            {
                nomeParque = prms[0];
                nomeAtividade = prms[1];
                descricaoAtividade = prms[2];
                lotacao = Convert.ToInt32(prms[3]);
                dataRealizacao = Convert.ToDateTime(prms[4]);
                preco = Convert.ToDecimal(prms[5]);
                num = Convert.ToInt32(prms[6]);
                ano = Convert.ToInt32(prms[7]);
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
                        cmd.CommandText = "InsertAtividade";

                        cmd.Parameters.Add("@num", SqlDbType.Int).Value = num;
                        cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano;
                        cmd.Parameters.Add("@parque", SqlDbType.NVarChar).Value = nomeParque;
                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeAtividade;
                        cmd.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = descricaoAtividade;
                        cmd.Parameters.Add("@lotacaoMaxima", SqlDbType.Int).Value = lotacao;
                        cmd.Parameters.Add("@dataRealizacao", SqlDbType.Date).Value = dataRealizacao;
                        cmd.Parameters.Add("@precoParticipante", SqlDbType.SmallMoney).Value = preco;

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Atividade inserida com sucesso!");
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
            int ano, num, lotacao;
            decimal preco;
            string nomeParque, nomeAtividade, descricaoAtividade;
            DateTime dataRealizacao;

            try
            {
                nomeParque = prms[0];
                nomeAtividade = prms[1];
                descricaoAtividade = prms[2];
                lotacao = Convert.ToInt32(prms[3]);
                dataRealizacao = Convert.ToDateTime(prms[4]);
                preco = Convert.ToDecimal(prms[5]);
                num = Convert.ToInt32(prms[6]);
                ano = Convert.ToInt32(prms[7]);
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
                    ctx.InsertAtividade(num, ano, nomeParque, nomeAtividade, descricaoAtividade, lotacao, dataRealizacao, preco);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Atividade introduzida com sucesso!");
        }
        private List<string> InfoGetter(List<string> list)
        {
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
                 
            Console.WriteLine("Introduza o numero da atividade.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza o ano da atividade.");
            list.Add(Console.ReadLine());

            return list;
        }

    }
}
