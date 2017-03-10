using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Grupo : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Grupo buscaGrupo(string cfop, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Grupo objGrupo = new CL_Grupo();
            cfop = cfop.Replace(".", "");
            string sql = "SELECT * FROM grupo WHERE gru_cod='" + cfop + "'";

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
                        objGrupo.gru_cfxml = dr["gru_cfxml"].ToString().Trim();
                        objGrupo.gru_grau = Convert.ToInt32(dr["gru_grau"]);
                        objGrupo.gru_fatur = dr["gru_fatur"].ToString().Trim();
                        objGrupo.gru_tipo = dr["gru_tipo"].ToString().Trim();
                        objGrupo.gru_tplct = dr["gru_tplct"].ToString().Trim();
                        objGrupo.gru_cdpis = dr["gru_cdpis"].ToString().Trim();
                        objGrupo.gru_nome = dr["gru_nome"].ToString().Trim();
                        objGrupo.gru_nome2 = dr["gru_nome2"].ToString().Trim();
                        objGrupo.gru_ctbil = dr["gru_ctbil"].ToString().Trim();
                        objGrupo.gru_cfisc = dr["gru_cfisc"].ToString().Trim();
                        objGrupo.gru_vctbil = Convert.ToDouble(dr["gru_vctbil"]);
                        objGrupo.gru_vbase = Convert.ToDouble(dr["gru_vbase"]);
                        objGrupo.gru_vimp = Convert.ToDouble(dr["gru_vimp"]);
                        objGrupo.gru_visen = Convert.ToDouble(dr["gru_visen"]);
                        objGrupo.gru_voutr = Convert.ToDouble(dr["gru_voutr"]);
                        objGrupo.gru_voutep = Convert.ToDouble(dr["gru_voutep"]);
                        objGrupo.gru_linicm = Convert.ToInt32(dr["gru_linicm"]);
                        objGrupo.gru_linipi = Convert.ToInt32(dr["gru_linipi"]);
                        objGrupo.gru_basei = Convert.ToDouble(dr["gru_basei"]);
                        objGrupo.gru_vimpi = Convert.ToDouble(dr["gru_vimpi"]);
                        objGrupo.gru_viseni = Convert.ToDouble(dr["gru_viseni"]);
                        objGrupo.gru_voutri = Convert.ToDouble(dr["gru_voutri"]);
                        objGrupo.gru_lingia = Convert.ToInt32(dr["gru_lingia"]);
                        objGrupo.gru_bsubs = Convert.ToDouble(dr["gru_bsubs"]);
                        objGrupo.gru_vsubs = Convert.ToDouble(dr["gru_vsubs"]);
                        objGrupo.gru_apur = dr["gru_apur"].ToString().Trim();
                        objGrupo.gru_temmov = dr["gru_temmov"].ToString().Trim();
                        objGrupo.gru_cdcof = dr["gru_cdcof"].ToString().Trim();
                        return objGrupo;
                    }
                    else
                    {
                        objGrupo = null;
                        return objGrupo;
                    }
                }
                else
                {
                    objGrupo = null;
                    return objGrupo;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objGrupo = null;
                return objGrupo;
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