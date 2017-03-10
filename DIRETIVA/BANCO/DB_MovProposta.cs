using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_MovProposta : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static List<CL_MovProposta> buscaProposta(int p_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM mov_proposta WHERE mp_proposta=@p_id";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_id", p_id);
            NpgsqlDataReader dr;
            List<CL_MovProposta> objList = new List<CL_MovProposta>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_MovProposta()
                        {
                            mp_id = Convert.ToInt32(dr["mp_id"]),
                            mp_perda = Convert.ToDouble(dr["mp_perda"]),
                            mp_area = Convert.ToDouble(dr["mp_area"]),
                            mp_nome = dr["mp_nome"].ToString().Trim(),
                            mp_numsinistro = dr["mp_numsinistro"].ToString(),
                        });
                    }
                    dr.Close();
                    return objList;
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