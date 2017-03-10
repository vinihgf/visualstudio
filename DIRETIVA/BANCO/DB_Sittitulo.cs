using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Sittitulo : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            int s_codigo = 0;

            string sql = "SELECT s_codigo FROM sittitulo ORDER BY s_codigo DESC LIMIT 1";

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
                        s_codigo = Convert.ToInt16(dr["s_codigo"]);
                        s_codigo = s_codigo + 1;

                        return s_codigo;
                    }
                    else
                    {
                        s_codigo = 0;
                        return s_codigo;
                    }
                }
                else
                {
                    s_codigo = 1;
                    return s_codigo;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                s_codigo = 0;
                return s_codigo;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Sittitulo> listagemSimples(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT s_codigo, s_descri FROM sittitulo ORDER BY s_codigo";
            List<CL_Sittitulo> objList = new List<CL_Sittitulo>();

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
                        objList.Add(new CL_Sittitulo()
                        {
                            s_codigo = dr["s_codigo"] is DBNull ? 0 : Convert.ToInt32(dr["s_codigo"]),
                            s_codDesci = dr["s_codigo"] is DBNull ? "0" : dr["s_codigo"].ToString().Trim() + " - " + dr["s_descri"].ToString().Trim(),
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

        public static List<CL_Sittitulo> buscaSituacoes(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM sittitulo";
            List<CL_Sittitulo> objList = new List<CL_Sittitulo>();

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
                        objList.Add(new CL_Sittitulo()
                        {
                            s_codigo = dr["s_codigo"] is DBNull ? 0 : Convert.ToInt32(dr["s_codigo"]),
                            s_descri = dr["s_descri"].ToString().Trim(),
                            s_tipo = dr["s_tipo"].ToString().Trim(),
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
                objList = null;
                return objList;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool cadSit(CL_Sittitulo objSit, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            try
            {
                string sql = "INSERT INTO sittitulo (s_codigo, s_descri, s_tipo) " +
                    "VALUES " +
                    "(@s_cod, @s_descri, @s_tipo)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("s_cod", objSit.s_codigo);
                cmd.Parameters.AddWithValue("s_descri", objSit.s_descri);
                cmd.Parameters.AddWithValue("s_tipo", objSit.s_tipo);
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

        public static bool alteraSit(CL_Sittitulo objSit, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            try
            {
                string sql = "UPDATE sittitulo SET s_descri=@s_descri, s_tipo=@s_tipo WHERE s_codigo=@s_cod";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("s_cod", objSit.s_codigo);
                cmd.Parameters.AddWithValue("s_descri", objSit.s_descri);
                cmd.Parameters.AddWithValue("s_tipo", objSit.s_tipo);
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

        public static bool excluiSit(CL_Sittitulo objSit, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            try
            {
                string sql = "DELETE FROM sittitulo WHERE s_codigo=@s_cod";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("s_cod", objSit.s_codigo);
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

        public static CL_Sittitulo buscaSit(int cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM sittitulo WHERE s_codigo=@s_cod";
            CL_Sittitulo objSit = new CL_Sittitulo();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("s_cod", cod);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objSit.s_codigo = dr["s_codigo"] is DBNull ? 0 : Convert.ToInt32(dr["s_codigo"]);
                        objSit.s_descri = dr["s_descri"].ToString().Trim();
                        objSit.s_tipo = dr["s_tipo"].ToString().Trim();
                        dr.Close();
                        return objSit;
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