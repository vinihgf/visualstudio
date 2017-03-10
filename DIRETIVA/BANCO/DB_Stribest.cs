using CLASSES;
using Npgsql;
using System;
using System.Data;
using System.Collections.Generic;

namespace BANCO
{
    public class DB_Stribest : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static CL_Stribest buscaStribest(int tribut, string estorg, string estdst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Stribest objStribest = new CL_Stribest();
            string sql = "SELECT * FROM stribest WHERE s_estorg='" + estorg + "' AND s_estdst='" + estdst + "' AND s_tribut=" + tribut;

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
                        objStribest.s_cod = Convert.ToInt32(dr["s_cod"]);
                        objStribest.s_tribut = Convert.ToInt32(dr["s_tribut"]);
                        objStribest.s_estorg = dr["s_estorg"].ToString().Trim();
                        objStribest.s_estdst = dr["s_estdst"].ToString().Trim();
                        objStribest.s_base = Convert.ToDouble(dr["s_base"]);
                        objStribest.s_aliq = Convert.ToDouble(dr["s_aliq"]);
                        objStribest.s_isent = Convert.ToDouble(dr["s_isent"]);
                        objStribest.s_outra = Convert.ToDouble(dr["s_outra"]);
                        objStribest.s_st = dr["s_st"].ToString().Trim();
                        objStribest.s_basest = Convert.ToDouble(dr["s_basest"]);
                        objStribest.s_icmst = Convert.ToDouble(dr["s_icmst"]);
                        objStribest.s_cst = dr["s_cst"].ToString().Trim();
                        objStribest.s_cfop = dr["s_cfop"].ToString().Trim();

                        return objStribest;
                    }
                    else
                    {
                        objStribest = null;
                        return objStribest;
                    }
                }
                else
                {
                    objStribest = null;
                    return objStribest;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objStribest = null;
                return objStribest;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool excluiParametro(CL_Stribest objStribest, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "DELETE FROM stribest WHERE s_cod=" + objStribest.s_cod;

                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
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

        public static bool alteraParametro(CL_Stribest objStribest, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "UPDATE stribest SET s_icmdst=@s_icmdst, s_basedst=@s_basedst, s_fcp=@s_fcp, s_tribut=@s_tribut, s_estorg=@s_estorg, s_estdst=@s_estdst, " +
                    "s_base=@s_base, s_aliq=@s_aliq, s_isent=@s_isent, s_outra=@s_outra, s_st=@s_st, s_basest=@s_basest, s_icmst=@s_icmst, s_cst=@s_cst, s_cfop=@s_cfop " +
                    "WHERE s_cod=@s_cod";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("s_icmdst", objStribest.s_icmdst);
                cmd.Parameters.AddWithValue("s_basedst", objStribest.s_basedst);
                cmd.Parameters.AddWithValue("s_fcp", objStribest.s_fcp);
                cmd.Parameters.AddWithValue("s_cod", objStribest.s_cod);
                cmd.Parameters.AddWithValue("s_tribut", objStribest.s_tribut);
                cmd.Parameters.AddWithValue("s_estorg", objStribest.s_estorg);
                cmd.Parameters.AddWithValue("s_estdst", objStribest.s_estdst);
                cmd.Parameters.AddWithValue("s_base", objStribest.s_base);
                cmd.Parameters.AddWithValue("s_aliq", objStribest.s_aliq);
                cmd.Parameters.AddWithValue("s_isent", objStribest.s_isent);
                cmd.Parameters.AddWithValue("s_outra", objStribest.s_outra);
                cmd.Parameters.AddWithValue("s_st", objStribest.s_st);
                cmd.Parameters.AddWithValue("s_basest", objStribest.s_basest);
                cmd.Parameters.AddWithValue("s_icmst", objStribest.s_icmst);
                cmd.Parameters.AddWithValue("s_cst", objStribest.s_cst);
                cmd.Parameters.AddWithValue("s_cfop", objStribest.s_cfop);

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

        public static bool cadParametro(CL_Stribest objStribest, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO stribest (s_icmdst, s_basedst, s_fcp, s_cod, s_tribut, s_estorg, s_estdst, s_base, s_aliq, s_isent, s_outra, s_st, s_basest, " +
                    "s_icmst, s_cst, s_cfop) VALUES (@s_icmdst, @s_basedst, @s_fcp, @s_cod, @s_tribut, @s_estorg, @s_estdst, @s_base, @s_aliq, @s_isent, @s_outra, @s_st, " +
                    "@s_basest, @s_icmst, @s_cst, @s_cfop)";
                    
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("s_icmdst", objStribest.s_icmdst);
                cmd.Parameters.AddWithValue("s_basedst", objStribest.s_basedst);
                cmd.Parameters.AddWithValue("s_fcp", objStribest.s_fcp);
                cmd.Parameters.AddWithValue("s_cod", objStribest.s_cod);
                cmd.Parameters.AddWithValue("s_tribut", objStribest.s_tribut);
                cmd.Parameters.AddWithValue("s_estorg", objStribest.s_estorg);
                cmd.Parameters.AddWithValue("s_estdst", objStribest.s_estdst);
                cmd.Parameters.AddWithValue("s_base", objStribest.s_base);
                cmd.Parameters.AddWithValue("s_aliq", objStribest.s_aliq);
                cmd.Parameters.AddWithValue("s_isent", objStribest.s_isent);
                cmd.Parameters.AddWithValue("s_outra", objStribest.s_outra);
                cmd.Parameters.AddWithValue("s_st", objStribest.s_st);
                cmd.Parameters.AddWithValue("s_basest", objStribest.s_basest);
                cmd.Parameters.AddWithValue("s_icmst", objStribest.s_icmst);
                cmd.Parameters.AddWithValue("s_cst", objStribest.s_cst);
                cmd.Parameters.AddWithValue("s_cfop", objStribest.s_cfop);

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

        public static CL_Stribest buscaStribestCod(int s_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Stribest objStribest = new CL_Stribest();
            string sql = "SELECT * FROM stribest WHERE s_cod=@s_cod";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("s_cod", s_cod);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objStribest.s_cod = Convert.ToInt32(dr["s_cod"]);
                        objStribest.s_tribut = Convert.ToInt32(dr["s_tribut"]);
                        objStribest.s_estorg = dr["s_estorg"].ToString().Trim();
                        objStribest.s_estdst = dr["s_estdst"].ToString().Trim();
                        objStribest.s_base = Convert.ToDouble(dr["s_base"]);
                        objStribest.s_aliq = Convert.ToDouble(dr["s_aliq"]);
                        objStribest.s_isent = Convert.ToDouble(dr["s_isent"]);
                        objStribest.s_outra = Convert.ToDouble(dr["s_outra"]);
                        objStribest.s_st = dr["s_st"].ToString().Trim();
                        objStribest.s_basest = Convert.ToDouble(dr["s_basest"]);
                        objStribest.s_icmst = Convert.ToDouble(dr["s_icmst"]);
                        objStribest.s_cst = dr["s_cst"].ToString().Trim();
                        objStribest.s_cfop = dr["s_cfop"].ToString().Trim();
                        objStribest.s_icmdst = Convert.ToDouble(dr["s_icmdst"]);
                        objStribest.s_basedst = Convert.ToDouble(dr["s_basedst"]);
                        objStribest.s_fcp = Convert.ToDouble(dr["s_fcp"]);

                        return objStribest;
                    }
                    else
                    {
                        objStribest = null;
                        return objStribest;
                    }
                }
                else
                {
                    objStribest = null;
                    return objStribest;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objStribest = null;
                return objStribest;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Stribest> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM stribest ORDER BY s_tribut, s_cod";
            CL_Stribest objStribest = null;
            List<CL_Stribest> objListStribest = new List<CL_Stribest>();

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
                        objStribest = new CL_Stribest();
                        objStribest.s_cod = Convert.ToInt32(dr["s_cod"]);
                        objStribest.s_tribut = Convert.ToInt32(dr["s_tribut"]);
                        objStribest.s_estorg = dr["s_estorg"].ToString().Trim();
                        objStribest.s_estdst = dr["s_estdst"].ToString().Trim();
                        objStribest.s_base = Convert.ToDouble(dr["s_base"]);
                        objStribest.s_aliq = Convert.ToDouble(dr["s_aliq"]);
                        objStribest.s_isent = Convert.ToDouble(dr["s_isent"]);
                        objStribest.s_outra = Convert.ToDouble(dr["s_outra"]);
                        objStribest.s_st = dr["s_st"].ToString().Trim();
                        objStribest.s_basest = Convert.ToDouble(dr["s_basest"]);
                        objStribest.s_icmst = Convert.ToDouble(dr["s_icmst"]);
                        objStribest.s_cst = dr["s_cst"].ToString().Trim();
                        objStribest.s_cfop = dr["s_cfop"].ToString().Trim();
                        objStribest.s_icmdst = Convert.ToDouble(dr["s_icmdst"]);
                        objStribest.s_basedst = Convert.ToDouble(dr["s_basedst"]);
                        objStribest.s_fcp = Convert.ToDouble(dr["s_fcp"]);
                        objListStribest.Add(objStribest);
                    }
                    dr.Close();
                    return objListStribest;
                }
                else
                {
                    objListStribest = null;
                    return objListStribest;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListStribest = null;
                return objListStribest;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            int s_cod = 0;
            string sql = "SELECT s_cod FROM stribest ORDER BY s_cod DESC LIMIT 1";

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
                        s_cod = Convert.ToInt32(dr["s_cod"]);
                        s_cod = s_cod + 1;

                        return s_cod;
                    }
                    else
                    {
                        s_cod = 0;
                        return s_cod;
                    }
                }
                else
                {
                    s_cod = 1;
                    return s_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                s_cod = 0;
                return s_cod;
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