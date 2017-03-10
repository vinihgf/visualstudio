using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Unmed : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static bool cadUnmed(CL_Unmed objUnmed, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO unmedida (u_unid, u_nome, u_multip)" +
                "VALUES (@u_unid, @u_nome, @u_multip)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("u_unid", objUnmed.u_unid);
                comand.Parameters.AddWithValue("u_nome", objUnmed.u_nome);
                comand.Parameters.AddWithValue("u_multip", objUnmed.u_multip);

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

        public static bool alteraUnmed(CL_Unmed objUnmed, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE unmedida SET u_nome='" + objUnmed.u_nome + "', u_multip=" + objUnmed.u_multip +
                    " WHERE u_unid='" + objUnmed.u_unid + "'";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
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

        public static bool excUnmed(CL_Unmed objUnmed, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM unmedida WHERE u_unid='" + objUnmed.u_unid + "'";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            Conn.Open();
            try
            {
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

        public static CL_Unmed buscaUnmed(CL_Unmed objUnmed, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM unmedida WHERE u_unid='" + objUnmed.u_unid + "'";

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
                        objUnmed.u_nome = dr["u_nome"].ToString().Trim();
                        objUnmed.u_multip = Convert.ToDouble(dr["u_multip"]);
                        return objUnmed;
                    }
                    else
                    {
                        objUnmed = null;
                        return objUnmed;
                    }
                }
                else
                {
                    objUnmed = null;
                    return objUnmed;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objUnmed = null;
                return objUnmed;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Unmed> listar(List<CL_Unmed> objListUnmed, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM unmedida";
            CL_Unmed obj = null;

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
                        obj = new CL_Unmed();
                        //leio as informações dos campos e jogo para o objeto
                        obj.u_unid = dr["u_unid"].ToString().Trim();
                        obj.u_nome = dr["u_nome"].ToString().Trim();
                        obj.u_multip = Convert.ToDouble(dr["u_multip"]);
                        objListUnmed.Add(obj);
                    }
                    dr.Close();
                    return objListUnmed;
                }
                else
                {
                    objListUnmed = null;
                    return objListUnmed;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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