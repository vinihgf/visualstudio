using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Funcoes : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCodigoEstado(string codEst, string con)
        {
            int est_cod = 0;

            string sql = "SELECT est_ibge FROM estado WHERE est_cod='" + codEst + "'";

            DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

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
                        est_cod = Convert.ToInt32(dr["est_ibge"]);
                        return est_cod;
                    }
                    else
                    {
                        est_cod = 0;
                        return est_cod;
                    }
                }
                else
                {
                    est_cod = 0;
                    return est_cod;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return est_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool confereEmail(string email, string con)
        {
            DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_email FROM usudac WHERE u_email=@u_email";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("u_email", email);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return true;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static void DesmontaConexao(string con)
        {
            try
            {
                string[] vetCon = con.Split('*');
                SERVER = vetCon[0].ToString();
                PORTA = vetCon[1].ToString();
                USER = vetCon[2].ToString();
                SENHA = vetCon[3].ToString();
                BANCO = vetCon[4].ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static bool acessoUsudac(string email, string con)
        {
            DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string u_gersis = "";
            string sql = "SELECT u_gersis FROM usudac WHERE u_email=('" + email + "')";

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
                        u_gersis = dr["u_gersis"].ToString().Trim();
                    }
                    if (u_gersis == "S")
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
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static CL_Convenio buscaConvenio(string con_cod, string con)
        {
            DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            String sql = "SELECT * FROM convenio WHERE con_cod =" + con_cod;

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
                        CL_Convenio objConvenio = new CL_Convenio();

                        objConvenio.con_cod = Convert.ToInt32(dr["con_cod"]);
                        objConvenio.con_nome = dr["con_nome"].ToString().Trim();

                        return objConvenio;
                    }
                    return null;
                }
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
            return null;
        }

        internal static int buscaRecno(int recno, string tabela, string con)
        {
            DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "SELECT sr_recno FROM " + tabela + " ORDER BY sr_recno DESC LIMIT 1";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                NpgsqlDataReader dr;
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        recno = Convert.ToInt32(dr["sr_recno"]);
                        return recno;
                    }
                    else
                    {
                        recno = 1;
                        return recno;
                    }
                }
                else
                {
                    recno = 1;
                    return recno;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                recno = 0;
                return recno;

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