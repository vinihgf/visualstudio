using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Lavoura : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static List<CL_Lavoura> listarLavoura(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM lavoura ORDER BY l_id";

            List<CL_Lavoura> objList = new List<CL_Lavoura>();
            CL_Lavoura obj = null;

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
                        obj = new CL_Lavoura();
                        obj.l_id = Convert.ToInt32(dr["l_id"]);
                        obj.l_nome = dr["l_nome"].ToString().Trim();
                        obj.l_prop = Convert.ToInt32(dr["l_prop"]);
                        obj.l_nomeprop = dr["l_nomeprop"].ToString().Trim();
                        obj.l_areatotal = Convert.ToInt32(dr["l_areatotal"]);
                        obj.l_areainutil = Convert.ToInt32(dr["l_areainutil"]);
                        obj.l_areareserva = Convert.ToInt32(dr["l_areareserva"]);
                        obj.l_areaplantada = Convert.ToInt32(dr["l_areaplantada"]);
                        obj.l_cultura = dr["l_cultura"].ToString().Trim();
                        obj.datacad = dr["l_datacad"].ToString().Trim();
                        obj.dtavcto = dr["l_dtavcto"].ToString().Trim();
                        if (obj.datacad != "")
                            obj.l_datacad = Convert.ToDateTime(obj.datacad);
                        if (obj.dtavcto != "")
                            obj.l_dtavcto = Convert.ToDateTime(obj.dtavcto);
                        obj.l_vlrarendam = Convert.ToInt32(dr["l_vlrarendam"]);
                        obj.l_tipo = dr["l_tipo"].ToString().Trim();
                        obj.l_topografia = dr["l_topografia"].ToString().Trim();
                        obj.l_localizacao = dr["l_localizacao"].ToString().Trim();
                        obj.l_obs = dr["l_obs"].ToString().Trim();
                        obj.l_financiada = dr["l_financiada"].ToString().Trim();
                        obj.l_matricula = dr["l_matricula"].ToString().Trim();
                        obj.l_nirf = dr["l_nirf"].ToString().Trim();
                        obj.l_incra = dr["l_incra"].ToString().Trim();
                        obj.l_itr = dr["l_itr"].ToString().Trim();
                        obj.l_cidade = dr["l_cidade"].ToString().Trim();
                        obj.l_uf = dr["l_uf"].ToString().Trim();
                        obj.l_localidade = dr["l_localidade"].ToString().Trim();
                        obj.l_roteiro = dr["l_roteiro"].ToString().Trim();
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

        public static bool excluiLavoura(CL_Lavoura objLavoura, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM lavoura WHERE l_id=" + objLavoura.l_id;

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                    return false;
                else
                {
                    string sql2 = "DELETE FROM lavoura WHERE l_id=" + objLavoura.l_id;
                    NpgsqlCommand comand2 = new NpgsqlCommand(sql2, Conn);
                    comand2.ExecuteScalar();
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

        public static bool alteraLavoura(CL_Lavoura objLavoura, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE lavoura SET l_nome=@l_nome ,l_prop=@l_prop, l_nomeprop=@l_nomeprop, l_areatotal=@l_areatotal, l_areainutil=@l_areainutil, "+
                             "l_areareserva=@l_areareserva, l_areaplantada=@l_areaplantada, l_cultura=@l_cultura, l_dtavcto=@l_dtavcto, l_vlrarendam=@l_vlrarendam, "+
                             "l_tipo=@l_tipo, l_topografia=@l_topografia, l_localizacao=@l_localizacao, l_obs=@l_obs, l_financiada=@l_financiada, l_matricula=@l_matricula, "+
                             "l_nirf=@l_nirf, l_incra=@l_incra, l_itr=@l_itr, l_cidade=@l_cidade, l_uf=@l_uf, l_localidade=@l_localidade, l_roteiro=@l_roteiro WHERE l_id=@l_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("l_id", objLavoura.l_id);
                comand.Parameters.AddWithValue("l_nome", objLavoura.l_nome);
                comand.Parameters.AddWithValue("l_prop", objLavoura.l_prop);
                comand.Parameters.AddWithValue("l_nomeprop", objLavoura.l_nomeprop);
                comand.Parameters.AddWithValue("l_areatotal", objLavoura.l_areatotal);
                comand.Parameters.AddWithValue("l_areainutil", objLavoura.l_areainutil);
                comand.Parameters.AddWithValue("l_areareserva", objLavoura.l_areareserva);
                comand.Parameters.AddWithValue("l_areaplantada", objLavoura.l_areaplantada);
                comand.Parameters.AddWithValue("l_cultura", objLavoura.l_cultura);
                comand.Parameters.AddWithValue("l_datacad", objLavoura.l_datacad);
                comand.Parameters.AddWithValue("l_dtavcto", objLavoura.l_dtavcto);
                comand.Parameters.AddWithValue("l_vlrarendam", objLavoura.l_vlrarendam);
                comand.Parameters.AddWithValue("l_tipo", objLavoura.l_tipo);
                comand.Parameters.AddWithValue("l_topografia", objLavoura.l_topografia);
                comand.Parameters.AddWithValue("l_localizacao", objLavoura.l_localizacao);
                comand.Parameters.AddWithValue("l_obs", objLavoura.l_obs);
                comand.Parameters.AddWithValue("l_financiada", objLavoura.l_financiada);
                comand.Parameters.AddWithValue("l_matricula", objLavoura.l_matricula);
                comand.Parameters.AddWithValue("l_nirf", objLavoura.l_nirf);
                comand.Parameters.AddWithValue("l_incra", objLavoura.l_incra);
                comand.Parameters.AddWithValue("l_itr", objLavoura.l_itr);
                comand.Parameters.AddWithValue("l_cidade", objLavoura.l_cidade);
                comand.Parameters.AddWithValue("l_uf", objLavoura.l_uf);
                comand.Parameters.AddWithValue("l_localidade", objLavoura.l_localidade);
                comand.Parameters.AddWithValue("l_roteiro", objLavoura.l_roteiro);
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

        public static bool cadLavoura(CL_Lavoura objLavoura, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO lavoura (l_id, l_nome, l_prop, l_nomeprop, l_areatotal, l_areainutil, l_areareserva, l_areaplantada," +
                "l_cultura, l_datacad, l_dtavcto, l_vlrarendam, l_tipo, l_topografia, l_localizacao, l_obs, l_financiada, l_matricula," +
                "l_nirf, l_incra, l_itr, l_cidade, l_uf, l_localidade, l_roteiro) "+
                "VALUES "+
                "(@l_id, @l_nome, @l_prop, @l_nomeprop, @l_areatotal, @l_areainutil, @l_areareserva, @l_areaplantada, @l_cultura, @l_datacad, @l_dtavcto, "+
                "@l_vlrarendam, @l_tipo, @l_topografia, @l_localizacao, @l_obs,@l_financiada, @l_matricula, @l_nirf, @l_incra, @l_itr, @l_cidade, "+
                "@l_uf, @l_localidade, @l_roteiro)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("l_id", objLavoura.l_id);
                comand.Parameters.AddWithValue("l_nome", objLavoura.l_nome);
                comand.Parameters.AddWithValue("l_prop", objLavoura.l_prop);
                comand.Parameters.AddWithValue("l_nomeprop", objLavoura.l_nomeprop);
                comand.Parameters.AddWithValue("l_areatotal", objLavoura.l_areatotal);
                comand.Parameters.AddWithValue("l_areainutil", objLavoura.l_areainutil);
                comand.Parameters.AddWithValue("l_areareserva", objLavoura.l_areareserva);
                comand.Parameters.AddWithValue("l_areaplantada", objLavoura.l_areaplantada);
                comand.Parameters.AddWithValue("l_cultura", objLavoura.l_cultura);
                comand.Parameters.AddWithValue("l_datacad", objLavoura.l_datacad);
                comand.Parameters.AddWithValue("l_dtavcto", objLavoura.l_dtavcto);
                comand.Parameters.AddWithValue("l_vlrarendam", objLavoura.l_vlrarendam);
                comand.Parameters.AddWithValue("l_tipo", objLavoura.l_tipo);
                comand.Parameters.AddWithValue("l_topografia", objLavoura.l_topografia);
                comand.Parameters.AddWithValue("l_localizacao", objLavoura.l_localizacao);
                comand.Parameters.AddWithValue("l_obs", objLavoura.l_obs);
                comand.Parameters.AddWithValue("l_financiada", objLavoura.l_financiada);
                comand.Parameters.AddWithValue("l_matricula", objLavoura.l_matricula);
                comand.Parameters.AddWithValue("l_nirf", objLavoura.l_nirf);
                comand.Parameters.AddWithValue("l_incra", objLavoura.l_incra);
                comand.Parameters.AddWithValue("l_itr", objLavoura.l_itr);
                comand.Parameters.AddWithValue("l_cidade", objLavoura.l_cidade);
                comand.Parameters.AddWithValue("l_uf", objLavoura.l_uf);
                comand.Parameters.AddWithValue("l_localidade", objLavoura.l_localidade);
                comand.Parameters.AddWithValue("l_roteiro", objLavoura.l_roteiro);

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

        public static CL_Lavoura buscaLavoura(CL_Lavoura objLavoura, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM lavoura WHERE l_id=" + objLavoura.l_id + " ORDER BY l_id";

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
                        objLavoura.l_nome = dr["l_nome"].ToString().Trim();
                        objLavoura.l_prop = Convert.ToInt32(dr["l_prop"]);
                        objLavoura.l_nomeprop = dr["l_nomeprop"].ToString().Trim();
                        objLavoura.l_areatotal = Convert.ToDouble(dr["l_areatotal"]);
                        objLavoura.l_areainutil = Convert.ToDouble(dr["l_areainutil"]);
                        objLavoura.l_areareserva = Convert.ToDouble(dr["l_areareserva"]);
                        objLavoura.l_areaplantada = Convert.ToDouble(dr["l_areaplantada"]);
                        objLavoura.l_cultura = dr["l_cultura"].ToString().Trim();
                        objLavoura.datacad = dr["l_datacad"].ToString().Trim();
                        objLavoura.dtavcto = dr["l_dtavcto"].ToString().Trim();
                        if (objLavoura.datacad != "")
                            objLavoura.l_datacad = Convert.ToDateTime(dr["l_datacad"]);
                        if (objLavoura.dtavcto != "")
                            objLavoura.l_dtavcto = Convert.ToDateTime(dr["l_dtavcto"]);
                        objLavoura.l_vlrarendam = Convert.ToDouble(dr["l_vlrarendam"]);
                        objLavoura.l_tipo = dr["l_tipo"].ToString().Trim();
                        objLavoura.l_topografia = dr["l_topografia"].ToString().Trim();
                        objLavoura.l_localizacao = dr["l_localizacao"].ToString().Trim();
                        objLavoura.l_obs = dr["l_obs"].ToString().Trim();
                        objLavoura.l_foto = dr["l_foto"].ToString().Trim();
                        objLavoura.l_financiada = dr["l_financiada"].ToString().Trim();
                        objLavoura.l_matricula = dr["l_matricula"].ToString().Trim();
                        objLavoura.l_nirf = dr["l_nirf"].ToString().Trim();
                        objLavoura.l_incra = dr["l_incra"].ToString().Trim();
                        objLavoura.l_itr = dr["l_itr"].ToString().Trim();
                        objLavoura.l_cidade = dr["l_cidade"].ToString().Trim();
                        objLavoura.l_uf = dr["l_uf"].ToString().Trim();
                        objLavoura.l_localidade = dr["l_localidade"].ToString().Trim();
                        objLavoura.l_roteiro = dr["l_roteiro"].ToString().Trim();
                        return objLavoura;
                    }
                    else
                    {
                        objLavoura = null;
                        return objLavoura;
                    }
                }
                else
                {
                    objLavoura = null;
                    return objLavoura;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objLavoura = null;
                return objLavoura;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static int buscaCodigo(int l_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT l_id FROM lavoura ORDER BY l_id DESC LIMIT 1";

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
                        l_id = Convert.ToInt32(dr["l_id"]);
                        l_id = l_id + 1;

                        return l_id;
                    }
                    else
                    {
                        l_id = 0;
                        return l_id;
                    }
                }
                else
                {
                    l_id = 1;
                    return l_id;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                l_id = 0;
                return l_id;
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