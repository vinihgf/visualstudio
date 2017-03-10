using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Movdia : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static List<CL_Movdia> buscaMov(DateTime dataI, DateTime dataF, string tipo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (tipo == "D")
                sql = "SELECT * FROM mov_dia WHERE m_data>=@dataI AND m_data<=@dataF";
            else
                sql = "SELECT SUM(m_avista) AS m_avista, SUM(m_aprazo) AS m_aprazo, SUM(m_receb) AS m_receb, SUM(m_pgto) AS m_pgto, SUM(m_naorec)AS m_naorec, " +
                      "SUM(m_naopg) AS m_naopg, SUM(m_atrasrec) AS m_atrasrec, SUM(m_atraspg) AS m_atraspg FROM mov_dia WHERE m_data>=@dataI AND m_data<=@dataF";
            List<CL_Movdia> objList = new List<CL_Movdia>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dataI", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataF", dataF.ToShortDateString());
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (tipo == "D")
                        {
                            objList.Add(new CL_Movdia()
                            {
                                m_data = dr["m_data"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["m_data"]),
                                m_avista = dr["m_avista"] is DBNull ? 0 : Convert.ToDouble(dr["m_avista"]),
                                m_aprazo = dr["m_aprazo"] is DBNull ? 0 : Convert.ToDouble(dr["m_aprazo"]),
                                m_atraspg = dr["m_atraspg"] is DBNull ? 0 : Convert.ToDouble(dr["m_aprazo"]),
                                m_atrasreceb = dr["m_atrasrec"] is DBNull ? 0 : Convert.ToDouble(dr["m_atrasrec"]),
                                m_naopg = dr["m_naopg"] is DBNull ? 0 : Convert.ToDouble(dr["m_naopg"]),
                                m_naoreceb = dr["m_naorec"] is DBNull ? 0 : Convert.ToDouble(dr["m_naorec"]),
                                m_pgto = dr["m_pgto"] is DBNull ? 0 : Convert.ToDouble(dr["m_pgto"]),
                                m_receb = dr["m_receb"] is DBNull ? 0 : Convert.ToDouble(dr["m_receb"]),
                            });
                        }
                        else
                        {
                            objList.Add(new CL_Movdia()
                            {
                                m_data = dataF,
                                m_avista = dr["m_avista"] is DBNull ? 0 : Convert.ToDouble(dr["m_avista"]),
                                m_aprazo = dr["m_aprazo"] is DBNull ? 0 : Convert.ToDouble(dr["m_aprazo"]),
                                m_atraspg = dr["m_atraspg"] is DBNull ? 0 : Convert.ToDouble(dr["m_aprazo"]),
                                m_atrasreceb = dr["m_atrasrec"] is DBNull ? 0 : Convert.ToDouble(dr["m_atrasrec"]),
                                m_naopg = dr["m_naopg"] is DBNull ? 0 : Convert.ToDouble(dr["m_naopg"]),
                                m_naoreceb = dr["m_naorec"] is DBNull ? 0 : Convert.ToDouble(dr["m_naorec"]),
                                m_pgto = dr["m_pgto"] is DBNull ? 0 : Convert.ToDouble(dr["m_pgto"]),
                                m_receb = dr["m_receb"] is DBNull ? 0 : Convert.ToDouble(dr["m_receb"]),
                            });
                        }
                    }
                    dr.Close();
                    return objList;
                }
                else
                {
                    return objList;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objList = null;
                return objList;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
    }
}