using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Convenio : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static CL_Convenio buscaConvenio(string con_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Convenio objConvenio = new CL_Convenio();

            string sql = "SELECT * FROM convenio WHERE con_cod=" + con_cod;

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
                        objConvenio.con_cod = Convert.ToInt32(dr["con_cod"]);
                        objConvenio.con_nome = dr["con_nome"].ToString().Trim();
                        objConvenio.con_comis = Convert.ToDouble(dr["con_comis"]);
                        objConvenio.con_agencia = dr["con_agenc"].ToString().Trim();
                        objConvenio.con_area = dr["con_area"].ToString().Trim();
                        objConvenio.con_banco = dr["con_banco"].ToString().Trim();
                        objConvenio.con_celul = dr["con_celul"].ToString().Trim();
                        objConvenio.con_cep = dr["con_cep"].ToString().Trim();
                        objConvenio.con_cgc = dr["con_cgc"].ToString().Trim();
                        objConvenio.con_cida = dr["con_cida"].ToString().Trim();
                        objConvenio.con_cont = dr["con_cont"].ToString().Trim();
                        objConvenio.con_conta = dr["con_conta"].ToString().Trim();
                        objConvenio.con_email = dr["con_email"].ToString().Trim();
                        objConvenio.con_ende = dr["con_ende"].ToString().Trim();
                        objConvenio.con_est = dr["con_est"].ToString().Trim();
                        objConvenio.con_fone = dr["con_fone"].ToString().Trim();
                        objConvenio.con_iest = dr["con_iest"].ToString().Trim();
                        objConvenio.con_login = dr["con_login"].ToString().Trim();
                        objConvenio.con_pedido = Convert.ToInt32(dr["con_pedido"]);
                        objConvenio.con_revend = dr["con_revend"].ToString().Trim();
                        objConvenio.con_situac = dr["con_situac"].ToString().Trim();
                        objConvenio.con_termi = dr["con_termi"].ToString().Trim();
                        objConvenio.con_tipo = dr["con_tipo"].ToString().Trim();
                        objConvenio.con_umov = dr["con_umov"].ToString().Trim();
                        objConvenio.con_corretor = dr["con_corretor"].ToString().Trim();

                        return objConvenio;
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

        public static bool conferePermissao(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_mvend FROM usudac WHERE u_email=@u_email";

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
                    {
                        string ret = dr["u_mvend"].ToString().Trim();
                        return ret == "S" ? true : false;
                    }
                    else
                        return false;
                }
                else
                    return false;

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

        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT con_cod FROM convenio ORDER BY con_cod DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["con_cod"]) + 1;
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

        public static bool cadConvenio(CL_Convenio objConvenio, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                objConvenio.con_cod = buscaCod(con);
                if (objConvenio.con_cod > 0)
                {
                    string sql = "INSERT INTO convenio (con_agenc, con_area, con_banco, con_celul, con_cep, con_cgc, con_cida, con_cod, con_comis, con_cont, con_conta, con_email, con_ende, con_est, con_fone, con_iest, con_login, con_nome, con_pedido, con_revend, con_senha, con_situac, con_termi, con_tipo, con_umov, con_corretor)" +
                    "VALUES (@con_agenc, @con_area, @con_banco, @con_celul, @con_cep, @con_cgc, @con_cida, @con_cod, @con_comis, @con_cont, @con_conta, @con_email, @con_ende, @con_est, @con_fone, @con_iest, @con_login, @con_nome, @con_pedido, @con_revend, @con_senha, @con_situac, @con_termi, @con_tipo, @con_umov, @con_corretor)";

                    NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                    comand.Parameters.AddWithValue("con_agenc", objConvenio.con_agencia);
                    comand.Parameters.AddWithValue("con_area", objConvenio.con_area);
                    comand.Parameters.AddWithValue("con_banco", objConvenio.con_banco);
                    comand.Parameters.AddWithValue("con_celul", objConvenio.con_celul);
                    comand.Parameters.AddWithValue("con_cep", objConvenio.con_cep);
                    comand.Parameters.AddWithValue("con_cgc", objConvenio.con_cgc);
                    comand.Parameters.AddWithValue("con_cida", objConvenio.con_cida);
                    comand.Parameters.AddWithValue("con_cod", objConvenio.con_cod);
                    comand.Parameters.AddWithValue("con_comis", objConvenio.con_comis);
                    comand.Parameters.AddWithValue("con_cont", objConvenio.con_cont);
                    comand.Parameters.AddWithValue("con_conta", objConvenio.con_conta);
                    comand.Parameters.AddWithValue("con_email", objConvenio.con_email);
                    comand.Parameters.AddWithValue("con_ende", objConvenio.con_ende);
                    comand.Parameters.AddWithValue("con_est", objConvenio.con_est);
                    comand.Parameters.AddWithValue("con_fone", objConvenio.con_fone);
                    comand.Parameters.AddWithValue("con_iest", objConvenio.con_iest);
                    comand.Parameters.AddWithValue("con_login", objConvenio.con_login);
                    comand.Parameters.AddWithValue("con_nome", objConvenio.con_nome);
                    comand.Parameters.AddWithValue("con_pedido", objConvenio.con_pedido);
                    comand.Parameters.AddWithValue("con_revend", objConvenio.con_revend);
                    comand.Parameters.AddWithValue("con_senha", objConvenio.con_senha);
                    comand.Parameters.AddWithValue("con_situac", objConvenio.con_situac);
                    comand.Parameters.AddWithValue("con_termi", objConvenio.con_termi);
                    comand.Parameters.AddWithValue("con_tipo", objConvenio.con_tipo);
                    comand.Parameters.AddWithValue("con_umov", objConvenio.con_umov);
                    comand.Parameters.AddWithValue("con_corretor", objConvenio.con_corretor);
                    Conn.Open();
                    comand.ExecuteScalar();
                    return true;
                }
                else
                    return false;
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

        public static bool alteraConvenio(CL_Convenio objConvenio, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE convenio SET con_agenc=@con_agenc, con_area=@con_area, con_banco=@con_banco, con_celul=@con_celul, " +
                    "con_cep=@con_cep, con_cgc=@con_cgc, con_cida=@con_cida, con_comis=@con_comis, con_cont=@con_cont, con_conta=@con_conta, " +
                    "con_email=@con_email, con_fone=@con_fone, con_est=@con_est, con_ende=@con_ende, con_iest=@con_iest, con_login=@con_login, " +
                    "con_nome=@con_nome, con_pedido=@con_pedido, con_revend=@con_revend, con_umov=@con_umov, con_situac=@con_situac, " +
                    "con_termi=@con_termi, con_tipo=@con_tipo, con_corretor=@con_corretor WHERE con_cod=@con_cod";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("con_agenc", objConvenio.con_agencia);
                comand.Parameters.AddWithValue("con_area", objConvenio.con_area);
                comand.Parameters.AddWithValue("con_banco", objConvenio.con_banco);
                comand.Parameters.AddWithValue("con_celul", objConvenio.con_celul);
                comand.Parameters.AddWithValue("con_cep", objConvenio.con_cep);
                comand.Parameters.AddWithValue("con_cgc", objConvenio.con_cgc);
                comand.Parameters.AddWithValue("con_cida", objConvenio.con_cida);
                comand.Parameters.AddWithValue("con_comis", objConvenio.con_comis);
                comand.Parameters.AddWithValue("con_cont", objConvenio.con_cont);
                comand.Parameters.AddWithValue("con_conta", objConvenio.con_conta);
                comand.Parameters.AddWithValue("con_email", objConvenio.con_email);
                comand.Parameters.AddWithValue("con_fone", objConvenio.con_fone);
                comand.Parameters.AddWithValue("con_est", objConvenio.con_est);
                comand.Parameters.AddWithValue("con_ende", objConvenio.con_ende);
                comand.Parameters.AddWithValue("con_iest", objConvenio.con_iest);
                comand.Parameters.AddWithValue("con_login", objConvenio.con_login);
                comand.Parameters.AddWithValue("con_nome", objConvenio.con_nome);
                comand.Parameters.AddWithValue("con_pedido", objConvenio.con_pedido);
                comand.Parameters.AddWithValue("con_revend", objConvenio.con_revend);
                comand.Parameters.AddWithValue("con_umov", objConvenio.con_umov);
                comand.Parameters.AddWithValue("con_situac", objConvenio.con_situac);
                comand.Parameters.AddWithValue("con_termi", objConvenio.con_termi);
                comand.Parameters.AddWithValue("con_tipo", objConvenio.con_tipo);
                comand.Parameters.AddWithValue("con_cod", objConvenio.con_cod);
                comand.Parameters.AddWithValue("con_corretor", objConvenio.con_corretor);
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

        public static bool excluiConvenio(CL_Convenio objConvenio, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM convenio WHERE con_cod=" + objConvenio.con_cod;
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

        public static List<CL_Convenio> listar(List<CL_Convenio> objListCon, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM convenio ORDER BY con_cod";
            CL_Convenio objConvenio = null;

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
                        objConvenio = new CL_Convenio();
                        objConvenio.con_cod = Convert.ToInt32(dr["con_cod"]);
                        objConvenio.con_nome = dr["con_nome"].ToString().Trim();
                        objConvenio.con_comis = Convert.ToDouble(dr["con_comis"]);
                        objConvenio.con_agencia = dr["con_agenc"].ToString().Trim();
                        objConvenio.con_area = dr["con_area"].ToString().Trim();
                        objConvenio.con_banco = dr["con_banco"].ToString().Trim();
                        objConvenio.con_celul = dr["con_celul"].ToString().Trim();
                        objConvenio.con_cep = dr["con_cep"].ToString().Trim();
                        objConvenio.con_cgc = dr["con_cgc"].ToString().Trim();
                        objConvenio.con_cida = dr["con_cida"].ToString().Trim();
                        objConvenio.con_cont = dr["con_cont"].ToString().Trim();
                        objConvenio.con_conta = dr["con_conta"].ToString().Trim();
                        objConvenio.con_email = dr["con_email"].ToString().Trim();
                        objConvenio.con_ende = dr["con_ende"].ToString().Trim();
                        objConvenio.con_est = dr["con_est"].ToString().Trim();
                        objConvenio.con_fone = dr["con_fone"].ToString().Trim();
                        objConvenio.con_iest = dr["con_iest"].ToString().Trim();
                        objConvenio.con_login = dr["con_login"].ToString().Trim();
                        objConvenio.con_pedido = Convert.ToInt32(dr["con_pedido"]);
                        objConvenio.con_revend = dr["con_revend"].ToString().Trim();
                        objConvenio.con_situac = dr["con_situac"].ToString().Trim();
                        objConvenio.con_termi = dr["con_termi"].ToString().Trim();
                        objConvenio.con_tipo = dr["con_tipo"].ToString().Trim();
                        objConvenio.con_umov = dr["con_umov"].ToString().Trim();
                        objConvenio.con_codnome = Convert.ToInt32(dr["con_cod"]) + " - " + dr["con_nome"].ToString().Trim();
                        objConvenio.con_corretor = dr["con_corretor"].ToString().Trim();
                        objListCon.Add(objConvenio);
                    }
                    dr.Close();
                    return objListCon;
                }
                else
                    return objListCon;
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

        public static CL_Convenio buscaConSenha(CL_Convenio objConvenio, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT con_cod, con_nome FROM convenio WHERE con_senha='" + objConvenio.con_senha + "'";

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
                        objConvenio.con_cod = Convert.ToInt32(dr["con_cod"]);
                        objConvenio.con_nome = dr["con_nome"].ToString().Trim();
                        return objConvenio;
                    }
                    else
                    {
                        objConvenio = null;
                        return objConvenio;
                    }
                }
                else
                {
                    objConvenio = null;
                    return objConvenio;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objConvenio = null;
                return objConvenio;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Convenio> listarApp(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM convenio WHERE con_umov='S' OR con_umov='A' ORDER BY con_cod";
            List<CL_Convenio> objListCon = new List<CL_Convenio>();
            CL_Convenio objConvenio = null;

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
                        objConvenio = new CL_Convenio();
                        objConvenio.con_cod = Convert.ToInt32(dr["con_cod"]);
                        objConvenio.con_nome = dr["con_nome"].ToString().Trim();
                        objConvenio.con_comis = Convert.ToDouble(dr["con_comis"]);
                        objConvenio.con_agencia = dr["con_agenc"].ToString().Trim();
                        objConvenio.con_area = dr["con_area"].ToString().Trim();
                        objConvenio.con_banco = dr["con_banco"].ToString().Trim();
                        objConvenio.con_celul = dr["con_celul"].ToString().Trim();
                        objConvenio.con_cep = dr["con_cep"].ToString().Trim();
                        objConvenio.con_cgc = dr["con_cgc"].ToString().Trim();
                        objConvenio.con_cida = dr["con_cida"].ToString().Trim();
                        objConvenio.con_cont = dr["con_cont"].ToString().Trim();
                        objConvenio.con_conta = dr["con_conta"].ToString().Trim();
                        objConvenio.con_email = dr["con_email"].ToString().Trim();
                        objConvenio.con_ende = dr["con_ende"].ToString().Trim();
                        objConvenio.con_est = dr["con_est"].ToString().Trim();
                        objConvenio.con_fone = dr["con_fone"].ToString().Trim();
                        objConvenio.con_iest = dr["con_iest"].ToString().Trim();
                        objConvenio.con_login = dr["con_login"].ToString().Trim();
                        objConvenio.con_pedido = Convert.ToInt32(dr["con_pedido"]);
                        objConvenio.con_revend = dr["con_revend"].ToString().Trim();
                        objConvenio.con_situac = dr["con_situac"].ToString().Trim();
                        objConvenio.con_termi = dr["con_termi"].ToString().Trim();
                        objConvenio.con_tipo = dr["con_tipo"].ToString().Trim();
                        objConvenio.con_umov = dr["con_umov"].ToString().Trim();
                        objListCon.Add(objConvenio);
                    }
                    dr.Close();
                    return objListCon;
                }
                else
                {
                    objListCon = null;
                    return objListCon;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListCon = null;
                return objListCon;
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