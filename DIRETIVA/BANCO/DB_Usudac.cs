using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Usudac : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static string connString;
        public static int buscaCodUsudac(int u_codigo, string con)
        {
            connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
            //connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=12345;Database=11157076000141";
            Conn = new NpgsqlConnection(connString);

            string sql = "SELECT u_codigo FROM usudac ORDER BY u_codigo DESC limit 1";

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
                        u_codigo = Convert.ToInt32(dr["u_codigo"]) + 1;
                        return u_codigo;
                    }
                    else
                    {
                        u_codigo = 0;
                        return u_codigo;
                    }
                }
                else
                {
                    u_codigo = 1;
                    return u_codigo;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                u_codigo = 0;
                return u_codigo;
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

            string sql = "SELECT u_usuario FROM usudac WHERE u_email=@u_email";

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
                        string ret = dr["u_usuario"].ToString().Trim();
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

        public static CL_Usudac buscaUsudacEmail(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_Usudac objUsudac = new CL_Usudac();
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            string sql = "SELECT u_tipoaces FROM usudac WHERE u_email=@email";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("email", email);
            NpgsqlDataReader dr;

            try
            {
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objUsudac.u_tipoaces = dr["u_tipoaces"].ToString().Trim();
                        return objUsudac;
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
                objUsudac = null;
                return objUsudac;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_Usudac buscaUsudac(string u_codigo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_nome, u_email, u_senha, u_bd, u_tipoaces, u_cvendas "+
                "u_usuario, u_mcliente, u_mctarec, u_mempresa, u_mvend, u_arma, u_mcheques FROM usudac WHERE u_codigo=@u_codigo";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("u_codigo", u_codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        CL_Usudac objUsudac = new CL_Usudac();
                        objUsudac.u_codigo = Convert.ToInt32(u_codigo);
                        objUsudac.u_nome = dr["u_nome"].ToString().Trim();
                        objUsudac.u_email = dr["u_email"].ToString().Trim();
                        objUsudac.u_senha = dr["u_senha"].ToString().Trim();
                        objUsudac.u_cgc = dr["u_bd"].ToString().Trim();
                        objUsudac.u_tipoaces = dr["u_tipoaces"].ToString().Trim();
                        objUsudac.u_usudac = dr["u_usuario"].ToString().Trim();
                        objUsudac.u_particip = dr["u_mcliente"].ToString().Trim();
                        objUsudac.u_financeiro = dr["u_mctarec"].ToString().Trim();
                        objUsudac.u_empresa = dr["u_mempresa"].ToString().Trim();
                        objUsudac.u_parceiro = dr["u_mvend"].ToString().Trim();
                        objUsudac.u_proposta = dr["u_arma"].ToString().Trim();
                        objUsudac.u_sinistro = dr["u_mcheques"].ToString().Trim();
                        objUsudac.u_conssinistro = dr["u_cvendas"].ToString().Trim();
                        return objUsudac;
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

        public static bool incluiUsudac(CL_Usudac objUsudac, string con)
        {
            //connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141";
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            objUsudac.u_cgc = Conn.Database;
            objUsudac.u_senhadb = Conn.Host == "postgres-diretiva.ddns.net" ? "Diretiva!@#" : "Pgadmin12345";
            objUsudac.u_host = Conn.Host;
            connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
            Conn = new NpgsqlConnection(connString);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                objUsudac.u_porta = Conn.Port;
                objUsudac.u_user = "postgres";

                string sql = "INSERT INTO usudac (u_codigo, u_nome, u_email, u_senha, u_bd, u_porta, u_user, u_senhadb, u_host, u_tipoaces, u_usuario, u_mcliente, u_mctarec, u_mempresa, u_mvend, u_arma, u_mcheques, u_cvendas) " +
                    "VALUES (@u_codigo, @u_nome, @u_email, @u_senha, @u_bd, @u_porta, @u_user, @u_senhadb, @u_host, @u_tipoaces, @u_usuario, @u_mcliente, @u_mctarec, @u_mempresa, @u_mvend, @u_arma, @u_mcheques, @u_cvendas)";
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_codigo", objUsudac.u_codigo);
                comand.Parameters.AddWithValue("u_nome", objUsudac.u_nome);
                comand.Parameters.AddWithValue("u_email", objUsudac.u_email);
                comand.Parameters.AddWithValue("u_senha", objUsudac.u_senha);
                comand.Parameters.AddWithValue("u_bd", objUsudac.u_cgc);
                comand.Parameters.AddWithValue("u_porta", objUsudac.u_porta);
                comand.Parameters.AddWithValue("u_user", objUsudac.u_user);
                comand.Parameters.AddWithValue("u_senhadb", objUsudac.u_senhadb);
                comand.Parameters.AddWithValue("u_host", objUsudac.u_host);
                comand.Parameters.AddWithValue("u_tipoaces", objUsudac.u_tipoaces);
                comand.Parameters.AddWithValue("u_usuario", objUsudac.u_usudac);
                comand.Parameters.AddWithValue("u_mcliente", objUsudac.u_particip);
                comand.Parameters.AddWithValue("u_mctarec", objUsudac.u_financeiro);
                comand.Parameters.AddWithValue("u_mempresa", objUsudac.u_empresa);
                comand.Parameters.AddWithValue("u_mvend", objUsudac.u_parceiro);
                comand.Parameters.AddWithValue("u_arma", objUsudac.u_proposta);
                comand.Parameters.AddWithValue("u_mcheques", objUsudac.u_sinistro);
                comand.Parameters.AddWithValue("u_cvendas", objUsudac.u_conssinistro);
                comand.ExecuteScalar();
                transaction.Commit();
                Conn.Close();

                DB_Funcoes.DesmontaConexao(con);
                CONEXAO = montaDAO(CONEXAO);
                Conn = new NpgsqlConnection(CONEXAO);
                Conn.Open();
                transaction = Conn.BeginTransaction();
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_codigo", objUsudac.u_codigo);
                comand.Parameters.AddWithValue("u_nome", objUsudac.u_nome);
                comand.Parameters.AddWithValue("u_email", objUsudac.u_email);
                comand.Parameters.AddWithValue("u_senha", objUsudac.u_senha);
                comand.Parameters.AddWithValue("u_bd", objUsudac.u_cgc);
                comand.Parameters.AddWithValue("u_porta", objUsudac.u_porta);
                comand.Parameters.AddWithValue("u_user", objUsudac.u_user);
                comand.Parameters.AddWithValue("u_senhadb", objUsudac.u_senhadb);
                comand.Parameters.AddWithValue("u_host", objUsudac.u_host);
                comand.Parameters.AddWithValue("u_tipoaces", objUsudac.u_tipoaces);
                comand.Parameters.AddWithValue("u_usuario", objUsudac.u_usudac);
                comand.Parameters.AddWithValue("u_mcliente", objUsudac.u_particip);
                comand.Parameters.AddWithValue("u_mctarec", objUsudac.u_financeiro);
                comand.Parameters.AddWithValue("u_mempresa", objUsudac.u_empresa);
                comand.Parameters.AddWithValue("u_mvend", objUsudac.u_parceiro);
                comand.Parameters.AddWithValue("u_arma", objUsudac.u_proposta);
                comand.Parameters.AddWithValue("u_mcheques", objUsudac.u_sinistro);
                comand.Parameters.AddWithValue("u_cvendas", objUsudac.u_conssinistro);
                comand.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool alteraUsudac(CL_Usudac objUsudac, string con)
        {
            //connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141";
            connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            objUsudac.u_cgc = Conn.Database;
            Conn = new NpgsqlConnection(connString);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "UPDATE usudac SET u_nome=@u_nome, u_email=@u_email, u_bd=@u_bd, u_usuario=@u_usuario, u_mcliente=@u_mcliente, u_cvendas=@u_cvendas " +
                             "u_mctarec=@u_mctarec, u_mempresa=@u_mempresa, u_mvend=@u_mvend, u_arma=@u_arma, u_mcheques=@u_mcheques WHERE u_codigo=@u_codigo";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_nome", objUsudac.u_nome);
                comand.Parameters.AddWithValue("u_email", objUsudac.u_email);
                comand.Parameters.AddWithValue("u_bd", objUsudac.u_cgc);
                comand.Parameters.AddWithValue("u_codigo", objUsudac.u_codigo);
                comand.Parameters.AddWithValue("u_usuario", objUsudac.u_usudac);
                comand.Parameters.AddWithValue("u_mcliente", objUsudac.u_particip);
                comand.Parameters.AddWithValue("u_mctarec", objUsudac.u_financeiro);
                comand.Parameters.AddWithValue("u_mempresa", objUsudac.u_empresa);
                comand.Parameters.AddWithValue("u_mvend", objUsudac.u_parceiro);
                comand.Parameters.AddWithValue("u_arma", objUsudac.u_proposta);
                comand.Parameters.AddWithValue("u_mcheques", objUsudac.u_sinistro);
                comand.Parameters.AddWithValue("u_cvendas", objUsudac.u_conssinistro);
                comand.ExecuteScalar();
                transaction.Commit();
                Conn.Close();

                DB_Funcoes.DesmontaConexao(con);
                CONEXAO = montaDAO(CONEXAO);
                Conn = new NpgsqlConnection(CONEXAO);
                Conn.Open();
                Conn.ChangeDatabase(objUsudac.u_cgc);
                transaction = Conn.BeginTransaction();
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_nome", objUsudac.u_nome);
                comand.Parameters.AddWithValue("u_email", objUsudac.u_email);
                comand.Parameters.AddWithValue("u_bd", objUsudac.u_cgc);
                comand.Parameters.AddWithValue("u_codigo", objUsudac.u_codigo);
                comand.Parameters.AddWithValue("u_usuario", objUsudac.u_usudac);
                comand.Parameters.AddWithValue("u_mcliente", objUsudac.u_particip);
                comand.Parameters.AddWithValue("u_mctarec", objUsudac.u_financeiro);
                comand.Parameters.AddWithValue("u_mempresa", objUsudac.u_empresa);
                comand.Parameters.AddWithValue("u_mvend", objUsudac.u_parceiro);
                comand.Parameters.AddWithValue("u_arma", objUsudac.u_proposta);
                comand.Parameters.AddWithValue("u_mcheques", objUsudac.u_sinistro);
                comand.ExecuteScalar();
                transaction.Commit();
                return true;

            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }


        private static int buscaCodigoUsudacDiretiva2(string email)
        {
            string sql = "SELECT u_codigo FROM usudac WHERE u_email ='" + email + "'";

            //connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141//";
            connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
            Conn = new NpgsqlConnection(connString);

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["u_codigo"]);
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

        public static bool excluiUsudac(CL_Usudac objUsudac, string con)
        {
            //connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141";
            connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
            Conn = new NpgsqlConnection(connString);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();

            string sql = "DELETE FROM usudac WHERE u_codigo=@u_codigo AND u_email=@u_email";

            try
            {
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("u_codigo", objUsudac.u_codigo);
                comand.Parameters.AddWithValue("u_email", objUsudac.u_email);
                comand.ExecuteScalar();
                transaction.Commit();
                Conn.Close();

                DB_Funcoes.DesmontaConexao(con);
                CONEXAO = montaDAO(CONEXAO);
                Conn = new NpgsqlConnection(CONEXAO);
                Conn.Open();
                Conn.ChangeDatabase(objUsudac.u_cgc);
                transaction = Conn.BeginTransaction();
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_codigo", objUsudac.u_codigo);
                comand.Parameters.AddWithValue("u_email", objUsudac.u_email);
                comand.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }


        public static bool alteraSenhaUsudac(CL_EsqueciSenha objEsqueciSenha, string con)
        {
            //connString = "Server=diretivasistemas.ddns.com.br;Port=5432;User Id=postgres;Password=Pgadmin12345;Database=11157076000141";
            connString = "Server=postgres-diretiva.ddns.net;Port=5432;User Id=postgres;Password=Diretiva!@#;Database=Diretiva";
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string bd = Conn.Database;
            Conn = new NpgsqlConnection(connString);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            string sql = "UPDATE usudac set u_senha=@u_nova WHERE u_email=@u_email AND u_senha=@u_antiga";
            try
            {
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_nova", objEsqueciSenha.u_esqSenhaNov);
                comand.Parameters.AddWithValue("u_antiga", objEsqueciSenha.u_esqSenhaAnt);
                comand.Parameters.AddWithValue("u_email", objEsqueciSenha.u_esqEmail);
                comand.ExecuteScalar();
                transaction.Commit();

                Conn.ChangeDatabase(bd);
                transaction = Conn.BeginTransaction();
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("u_nova", objEsqueciSenha.u_esqSenhaNov);
                comand.Parameters.AddWithValue("u_antiga", objEsqueciSenha.u_esqSenhaAnt);
                comand.Parameters.AddWithValue("u_email", objEsqueciSenha.u_esqEmail);
                comand.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        
        public List<CL_Usudac> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_codigo, u_nome, u_email, u_senha, u_bd, u_tipoaces, u_cvendas " +
                "u_usuario, u_mcliente, u_mctarec, u_mempresa, u_mvend, u_arma, u_mcheques FROM usudac";

            List<CL_Usudac> objList = new List<CL_Usudac>();
            CL_Usudac objUsudac = null;

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
                        objUsudac = new CL_Usudac();
                        objUsudac.u_codigo = Convert.ToInt32(dr["u_codigo"]);
                        objUsudac.u_nome = dr["u_nome"].ToString().Trim();
                        objUsudac.u_email = dr["u_email"].ToString().Trim();
                        objUsudac.u_senha = dr["u_senha"].ToString().Trim();
                        objUsudac.u_cgc = dr["u_bd"].ToString().Trim();
                        objUsudac.u_tipoaces = dr["u_tipoaces"].ToString().Trim();
                        objUsudac.u_usudac = dr["u_usuario"].ToString().Trim();
                        objUsudac.u_particip = dr["u_mcliente"].ToString().Trim();
                        objUsudac.u_financeiro = dr["u_mctarec"].ToString().Trim();
                        objUsudac.u_empresa = dr["u_mempresa"].ToString().Trim();
                        objUsudac.u_parceiro = dr["u_mvend"].ToString().Trim();
                        objUsudac.u_proposta = dr["u_arma"].ToString().Trim();
                        objUsudac.u_sinistro = dr["u_mcheques"].ToString().Trim();
                        objUsudac.u_conssinistro = dr["u_cvendas"].ToString().Trim();

                        objList.Add(objUsudac);
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
                {
                    Conn.Close();
                }
            }
        }
    }
}