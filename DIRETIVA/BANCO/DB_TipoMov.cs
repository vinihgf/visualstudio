using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_TipoMov : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static CL_TipoMov buscaTipo(int cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM tipomovtitulo WHERE t_codigo=@t_cod";
            CL_TipoMov objTipo = new CL_TipoMov();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("t_cod", cod);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objTipo.t_codigo = dr["t_codigo"] is DBNull ? 0 : Convert.ToInt32(dr["t_codigo"]);
                        objTipo.t_descri = dr["t_descri"].ToString().Trim();
                        objTipo.t_tipo = dr["t_tipo"].ToString().Trim();
                        objTipo.t_acumul = dr["t_acumul"].ToString().Trim();
                        objTipo.t_somatot = dr["t_somatot"].ToString().Trim();
                        objTipo.t_ctacre = dr["t_ctacre"].ToString().Trim();
                        objTipo.t_ctadeb = dr["t_ctadeb"].ToString().Trim();
                        dr.Close();
                        return objTipo;
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

        public static List<CL_TipoMov> buscaTipos(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM tipomovtitulo";
            List<CL_TipoMov> objList = new List<CL_TipoMov>();

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
                        objList.Add(new CL_TipoMov()
                        {
                            t_codigo = dr["t_codigo"] is DBNull ? 0 : Convert.ToInt32(dr["t_codigo"]),
                            t_descri = dr["t_descri"].ToString().Trim(),
                            t_tipo = dr["t_tipo"].ToString().Trim(),
                            t_acumul = dr["t_acumul"].ToString().Trim(),
                            t_somatot = dr["t_somatot"].ToString().Trim(),
                            t_ctacre = dr["t_ctacre"].ToString().Trim(),
                            t_ctadeb = dr["t_ctadeb"].ToString().Trim(),
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

        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT t_codigo FROM tipomovtitulo ORDER BY t_codigo DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["t_codigo"]) + 1;
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
                {
                    Conn.Close();
                }
            }
        }

        public static bool alterar(CL_TipoMov objTipo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            try
            {
                string sql = "UPDATE tipomovtitulo SET t_descri=@t_descri, t_tipo=@t_tipo, t_acumul=@t_acumul, t_somatot=@t_somatot, "+
                    "t_ctacre=@t_ctacre, t_ctadeb=@t_ctadeb WHERE t_codigo=@t_codigo";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("t_codigo", objTipo.t_codigo);
                cmd.Parameters.AddWithValue("t_descri", objTipo.t_descri);
                cmd.Parameters.AddWithValue("t_tipo", objTipo.t_tipo);
                cmd.Parameters.AddWithValue("t_acumul", objTipo.t_acumul);
                cmd.Parameters.AddWithValue("t_somatot", objTipo.t_somatot);
                cmd.Parameters.AddWithValue("t_ctacre", objTipo.t_ctacre);
                cmd.Parameters.AddWithValue("t_ctadeb", objTipo.t_ctadeb);
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

        public static bool cadTipo(CL_TipoMov objTipo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            try
            {
                string sql = "INSERT INTO tipomovtitulo (t_codigo, t_descri, t_tipo, t_acumul, t_somatot, t_ctacre, t_ctadeb) " +
                    "VALUES " +
                    "(@t_codigo, @t_descri, @t_tipo, @t_acumul, @t_somatot, @t_ctacre, @t_ctadeb)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("t_codigo", objTipo.t_codigo);
                cmd.Parameters.AddWithValue("t_descri", objTipo.t_descri);
                cmd.Parameters.AddWithValue("t_tipo", objTipo.t_tipo);
                cmd.Parameters.AddWithValue("t_acumul", objTipo.t_acumul);
                cmd.Parameters.AddWithValue("t_somatot", objTipo.t_somatot);
                cmd.Parameters.AddWithValue("t_ctacre", objTipo.t_ctacre);
                cmd.Parameters.AddWithValue("t_ctadeb", objTipo.t_ctadeb);
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

        public static bool excluitTipo(CL_TipoMov objTipo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            try
            {
                string sql = "DELETE FROM tipomovtitulo WHERE t_codigo=@t_cod";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("t_cod", objTipo.t_codigo);
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
    }
}