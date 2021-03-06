﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class InsertExtraAloj : ICmd
    {
        public readonly string Description;
        public InsertExtraAloj(string desc)
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

                        cmd.CommandText = "InsertExtraAloj";

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = tipo;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Extra de Alojamento inserido com sucesso!");
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
                    ctx.InsertExtraAloj(id, tipo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }

            Console.WriteLine("Extra de Alojamento inserido com sucesso!");
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Insercao de um extra de alojamento");

            Console.WriteLine("Introduza o tipo do Extra (ac - animal de companhia ou pe - pessoa extra ou ea - estacionamento automovel)");
            list.Add(Console.ReadLine());
            Console.WriteLine("Introduza o id do Extra)");
            list.Add(Console.ReadLine());

            return list;
        }
    }
}
