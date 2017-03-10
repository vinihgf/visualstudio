using System;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_LogMecanic : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static bool gravaLog(CL_LogMecanic objLogMecanic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO log_mecanico (l_id, l_meccod, l_mecnome, l_data, l_localiz, l_mectipo, l_idapp) " +
                    "VALUES " +
                    "(@l_id, @l_meccod, @l_mecnome, @l_data, @l_localiz, @l_mectipo, @l_idapp)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("l_id", objLogMecanic.l_id);
                cmd.Parameters.AddWithValue("l_meccod", objLogMecanic.l_meccod);
                cmd.Parameters.AddWithValue("l_mecnome", objLogMecanic.l_mecnome);
                cmd.Parameters.AddWithValue("l_data", objLogMecanic.l_data);
                cmd.Parameters.AddWithValue("l_localiz", objLogMecanic.l_localiz);
                cmd.Parameters.AddWithValue("l_mectipo", objLogMecanic.l_mectipo);
                cmd.Parameters.AddWithValue("l_idapp", objLogMecanic.l_idapp);

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

        public static bool verificaLog(int obj, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT l_id FROM log_mecanico WHERE l_idapp=" + obj;
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return true;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_LogMecanic buscaLogMecanic(int obj, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM log_mecanico WHERE l_idapp=" + obj;
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            CL_LogMecanic objLog = new CL_LogMecanic();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objLog.l_id = Convert.ToInt32(dr["l_id"]);
                        objLog.l_localiz = dr["l_localiz"].ToString().Trim();
                        objLog.l_meccod = Convert.ToInt32(dr["l_meccod"]);
                        objLog.l_mecnome = dr["l_mecnome"].ToString().Trim();
                        objLog.l_mectipo = dr["l_mectipo"].ToString().Trim();
                        objLog.l_data = Convert.ToDateTime(dr["l_data"]);
                        objLog.l_idapp = Convert.ToInt64(dr["l_idapp"]);
                        return objLog;
                    }
                    else
                    {
                        objLog = null;
                        return objLog;
                    }
                }
                else
                {
                    objLog = null;
                    return objLog;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objLog = null;
                return objLog;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static int buscaID(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT l_id FROM log_mecanico ORDER BY l_id DESC LIMIT 1";
            int l_id = 0;
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