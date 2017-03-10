using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Pluviometro : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaCodigo(int p_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_id FROM pluviometro ORDER BY p_id DESC LIMIT 1";

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
                        p_id = Convert.ToInt16(dr["p_id"]);
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

        public static bool excluiPluviometro(CL_Pluviometro objPluv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM pluviometro WHERE p_id=" + objPluv.p_id;

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
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
                    string sql2 = "DELETE FROM pluviometro WHERE p_id=" + objPluv.p_id;
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

        public static bool alteraPluviometro(CL_Pluviometro objPluv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE pluviometro SET p_data='" + objPluv.p_data + "', p_turno='" + objPluv.p_turno +
                             "', p_duracao='" + objPluv.p_duracao + "', p_qtdade='" + objPluv.p_qtdade + "', p_idlavoura='" + objPluv.p_idlavoura +
                             "' WHERE p_id=" + objPluv.p_id;

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

        public static bool cadPluviometro(CL_Pluviometro objPluv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO pluviometro (p_id,p_data,p_turno,p_duracao,p_qtdade,p_idlavoura) VALUES (@p_id,@p_data,@p_turno,@p_duracao,@p_qtdade,@p_idlavoura)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("p_id", objPluv.p_id);
                comand.Parameters.AddWithValue("p_data", objPluv.p_data);
                comand.Parameters.AddWithValue("p_turno", objPluv.p_turno);
                comand.Parameters.AddWithValue("p_duracao", objPluv.p_duracao);
                comand.Parameters.AddWithValue("p_qtdade", objPluv.p_qtdade);
                comand.Parameters.AddWithValue("p_idlavoura", objPluv.p_idlavoura);

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

        public static CL_Pluviometro buscaPluv(CL_Pluviometro objPluv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM pluviometro WHERE p_id=" + objPluv.p_id + " ORDER BY p_id";

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
                        objPluv.p_data = Convert.ToDateTime(dr["p_data"]);
                        objPluv.p_turno = dr["p_turno"].ToString().Trim();
                        objPluv.p_duracao = Convert.ToDouble(dr["p_duracao"]);
                        objPluv.p_qtdade = Convert.ToDouble(dr["p_qtdade"]);
                        objPluv.p_idlavoura = Convert.ToInt32(dr["p_idlavoura"]);
                        return objPluv;
                    }
                    else
                    {
                        objPluv = null;
                        return objPluv;
                    }
                }
                else
                {
                    objPluv = null;
                    return objPluv;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objPluv = null;
                return objPluv;
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