using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_CicloProducao : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_id FROM prod_agricola ORDER BY p_id DESC LIMIT 1";
            int p_id = 0;
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
                        p_id = Convert.ToInt32(dr["p_id"]);
                        p_id = p_id + 1;

                        return p_id;
                    }
                    else
                    {
                        p_id = 0;
                        return p_id;
                    }
                }
                else
                {
                    p_id = 1;
                    return p_id;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                p_id = 0;
                return p_id;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_CicloProducao> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM prod_agricola ORDER BY p_id";
            CL_CicloProducao objProducao = null;
            List<CL_CicloProducao> objListProducao = new List<CL_CicloProducao>();

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
                        objProducao = new CL_CicloProducao();
                        objProducao.p_id = dr["p_id"] is DBNull ? 0 : Convert.ToInt32(dr["p_id"]);
                        objProducao.p_nome = dr["p_nome"].ToString().Trim();
                        objProducao.p_situacao = dr["p_situacao"].ToString().Trim();
                        objProducao.p_inicio = dr["p_inicio"].ToString().Trim();
                        objProducao.p_fim = dr["p_fim"].ToString().Trim();
                        objProducao.p_qtdhec = Convert.ToInt32(dr["p_qtdhec"]);
                        objListProducao.Add(objProducao);
                    }
                    dr.Close();
                    return objListProducao;
                }
                else
                {
                    objListProducao = null;
                    return objListProducao;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListProducao = null;
                return objListProducao;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadProducao(CL_CicloProducao objProducao, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO prod_agricola (p_nome, p_situacao, p_id, p_qtdhec, p_inicio, p_fim) " +
                    "VALUES " +
                    "(@p_nome, @p_situacao, @p_id, @p_qtdhec, @p_inicio, @p_fim)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("p_nome", objProducao.p_nome);
                cmd.Parameters.AddWithValue("p_situacao", objProducao.p_situacao);
                cmd.Parameters.AddWithValue("p_id", objProducao.p_id);
                cmd.Parameters.AddWithValue("p_qtdhec", objProducao.p_qtdhec);
                cmd.Parameters.AddWithValue("p_inicio", objProducao.p_inicio);
                cmd.Parameters.AddWithValue("p_fim", objProducao.p_fim);

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
                    Conn.Close();
            }
        }

        public static bool alteraProducao(CL_CicloProducao objProducao, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "UPDATE prod_agricola SET p_nome=@p_nome, p_situacao=@p_situacao, p_qtdhec=@p_qtdhec, p_inicio=@p_inicio, p_fim=@p_fim " +
                    "WHERE p_id=@p_id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("p_nome", objProducao.p_nome);
                cmd.Parameters.AddWithValue("p_situacao", objProducao.p_situacao);
                cmd.Parameters.AddWithValue("p_id", objProducao.p_id);
                cmd.Parameters.AddWithValue("p_qtdhec", objProducao.p_qtdhec);
                cmd.Parameters.AddWithValue("p_inicio", objProducao.p_inicio);
                cmd.Parameters.AddWithValue("p_fim", objProducao.p_fim);

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

        public static bool excluirProducao(CL_CicloProducao objProducao, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM prod_agricola WHERE p_id=" + objProducao.p_id;
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
                {
                    Conn.Close();
                }
            }
        }

        public static CL_CicloProducao buscaProducao(int p_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM prod_agricola WHERE p_id=@p_id ORDER BY p_id";
            CL_CicloProducao objProducao = new CL_CicloProducao();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_id", p_id);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if(dr.Read())
                    {
                        //instancio objeto cliente a cada item da lista de registos
                        objProducao = new CL_CicloProducao();
                        objProducao.p_id = Convert.ToInt16(dr["p_id"]);
                        objProducao.p_nome = dr["p_nome"].ToString().Trim();
                        objProducao.p_situacao = dr["p_situacao"].ToString().Trim();
                        objProducao.p_inicio = dr["p_inicio"].ToString().Trim();
                        objProducao.p_fim = dr["p_fim"].ToString().Trim();
                        objProducao.p_qtdhec = Convert.ToInt32(dr["p_qtdhec"]);
                        dr.Close();
                        return objProducao;
                    }
                    else
                    {
                        objProducao = null;
                        return objProducao;
                    }
                }
                else
                {
                    objProducao = null;
                    return objProducao;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objProducao = null;
                return objProducao;
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