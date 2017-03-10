using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_ComlModelo : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCod(int m_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT m_codigo FROM coml_modelo ORDER BY m_codigo DESC LIMIT 1";

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
                        m_cod = Convert.ToInt16(dr["m_codigo"]);
                        m_cod = m_cod + 1;

                        return m_cod;
                    }
                    else
                    {
                        m_cod = 0;
                        return m_cod;
                    }
                }
                else
                {
                    m_cod = 1;
                    return m_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                m_cod = 0;
                return m_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_ComlModelo buscaModelo(CL_ComlModelo objComlModelo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM coml_modelo WHERE m_codigo=@m_codigo ORDER BY m_codigo";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("m_codigo", objComlModelo.m_codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objComlModelo.m_codigo = Convert.ToInt16(dr["m_codigo"]);
                        objComlModelo.m_nome = dr["m_nome"].ToString().Trim();
                        objComlModelo.m_infor = dr["m_infor"].ToString().Trim();
                        objComlModelo.m_marca = Convert.ToInt32(dr["m_marca"]);

                        return objComlModelo;
                    }
                    else
                    {
                        objComlModelo = null;
                        return objComlModelo;
                    }
                }
                else
                {
                    objComlModelo = null;
                    return objComlModelo;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objComlModelo = null;
                return objComlModelo;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadModelo(CL_ComlModelo objComlModelo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO coml_modelo (m_codigo, m_nome, m_infor, m_marca) VALUES (@m_codigo, @m_nome, @m_infor, @m_marca)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("m_codigo", objComlModelo.m_codigo);
                comand.Parameters.AddWithValue("m_nome", objComlModelo.m_nome);
                comand.Parameters.AddWithValue("m_infor", objComlModelo.m_infor);
                comand.Parameters.AddWithValue("m_marca", objComlModelo.m_marca);
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

        public static bool alteraModelo(CL_ComlModelo objComlModelo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE coml_modelo SET m_nome=@m_nome, m_infor=@m_infor, m_marca=@m_marca WHERE m_codigo=@m_codigo";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("m_nome", objComlModelo.m_nome);
                comand.Parameters.AddWithValue("m_infor", objComlModelo.m_infor);
                comand.Parameters.AddWithValue("m_marca", objComlModelo.m_marca);
                comand.Parameters.AddWithValue("m_codigo", objComlModelo.m_codigo);
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

        public static bool excluiModelo(CL_ComlModelo objComlModelo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM equipamento WHERE e_nmodelo=@e_nmodelo";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("e_nmodelo", objComlModelo.m_codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    string sql2 = "DELETE FROM coml_modelo WHERE m_codigo=" + objComlModelo.m_codigo;
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

        public static List<CL_ComlModelo> listar(string con, string marca)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM coml_modelo WHERE m_marca=@m_marca ORDER BY m_codigo";

            List<CL_ComlModelo> objList = new List<CL_ComlModelo>();
            CL_ComlModelo obj = null;

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("m_marca", marca);
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
                        obj = new CL_ComlModelo();
                        //leio as informações dos campos e jogo para o objeto
                        obj.m_codigo = Convert.ToInt32(dr["m_codigo"]);
                        obj.m_nome = dr["m_nome"].ToString().Trim();
                        obj.m_marca = Convert.ToInt32(dr["m_marca"]);
                        obj.m_infor = dr["m_infor"].ToString().Trim();

                        objList.Add(obj);
                    }
                    dr.Close();
                    return objList;
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