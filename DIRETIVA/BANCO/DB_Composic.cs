using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BANCO
{
    public class DB_Composic : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static CL_Composic buscaCod(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT com_cod FROM composic ORDER BY com_cod DESC LIMIT 1";

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
                        objComposic.com_cod = Convert.ToInt32(dr["com_cod"]) + 1;
                        return objComposic;
                    }
                    else
                        return null;
                }
                else
                {
                    objComposic.com_cod = 1;
                    return objComposic;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objComposic = null;
                return objComposic;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static CL_Composic buscaCodf(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT com_codf FROM composic WHERE com_cod=@com_cod ORDER BY com_codf DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("com_cod", objComposic.com_cod);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objComposic.com_codf = Convert.ToInt32(dr["com_codf"]) + 1;
                        return objComposic;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    objComposic.com_codf = 1;
                    return objComposic;
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
        }

        public static CL_Composic buscaComposic(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM composic WHERE com_cod=@com_cod ORDER BY com_codf DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("com_cod", objComposic.com_cod);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objComposic.com_nome = dr["com_nome"].ToString().Trim();
                        objComposic.com_codf = Convert.ToInt32(dr["com_codf"]) + 1;
                        objComposic.com_situac = dr["com_situac"].ToString().Trim();
                        return objComposic;
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

        public static CL_Composic buscaComposicf(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM composic WHERE com_cod=@com_cod AND com_codf=@com_codf";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("com_cod", objComposic.com_cod);
            comand.Parameters.AddWithValue("com_codf", objComposic.com_codf);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objComposic.com_famil = dr["com_famil"].ToString().Trim();
                        objComposic.com_linha = dr["com_linha"].ToString().Trim();
                        objComposic.com_modelo = dr["com_modelo"].ToString().Trim();
                        return objComposic;
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

        public static bool cadComposic(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO composic (com_cod, com_codf, com_famil, com_linha, com_modelo, com_nome, com_situac) VALUES (@com_cod, @com_codf, @com_famil, @com_linha, @com_modelo, @com_nome, @com_situac)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("com_cod", objComposic.com_cod);
                comand.Parameters.AddWithValue("com_codf", objComposic.com_codf);
                comand.Parameters.AddWithValue("com_famil", objComposic.com_famil);
                comand.Parameters.AddWithValue("com_linha", objComposic.com_linha);
                comand.Parameters.AddWithValue("com_modelo", objComposic.com_modelo);
                comand.Parameters.AddWithValue("com_nome", objComposic.com_nome);
                comand.Parameters.AddWithValue("com_situac", objComposic.com_situac);

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

        public static bool alteraComposic(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE composic SET com_nome='" + objComposic.com_nome + "', com_famil='" + objComposic.com_famil + "', com_linha='" + objComposic.com_linha +
                    "', com_modelo='" + objComposic.com_modelo + "', com_situac='" + objComposic.com_situac + "' WHERE com_cod=" + objComposic.com_cod + " AND com_codf=" + objComposic.com_codf;

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
                    Conn.Close();
            }
        }

        public static bool excluiComposic(CL_Composic objComposic, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM composic WHERE com_cod=" + objComposic.com_cod + " AND com_codf=" + objComposic.com_codf;

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
                    Conn.Close();
            }
        }

        public static List<CL_Composic> buscaGrupos(List<CL_Composic> objListGrupo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT com_cod, com_nome, com_situac FROM composic ORDER BY com_cod";
            CL_Composic obj = null;
            List<CL_Composic> objListComposic = new List<CL_Composic>();

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
                        obj = new CL_Composic();
                        //leio as informações dos campos e jogo para o objeto
                        obj.com_cod = Convert.ToInt32(dr["com_cod"]);
                        obj.com_nome = dr["com_nome"].ToString().Trim();
                        obj.com_situac = dr["com_situac"].ToString().Trim();
                        objListGrupo.Add(obj);
                    }

                    CL_Composic objZ = new CL_Composic();

                    for (int y = 0; y < objListGrupo.Count; y++)
                    {
                        CL_Composic objComposic = objListGrupo.ElementAt(y);
                        bool exist = false;
                        if (objListComposic.Count > 0)
                        {
                            for (int z = 0; z < objListComposic.Count; z++)
                            {
                                objZ = objListComposic.ElementAt(z);
                                if (objZ.com_nome == objComposic.com_nome)
                                    exist = true;
                                else
                                    exist = false;
                            }
                            if (!exist)
                                objListComposic.Add(objComposic);
                        }
                        else
                            objListComposic.Add(objComposic);
                    }
                    dr.Close();
                    return objListComposic;
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

        public static List<CL_Composic> buscaSubs(string con_grupo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT com_codf, com_famil FROM composic WHERE com_cod=" + con_grupo + " ORDER BY com_codf";
            CL_Composic obj = null;
            List<CL_Composic> objListSubGrupo = new List<CL_Composic>();

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
                        obj = new CL_Composic();
                        //leio as informações dos campos e jogo para o objeto
                        obj.com_codf = dr["com_codf"] is DBNull ? 0 : Convert.ToInt32(dr["com_codf"]);
                        obj.com_famil = dr["com_famil"].ToString().Trim();
                        objListSubGrupo.Add(obj);
                    }
                }
                dr.Close();
                return objListSubGrupo;
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