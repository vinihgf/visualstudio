using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Histnf : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Histnf buscaHist(int cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM histnf WHERE his_cod=@hist_cod";
            CL_Histnf objHistnf = new CL_Histnf();
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
                        objHistnf.his_cod = cod;
                        objHistnf.his_nome1 = dr["his_nome1"].ToString().Trim();
                        objHistnf.his_nome2 = dr["his_nome2"].ToString().Trim();
                        objHistnf.his_nome3 = dr["his_nome3"].ToString().Trim();
                        objHistnf.his_nome4 = dr["his_nome4"].ToString().Trim();
                        objHistnf.his_nome5 = dr["his_nome5"].ToString().Trim();
                        return objHistnf;
                    }
                    return objHistnf;
                }
                else
                    return objHistnf;

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