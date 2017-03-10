using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Landesp : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaID(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT l_id FROM movdesp ORDER BY l_id DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["l_id"]) + 1;
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

        public static bool excluiDesp(int l_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM movdesp WHERE l_id=@l_id";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("l_id", l_id);
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

        public static bool cadDesp(CL_Landesp objLandesp, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO movdesp (l_data, l_tipo, l_valor, l_forma, l_obs) " +
                    "VALUES " +
                    "(@l_data, @l_tipo, @l_valor, @l_forma, @l_obs)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("l_data", objLandesp.l_data.ToShortDateString());
                cmd.Parameters.AddWithValue("l_tipo", objLandesp.l_tipo);
                cmd.Parameters.AddWithValue("l_valor", objLandesp.l_valor);
                cmd.Parameters.AddWithValue("l_forma", objLandesp.l_forma);
                cmd.Parameters.AddWithValue("l_obs", objLandesp.l_obs);
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

        public static bool alteraDesp(CL_Landesp objLandesp, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "UPDATE movdesp SET l_data=@l_data, l_tipo=@l_tipo, l_valor=@l_valor, l_forma=@l_forma, l_obs=@l_obs WHERE l_id=@l_id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("l_id", objLandesp.l_id);
                cmd.Parameters.AddWithValue("l_data", objLandesp.l_data.ToShortDateString());
                cmd.Parameters.AddWithValue("l_tipo", objLandesp.l_tipo);
                cmd.Parameters.AddWithValue("l_valor", objLandesp.l_valor);
                cmd.Parameters.AddWithValue("l_forma", objLandesp.l_forma);
                cmd.Parameters.AddWithValue("l_obs", objLandesp.l_obs);
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

        public static List<CL_Landesp> pesquisa(DateTime data, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM movdesp WHERE l_data=@l_data";
            List<CL_Landesp> objList = new List<CL_Landesp>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("l_data", data.ToShortDateString());
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Landesp()
                        {
                            l_id = Convert.ToInt32(dr["l_id"]),
                            l_data = Convert.ToDateTime(dr["l_data"]),
                            l_tipo = dr["l_tipo"].ToString().Trim(),
                            l_valor = Convert.ToDouble(dr["l_valor"]),
                            l_forma = dr["l_forma"].ToString().Trim(),
                            l_obs = dr["l_obs"].ToString().Trim(),
                        });
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

        public static CL_Landesp buscaDesp(int l_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Landesp objLand = new CL_Landesp();
            string sql = "SELECT * FROM movdesp WHERE l_id=@l_id";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("l_id", l_id);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objLand.l_id = l_id;
                        objLand.l_data = Convert.ToDateTime(dr["l_data"]);
                        objLand.l_tipo = dr["l_tipo"].ToString().Trim();
                        objLand.l_valor = Convert.ToDouble(dr["l_valor"]);
                        objLand.l_forma = dr["l_forma"].ToString().Trim();
                        objLand.l_obs = dr["l_obs"].ToString().Trim();
                        return objLand;
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