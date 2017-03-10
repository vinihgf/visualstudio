using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_ComlMarca : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCod(int m_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT m_codigo FROM coml_marca ORDER BY m_codigo DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["m_codigo"]) + 1;
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
        public static CL_ComlMarca buscaMarca(CL_ComlMarca objComlMarca, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM coml_marca WHERE m_codigo=@m_codigo ORDER BY m_codigo";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("m_codigo", objComlMarca.m_codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objComlMarca.m_codigo = Convert.ToInt16(dr["m_codigo"]);
                        objComlMarca.m_nome = dr["m_nome"].ToString().Trim();
                        return objComlMarca;
                    }
                    else
                    {
                        objComlMarca = null;
                        return objComlMarca;
                    }
                }
                else
                {
                    objComlMarca = null;
                    return objComlMarca;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objComlMarca = null;
                return objComlMarca;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static bool cadMarca(CL_ComlMarca objComlMarca, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO coml_marca (m_codigo, m_nome) VALUES (@m_codigo, @m_nome)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("m_codigo", objComlMarca.m_codigo);
                comand.Parameters.AddWithValue("m_nome", objComlMarca.m_nome);

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
        public static bool alteraMarca(CL_ComlMarca objComlMarca, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE coml_marca SET m_nome=@m_nome WHERE m_codigo=@m_codigo";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("m_nome", objComlMarca.m_nome);
                comand.Parameters.AddWithValue("m_codigo", objComlMarca.m_codigo);
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
        public static bool excluiMarca(CL_ComlMarca objComlMarca, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM coml_modelo WHERE m_marca=@m_marca";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("m_marca", objComlMarca.m_codigo);
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
                    string sql2 = "DELETE FROM coml_marca WHERE m_codigo=" + objComlMarca.m_codigo;
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
        public static List<CL_ComlMarca> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM coml_marca ORDER BY m_codigo";
            List<CL_ComlMarca> objList = new List<CL_ComlMarca>();

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
                        objList.Add(new CL_ComlMarca()
                        {
                            m_codigo = dr["m_codigo"] is DBNull ? 0 : Convert.ToInt32(dr["m_codigo"]),
                            m_nome = dr["m_nome"].ToString().Trim(),
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
    }
}