using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Ccusto : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCodigo(int c_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT c_id FROM ccustos ORDER BY c_id DESC LIMIT 1";

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
                        c_id = dr["c_id"] is DBNull ? -1 : Convert.ToInt32(dr["c_id"]);
                        c_id = c_id + 1;
                        return c_id;
                    }
                    else
                    {
                        c_id = 0;
                        return c_id;
                    }
                }
                else
                {
                    c_id = 1;
                    return c_id;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                c_id = 0;
                return c_id;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_Ccusto buscaCcusto(CL_Ccusto objCcusto, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM ccustos WHERE c_id=@c_id ORDER BY c_id";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("c_id", objCcusto.c_id);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objCcusto.c_descri = dr["c_descri"].ToString().Trim();
                        objCcusto.c_modelo = dr["c_modelo"].ToString().Trim();
                        objCcusto.c_tipo = dr["c_tipo"].ToString().Trim();
                        objCcusto.c_tipodesc = dr["c_tipodesc"].ToString().Trim();
                        return objCcusto;
                    }
                    else
                    {
                        objCcusto = null;
                        return objCcusto;
                    }
                }
                else
                {
                    objCcusto = null;
                    return objCcusto;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objCcusto = null;
                return objCcusto;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool excluiCcusto(CL_Ccusto objCcusto, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM ccustos WHERE c_id=" + objCcusto.c_id;

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
                    string sql2 = "DELETE FROM ccustos WHERE c_id=" + objCcusto.c_id;
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

        public static bool alteraCcusto(CL_Ccusto objCcusto, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE ccustos SET c_descri=@c_descri, c_tipo=@c_tipo, c_tipodesc=@c_tipodesc, " +
                    "c_modelo=@c_modelo' WHERE c_id=@c_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("c_descri", objCcusto.c_descri);
                comand.Parameters.AddWithValue("c_tipo", objCcusto.c_tipo);
                comand.Parameters.AddWithValue("c_tipodesc", objCcusto.c_tipodesc);
                comand.Parameters.AddWithValue("c_modelo", objCcusto.c_modelo);
                comand.Parameters.AddWithValue("c_id", objCcusto.c_id);
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

        public static bool cadCcusto(CL_Ccusto objCcusto, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO ccustos (c_id,c_descri,c_tipo,c_tipodesc,c_modelo) VALUES (@c_id,@c_descri,@c_tipo,@c_tipodesc,@c_modelo)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("c_id", objCcusto.c_id);
                comand.Parameters.AddWithValue("c_descri", objCcusto.c_descri);
                comand.Parameters.AddWithValue("c_tipo", objCcusto.c_tipo);
                comand.Parameters.AddWithValue("c_tipodesc", objCcusto.c_tipodesc);
                comand.Parameters.AddWithValue("c_modelo", objCcusto.c_modelo);

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

        public static List<CL_Ccusto> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM ccustos ORDER BY c_id";
            List<CL_Ccusto> objList = new List<CL_Ccusto>();
            CL_Ccusto obj = null;

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
                        obj = new CL_Ccusto();
                        //leio as informações dos campos e jogo para o objeto
                        obj.c_id = Convert.ToInt32(dr["c_id"]);
                        obj.c_descri = dr["c_descri"].ToString().Trim();
                        obj.c_tipo = dr["c_tipo"].ToString().Trim();
                        obj.c_modelo = dr["c_modelo"].ToString().Trim();
                        objList.Add(obj);
                    }
                    dr.Close();
                    return objList;
                }
                else
                {
                    objList = null;
                    return objList;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objList = null;
                return objList;
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