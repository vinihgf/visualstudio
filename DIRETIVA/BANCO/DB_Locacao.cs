using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BANCO
{
    public class DB_Locacao : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static NpgsqlConnection Conn2 { get; set; }
        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT l_codigo FROM locacao ORDER BY l_codigo DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["l_codigo"]) + 1;
                    else
                        return 0;
                }
                else
                    return 1;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        public static string buscaContr(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT l_contr FROM locacao ORDER BY l_contr DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return (Convert.ToInt32(dr["l_contr"]) + 1).ToString();
                    else
                        return "0";
                }
                else
                    return "1";

            }
            catch (Exception ex)
            {
                ex.ToString();
                return "0";
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        public static bool incluiLocacao(CL_Locacao objLocacao, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO locacao (l_codigo, l_clicod, l_clinome, l_contr, l_emis, l_dev, l_patrimon, l_descri, l_nmarca, l_nmod, l_valor, l_vend, l_comis, l_vlcomis, l_tempo, l_dmy, l_situac)" +
                "VALUES (@l_codigo, @l_clicod, @l_clinome, @l_contr, @l_emis, @l_dev, @l_patrimon, @l_descri, @l_nmarca, @l_nmod, @l_valor, @l_vend, @l_comis, @l_vlcomis, @l_tempo, @l_dmy, @l_situac)";
                Conn.Open();

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("l_codigo", objLocacao.l_cod);
                comand.Parameters.AddWithValue("l_clicod", objLocacao.l_clicod);
                comand.Parameters.AddWithValue("l_clinome", objLocacao.l_clinome);
                comand.Parameters.AddWithValue("l_contr", objLocacao.l_contr);
                comand.Parameters.AddWithValue("l_emis", objLocacao.l_emis.ToShortDateString());
                comand.Parameters.AddWithValue("l_dev", objLocacao.l_dev.ToShortDateString());
                comand.Parameters.AddWithValue("l_patrimon", objLocacao.l_equip.e_nPatrimon);
                comand.Parameters.AddWithValue("l_descri", objLocacao.l_equip.e_descri);
                comand.Parameters.AddWithValue("l_nmarca", objLocacao.l_equip.e_nmarca);
                comand.Parameters.AddWithValue("l_nmod", objLocacao.l_equip.e_nmodelo);
                comand.Parameters.AddWithValue("l_valor", objLocacao.l_valor);
                comand.Parameters.AddWithValue("l_vend", objLocacao.l_codVend);
                comand.Parameters.AddWithValue("l_comis", objLocacao.l_comis);
                comand.Parameters.AddWithValue("l_vlcomis", objLocacao.l_vlComis);
                comand.Parameters.AddWithValue("l_tempo", objLocacao.l_tempo);
                comand.Parameters.AddWithValue("l_dmy", objLocacao.l_dmy);
                comand.Parameters.AddWithValue("l_situac", "D");
                comand.ExecuteScalar();

                string sql2 = "UPDATE equipamento SET e_ncontrato=" + objLocacao.l_contr + " WHERE e_patrimon='" + objLocacao.l_equip.e_nPatrimon + "'";
                NpgsqlCommand comand2 = new NpgsqlCommand(sql2, Conn);
                comand2.ExecuteScalar();
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
        public static List<CL_Locacao> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM locacao, equipamento WHERE l_patrimon = e_patrimon ORDER BY l_codigo";

            List<CL_Locacao> objList = new List<CL_Locacao>();
            CL_Locacao obj = null;

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
                        obj = new CL_Locacao();
                        obj.l_clicod = Convert.ToInt32(dr["l_clicod"]);
                        obj.l_clinome = dr["l_clinome"].ToString().Trim();
                        obj.l_cod = Convert.ToInt32(dr["l_codigo"]);
                        obj.l_codVend = Convert.ToInt32(dr["l_vend"]);
                        obj.l_comis = Convert.ToDouble(dr["l_comis"]);
                        obj.l_contr = Convert.ToInt32(dr["l_contr"]);
                        obj.l_dev = Convert.ToDateTime(dr["l_dev"]);
                        obj.l_emis = Convert.ToDateTime(dr["l_emis"]);
                        obj.l_valor = Convert.ToDouble(dr["l_valor"]);
                        obj.l_vlComis = Convert.ToDouble(dr["l_vlcomis"]);
                        obj.l_tempo = Convert.ToInt32(dr["l_tempo"]);
                        obj.l_dmy = dr["l_dmy"].ToString().Trim();
                        obj.l_equip.e_nmarca = Convert.ToInt32(dr["l_nmarca"]);
                        obj.l_equip.e_nmodelo = Convert.ToInt32(dr["l_nmod"]);
                        obj.l_equip.e_nPatrimon = dr["l_patrimon"].ToString().Trim();
                        obj.l_equip.e_descri = dr["l_descri"].ToString().Trim();
                        obj.l_equip.e_nserie = dr["e_patrimon"].ToString().Trim();
                        obj.l_situac = dr["l_situac"].ToString().Trim();
                        objList.Add(obj);
                    }
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
        public static List<CL_Locacao> buscaLocacao(int l_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM locacao WHERE l_codigo =" + l_cod + " ORDER BY l_codigo";

            List<CL_Locacao> objList = new List<CL_Locacao>();
            CL_Locacao obj = null;

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
                        obj = new CL_Locacao();
                        obj.l_clicod = Convert.ToInt32(dr["l_clicod"]);
                        obj.l_clinome = dr["l_clinome"].ToString().Trim();
                        obj.l_cod = Convert.ToInt32(dr["l_codigo"]);
                        obj.l_codVend = Convert.ToInt32(dr["l_vend"]);
                        obj.l_comis = Convert.ToDouble(dr["l_comis"]);
                        obj.l_contr = Convert.ToInt32(dr["l_contr"]);
                        obj.l_dev = Convert.ToDateTime(dr["l_dev"]);
                        obj.l_emis = Convert.ToDateTime(dr["l_emis"]);
                        obj.l_valor = Convert.ToDouble(dr["l_valor"]);
                        obj.l_vlComis = Convert.ToDouble(dr["l_vlcomis"]);
                        obj.l_tempo = Convert.ToInt32(dr["l_tempo"]);
                        obj.l_dmy = dr["l_dmy"].ToString().Trim();
                        obj.l_equip.e_nmarca = Convert.ToInt32(dr["l_nmarca"]);
                        obj.l_equip.e_nmodelo = Convert.ToInt32(dr["l_nmod"]);
                        obj.l_equip.e_nPatrimon = dr["l_patrimon"].ToString().Trim();
                        obj.l_equip.e_descri = dr["l_descri"].ToString().Trim();
                        objList.Add(obj);
                    }
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
        public static bool excluiLocacao(CL_Locacao objExcluiEquip, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {

                string sql = "DELETE FROM locacao WHERE l_codigo=" + objExcluiEquip.l_cod + " AND l_patrimon='" + objExcluiEquip.l_equip.e_nPatrimon + "' AND l_contr=" + objExcluiEquip.l_contr;
                Conn.Open();

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.ExecuteScalar();

                string sql2 = "UPDATE equipamento SET e_ncontrato=0 WHERE e_patrimon='" + objExcluiEquip.l_equip.e_nPatrimon + "'";
                NpgsqlCommand comand2 = new NpgsqlCommand(sql2, Conn);

                comand2.ExecuteScalar();
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
        public static List<CL_Locacao> getRelatorio(int l_codigo, int l_contr, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn2 = new NpgsqlConnection(CONEXAO);

            List<CL_Locacao> objList = new List<CL_Locacao>();
            CL_Locacao obj = null;
            
            string sql = "SELECT l_codigo, l_clicod, l_clinome, l_contr, l_emis, l_dev, l_patrimon, l_descri, "+
                            "l_nmarca, l_nmod, l_valor, l_dmy, l_tempo, ma.m_nome as marca, mo.m_nome as modelo, e_nserie "+
                            "FROM locacao, coml_modelo mo, coml_marca ma, equipamento "+
                            "WHERE l_codigo=@l_codigo AND l_contr=@l_contr " +
                            "AND ma.m_codigo=l_nmarca "+
                            "AND mo.m_codigo=l_nmod "+
                            "AND e_patrimon=l_patrimon "+
                            "AND e_nmarca=l_nmarca "+
                            "AND e_nmodelo=l_nmod";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("l_codigo", l_codigo);
            comand.Parameters.AddWithValue("l_contr", l_contr);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        obj = new CL_Locacao();
                        obj.l_clicod = Convert.ToInt32(dr["l_clicod"]);
                        obj.l_clinome = dr["l_clinome"].ToString().Trim();
                        obj.l_cod = Convert.ToInt32(dr["l_codigo"]);
                        obj.l_contr = Convert.ToInt32(dr["l_contr"]);
                        obj.l_dev = Convert.ToDateTime(dr["l_dev"]);
                        obj.l_emis = Convert.ToDateTime(dr["l_emis"]);
                        obj.l_valor = Convert.ToDouble(dr["l_valor"]);
                        obj.l_equip.e_nmarca = Convert.ToInt32(dr["l_nmarca"]);
                        obj.l_equip.e_nmodelo = Convert.ToInt32(dr["l_nmod"]);
                        obj.l_equip.e_nPatrimon = dr["l_patrimon"].ToString().Trim();
                        obj.patrimon = obj.l_equip.e_nPatrimon;
                        obj.l_equip.e_descri = dr["l_descri"].ToString().Trim();
                        obj.descri = obj.l_equip.e_descri;
                        obj.serie = dr["e_nserie"].ToString().Trim();
                        obj.modelo = dr["modelo"].ToString().Trim();
                        obj.marca = dr["marca"].ToString().Trim();
                        objList.Add(obj);
                    }
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
        public static List<CL_Locacao> buscaLocacaoContr(int l_contr, string con)
        {
            return null;
        }
        public static bool encerraLocacao(int l_contr, string con, string l_ocorr)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn2 = new NpgsqlConnection(CONEXAO);

            try
            {
                Conn.Open();
                Conn2.Open();

                string sql = "UPDATE locacao SET l_situac='E', l_ocor='" + l_ocorr + "' WHERE l_contr=" + l_contr;
                string sql2 = "UPDATE equipamento SET e_ncontrato=0 WHERE e_ncontrato=" + l_contr;
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                NpgsqlCommand comand2 = new NpgsqlCommand(sql2, Conn2);
                comand.ExecuteScalar();
                comand2.ExecuteScalar();

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
                if (Conn2.State == ConnectionState.Open)
                {
                    Conn2.Close();
                }
            }
        }
    }
}