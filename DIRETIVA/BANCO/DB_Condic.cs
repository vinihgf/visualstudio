using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Condic : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Condic buscaCondic(CL_Condic objCondic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM condic WHERE c_cod=" + objCondic.c_cod;

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
                        objCondic.c_cod = Convert.ToInt16(dr["c_cod"]);
                        objCondic.c_nome = dr["c_nome"].ToString().Trim();
                        objCondic.c_tab = Convert.ToInt32(dr["c_tab"]);
                        objCondic.c_cond1 = Convert.ToInt32(dr["c_cond1"]);
                        objCondic.c_cond2 = Convert.ToInt32(dr["c_cond2"]);
                        objCondic.c_cond3 = Convert.ToInt32(dr["c_cond3"]);
                        objCondic.c_cond4 = Convert.ToInt32(dr["c_cond4"]);
                        objCondic.c_cond5 = Convert.ToInt32(dr["c_cond5"]);
                        objCondic.c_cond6 = Convert.ToInt32(dr["c_cond6"]);
                        objCondic.c_cond7 = Convert.ToInt32(dr["c_cond7"]);
                        objCondic.c_cond8 = Convert.ToInt32(dr["c_cond8"]);
                        objCondic.c_cond9 = Convert.ToInt32(dr["c_cond9"]);
                        objCondic.c_cond10 = Convert.ToInt32(dr["c_cond10"]);
                        objCondic.c_cond11 = Convert.ToInt32(dr["c_cond11"]);
                        objCondic.c_cond12 = Convert.ToInt32(dr["c_cond12"]);
                        objCondic.c_cond13 = Convert.ToInt32(dr["c_cond13"]);
                        objCondic.c_cond14 = Convert.ToInt32(dr["c_cond14"]);
                        objCondic.c_cond15 = Convert.ToInt32(dr["c_cond15"]);
                        objCondic.c_cond16 = Convert.ToInt32(dr["c_cond16"]);
                        objCondic.c_cond17 = Convert.ToInt32(dr["c_cond17"]);
                        objCondic.c_cond18 = Convert.ToInt32(dr["c_cond18"]);
                        objCondic.c_cond19 = Convert.ToInt32(dr["c_cond19"]);
                        objCondic.c_cond20 = Convert.ToInt32(dr["c_cond20"]);
                        objCondic.c_cond21 = Convert.ToInt32(dr["c_cond21"]);
                        objCondic.c_cond22 = Convert.ToInt32(dr["c_cond22"]);
                        objCondic.c_cond23 = Convert.ToInt32(dr["c_cond23"]);
                        objCondic.c_cond24 = Convert.ToInt32(dr["c_cond24"]);
                        objCondic.c_dupl = dr["c_dupl"].ToString().Trim();
                        objCondic.c_movest = dr["c_movest"].ToString().Trim();
                        objCondic.c_tplcli = dr["c_tplcli"].ToString().Trim();
                        objCondic.c_taxa = Convert.ToDouble(dr["c_taxa"]);
                        objCondic.c_cfisc = dr["c_cfisc"].ToString().Trim();
                        objCondic.c_cfiscuf = dr["c_cfiscuf"].ToString().Trim();
                        objCondic.c_complem = dr["c_complem"].ToString().Trim();
                        objCondic.c_venda = dr["c_venda"].ToString().Trim();
                        objCondic.c_tribut = Convert.ToInt32(dr["c_tribut"]);
                        objCondic.c_comis = Convert.ToInt32(dr["c_comis"]);
                        objCondic.c_acumul = Convert.ToInt32(dr["c_acumul"]);
                        objCondic.c_codimp = Convert.ToInt32(dr["c_codimp"]);
                        objCondic.c_modxml = dr["c_modxml"].ToString().Trim();
                        objCondic.c_notaref = dr["c_notaref"].ToString().Trim();
                        objCondic.c_indust = dr["c_indust"].ToString().Trim();
                        objCondic.c_origem = dr["c_origem"].ToString().Trim();
                        objCondic.c_usacfop = dr["c_usacfop"].ToString().Trim();
                        objCondic.c_impctb = dr["c_impctb"].ToString().Trim();
                        objCondic.c_forpgto = dr["c_forpgto"].ToString().Trim();
                        objCondic.c_usada = dr["c_usada"].ToString().Trim();
                        objCondic.c_vlrvda = Convert.ToDouble(dr["c_vlrvda"]);
                        objCondic.c_ctadeb = dr["c_ctadeb"].ToString().Trim();
                        objCondic.c_ctacre = dr["c_ctacre"].ToString().Trim();
                        objCondic.c_boleto = dr["c_boleto"].ToString().Trim();

                        return objCondic;
                    }
                    else
                    {
                        objCondic = null;
                        return objCondic;
                    }
                }
                else
                {
                    objCondic = null;
                    return objCondic;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objCondic = null;
                return objCondic;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static bool excluirCondic(CL_Condic objCondic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM condic WHERE c_cod=" + objCondic.c_cod;
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            try
            {
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
                    Conn.Close();
            }
        }
        public static bool alterarCondic(CL_Condic objCondic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "UPDATE condic SET c_cod=@c_cod, c_nome=@c_nome, c_forpgto=@c_forpgto, c_movest=@c_movest, c_tplcli=@c_tplcli, c_venda=@c_venda, c_complem=@c_complem, c_impctb=@c_impctb, c_tribut=@c_tribut , c_acumul=@c_acumul, c_codimp=@c_codimp, c_modxml=@c_modxml, " +
                    "c_notaref=@c_notaref, c_tab=@c_tab, c_taxa=@c_taxa, c_comis=@c_comis, c_usacfop=@c_usacfop, c_cfisc=@c_cfisc, c_cfiscuf=@c_cfiscuf, c_ctadeb=@c_ctadeb, c_ctacre=@c_ctacre, c_indust=@c_indust, c_dupl=@c_dupl, c_boleto=@c_boleto, c_dtafixa=@c_dtafixa, c_cond1=@c_cond1, c_cond2=@c_cond2, c_cond3=@c_cond3, " +
                    "c_cond4=@c_cond4, c_cond5=@c_cond5, c_cond6=@c_cond6, c_cond7=@c_cond7, c_cond8=@c_cond8, c_cond9=@c_cond9, c_cond10=@c_cond10, c_cond11=@c_cond11, c_cond12=@c_cond12, c_cond13=@c_cond13, c_cond14=@c_cond14, c_cond15=@c_cond15, c_cond16=@c_cond16, c_cond17=@c_cond17, c_cond18=@c_cond18, " +
                    "c_cond19=@c_cond19, c_cond20=@c_cond20, c_cond21=@c_cond21, c_cond22=@c_cond22, c_cond23=@c_cond23, c_cond24=@c_cond24";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("c_cod", objCondic.c_cod);
                cmd.Parameters.AddWithValue("c_nome", objCondic.c_nome);
                cmd.Parameters.AddWithValue("c_forpgto", objCondic.c_forpgto);
                cmd.Parameters.AddWithValue("c_movest", objCondic.c_movest);
                cmd.Parameters.AddWithValue("c_tplcli", objCondic.c_tplcli);
                cmd.Parameters.AddWithValue("c_venda", objCondic.c_venda);
                cmd.Parameters.AddWithValue("c_complem", objCondic.c_complem);
                cmd.Parameters.AddWithValue("c_impctb", objCondic.c_impctb);
                cmd.Parameters.AddWithValue("c_tribut", objCondic.c_tribut);
                cmd.Parameters.AddWithValue("c_acumul", objCondic.c_acumul);
                cmd.Parameters.AddWithValue("c_codimp", objCondic.c_codimp);
                cmd.Parameters.AddWithValue("c_modxml", objCondic.c_modxml);
                cmd.Parameters.AddWithValue("c_notaref", objCondic.c_notaref);
                cmd.Parameters.AddWithValue("c_tab", objCondic.c_tab);
                cmd.Parameters.AddWithValue("c_taxa", objCondic.c_taxa);
                cmd.Parameters.AddWithValue("c_comis", objCondic.c_comis);
                cmd.Parameters.AddWithValue("c_usacfop", objCondic.c_usacfop);
                cmd.Parameters.AddWithValue("c_cfisc", objCondic.c_cfisc);
                cmd.Parameters.AddWithValue("c_cfiscuf", objCondic.c_cfiscuf);
                cmd.Parameters.AddWithValue("c_ctadeb", objCondic.c_ctadeb);
                cmd.Parameters.AddWithValue("c_ctacre", objCondic.c_ctacre);
                cmd.Parameters.AddWithValue("c_indust", objCondic.c_indust);
                cmd.Parameters.AddWithValue("c_dupl", objCondic.c_dupl);
                cmd.Parameters.AddWithValue("c_boleto", objCondic.c_boleto);
                cmd.Parameters.AddWithValue("c_dtafixa", objCondic.c_dtafixa);
                cmd.Parameters.AddWithValue("c_cond1", objCondic.c_cond1);
                cmd.Parameters.AddWithValue("c_cond2", objCondic.c_cond2);
                cmd.Parameters.AddWithValue("c_cond3", objCondic.c_cond3);
                cmd.Parameters.AddWithValue("c_cond4", objCondic.c_cond4);
                cmd.Parameters.AddWithValue("c_cond5", objCondic.c_cond5);
                cmd.Parameters.AddWithValue("c_cond6", objCondic.c_cond6);
                cmd.Parameters.AddWithValue("c_cond7", objCondic.c_cond7);
                cmd.Parameters.AddWithValue("c_cond8", objCondic.c_cond8);
                cmd.Parameters.AddWithValue("c_cond9", objCondic.c_cond9);
                cmd.Parameters.AddWithValue("c_cond10", objCondic.c_cond10);
                cmd.Parameters.AddWithValue("c_cond11", objCondic.c_cond11);
                cmd.Parameters.AddWithValue("c_cond12", objCondic.c_cond12);
                cmd.Parameters.AddWithValue("c_cond13", objCondic.c_cond13);
                cmd.Parameters.AddWithValue("c_cond14", objCondic.c_cond14);
                cmd.Parameters.AddWithValue("c_cond15", objCondic.c_cond15);
                cmd.Parameters.AddWithValue("c_cond16", objCondic.c_cond16);
                cmd.Parameters.AddWithValue("c_cond17", objCondic.c_cond17);
                cmd.Parameters.AddWithValue("c_cond18", objCondic.c_cond18);
                cmd.Parameters.AddWithValue("c_cond19", objCondic.c_cond19);
                cmd.Parameters.AddWithValue("c_cond20", objCondic.c_cond20);
                cmd.Parameters.AddWithValue("c_cond21", objCondic.c_cond21);
                cmd.Parameters.AddWithValue("c_cond22", objCondic.c_cond22);
                cmd.Parameters.AddWithValue("c_cond23", objCondic.c_cond23);
                cmd.Parameters.AddWithValue("c_cond24", objCondic.c_cond24);
                cmd.ExecuteScalar();
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
        public static bool cadCondic(CL_Condic objCondic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO condic (c_cod, c_nome, c_forpgto, c_movest, c_tplcli, c_venda, c_complem, c_impctb, c_tribut, c_acumul, c_codimp, c_modxml, " +
                    "c_notaref, c_tab, c_taxa, c_comis, c_usacfop, c_cfisc, c_cfiscuf, c_ctadeb, c_ctacre, c_indust, c_dupl, c_boleto, c_dtafixa, c_cond1, c_cond2, c_cond3, " +
                    "c_cond4, c_cond5, c_cond6, c_cond7, c_cond8, c_cond9, c_cond10, c_cond11, c_cond12, c_cond13, c_cond14, c_cond15, c_cond16, c_cond17, c_cond18, " +
                    "c_cond19, c_cond20, c_cond21, c_cond22, c_cond23, c_cond24) " +
                    "VALUES " +
                    "(@c_cod, @c_nome, @c_forpgto, @c_movest, @c_tplcli, @c_venda, @c_complem, @c_impctb, @c_tribut, @c_acumul, @c_codimp, @c_modxml, " +
                    "@c_notaref, @c_tab, @c_taxa, @c_comis, @c_usacfop, @c_cfisc, @c_cfiscuf, @c_ctadeb, @c_ctacre, @c_indust, @c_dupl, @c_boleto, @c_dtafixa, @c_cond1, @c_cond2, @c_cond3, " +
                    "@c_cond4, @c_cond5, @c_cond6, @c_cond7, @c_cond8, @c_cond9, @c_cond10, @c_cond11, @c_cond12, @c_cond13, @c_cond14, @c_cond15, @c_cond16, @c_cond17, @c_cond18, " +
                    "@c_cond19, @c_cond20, @c_cond21, @c_cond22, @c_cond23, @c_cond24)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("c_cod", objCondic.c_cod);
                cmd.Parameters.AddWithValue("c_nome", objCondic.c_nome);
                cmd.Parameters.AddWithValue("c_forpgto", objCondic.c_forpgto);
                cmd.Parameters.AddWithValue("c_movest", objCondic.c_movest);
                cmd.Parameters.AddWithValue("c_tplcli", objCondic.c_tplcli);
                cmd.Parameters.AddWithValue("c_venda", objCondic.c_venda);
                cmd.Parameters.AddWithValue("c_complem", objCondic.c_complem);
                cmd.Parameters.AddWithValue("c_impctb", objCondic.c_impctb);
                cmd.Parameters.AddWithValue("c_tribut", objCondic.c_tribut);
                cmd.Parameters.AddWithValue("c_acumul", objCondic.c_acumul);
                cmd.Parameters.AddWithValue("c_codimp", objCondic.c_codimp);
                cmd.Parameters.AddWithValue("c_modxml", objCondic.c_modxml);
                cmd.Parameters.AddWithValue("c_notaref", objCondic.c_notaref);
                cmd.Parameters.AddWithValue("c_tab", objCondic.c_tab);
                cmd.Parameters.AddWithValue("c_taxa", objCondic.c_taxa);
                cmd.Parameters.AddWithValue("c_comis", objCondic.c_comis);
                cmd.Parameters.AddWithValue("c_usacfop", objCondic.c_usacfop);
                cmd.Parameters.AddWithValue("c_cfisc", objCondic.c_cfisc);
                cmd.Parameters.AddWithValue("c_cfiscuf", objCondic.c_cfiscuf);
                cmd.Parameters.AddWithValue("c_ctadeb", objCondic.c_ctadeb);
                cmd.Parameters.AddWithValue("c_ctacre", objCondic.c_ctacre);
                cmd.Parameters.AddWithValue("c_indust", objCondic.c_indust);
                cmd.Parameters.AddWithValue("c_dupl", objCondic.c_dupl);
                cmd.Parameters.AddWithValue("c_boleto", objCondic.c_boleto);
                cmd.Parameters.AddWithValue("c_dtafixa", objCondic.c_dtafixa);
                cmd.Parameters.AddWithValue("c_cond1", objCondic.c_cond1);
                cmd.Parameters.AddWithValue("c_cond2", objCondic.c_cond2);
                cmd.Parameters.AddWithValue("c_cond3", objCondic.c_cond3);
                cmd.Parameters.AddWithValue("c_cond4", objCondic.c_cond4);
                cmd.Parameters.AddWithValue("c_cond5", objCondic.c_cond5);
                cmd.Parameters.AddWithValue("c_cond6", objCondic.c_cond6);
                cmd.Parameters.AddWithValue("c_cond7", objCondic.c_cond7);
                cmd.Parameters.AddWithValue("c_cond8", objCondic.c_cond8);
                cmd.Parameters.AddWithValue("c_cond9", objCondic.c_cond9);
                cmd.Parameters.AddWithValue("c_cond10", objCondic.c_cond10);
                cmd.Parameters.AddWithValue("c_cond11", objCondic.c_cond11);
                cmd.Parameters.AddWithValue("c_cond12", objCondic.c_cond12);
                cmd.Parameters.AddWithValue("c_cond13", objCondic.c_cond13);
                cmd.Parameters.AddWithValue("c_cond14", objCondic.c_cond14);
                cmd.Parameters.AddWithValue("c_cond15", objCondic.c_cond15);
                cmd.Parameters.AddWithValue("c_cond16", objCondic.c_cond16);
                cmd.Parameters.AddWithValue("c_cond17", objCondic.c_cond17);
                cmd.Parameters.AddWithValue("c_cond18", objCondic.c_cond18);
                cmd.Parameters.AddWithValue("c_cond19", objCondic.c_cond19);
                cmd.Parameters.AddWithValue("c_cond20", objCondic.c_cond20);
                cmd.Parameters.AddWithValue("c_cond21", objCondic.c_cond21);
                cmd.Parameters.AddWithValue("c_cond22", objCondic.c_cond22);
                cmd.Parameters.AddWithValue("c_cond23", objCondic.c_cond23);
                cmd.Parameters.AddWithValue("c_cond24", objCondic.c_cond24);

                cmd.ExecuteScalar();
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
        public static int buscaCod(int c_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT c_cod FROM condic ORDER BY c_cod DESC LIMIT 1";

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
                        c_cod = Convert.ToInt32(dr["c_cod"]);
                        c_cod = c_cod + 1;

                        return c_cod;
                    }
                    else
                    {
                        c_cod = 0;
                        return c_cod;
                    }
                }
                else
                {
                    c_cod = 1;
                    return c_cod;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                c_cod = 0;
                return c_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static List<CL_Condic> listar(List<CL_Condic> objListCondic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM condic ORDER BY c_cod";
            CL_Condic objCondic = null;

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
                        //instancio objeto cliente a cada item da lista de registos
                        objCondic = new CL_Condic();
                        objCondic.c_cod = Convert.ToInt16(dr["c_cod"]);
                        objCondic.c_nome = dr["c_nome"].ToString().Trim();
                        objCondic.c_tab = Convert.ToInt32(dr["c_tab"]);
                        objCondic.c_cond1 = Convert.ToInt32(dr["c_cond1"]);
                        objCondic.c_cond2 = Convert.ToInt32(dr["c_cond2"]);
                        objCondic.c_cond3 = Convert.ToInt32(dr["c_cond3"]);
                        objCondic.c_cond4 = Convert.ToInt32(dr["c_cond4"]);
                        objCondic.c_cond5 = Convert.ToInt32(dr["c_cond5"]);
                        objCondic.c_cond6 = Convert.ToInt32(dr["c_cond6"]);
                        objCondic.c_cond7 = Convert.ToInt32(dr["c_cond7"]);
                        objCondic.c_cond8 = Convert.ToInt32(dr["c_cond8"]);
                        objCondic.c_cond9 = Convert.ToInt32(dr["c_cond9"]);
                        objCondic.c_cond10 = Convert.ToInt32(dr["c_cond10"]);
                        objCondic.c_cond11 = Convert.ToInt32(dr["c_cond11"]);
                        objCondic.c_cond12 = Convert.ToInt32(dr["c_cond12"]);
                        objCondic.c_cond13 = Convert.ToInt32(dr["c_cond13"]);
                        objCondic.c_cond14 = Convert.ToInt32(dr["c_cond14"]);
                        objCondic.c_cond15 = Convert.ToInt32(dr["c_cond15"]);
                        objCondic.c_cond16 = Convert.ToInt32(dr["c_cond16"]);
                        objCondic.c_cond17 = Convert.ToInt32(dr["c_cond17"]);
                        objCondic.c_cond18 = Convert.ToInt32(dr["c_cond18"]);
                        objCondic.c_cond19 = Convert.ToInt32(dr["c_cond19"]);
                        objCondic.c_cond20 = Convert.ToInt32(dr["c_cond20"]);
                        objCondic.c_cond21 = Convert.ToInt32(dr["c_cond21"]);
                        objCondic.c_cond22 = Convert.ToInt32(dr["c_cond22"]);
                        objCondic.c_cond23 = Convert.ToInt32(dr["c_cond23"]);
                        objCondic.c_cond24 = Convert.ToInt32(dr["c_cond24"]);
                        objCondic.c_dupl = dr["c_dupl"].ToString().Trim();
                        objCondic.c_movest = dr["c_movest"].ToString().Trim();
                        objCondic.c_tplcli = dr["c_tplcli"].ToString().Trim();
                        objCondic.c_taxa = Convert.ToDouble(dr["c_taxa"]);
                        objCondic.c_cfisc = dr["c_cfisc"].ToString().Trim();
                        objCondic.c_cfiscuf = dr["c_cfiscuf"].ToString().Trim();
                        objCondic.c_complem = dr["c_complem"].ToString().Trim();
                        objCondic.c_venda = dr["c_venda"].ToString().Trim();
                        objCondic.c_tribut = Convert.ToInt32(dr["c_tribut"]);
                        objCondic.c_comis = Convert.ToInt32(dr["c_comis"]);
                        objCondic.c_acumul = Convert.ToInt32(dr["c_acumul"]);
                        objCondic.c_codimp = Convert.ToInt32(dr["c_codimp"]);
                        objCondic.c_modxml = dr["c_modxml"].ToString().Trim();
                        objCondic.c_notaref = dr["c_notaref"].ToString().Trim();
                        objCondic.c_indust = dr["c_indust"].ToString().Trim();
                        objCondic.c_origem = dr["c_origem"].ToString().Trim();
                        objCondic.c_usacfop = dr["c_usacfop"].ToString().Trim();
                        objCondic.c_impctb = dr["c_impctb"].ToString().Trim();
                        objCondic.c_forpgto = dr["c_forpgto"].ToString().Trim();
                        objCondic.c_usada = dr["c_usada"].ToString().Trim();
                        objCondic.c_vlrvda = Convert.ToDouble(dr["c_vlrvda"]);
                        objCondic.c_ctadeb = dr["c_ctadeb"].ToString().Trim();
                        objCondic.c_ctacre = dr["c_ctacre"].ToString().Trim();
                        objCondic.c_boleto = dr["c_boleto"].ToString().Trim();
                        objListCondic.Add(objCondic);
                    }
                    dr.Close();
                    return objListCondic;
                }
                else
                {
                    objListCondic = null;
                    return objListCondic;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListCondic = null;
                return objListCondic;
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