using Npgsql;
using System;
using System.Data;
using CLASSES;
using System.Collections.Generic;

namespace BANCO
{
    public class DB_Sinistro : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCodigo(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT s_cod FROM sinistro ORDER BY s_cod DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["s_cod"]) + 1;
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

        public static bool conferePermissaoConsulta(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_cvendas FROM usudac WHERE u_email=@u_email";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("u_email", email);
            NpgsqlDataReader dr;
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string ret = dr["u_cvendas"].ToString().Trim();
                        return ret == "S" ? true : false;
                    }
                    else
                        return false;
                }
                else
                    return false;

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

        public static bool conferePermissao(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_mcheques FROM usudac WHERE u_email=@u_email";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("u_email", email);
            NpgsqlDataReader dr;
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string ret = dr["u_mcheques"].ToString().Trim();
                        return ret == "S" ? true : false;
                    }
                    else
                        return false;
                }
                else
                    return false;

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

        public static List<CL_Sinistro> listar(string pesquisa, string filtro, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (pesquisa == "")
            {
                sql = "SELECT s_cod, s_proposta, s_comunica, s_data, s_evento, s_eventof, s_deferido, s_deferimento, p_apolice, p_cultura, p_proposta, p_area, p_talhoes, p_municip, p_nome, p_clicod " +
                        "FROM sinistro, proposta, particip " +
                        "WHERE s_proposta = p_id " +
                        "AND p_clicod = p_cod";
            }
            else
            {
                sql = "SELECT s_cod, s_proposta, s_comunica, s_data, s_evento, s_eventof, s_deferido, s_deferimento, p_apolice, p_cultura, p_proposta, p_area, p_talhoes, p_municip, p_nome, p_clicod " +
                        "FROM sinistro, proposta, particip " +
                        "WHERE s_proposta = p_id " +
                        "AND p_clicod = p_cod";
                if (filtro == "1")//NOME
                    sql += " AND p_nome LIKE '%" + pesquisa + "%'";
                else if (filtro == "2")//PROPOTA
                    sql += " AND p_proposta=@pesquisa";
                else if (filtro == "3")//APOLICE
                    sql += "AND p_apolice=@pesquisa";
            }
            List<CL_Sinistro> objList = new List<CL_Sinistro>();
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("pesquisa", pesquisa);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Sinistro()
                        {
                            s_id = Convert.ToInt32(dr["s_cod"]),
                            s_idproposta = Convert.ToInt32(dr["s_proposta"]),
                            s_comunica = Convert.ToDateTime(dr["s_comunica"]),
                            s_data = Convert.ToDateTime(dr["s_data"]),
                            s_evento = dr["s_eventof"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_evento"]),
                            s_eventofim = dr["s_eventof"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_eventof"]),
                            s_apolice = dr["p_apolice"].ToString().Trim(),
                            s_proposta = dr["p_proposta"].ToString().Trim(),
                            s_cultura = dr["p_cultura"].ToString().Trim(),
                            s_area = Convert.ToDouble(dr["p_area"]),
                            s_talhoes = Convert.ToInt32(dr["p_talhoes"]),
                            s_municip = dr["p_municip"].ToString().Trim(),
                            s_clinome = dr["p_clicod"].ToString().Trim() + " - " + dr["p_nome"].ToString().Trim(),
                            s_deferido = dr["s_deferido"].ToString().Trim(),
                            s_deferimento = dr["s_deferimento"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objList;
                }
                else
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

        public static List<CL_Sinistro> listaPendencias(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT s_cod, s_proposta, s_comunica, s_data, s_numsinistro, s_evento, s_eventof, s_deferido, s_deferimento, " +
                        "p_clicod, p_nome, p_apolice, p_proposta, p_cultura, p_area, p_talhoes, p_municip "+
                        "FROM sinistro, proposta, particip "+
                        "WHERE s_proposta = p_id "+
                        "AND p_clicod = p_cod "+
                        "AND current_date <= (s_comunica + INTERVAL '120 DAYS') "+
                        "AND (s_deferimento IS NULL OR s_deferimento='') "+
                        "ORDER BY s_cod";
            List<CL_Sinistro> objList = new List<CL_Sinistro>();
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
                        objList.Add(new CL_Sinistro()
                        {
                            s_id = Convert.ToInt32(dr["s_cod"]),
                            s_idproposta = Convert.ToInt32(dr["s_proposta"]),
                            s_comunica = Convert.ToDateTime(dr["s_comunica"]),
                            s_data = Convert.ToDateTime(dr["s_data"]),
                            s_evento = dr["s_evento"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_evento"]),
                            s_eventofim = dr["s_eventof"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_eventof"]),
                            s_numsinistro = dr["s_numsinistro"].ToString().Trim(),
                            s_clinome = dr["p_clicod"].ToString().Trim() + " - " + dr["p_nome"].ToString().Trim(),
                            s_apolice = dr["p_apolice"].ToString().Trim(),
                            s_proposta = dr["p_proposta"].ToString().Trim(),
                            s_area = Convert.ToDouble(dr["p_area"]),
                            s_talhoes = Convert.ToInt32(dr["p_talhoes"]),
                            s_municip = dr["p_municip"].ToString().Trim(),
                            s_deferido = dr["s_deferido"].ToString().Trim(),
                            s_deferimento = dr["s_deferimento"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objList;
                }
                else
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

        public static bool excluiSinistro(CL_Sinistro objSinistro, string atualizaPerda, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "DELETE FROM sinistro WHERE s_cod=@s_cod";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("s_cod", objSinistro.s_id);
                cmd.ExecuteScalar();
                cmd = new NpgsqlCommand(atualizaPerda, Conn, transaction);
                cmd.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool alteraSinistro(CL_Sinistro objSinistro, string atualizaPerda, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "UPDATE sinistro SET s_proposta=@s_proposta, s_chuva=@s_chuva, s_comunica=@s_comunica, s_data=@s_data, s_filtro=@s_filtro, s_geada=@s_geada, " +
                    "s_granizo=@s_granizo, s_numsinistro=@s_numsinistro, s_raios=@s_raios, s_seca=@s_seca, s_resumo=@s_resumo, s_tromba=@s_tromba, s_variacao=@s_variacao, " +
                    "s_vforte=@s_vforte, s_vfrio=@s_vfrio, s_evento=@s_evento, s_eventof=@s_eventof, s_deferido=@s_deferido, s_deferimento=@s_deferimento WHERE s_cod=@s_cod";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("s_cod", objSinistro.s_id);
                cmd.Parameters.AddWithValue("s_proposta", objSinistro.s_proposta);
                cmd.Parameters.AddWithValue("s_chuva", objSinistro.s_chuva);
                cmd.Parameters.AddWithValue("s_comunica", objSinistro.s_comunica);
                cmd.Parameters.AddWithValue("s_data", objSinistro.s_data.ToShortDateString());
                cmd.Parameters.AddWithValue("s_filtro", objSinistro.s_filtro);
                cmd.Parameters.AddWithValue("s_geada", objSinistro.s_geada);
                cmd.Parameters.AddWithValue("s_granizo", objSinistro.s_granizo);
                cmd.Parameters.AddWithValue("s_numsinistro", objSinistro.s_numsinistro);
                cmd.Parameters.AddWithValue("s_raios", objSinistro.s_raios);
                cmd.Parameters.AddWithValue("s_seca", objSinistro.s_seca);
                cmd.Parameters.AddWithValue("s_resumo", objSinistro.s_resumo);
                cmd.Parameters.AddWithValue("s_tromba", objSinistro.s_tromba);
                cmd.Parameters.AddWithValue("s_variacao", objSinistro.s_variacao);
                cmd.Parameters.AddWithValue("s_vforte", objSinistro.s_vforte);
                cmd.Parameters.AddWithValue("s_vfrio", objSinistro.s_vfrio);
                cmd.Parameters.AddWithValue("s_evento", objSinistro.s_evento);
                cmd.Parameters.AddWithValue("s_eventof", objSinistro.s_eventofim);
                cmd.Parameters.AddWithValue("s_deferido", objSinistro.s_deferido);
                cmd.Parameters.AddWithValue("s_deferimento", objSinistro.s_deferimento);
                cmd.ExecuteScalar();
                cmd = new NpgsqlCommand(atualizaPerda, Conn, transaction);
                cmd.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool cadastraSinistro(CL_Sinistro objSinistro, string atualizaPerda, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "INSERT INTO sinistro (s_proposta, s_chuva, s_comunica, s_data, s_filtro, s_geada, s_granizo, s_numsinistro, s_raios, s_seca, s_resumo, s_tromba, s_variacao, s_vforte, s_vfrio, s_evento, s_eventof, s_deferido, s_deferimento) " +
                    "VALUES " +
                    "(@s_proposta, @s_chuva, @s_comunica, @s_data, @s_filtro, @s_geada, @s_granizo, @s_numsinistro, @s_raios, @s_seca, @s_resumo, @s_tromba, @s_variacao, @s_vforte, @s_vfrio, @s_evento, @s_eventof, @s_deferido, @s_deferimento)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("s_proposta", objSinistro.s_proposta);
                cmd.Parameters.AddWithValue("s_chuva", objSinistro.s_chuva);
                cmd.Parameters.AddWithValue("s_comunica", objSinistro.s_comunica);
                cmd.Parameters.AddWithValue("s_data", objSinistro.s_data.ToShortDateString());
                cmd.Parameters.AddWithValue("s_filtro", objSinistro.s_filtro);
                cmd.Parameters.AddWithValue("s_geada", objSinistro.s_geada);
                cmd.Parameters.AddWithValue("s_granizo", objSinistro.s_granizo);
                cmd.Parameters.AddWithValue("s_numsinistro", objSinistro.s_numsinistro);
                cmd.Parameters.AddWithValue("s_raios", objSinistro.s_raios);
                cmd.Parameters.AddWithValue("s_seca", objSinistro.s_seca);
                cmd.Parameters.AddWithValue("s_resumo", objSinistro.s_resumo);
                cmd.Parameters.AddWithValue("s_tromba", objSinistro.s_tromba);
                cmd.Parameters.AddWithValue("s_variacao", objSinistro.s_variacao);
                cmd.Parameters.AddWithValue("s_vforte", objSinistro.s_vforte);
                cmd.Parameters.AddWithValue("s_vfrio", objSinistro.s_vfrio);
                cmd.Parameters.AddWithValue("s_evento", objSinistro.s_evento);
                cmd.Parameters.AddWithValue("s_eventof", objSinistro.s_eventofim);
                cmd.Parameters.AddWithValue("s_deferido", objSinistro.s_deferido);
                cmd.Parameters.AddWithValue("s_deferimento", objSinistro.s_deferimento);
                cmd.ExecuteScalar();
                cmd = new NpgsqlCommand(atualizaPerda, Conn, transaction);
                cmd.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static CL_Sinistro buscaSinistro(int codigo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM sinistro WHERE s_cod=@s_cod";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("s_cod", codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        CL_Sinistro objSinistro = new CL_Sinistro();
                        objSinistro.s_id = codigo;
                        objSinistro.s_proposta = dr["s_proposta"].ToString().Trim();
                        objSinistro.s_chuva = dr["s_chuva"].ToString().Trim();
                        objSinistro.s_geada = dr["s_geada"].ToString().Trim();
                        objSinistro.s_granizo = dr["s_granizo"].ToString().Trim();
                        objSinistro.s_raios = dr["s_raios"].ToString().Trim();
                        objSinistro.s_seca = dr["s_seca"].ToString().Trim();
                        objSinistro.s_resumo = dr["s_resumo"].ToString().Trim();
                        objSinistro.s_tromba = dr["s_tromba"].ToString().Trim();
                        objSinistro.s_variacao = dr["s_variacao"].ToString().Trim();
                        objSinistro.s_vforte = dr["s_vforte"].ToString().Trim();
                        objSinistro.s_vfrio = dr["s_vfrio"].ToString().Trim();
                        objSinistro.s_filtro = dr["s_filtro"].ToString().Trim();
                        objSinistro.s_numsinistro = dr["s_numsinistro"].ToString().Trim();
                        objSinistro.s_comunica = Convert.ToDateTime(dr["s_comunica"]);
                        objSinistro.s_data = Convert.ToDateTime(dr["s_data"]);
                        objSinistro.s_evento = Convert.ToDateTime(dr["s_evento"]);
                        objSinistro.s_eventofim = dr["s_eventof"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_eventof"]);
                        objSinistro.s_deferido = dr["s_deferido"].ToString().Trim();
                        objSinistro.s_deferimento = dr["s_deferimento"].ToString().Trim();
                        return objSinistro;
                    }
                    else
                        return null;
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