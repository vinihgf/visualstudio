using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Equipamento : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCod(int e_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT e_cod FROM equipamento ORDER BY e_cod DESC LIMIT 1";

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
                        e_cod = Convert.ToInt16(dr["e_cod"]);
                        e_cod = e_cod + 1;
                        return e_cod;
                    }
                    else
                    {
                        e_cod = 0;
                        return e_cod;
                    }
                }
                else
                {
                    e_cod = 1;
                    return e_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                e_cod = 0;
                return e_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static CL_Equipamento buscaEquip(CL_Equipamento objEquipamento, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM equipamento WHERE e_cod=" + objEquipamento.e_cod + " ORDER BY e_cod";

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
                        objEquipamento.e_descri = dr["e_descri"].ToString().Trim();
                        objEquipamento.e_marca = dr["e_marca"].ToString().Trim();
                        objEquipamento.e_modelo = dr["e_modelo"].ToString().Trim();
                        objEquipamento.e_nmarca = Convert.ToInt32(dr["e_nmarca"]);
                        objEquipamento.e_nmodelo = Convert.ToInt32(dr["e_nmodelo"]);
                        objEquipamento.e_nPatrimon = dr["e_patrimon"].ToString().Trim();
                        objEquipamento.e_nserie = dr["e_nserie"].ToString().Trim();
                        return objEquipamento;
                    }
                    else
                    {
                        objEquipamento = null;
                        return objEquipamento;
                    }
                }
                else
                {
                    objEquipamento = null;
                    return objEquipamento;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objEquipamento = null;
                return objEquipamento;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static List<CL_Equipamento> listar(CL_ComlModelo objComlModelo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM equipamento WHERE e_nmarca=@e_nmarca AND e_nmodelo=@e_nmodelo ORDER BY e_cod";

            List<CL_Equipamento> objList = new List<CL_Equipamento>();
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("e_nmarca", objComlModelo.m_marca);
            comand.Parameters.AddWithValue("e_nmodelo", objComlModelo.m_codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Equipamento()
                        {
                            e_cod = dr["e_cod"] is DBNull ? 0 : Convert.ToInt32(dr["e_cod"]),
                            e_nserie = dr["e_nserie"].ToString().Trim(),
                            e_ncontrato = dr["e_ncontrato"] is DBNull ? 0 : Convert.ToInt32(dr["e_ncontrato"]),
                            e_nPatrimon = dr["e_patrimon"].ToString().Trim(),
                            e_nloca = dr["e_nloca"] is DBNull ? 0 : Convert.ToInt32(dr["e_nloca"]),
                            e_renova = dr["e_renova"] is DBNull ? 0 : Convert.ToInt32(dr["e_renova"]),
                            e_nmodelo  = objComlModelo.m_codigo,
                            e_nmarca = objComlModelo.m_marca,
                            e_marca = dr["e_marca"].ToString().Trim(),
                            e_descri = dr["e_descri"].ToString().Trim(),
                            e_modelo = dr["e_modelo"].ToString().Trim(),
                        });
                    }
                    return objList;
                }
                else
                {
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

        public static bool cadEquip(CL_Equipamento objEquipamento, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            try
            {
                string sql = "INSERT INTO equipamento (e_cod, e_nmarca, e_marca, e_nmodelo, e_modelo, e_descri, e_patrimon, e_nserie, e_clicod, e_cliente, e_ncontrato, e_nloca, e_renova) " +
                "VALUES (@e_cod, @e_nmarca, @e_marca, @e_nmodelo, @e_modelo, @e_descri, @e_patrimon, @e_nserie, @e_clicod, @e_cliente, @e_ncontrato, @e_nloca, @e_renova)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("e_cod", objEquipamento.e_cod);
                comand.Parameters.AddWithValue("e_nmarca", objEquipamento.e_nmarca);
                comand.Parameters.AddWithValue("e_marca", objEquipamento.e_marca);
                comand.Parameters.AddWithValue("e_nmodelo", objEquipamento.e_nmodelo);
                comand.Parameters.AddWithValue("e_modelo", objEquipamento.e_modelo);
                comand.Parameters.AddWithValue("e_descri", objEquipamento.e_descri);
                comand.Parameters.AddWithValue("e_patrimon", objEquipamento.e_nPatrimon);
                comand.Parameters.AddWithValue("e_nserie", objEquipamento.e_nserie);
                comand.Parameters.AddWithValue("e_clicod", objEquipamento.e_clicod);
                comand.Parameters.AddWithValue("e_cliente", objEquipamento.e_cliente);
                comand.Parameters.AddWithValue("e_ncontrato", objEquipamento.e_ncontrato);
                comand.Parameters.AddWithValue("e_nloca", objEquipamento.e_nloca);
                comand.Parameters.AddWithValue("e_renova", objEquipamento.e_renova);


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

        public static bool alteraEquip(CL_Equipamento objEquipamento, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "UPDATE equipamento SET e_nmarca=@e_nmarca, e_nmodelo=@e_nmodelo, e_patrimon=@e_patrimon, e_marca=@e_marca, " +
                "e_modelo=@e_modelo, e_nserie=e_nserie, e_descri=@e_descri WHERE e_cod=@e_cod";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("e_nmarca", objEquipamento.e_nmarca);
            comand.Parameters.AddWithValue("e_nmodelo", objEquipamento.e_nmodelo);
            comand.Parameters.AddWithValue("e_patrimon", objEquipamento.e_nPatrimon);
            comand.Parameters.AddWithValue("e_marca", objEquipamento.e_marca);
            comand.Parameters.AddWithValue("e_modelo", objEquipamento.e_modelo);
            comand.Parameters.AddWithValue("e_nserie", objEquipamento.e_nserie);
            comand.Parameters.AddWithValue("e_descri", objEquipamento.e_descri);
            comand.Parameters.AddWithValue("e_cod", objEquipamento.e_cod);

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

        public static bool excluiEquip(CL_Equipamento objEquipamento, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM equipamento WHERE e_cod=" + objEquipamento.e_cod + " AND e_ncontrato=0";
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

        public static CL_Equipamento buscaEquipPatrimon(CL_Equipamento objEquipamento, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM equipamento WHERE e_patrimon='" + objEquipamento.e_nPatrimon + "' ORDER BY e_cod";

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
                        objEquipamento.e_descri = dr["e_descri"].ToString().Trim();
                        objEquipamento.e_marca = dr["e_marca"].ToString().Trim();
                        objEquipamento.e_modelo = dr["e_modelo"].ToString().Trim();
                        objEquipamento.e_nmarca = Convert.ToInt32(dr["e_nmarca"]);
                        objEquipamento.e_nmodelo = Convert.ToInt32(dr["e_nmodelo"]);
                        objEquipamento.e_nPatrimon = dr["e_patrimon"].ToString().Trim();
                        objEquipamento.e_nserie = dr["e_nserie"].ToString().Trim();
                        objEquipamento.e_ncontrato = Convert.ToInt32(dr["e_ncontrato"]);
                        return objEquipamento;
                    }
                    else
                    {
                        objEquipamento = null;
                        return objEquipamento;
                    }
                }
                else
                {
                    objEquipamento = null;
                    return objEquipamento;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objEquipamento = null;
                return objEquipamento;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }        

        public static bool verificaPatrimon(string e_nPatrimon, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM equipamento WHERE e_patrimon='" + e_nPatrimon + "'";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    return true;
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

        public static bool verificaSerie(string e_nSerie, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM equipamento WHERE e_nserie='" + e_nSerie + "'";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    return true;
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