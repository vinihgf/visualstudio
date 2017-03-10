using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using CLASSES;
using System.Data;

namespace BANCO
{
    public class DB_Bancos : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static List<CL_Bancos> listagemSimples(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT bco_cod, bco_nome FROM bancos ORDER BY bco_cod";
            List<CL_Bancos> objList = new List<CL_Bancos>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Bancos()
                        {
                            bco_cod = dr["bco_cod"] is DBNull ? 0 : Convert.ToInt32(dr["bco_cod"]),
                            bco_codNome = dr["bco_cod"] is DBNull ? "0" : dr["bco_cod"].ToString().Trim() + " - " + dr["bco_nome"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objList;
                }
                else
                    return objList;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
    }
}