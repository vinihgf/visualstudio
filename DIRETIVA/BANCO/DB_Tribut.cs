using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Tribut : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Tribut buscaTribut(int tri_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Tribut objTribut = new CL_Tribut();

            string sql = "SELECT * FROM tribut WHERE tri_cod=" + tri_cod;

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
                        objTribut.tri_alcof = dr["tri_alcof"] is DBNull ? 0 : Convert.ToDouble(dr["tri_alcof"]);
                        objTribut.tri_aliq = dr["tri_aliq"] is DBNull ? 0 : Convert.ToDouble(dr["tri_aliq"]);
                        objTribut.tri_alpis = dr["tri_alpis"] is DBNull ? 0 : Convert.ToDouble(dr["tri_alpis"]);
                        objTribut.tri_base = dr["tri_base"] is DBNull ? 0 : Convert.ToDouble(dr["tri_base"]);
                        objTribut.tri_basei = dr["tri_basei"] is DBNull ? 0 : Convert.ToDouble(dr["tri_basei"]);
                        objTribut.tri_basest = dr["tri_basest"] is DBNull ? 0 : Convert.ToDouble(dr["tri_basest"]);
                        objTribut.tri_bscof = dr["tri_bscof"] is DBNull ? 0 : Convert.ToDouble(dr["tri_bscof"]);
                        objTribut.tri_bspis = dr["tri_bspis"] is DBNull ? 0 : Convert.ToDouble(dr["tri_bspis"]);
                        objTribut.tri_bssimp = dr["tri_bssimp"] is DBNull ? 0 : Convert.ToDouble(dr["tri_bssimp"]);
                        objTribut.tri_cae = dr["tri_cae"].ToString().Trim();
                        objTribut.tri_cod = dr["tri_cod"] is DBNull ? 0 : Convert.ToInt32(dr["tri_cod"]);
                        objTribut.tri_cofins = dr["tri_cofins"].ToString().Trim();
                        objTribut.tri_credito = dr["tri_credit"] is DBNull ? 0 : Convert.ToDouble(dr["tri_credit"]);
                        objTribut.tri_csoc = dr["tri_csoc"].ToString().Trim();
                        objTribut.tri_ctbil = dr["tri_ctbil"] is DBNull ? 0 : Convert.ToDouble(dr["tri_ctbil"]);
                        objTribut.tri_custo = dr["tri_custo"].ToString().Trim();
                        objTribut.tri_cvicms = dr["tri_cvicms"] is DBNull ? 0 : Convert.ToDouble(dr["tri_cvicms"]);
                        objTribut.tri_cfisc = dr["tri_cfisc"].ToString().Trim();
                        objTribut.tri_cfouf = dr["tri_cfouf"].ToString().Trim();
                        objTribut.tri_debito = dr["tri_debito"] is DBNull ? 0 : Convert.ToDouble(dr["tri_debito"]);
                        objTribut.tri_frural = dr["tri_frural"].ToString().Trim();
                        objTribut.tri_hiscn = dr["tri_hiscn"] is DBNull ? 0 : Convert.ToDouble(dr["tri_hiscn"]);
                        objTribut.tri_hist = dr["tri_hist"] is DBNull ? 0 :  Convert.ToDouble(dr["tri_hist"]);
                        objTribut.tri_histn = dr["tri_histn"].ToString().Trim();
                        objTribut.tri_icmecf = dr["tri_icmecf"] is DBNull ? 0 : Convert.ToDouble(dr["tri_icmecf"]);
                        objTribut.tri_icmsim = dr["tri_icmsim"] is DBNull ? 0 : Convert.ToDouble(dr["tri_icmsim"]);
                        objTribut.tri_icmst = dr["tri_icmst"] is DBNull ? 0 : Convert.ToDouble(dr["tri_icmst"]);
                        objTribut.tri_ir = dr["tri_ir"].ToString().Trim();
                        objTribut.tri_irr = dr["tri_irr"].ToString().Trim();
                        objTribut.tri_isent = dr["tri_isent"] is DBNull ? 0 : Convert.ToDouble(dr["tri_isent"]);
                        objTribut.tri_isenti = dr["tri_isenti"] is DBNull ? 0 : Convert.ToDouble(dr["tri_isenti"]);
                        objTribut.tri_isimp = dr["tri_isimp"].ToString().Trim();
                        objTribut.tri_issqn = dr["tri_issqn"].ToString().Trim();
                        objTribut.tri_mdecf = dr["tri_mdecf"].ToString().Trim();
                        objTribut.tri_modbc = dr["tri_modbc"] is DBNull ? 0 : Convert.ToDouble(dr["tri_modbc"]);
                        objTribut.tri_noecf = dr["tri_noecf"] is DBNull ? 0 : Convert.ToDouble(dr["tri_noecf"]);
                        objTribut.tri_nome = dr["tri_nome"].ToString().Trim();
                        objTribut.tri_nsecf = dr["tri_nsecf"].ToString().Trim();
                        objTribut.tri_obs1 = dr["tri_obs1"].ToString().Trim();
                        objTribut.tri_obs2 = dr["tri_obs2"].ToString().Trim();
                        objTribut.tri_outra = dr["tri_outra"] is DBNull ? 0 : Convert.ToDouble(dr["tri_outra"]);
                        objTribut.tri_outrai = dr["tri_outrai"] is DBNull ? 0 : Convert.ToDouble(dr["tri_outrai"]);
                        objTribut.tri_pcissq = dr["tri_pcissq"] is DBNull ? 0 : Convert.ToDouble(dr["tri_pcissq"]);
                        objTribut.tri_pcrura = dr["tri_pcrura"] is DBNull ? 0 : Convert.ToDouble(dr["tri_pcrura"]);
                        objTribut.tri_period = dr["tri_period"].ToString().Trim();
                        objTribut.tri_pis = dr["tri_pis"].ToString().Trim(); ;
                        objTribut.tri_prazo = dr["tri_prazo"].ToString().Trim();
                        objTribut.tri_scofef = dr["tri_scofef"].ToString().Trim();
                        objTribut.tri_spisef = dr["tri_spisef"].ToString().Trim();
                        objTribut.tri_stcof = dr["tri_stcof"].ToString().Trim();
                        objTribut.tri_sticms = dr["tri_sticms"].ToString().Trim();
                        objTribut.tri_stipi = dr["tri_stipi"].ToString().Trim();
                        objTribut.tri_stpis = dr["tri_stpis"].ToString().Trim();
                        objTribut.tri_strib = dr["tri_strib"].ToString().Trim();
                        objTribut.tri_tbecf = dr["tri_tbecf"].ToString().Trim();
                        objTribut.tri_tipolc = dr["tri_tipolc"] is DBNull ? 0 : Convert.ToDouble(dr["tri_tipolc"]);
                        objTribut.tri_usada = dr["tri_usada"].ToString().Trim();
                        objTribut.tri_vlrdes = dr["tri_vlrdes"] is DBNull ? 0 : Convert.ToDouble(dr["tri_vlrdes"]);
                        objTribut.tri_vlrtot = dr["tri_vlrtot"] is DBNull ? 0 : Convert.ToDouble(dr["tri_vlrtot"]);
                        objTribut.tri_rcsl = dr["tri_rcsl"].ToString().Trim();
                        objTribut.tri_rpis = dr["tri_rpis"].ToString().Trim();
                        objTribut.tri_rcof = dr["tri_rcof"].ToString().Trim();
                        return objTribut;
                    }
                    else
                    {
                        objTribut = null;
                        return objTribut;
                    }
                }
                else
                {
                    objTribut = null;
                    return objTribut;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objTribut = null;
                return objTribut;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool excluiTribut(CL_Tribut objTribut, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "DELETE FROM tribut WHERE tri_cod=@tri_cod";
            try
            {
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("tri_cod", objTribut.tri_cod);
                Conn.Open();
                comand.ExecuteScalar();
                return true;
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

        public static bool alteraTribut(CL_Tribut objTribut, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "UPDATE tribut SET tri_cod=@tri_cod, tri_nome=@tri_nome, tri_cfisc=@tri_cfisc, tri_cfouf=@tri_cfouf, tri_base=@tri_base, tri_aliq=@tri_aliq, " +
                "tri_tbecf=@tri_tbecf, tri_isent=@tri_isent, tri_outra=@tri_outra, tri_basei=@tri_basei, tri_isenti=@tri_isenti, tri_outrai=tri_outrai, tri_pis=@tri_pis, " +
                "tri_cofins=@tri_cofins, tri_issqn=@tri_issqn, tri_ir=@tri_ir, tri_csoc=@tri_csoc, tri_pcissq=@tri_pcissq, tri_isimp=@tri_isimp, tri_irr=@tri_irr, " +
                "tri_custo=@tri_custo, tri_histn=@tri_histn, tri_sticms=@tri_sticms, tri_stipi=@tri_stipi, tri_bssimp=@tri_bssimp, tri_icmsim=@tri_icmsim, " +
                "tri_modbc=@tri_modbc, tri_icmecf=@tri_icmecf, tri_perirr=@tri_perirr, tri_rcsl=@tri_rcsl, tri_rpis=@tri_rpis, tri_prpis=@tri_prpis, " +
                "tri_rcof=@tri_rcof, tri_prcof=@tri_prcof WHERE tri_cod=@tri_cod";
            try
            {
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("tri_cod", objTribut.tri_cod);
                comand.Parameters.AddWithValue("tri_nome", objTribut.tri_nome);
                comand.Parameters.AddWithValue("tri_cfisc", objTribut.tri_cfisc);
                comand.Parameters.AddWithValue("tri_cfouf", objTribut.tri_cfouf);
                comand.Parameters.AddWithValue("tri_base", objTribut.tri_base);
                comand.Parameters.AddWithValue("tri_aliq", objTribut.tri_aliq);
                comand.Parameters.AddWithValue("tri_tbecf", objTribut.tri_tbecf);
                comand.Parameters.AddWithValue("tri_isent", objTribut.tri_isent);
                comand.Parameters.AddWithValue("tri_outra", objTribut.tri_outra);
                comand.Parameters.AddWithValue("tri_basei", objTribut.tri_basei);
                comand.Parameters.AddWithValue("tri_isenti", objTribut.tri_isenti);
                comand.Parameters.AddWithValue("tri_outrai", objTribut.tri_outrai);
                comand.Parameters.AddWithValue("tri_pis", objTribut.tri_pis);
                comand.Parameters.AddWithValue("tri_cofins", objTribut.tri_cofins);
                comand.Parameters.AddWithValue("tri_issqn", objTribut.tri_issqn);
                comand.Parameters.AddWithValue("tri_ir", objTribut.tri_ir);
                comand.Parameters.AddWithValue("tri_csoc", objTribut.tri_csoc);
                comand.Parameters.AddWithValue("tri_pcissq", objTribut.tri_pcissq);
                comand.Parameters.AddWithValue("tri_isimp", objTribut.tri_isimp);
                comand.Parameters.AddWithValue("tri_irr", objTribut.tri_irr);
                comand.Parameters.AddWithValue("tri_custo", objTribut.tri_custo);
                comand.Parameters.AddWithValue("tri_histn", objTribut.tri_histn);
                comand.Parameters.AddWithValue("tri_hiscn", objTribut.tri_hiscn);
                comand.Parameters.AddWithValue("tri_basest", objTribut.tri_basest);
                comand.Parameters.AddWithValue("tri_sticms", objTribut.tri_sticms);
                comand.Parameters.AddWithValue("tri_stipi", objTribut.tri_stipi);
                comand.Parameters.AddWithValue("tri_bssimp", objTribut.tri_bssimp);
                comand.Parameters.AddWithValue("tri_icmsim", objTribut.tri_icmsim);
                comand.Parameters.AddWithValue("tri_modbc", objTribut.tri_modbc);
                comand.Parameters.AddWithValue("tri_icmecf", objTribut.tri_icmecf);
                comand.Parameters.AddWithValue("tri_perirr", objTribut.tri_perirr);
                comand.Parameters.AddWithValue("tri_rcsl", objTribut.tri_rcsl);
                comand.Parameters.AddWithValue("tri_prcsl", objTribut.tri_prcsl);
                comand.Parameters.AddWithValue("tri_rpis", objTribut.tri_rpis);
                comand.Parameters.AddWithValue("tri_prpis", objTribut.tri_prpis);
                comand.Parameters.AddWithValue("tri_rcof", objTribut.tri_rcof);
                comand.Parameters.AddWithValue("tri_prcof", objTribut.tri_prcof);
                Conn.Open();
                comand.ExecuteScalar();
                return true;
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

        public static bool cadTribut(CL_Tribut objTribut, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                string sql = "INSERT INTO tribut (tri_cod, tri_nome, tri_cfisc, tri_cfouf, tri_base, tri_aliq, tri_tbecf, tri_isent, tri_outra, tri_basei, tri_isenti, " +
                    "tri_outrai, tri_pis, tri_cofins, tri_issqn, tri_ir, tri_csoc, tri_pcissq, tri_isimp, tri_irr, tri_custo, tri_histn, tri_hiscn, tri_sticms, " +
                    "tri_stipi, tri_bssimp, tri_icmsim, tri_modbc, tri_icmecf, tri_perirr, tri_rcsl, tri_prcsl, tri_rpis, tri_prpis, tri_rcof, tri_prcof) " +
                "VALUES (@tri_cod, @tri_nome, @tri_cfisc, @tri_cfouf, @tri_base, @tri_aliq, @tri_tbecf, @tri_isent, @tri_outra, @tri_basei, @tri_isenti, " +
                    "@tri_outrai, @tri_pis, @tri_cofins, @tri_issqn, @tri_ir, @tri_csoc, @tri_pcissq, @tri_isimp, @tri_irr, @tri_custo, @tri_histn, @tri_hiscn, @tri_sticms, " +
                    "@tri_stipi, @tri_bssimp, @tri_icmsim, @tri_modbc, @tri_icmecf, @tri_perirr, @tri_rcsl, @tri_prcsl, @tri_rpis, @tri_prpis, @tri_rcof, @tri_prcof)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("tri_cod", objTribut.tri_cod);
                comand.Parameters.AddWithValue("tri_nome", objTribut.tri_nome);
                comand.Parameters.AddWithValue("tri_cfisc", objTribut.tri_cfisc);
                comand.Parameters.AddWithValue("tri_cfouf", objTribut.tri_cfouf);
                comand.Parameters.AddWithValue("tri_base", objTribut.tri_base);
                comand.Parameters.AddWithValue("tri_aliq", objTribut.tri_aliq);
                comand.Parameters.AddWithValue("tri_tbecf", objTribut.tri_tbecf);
                comand.Parameters.AddWithValue("tri_isent", objTribut.tri_isent);
                comand.Parameters.AddWithValue("tri_outra", objTribut.tri_outra);
                comand.Parameters.AddWithValue("tri_basei", objTribut.tri_basei);
                comand.Parameters.AddWithValue("tri_isenti", objTribut.tri_isenti);
                comand.Parameters.AddWithValue("tri_outrai", objTribut.tri_outrai);
                comand.Parameters.AddWithValue("tri_pis", objTribut.tri_pis);
                comand.Parameters.AddWithValue("tri_cofins", objTribut.tri_cofins);
                comand.Parameters.AddWithValue("tri_issqn", objTribut.tri_issqn);
                comand.Parameters.AddWithValue("tri_ir", objTribut.tri_ir);
                comand.Parameters.AddWithValue("tri_csoc", objTribut.tri_csoc);
                comand.Parameters.AddWithValue("tri_pcissq", objTribut.tri_pcissq);
                comand.Parameters.AddWithValue("tri_isimp", objTribut.tri_isimp);
                comand.Parameters.AddWithValue("tri_irr", objTribut.tri_irr);
                comand.Parameters.AddWithValue("tri_custo", objTribut.tri_custo);
                comand.Parameters.AddWithValue("tri_histn", objTribut.tri_histn);
                comand.Parameters.AddWithValue("tri_hiscn", objTribut.tri_hiscn);
                comand.Parameters.AddWithValue("tri_basest", objTribut.tri_basest);
                comand.Parameters.AddWithValue("tri_sticms", objTribut.tri_sticms);
                comand.Parameters.AddWithValue("tri_stipi", objTribut.tri_stipi);
                comand.Parameters.AddWithValue("tri_bssimp", objTribut.tri_bssimp);
                comand.Parameters.AddWithValue("tri_icmsim", objTribut.tri_icmsim);
                comand.Parameters.AddWithValue("tri_modbc", objTribut.tri_modbc);
                comand.Parameters.AddWithValue("tri_icmecf", objTribut.tri_icmecf);
                comand.Parameters.AddWithValue("tri_perirr", objTribut.tri_perirr);
                comand.Parameters.AddWithValue("tri_rcsl", objTribut.tri_rcsl);
                comand.Parameters.AddWithValue("tri_prcsl", objTribut.tri_prcsl);
                comand.Parameters.AddWithValue("tri_rpis", objTribut.tri_rpis);
                comand.Parameters.AddWithValue("tri_prpis", objTribut.tri_prpis);
                comand.Parameters.AddWithValue("tri_rcof", objTribut.tri_rcof);
                comand.Parameters.AddWithValue("tri_prcof", objTribut.tri_prcof);

                Conn.Open();
                comand.ExecuteScalar();
                return true;
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

        public static int buscaCod(int tri_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT tri_cod FROM tribut ORDER BY tri_cod DESC LIMIT 1";

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
                        tri_cod = Convert.ToInt32(dr["tri_cod"]);
                        tri_cod = tri_cod + 1;

                        return tri_cod;
                    }
                    else
                    {
                        tri_cod = 0;
                        return tri_cod;
                    }
                }
                else
                {
                    tri_cod = 1;
                    return tri_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                tri_cod = 0;
                return tri_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Tribut> listar(string tribut, string filtroPesq, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";
            if (tribut == "")
            {
                sql = "SELECT * FROM tribut";
            }
            else
            {
                if (filtroPesq == "1")
                {
                    tribut.Replace(".", "");
                    tribut.Replace(" ", "");
                    sql = "SELECT * FROM tribut WHERE tri_cfisc='" + tribut + "'";
                }
            }

            List<CL_Tribut> objList = new List<CL_Tribut>();
            CL_Tribut obj = null;

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
                        obj = new CL_Tribut();
                        obj.tri_cod = Convert.ToInt32(dr["tri_cod"]);
                        obj.tri_nome = dr["tri_nome"].ToString().Trim();
                        obj.tri_sticms = dr["tri_sticms"].ToString().Trim();
                        obj.tri_strib = dr["tri_strib"].ToString().Trim();
                        obj.tri_base = dr["tri_base"] is DBNull ? 0 : Convert.ToDouble(dr["tri_base"]);
                        obj.tri_aliq = dr["tri_aliq"] is DBNull ? 0 : Convert.ToDouble(dr["tri_aliq"]);
                        obj.tri_isent = dr["tri_isent"] is DBNull ? 0 : Convert.ToDouble(dr["tri_isent"]);
                        obj.tri_outra = dr["tri_outra"] is DBNull ? 0 : Convert.ToDouble(dr["tri_outra"]);
                        obj.tri_cfisc = dr["tri_cfisc"].ToString().Trim();
                        obj.tri_cfouf = dr["tri_cfouf"].ToString().Trim();
                        objList.Add(obj);
                    }
                }
                dr.Close();
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