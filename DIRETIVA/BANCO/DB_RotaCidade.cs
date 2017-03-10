using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_RotaCidade : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaID(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT r_id FROM rota_cidade ORDER BY r_id DESC LIMIT 1";
            int r_id = 0;
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
                        r_id = Convert.ToInt16(dr["r_id"]);
                        r_id = r_id + 1;
                        return r_id;
                    }
                    else
                    {
                        r_id = 0;
                        return r_id;
                    }
                }
                else
                {
                    r_id = 1;
                    return r_id;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                r_id = 0;
                return r_id;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_RotaCidade> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT r_id, r_bairro, r_cidade, r_identreg, mec_nome FROM rota_cidade, mecanico WHERE mec_cod=r_identreg";
            List<CL_RotaCidade> objListRota = new List<CL_RotaCidade>();

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
                        objListRota.Add(new CL_RotaCidade()
                        {
                            r_id = Convert.ToInt32(dr["r_id"]),
                            r_nome = dr["mec_nome"].ToString().Trim(),
                            r_identreg = Convert.ToInt32(dr["r_identreg"]),
                            r_bairro = dr["r_bairro"].ToString().Trim(),
                            r_cidade = dr["r_cidade"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListRota;
                }
                else
                {
                    return objListRota;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListRota = null;
                return objListRota;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_RotaCidade buscaRota(int r_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM rota_cidade WHERE r_id=@r_id";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("r_id", r_id);
            NpgsqlDataReader dr;
            CL_RotaCidade objRota = new CL_RotaCidade();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objRota.r_id = r_id;
                        objRota.r_bairro = dr["r_bairro"].ToString().Trim();
                        objRota.r_cidade = dr["r_cidade"].ToString().Trim();
                        objRota.r_identreg = Convert.ToInt32(dr["r_identreg"]);
                        return objRota;
                    }
                    else
                    {
                        objRota = null;
                        return objRota;
                    }
                }
                else
                {
                    objRota = null;
                    return objRota;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objRota = null;
                return objRota;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_RotaCidade> buscaRotasEntregadores(string identreg, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT r_id, r_bairro, r_cidade, r_identreg, mec_nome FROM rota_cidade, mecanico WHERE r_identreg=@r_identreg AND mec_cod=r_identreg";
            List<CL_RotaCidade> objListRota = new List<CL_RotaCidade>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("r_identreg", identreg);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListRota.Add(new CL_RotaCidade()
                        {
                            r_id = Convert.ToInt32(dr["r_id"]),
                            r_nome = dr["mec_nome"].ToString().Trim(),
                            r_identreg = Convert.ToInt32(identreg),
                            r_bairro = dr["r_bairro"].ToString().Trim(),
                            r_cidade = dr["r_cidade"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListRota;
                }
                else
                {
                    return objListRota;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListRota = null;
                return objListRota;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadRota(CL_RotaCidade objRotaCidade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO rota_cidade (r_identreg, r_cidade, r_bairro) VALUES (@r_identreg, @r_cidade, @r_bairro)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("r_identreg", objRotaCidade.r_identreg);
                comand.Parameters.AddWithValue("r_cidade", objRotaCidade.r_cidade);
                comand.Parameters.AddWithValue("r_bairro", objRotaCidade.r_bairro);

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

        public static bool alteraRota(CL_RotaCidade objRotaCidade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE rota_cidade SET r_identreg=@r_identreg, r_cidade=@r_cidade, r_bairro=@r_bairro WHERE r_id=@r_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("r_identreg", objRotaCidade.r_identreg);
                comand.Parameters.AddWithValue("r_cidade", objRotaCidade.r_cidade);
                comand.Parameters.AddWithValue("r_bairro", objRotaCidade.r_bairro);
                comand.Parameters.AddWithValue("r_id", objRotaCidade.r_id);

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

        public static bool excluirRota(CL_RotaCidade objRotaCidade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "DELETE FROM rota_cidade WHERE r_id=@r_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("r_id", objRotaCidade.r_id);

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
    }
}