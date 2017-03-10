using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Mecanico : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCod(int mec_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT mec_cod FROM mecanico ORDER BY mec_cod DESC LIMIT 1";

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
                        mec_cod = Convert.ToInt32(dr["mec_cod"]) + 1;
                        return mec_cod;
                    }
                    else
                    {
                        mec_cod = 0;
                        return mec_cod;
                    }
                }
                else
                {
                    mec_cod = 1;
                    return mec_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                mec_cod = 0;
                return mec_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_Mecanico buscaMec(string mec_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM mecanico WHERE mec_cod=" + mec_cod;
            CL_Mecanico objMecanico = new CL_Mecanico();

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
                        objMecanico.mec_cod = Convert.ToInt32(mec_cod);
                        objMecanico.mec_nome = dr["mec_nome"].ToString().Trim();
                        objMecanico.mec_vlhora = Convert.ToDouble(dr["mec_vlhora"]);
                        objMecanico.mec_tipo = dr["mec_ativo"].ToString().Trim();
                        objMecanico.mec_ende = dr["mec_ende"].ToString().Trim();
                        objMecanico.mec_cida = dr["mec_cida"].ToString().Trim();
                        objMecanico.mec_fone = dr["mec_fone"].ToString().Trim();
                        objMecanico.mec_espec = dr["mec_espec"].ToString().Trim();
                        objMecanico.mec_funcao = dr["mec_funcao"].ToString().Trim();
                        objMecanico.mec_login = dr["mec_login"].ToString().Trim();
                        objMecanico.mec_senAnt = dr["mec_senha"].ToString().Trim();
                        objMecanico.mec_localiz = dr["mec_locali"].ToString().Trim();

                        return objMecanico;
                    }
                    else
                    {
                        objMecanico = null;
                        return objMecanico;
                    }
                }
                else
                {
                    objMecanico = null;
                    return objMecanico;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objMecanico = null;
                return objMecanico;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Mecanico> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM mecanico ORDER BY mec_cod";
            List<CL_Mecanico> objListMec = new List<CL_Mecanico>();
            CL_Mecanico objMecanico = null;

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
                        objMecanico = new CL_Mecanico();
                        objMecanico.mec_cod = Convert.ToInt32(dr["mec_cod"]);
                        objMecanico.mec_nome = dr["mec_nome"].ToString().Trim();
                        objMecanico.mec_vlhora = Convert.ToDouble(dr["mec_vlhora"]);
                        objMecanico.mec_tipo = dr["mec_tipo"].ToString().Trim();
                        objMecanico.mec_ende = dr["mec_ende"].ToString().Trim();
                        objMecanico.mec_cida = dr["mec_cida"].ToString().Trim();
                        objMecanico.mec_fone = dr["mec_fone"].ToString().Trim();
                        objMecanico.mec_espec = dr["mec_espec"].ToString().Trim();
                        objMecanico.mec_funcao = dr["mec_funcao"].ToString().Trim();
                        objMecanico.mec_localiz = dr["mec_locali"].ToString().Trim();
                        objMecanico.mec_codNome = objMecanico.mec_cod.ToString().Trim() + " - " + objMecanico.mec_nome.Trim();
                        objListMec.Add(objMecanico);
                    }
                    dr.Close();
                    return objListMec;
                }
                else
                    return objListMec;
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

        public static bool cadMec(CL_Mecanico objMecanico, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO mecanico (mec_cod, mec_nome, mec_vlhora, mec_tipo, mec_ende, mec_cida, mec_fone, mec_espec, mec_funcao, mec_login, mec_senha, mec_ativo, mec_locali) " +
                    "VALUES " +
                    "(@mec_cod, @mec_nome, @mec_vlhora, @mec_tipo, @mec_ende, @mec_cida, @mec_fone, @mec_espec, @mec_funcao, @mec_login, @mec_senha, @mec_ativo, @mec_locali)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("mec_cod", objMecanico.mec_cod);
                cmd.Parameters.AddWithValue("mec_nome", objMecanico.mec_nome);
                cmd.Parameters.AddWithValue("mec_vlhora", objMecanico.mec_vlhora);
                cmd.Parameters.AddWithValue("mec_tipo", objMecanico.mec_tipo);
                cmd.Parameters.AddWithValue("mec_ende", objMecanico.mec_ende);
                cmd.Parameters.AddWithValue("mec_cida", objMecanico.mec_cida);
                cmd.Parameters.AddWithValue("mec_fone", objMecanico.mec_fone);
                cmd.Parameters.AddWithValue("mec_espec", objMecanico.mec_espec);
                cmd.Parameters.AddWithValue("mec_funcao", objMecanico.mec_funcao);
                cmd.Parameters.AddWithValue("mec_login", objMecanico.mec_login);
                cmd.Parameters.AddWithValue("mec_senha", objMecanico.mec_senha);
                cmd.Parameters.AddWithValue("mec_ativo", objMecanico.mec_tipo);
                cmd.Parameters.AddWithValue("mec_locali", objMecanico.mec_localiz);
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

        public static bool alteraMec(CL_Mecanico objMecanico, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "UPDATE mecanico SET mec_cod=@mec_cod, mec_nome=@mec_nome, mec_vlhora=@mec_vlhora, mec_tipo=@mec_tipo, mec_locali=@mec_locali, " +
                    "mec_ende=@mec_ende, mec_cida=@mec_cida, mec_fone=@mec_fone, mec_espec=@mec_espec, mec_funcao=@mec_funcao, mec_login=@mec_login, mec_ativo=@mec_ativo WHERE mec_cod=" + objMecanico.mec_cod;
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("mec_cod", objMecanico.mec_cod);
                cmd.Parameters.AddWithValue("mec_nome", objMecanico.mec_nome);
                cmd.Parameters.AddWithValue("mec_vlhora", objMecanico.mec_vlhora);
                cmd.Parameters.AddWithValue("mec_tipo", objMecanico.mec_tipo);
                cmd.Parameters.AddWithValue("mec_ende", objMecanico.mec_ende);
                cmd.Parameters.AddWithValue("mec_cida", objMecanico.mec_cida);
                cmd.Parameters.AddWithValue("mec_fone", objMecanico.mec_fone);
                cmd.Parameters.AddWithValue("mec_espec", objMecanico.mec_espec);
                cmd.Parameters.AddWithValue("mec_funcao", objMecanico.mec_funcao);
                cmd.Parameters.AddWithValue("mec_login", objMecanico.mec_login);
                cmd.Parameters.AddWithValue("mec_ativo", objMecanico.mec_tipo);
                cmd.Parameters.AddWithValue("mec_locali", objMecanico.mec_localiz);
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

        public static bool excluiMec(CL_Mecanico objMecanico, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM mecanico WHERE mec_cod=" + objMecanico.mec_cod;
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
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
                {
                    Conn.Close();
                }
            }
        }

        public static bool alteraSenha(CL_Mecanico objMecanico, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string senhaAnt;

            string sql = "SELECT * FROM mecanico WHERE mec_cod=" + objMecanico.mec_cod;

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
                        senhaAnt = dr["mec_senha"].ToString().Trim();
                        if (senhaAnt == objMecanico.mec_senAnt)
                        {
                            Conn.Close();
                            Conn.Open();
                            sql = "UPDATE mecanico SET mec_senha='" + objMecanico.mec_senNov + "' WHERE mec_cod=" + objMecanico.mec_cod;
                            comand = new NpgsqlCommand(sql, Conn);
                            comand.ExecuteScalar();
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
                {
                    Conn.Close();
                }
            }
        }
    }
}