using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BANCO
{
    public class DB_Conctb : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static CL_Conctb buscaConctb(string con_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_Conctb obj = new CL_Conctb();

            string sql = "SELECT con_nome FROM conctb WHERE con_cod='" + con_cod + "'";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        obj.con_nome = dr["con_nome"].ToString().Trim();
                        return obj;
                    }
                    else
                        return null;
                }
                else
                    return null;

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