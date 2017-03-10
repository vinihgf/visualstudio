using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Maquinario : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static List<CL_Maquinario> listar(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM veiculo ORDER BY sr_recno";
            CL_Maquinario objMaquinario = null;
            List<CL_Maquinario> objListMaquinario = new List<CL_Maquinario>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        objMaquinario = new CL_Maquinario();
                        objMaquinario.ve_placa = dr["ve_placa"].ToString().Trim();
                        objMaquinario.ve_marca = dr["ve_marca"].ToString().Trim();
                        objMaquinario.ve_chassi = dr["ve_chassi"].ToString().Trim();
                        objMaquinario.ve_concvda = dr["ve_concvda"].ToString().Trim();
                        objMaquinario.ve_anomod = dr["ve_anomod"].ToString().Trim();
                        objMaquinario.ve_anofab = dr["ve_anofab"].ToString().Trim();
                        objMaquinario.ve_modelo = dr["ve_modelo"].ToString().Trim();
                        objMaquinario.ve_serie = dr["ve_serie"].ToString().Trim();
                        if (dr["ve_dtcpa"].ToString().Trim() != "")
                            objMaquinario.ve_dtcpa = Convert.ToDateTime(dr["ve_dtcpa"]);
                        if (dr["ve_vctgar"].ToString().Trim() != "")
                            objMaquinario.ve_vctgar = Convert.ToDateTime(dr["ve_vctgar"]);
                        if (dr["ve_dtaliberacao"].ToString() != "")
                            objMaquinario.ve_dtaliberacao = Convert.ToDateTime(dr["ve_dtaliberacao"]);
                        objMaquinario.ve_instituicao = dr["ve_instituicao"].ToString().Trim();
                        objMaquinario.ve_potencia = dr["ve_potencia"].ToString().Trim();
                        objMaquinario.ve_ntcpa = Convert.ToInt32(dr["ve_ntcpa"]);
                        objMaquinario.ve_vlaqui = Convert.ToDouble(dr["ve_vlraqui"]);
                        objMaquinario.ve_percpropriet = Convert.ToDouble(dr["ve_percpropriet"]);
                        objMaquinario.ve_vlrhora = Convert.ToDouble(dr["ve_vlrhora"]);
                        objListMaquinario.Add(objMaquinario);
                    }
                    dr.Close();
                    return objListMaquinario;
                }
                else
                {
                    objListMaquinario = null;
                    return objListMaquinario;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListMaquinario = null;
                return objListMaquinario;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_Maquinario buscaMaquinario(string chassi, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM veiculo WHERE ve_chassi=@ve_chassi ORDER BY sr_recno";
            CL_Maquinario objMaquinario = new CL_Maquinario();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("ve_chassi", chassi);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objMaquinario.ve_placa = dr["ve_placa"].ToString().Trim();
                        objMaquinario.ve_marca = dr["ve_marca"].ToString().Trim();
                        objMaquinario.ve_chassi = dr["ve_chassi"].ToString().Trim();
                        objMaquinario.ve_concvda = dr["ve_concvda"].ToString().Trim();
                        objMaquinario.ve_anomod = dr["ve_anomod"].ToString().Trim();
                        objMaquinario.ve_anofab = dr["ve_anofab"].ToString().Trim();
                        objMaquinario.ve_modelo = dr["ve_modelo"].ToString().Trim();
                        objMaquinario.ve_serie = dr["ve_serie"].ToString().Trim();
                        if (dr["ve_dtcpa"].ToString().Trim() != "")
                            objMaquinario.ve_dtcpa = Convert.ToDateTime(dr["ve_dtcpa"]);
                        if (dr["ve_vctgar"].ToString().Trim() != "")
                            objMaquinario.ve_vctgar = Convert.ToDateTime(dr["ve_vctgar"]);
                        if (dr["ve_dtaliberacao"].ToString().Trim() != "")
                            objMaquinario.ve_dtaliberacao = Convert.ToDateTime(dr["ve_dtaliberacao"]);
                        objMaquinario.ve_instituicao = dr["ve_instituicao"].ToString().Trim();
                        objMaquinario.ve_potencia = dr["ve_potencia"].ToString().Trim();
                        objMaquinario.ve_ntcpa = Convert.ToInt32(dr["ve_ntcpa"]);
                        objMaquinario.ve_vlaqui = Convert.ToDouble(dr["ve_vlraqui"]);
                        objMaquinario.ve_percpropriet = Convert.ToDouble(dr["ve_percpropriet"]);
                        objMaquinario.ve_vlrhora = Convert.ToDouble(dr["ve_vlrhora"]);
                        dr.Close();
                        return objMaquinario;
                    }
                    else
                    {
                        objMaquinario = null;
                        return objMaquinario;
                    }
                }
                else
                {
                    objMaquinario = null;
                    return objMaquinario;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objMaquinario = null;
                return objMaquinario;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadMaquinario(CL_Maquinario objMaquinario, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "INSERT INTO veiculo (ve_marca, ve_chassi, ve_dtvenda, ve_concvda, ve_anomod, ve_anofab, ve_modelo, ve_placa, ve_serie, ve_dtcpa, ve_vctgar, " +
                    "ve_estmaq, ve_mdmotor, ve_alienada, ve_dtaliberacao, ve_instituicao, ve_potencia, ve_ntcpa, ve_prop, ve_vlraqui, ve_percpropriet, ve_vlrhora) " +
                    "VALUES " +
                    "(@ve_marca, @ve_chassi, @ve_dtvenda, @ve_concvda, @ve_anomod, @ve_anofab, @ve_modelo, @ve_placa, @ve_serie, @ve_dtcpa, @ve_vctgar, " +
                    "@ve_estmaq, @ve_mdmotor, @ve_alienada, @ve_dtaliberacao, @ve_instituicao, @ve_potencia, @ve_ntcpa, @ve_prop, @ve_vlraqui, @ve_percpropriet, @ve_vlrhora)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("ve_marca", objMaquinario.ve_marca);
                cmd.Parameters.AddWithValue("ve_chassi", objMaquinario.ve_chassi);
                cmd.Parameters.AddWithValue("ve_dtvenda", objMaquinario.ve_dtvenda.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_concvda", objMaquinario.ve_concvda);
                cmd.Parameters.AddWithValue("ve_anomod", objMaquinario.ve_anomod);
                cmd.Parameters.AddWithValue("ve_anofab", objMaquinario.ve_anofab);
                cmd.Parameters.AddWithValue("ve_modelo", objMaquinario.ve_modelo);
                cmd.Parameters.AddWithValue("ve_placa", objMaquinario.ve_placa);
                cmd.Parameters.AddWithValue("ve_serie", objMaquinario.ve_serie);
                cmd.Parameters.AddWithValue("ve_dtcpa", objMaquinario.ve_dtcpa.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_vctgar", objMaquinario.ve_vctgar.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_estmaq", objMaquinario.ve_estmaq);
                cmd.Parameters.AddWithValue("ve_mdmotor", objMaquinario.ve_mdmotor);
                cmd.Parameters.AddWithValue("ve_alienada", objMaquinario.ve_alienada);
                cmd.Parameters.AddWithValue("ve_dtaliberacao", objMaquinario.ve_dtaliberacao.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_instituicao", objMaquinario.ve_instituicao);
                cmd.Parameters.AddWithValue("ve_potencia", objMaquinario.ve_potencia);
                cmd.Parameters.AddWithValue("ve_ntcpa", objMaquinario.ve_ntcpa);
                cmd.Parameters.AddWithValue("ve_prop", objMaquinario.ve_prop);
                cmd.Parameters.AddWithValue("ve_vlraqui", objMaquinario.ve_vlaqui);
                cmd.Parameters.AddWithValue("ve_percpropriet", objMaquinario.ve_percpropriet);
                cmd.Parameters.AddWithValue("ve_vlrhora", objMaquinario.ve_vlrhora);

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

        public static bool alteraMaquinario(CL_Maquinario objMaquinario, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "UPDATE veiculo SET ve_marca=@ve_marca, ve_dtvenda=@ve_dtvenda, ve_concvda=@ve_concvda, ve_anomod=@ve_anomod, ve_anofab=@ve_anofab, " +
                    "ve_modelo=@ve_modelo, ve_placa=@ve_placa, ve_serie=@ve_serie, ve_dtcpa=@ve_dtcpa, ve_vctgar=@ve_vctgar, ve_estmaq=@ve_estmaq, ve_mdmotor=@ve_mdmotor, "+
                    "ve_alienada=@ve_alienada, ve_dtaliberacao=@ve_dtaliberacao, ve_instituicao=@ve_instituicao, ve_potencia=@ve_potencia, ve_ntcpa=@ve_ntcpa, " +
                    "ve_prop=@ve_prop, ve_vlraqui=@ve_vlraqui, ve_percpropriet=@ve_percpropriet, ve_vlrhora=@ve_vlrhora WHERE ve_chassi=@ve_chassi";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("ve_marca", objMaquinario.ve_marca);
                cmd.Parameters.AddWithValue("ve_chassi", objMaquinario.ve_chassi);
                cmd.Parameters.AddWithValue("ve_dtvenda", objMaquinario.ve_dtvenda.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_concvda", objMaquinario.ve_concvda);
                cmd.Parameters.AddWithValue("ve_anomod", objMaquinario.ve_anomod);
                cmd.Parameters.AddWithValue("ve_anofab", objMaquinario.ve_anofab);
                cmd.Parameters.AddWithValue("ve_modelo", objMaquinario.ve_modelo);
                cmd.Parameters.AddWithValue("ve_placa", objMaquinario.ve_placa);
                cmd.Parameters.AddWithValue("ve_serie", objMaquinario.ve_serie);
                cmd.Parameters.AddWithValue("ve_dtcpa", objMaquinario.ve_dtcpa.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_vctgar", objMaquinario.ve_vctgar.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_estmaq", objMaquinario.ve_estmaq);
                cmd.Parameters.AddWithValue("ve_mdmotor", objMaquinario.ve_mdmotor);
                cmd.Parameters.AddWithValue("ve_alienada", objMaquinario.ve_alienada);
                cmd.Parameters.AddWithValue("ve_dtaliberacao", objMaquinario.ve_dtaliberacao.ToShortDateString());
                cmd.Parameters.AddWithValue("ve_instituicao", objMaquinario.ve_instituicao);
                cmd.Parameters.AddWithValue("ve_potencia", objMaquinario.ve_potencia);
                cmd.Parameters.AddWithValue("ve_ntcpa", objMaquinario.ve_ntcpa);
                cmd.Parameters.AddWithValue("ve_prop", objMaquinario.ve_prop);
                cmd.Parameters.AddWithValue("ve_vlraqui", objMaquinario.ve_vlaqui);
                cmd.Parameters.AddWithValue("ve_percpropriet", objMaquinario.ve_percpropriet);
                cmd.Parameters.AddWithValue("ve_vlrhora", objMaquinario.ve_vlrhora);

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

        public static bool excluiMaquinario(CL_Maquinario objMaquinario, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                Conn.Open();
                string sql = "DELETE FROM veiculo WHERE ve_chassi='" + objMaquinario.ve_chassi + "'";
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
    }
}
 