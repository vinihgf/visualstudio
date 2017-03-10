using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BANCO
{
    public class DB_Visita : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static bool incluiVisita(CL_Visita objVisita, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO visitas (v_lcto, v_clicod, v_cnpj, v_desc, v_hist1, v_prxvis, v_ultvisit, v_vend, v_idumov, v_situac, v_foto, v_assina) " +
                    "VALUES " +
                    "(@v_lcto, @v_clicod, @v_cnpj, @v_desc, @v_hist1, @v_prxvis, @v_ultvisit, @v_vend, @v_idumov, @v_situac, @v_foto, @v_assina)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("v_lcto", objVisita.v_lcto);
                comand.Parameters.AddWithValue("v_clicod", objVisita.v_clicod);
                comand.Parameters.AddWithValue("v_cnpj", objVisita.v_cnpj);
                comand.Parameters.AddWithValue("v_desc", objVisita.v_desc);
                comand.Parameters.AddWithValue("v_hist1", objVisita.v_hist1);
                comand.Parameters.AddWithValue("v_prxvis", objVisita.v_prxvis.ToShortDateString());
                comand.Parameters.AddWithValue("v_ultvisit", objVisita.v_ultvisit.ToShortDateString());
                comand.Parameters.AddWithValue("v_vend", objVisita.v_vend);
                comand.Parameters.AddWithValue("v_idumov", objVisita.v_idumov);
                comand.Parameters.AddWithValue("v_situac", objVisita.v_situac);
                comand.Parameters.AddWithValue("v_foto", objVisita.v_foto);
                comand.Parameters.AddWithValue("v_assina", objVisita.v_assina);

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
        public static List<CL_Visita> pesqVisita(string sql, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            List<CL_Visita> objList = new List<CL_Visita>();
            CL_Visita objVisita = null;

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
                        objVisita = new CL_Visita();
                        objVisita.v_lcto = Convert.ToInt32(dr["v_lcto"]);
                        objVisita.v_clicod = Convert.ToInt32(dr["v_clicod"]);
                        objVisita.v_clinome = dr["p_nome"].ToString().Trim();
                        objVisita.v_ultvisit = Convert.ToDateTime(dr["v_ultvisit"]);
                        objVisita.v_vend = Convert.ToInt32(dr["v_vend"]);
                        objVisita.v_vendnom = dr["con_nome"].ToString().Trim();
                        objVisita.v_desc = dr["v_desc"].ToString().Trim();
                        objVisita.v_prxvis = Convert.ToDateTime(dr["v_prxvis"]);
                        objVisita.v_hist1 = dr["v_hist1"].ToString().Trim();
                        objVisita.v_idumov = Convert.ToInt64(dr["v_idumov"]);
                        objVisita.v_situac = dr["v_situac"].ToString().Trim();
                        objVisita.v_cnpj = dr["p_cgc"].ToString().Trim();
                        objList.Add(objVisita);
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
        public static int buscaCod(int v_lcto, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT v_lcto FROM visitas ORDER BY v_lcto DESC LIMIT 1";

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
                        v_lcto = Convert.ToInt16(dr["v_lcto"]);
                        v_lcto = v_lcto + 1;

                        return v_lcto;
                    }
                    else
                    {
                        v_lcto = 0;
                        return v_lcto;
                    }
                }
                else
                {
                    v_lcto = 1;
                    return v_lcto;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                v_lcto = 0;
                return v_lcto;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static CL_Visita buscaVisitaIDUmov(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT v_lcto, v_clicod, p_nome, v_idumov, con_nome, v_hist1, v_desc " +
                "FROM visitas, convenio, particip WHERE v_idumov=@v_idumov AND p_cod=v_clicod AND v_vend=con_cod ORDER BY v_lcto";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("v_idumov", idUmov);
            NpgsqlDataReader dr;
            CL_Visita obj = new CL_Visita();

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        obj.v_lcto = Convert.ToInt32(dr["v_lcto"]);
                        obj.v_clicod = Convert.ToInt32(dr["v_clicod"]);
                        obj.v_clinome = dr["p_nome"].ToString().Trim();
                        obj.v_idumov = idUmov;
                        obj.v_vendnom = dr["con_nome"].ToString().Trim();
                        obj.v_hist1 = dr["v_hist1"].ToString().Trim();
                        obj.v_desc = dr["v_desc"].ToString().Trim();

                        return obj;
                    }
                    else
                    {
                        obj = null;
                        return obj;
                    }
                }
                else
                {
                    obj = null;
                    return obj;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                obj = null;
                return obj;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static bool verificaVisita(string v_lcto, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT v_lcto FROM visitas WHERE v_lcto=" + v_lcto + " AND v_situac='S'";
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
        public static bool verificaUmov(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT v_lcto FROM visitas WHERE v_idumov=" + idUmov;
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
    }
}