using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Clasite : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Clasite buscaClasite(string ncm, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);


            string sql = "SELECT * FROM clasite WHERE cf_codigo=@ncm";

            CL_Clasite objClasite = new CL_Clasite();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("ncm", ncm);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objClasite.cf_codigo = dr["cf_codigo"].ToString().Trim();
                        objClasite.cf_bsstint = Convert.ToDouble(dr["cf_bsstint"]);
                        objClasite.cf_alstint = Convert.ToDouble(dr["cf_alstint"]);
                        objClasite.cf_bsstext = Convert.ToDouble(dr["cf_bsstext"]);
                        objClasite.cf_alstext = Convert.ToDouble(dr["cf_alstext"]);
                        objClasite.cf_tipopro = dr["cf_tipopro"].ToString().Trim();
                        objClasite.cf_venda = Convert.ToDouble(dr["cf_venda"]);
                        objClasite.cf_cstpise = dr["cf_cstpise"].ToString().Trim();
                        objClasite.cf_cstpiss = dr["cf_cstpiss"].ToString().Trim();
                        objClasite.cf_incpis = dr["cf_incpis"].ToString().Trim();
                        objClasite.cf_basepis = Convert.ToDouble(dr["cf_basepis"]);
                        objClasite.cf_aliqpis = Convert.ToDouble(dr["cf_aliqpis"]);
                        objClasite.cf_basecof = Convert.ToDouble(dr["cf_basecof"]);
                        objClasite.cf_aliqcof = Convert.ToDouble(dr["cf_aliqcof"]);
                        objClasite.cf_tabpis = dr["cf_tabpis"].ToString().Trim();
                        objClasite.cf_aliqnac = Convert.ToDouble(dr["cf_aliqnac"]);
                        objClasite.cf_aliqimp = Convert.ToDouble(dr["cf_aliqimp"]);
                        objClasite.cf_blcefd = dr["cf_blcefd"].ToString().Trim();
                        objClasite.cf_incentr = dr["cf_incentr"].ToString().Trim();
                        return objClasite;
                    }
                    else
                    {
                        objClasite = null;
                        return objClasite;
                    }
                }
                else
                {
                    objClasite = null;
                    return objClasite;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objClasite = null;
                return objClasite;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static List<CL_Clasite> listar(string ncm, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";
            if (ncm == "")
            {
                sql = "SELECT * FROM clasite";
            }
            else
            {
                ncm.Replace(".", "");
                ncm = Convert.ToUInt64(ncm).ToString(@"0000\.00\.00\");
                sql = "SELECT * FROM clasite WHERE cf_codigo LIKE '%" + ncm + "%'";
            }

            List<CL_Clasite> objList = new List<CL_Clasite>();
            CL_Clasite obj = null;

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
                        obj = new CL_Clasite();
                        obj.cf_codigo = dr["cf_codigo"].ToString().Trim();
                        obj.cf_cstpise = dr["cf_cstpise"].ToString().Trim();
                        obj.cf_cstpiss = dr["cf_cstpiss"].ToString().Trim();
                        obj.cf_aliqpis = Convert.ToDouble(dr["cf_aliqpis"]);
                        obj.cf_aliqcof = Convert.ToDouble(dr["cf_aliqcof"]);
                        obj.cf_tabpis = dr["cf_tabpis"].ToString().Trim();

                        objList.Add(obj);
                    }
                    dr.Close();
                    return objList;
                }
                else
                {
                    objList = null;
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