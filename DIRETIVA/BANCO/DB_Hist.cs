using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Hist : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Hist buscaHist(int cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM hist WHERE his_cod=@hist_cod";
            CL_Hist objHist = new CL_Hist();
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("hist_cod", cod);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objHist.his_cod = cod;
                        objHist.his_nome1 = dr["his_nome1"].ToString().Trim();
                        objHist.his_nome2 = dr["his_nome2"].ToString().Trim();
                        objHist.his_nome3 = dr["his_nome3"].ToString().Trim();
                        objHist.his_nome4 = dr["his_nome4"].ToString().Trim();
                        objHist.his_nome5 = dr["his_nome5"].ToString().Trim();
                        return objHist;
                    }
                    return objHist;
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