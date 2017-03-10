using CLASSES;
using Npgsql;
using System;
using System.Data;
using System.Collections.Generic;

namespace BANCO
{
    public class DB_Movest : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static bool ultMov(CL_Est objEst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            DateTime data;

            try
            {
                string sql = "SELECT mov_data FROM movest WHERE est_cod='" + objEst.est_cod + "' ORDER BY mov_data DESC";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                NpgsqlDataReader dr;

                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        data = Convert.ToDateTime(dr["mov_data"]);
                        if (data.AddYears(5) > DateTime.Now)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Movest> buscaMovest(string nfisc, string con)
        {
            List<CL_Movest> objListMovest = new List<CL_Movest>();
            try
            {
                DB_Funcoes.DesmontaConexao(con);
                CONEXAO = montaDAO(CONEXAO);
                Conn = new NpgsqlConnection(CONEXAO);
                string sql = "SELECT * FROM movest WHERE mov_nfisc=@mov_nfisc";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("mov_nfisc", nfisc);

                NpgsqlDataReader dr;

                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListMovest.Add(new CL_Movest()
                        {
                            mov_qtdade = Convert.ToDouble(dr["mov_qtdade"]),
                            mov_valor = Convert.ToDouble(dr["mov_valor"]),
                            est_cod = dr["est_cod"].ToString().Trim(),
                            est_nome = dr["est_nome"].ToString().Trim(),
                            est_nome2 = dr["est_nome2"].ToString().Trim(),
                            mov_valor1 = Convert.ToDouble(dr["mov_valor1"]),
                            mov_nfisc = Convert.ToInt64(nfisc),
                            mov_desc = Convert.ToDouble(dr["mov_desc"]),
                            mov_pedido = Convert.ToInt64(dr["mov_pedido"]),
                            mov_cgc = dr["mov_cgc"].ToString().Trim(),
                            mov_ende = dr["mov_ende"].ToString().Trim(),
                            mov_clnome = dr["mov_clnome"].ToString().Trim(),
                            mov_ean = dr["mov_ean"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListMovest;
                }
                else
                {
                    objListMovest = null;
                    return objListMovest;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListMovest = null;
                return objListMovest;
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