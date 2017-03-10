using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Login : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        //CONEXAO DE FORA
        //public static string connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
        //ANTIGO
        //public static string connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141";

        //CONEXAO LOCAL
        public static string connString = "Server=192.168.15.100;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
        public static string validaLoginFlavio(CL_Login objLogin)
        {
            try
            {
                string sql = "SELECT * FROM usudac WHERE u_email=@email AND u_senha=@senha";
                string acesso = "";
                string con = "";
                string connFlavio = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141";
                Conn = new NpgsqlConnection(connFlavio);
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("email", objLogin.u_login);
                comand.Parameters.AddWithValue("senha", objLogin.u_senha);
                NpgsqlDataReader dr;
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        acesso = dr["u_tipoaces"] is DBNull ? "" : dr["u_tipoaces"].ToString().Trim();
                    }
                    if (acesso != "")
                    {
                        con = dr["u_host"].ToString().Trim();
                        con += "*" + dr["u_porta"].ToString().Trim();
                        con += "*" + dr["u_user"].ToString().Trim();
                        con += "*" + dr["u_senhadb"].ToString().Trim();
                        con += "*" + dr["u_bd"].ToString().Trim();
                        return con;
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "";
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static string validaLogin(CL_Login objLogin)
        {
            string sql = "SELECT * FROM usudac WHERE u_email=@email AND u_senha=@senha";
            string acesso = "";
            string con = "";

            try
            {
                Conn = new NpgsqlConnection(connString);
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("email", objLogin.u_login);
                comand.Parameters.AddWithValue("senha", objLogin.u_senha);
                NpgsqlDataReader dr;
                if(Conn.State == ConnectionState.Closed)
                    Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        acesso = dr["u_tipoaces"] is DBNull ? "" : dr["u_tipoaces"].ToString().Trim();
                    }
                    if (acesso != "")
                    {
                        con = dr["u_host"].ToString().Trim();
                        con += "*" + dr["u_porta"].ToString().Trim();
                        con += "*" + dr["u_user"].ToString().Trim();
                        con += "*" + dr["u_senhadb"].ToString().Trim();
                        con += "*" + dr["u_bd"].ToString().Trim();
                        return con;
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "";
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static CL_Login buscaSenha(CL_Login objLogin)
        {
            Conn = new NpgsqlConnection(connString);

            string sql = "SELECT u_senha FROM usudac WHERE u_email = '" + objLogin.u_login + "'";
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
                        objLogin.u_senha = dr["u_senha"].ToString().Trim();
                        return objLogin;
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