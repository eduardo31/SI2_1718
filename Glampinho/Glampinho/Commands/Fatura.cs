using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Text;

namespace Glampinho.Commands
{
    public class Fatura : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();
        public Fatura(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Pagamento de uma estada com a emissao da respetiva fatura.");

            Console.WriteLine("Introduza o id da Estada.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza o ano.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Insira o id da Fatura.");
            list.Add(Console.ReadLine());

            return list;
        }
        public override void Execute(string con)
        {
            int id_Estada, ano;
            try
            {
                id_Estada = Convert.ToInt32(list[0]);
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
                        cmd.CommandText = "PagarEstada";

                        cmd.Parameters.Add("@Id_Estada", SqlDbType.Int).Value = id_Estada;
                        cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano;
                        var id_Fatura = cmd.Parameters.Add("@Id_Factura", SqlDbType.Int);

                        id_Fatura.Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Pgamento da Estada com respetiva fatura com o id {0}"+id_Fatura);
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
            int id_Estada, ano;
            try
            {
                id_Estada = Convert.ToInt32(list[0]);
                ano = Convert.ToInt32(list[1]);   
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            var id_Fatura = new ObjectParameter("Id_Factura", typeof(Int32));
            using (var ctx = new GlampinhoEntities())
            {
                try
                {
                    ctx.PagarEstada(id_Estada, ano, id_Fatura);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
                Console.WriteLine("Pagamento da Estada com respetiva fatura com o id {0}" + id_Fatura);
            }
            
        }
    }
}
