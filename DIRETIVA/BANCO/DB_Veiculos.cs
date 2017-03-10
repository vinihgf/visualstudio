using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Veiculos : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static bool excluiVeiculo(CL_Veiculos objVeiculos, string con)
        {
            string sql = "DELETE FROM veiculo WHERE ve_chassi =('" + objVeiculos.ve_chassi + "')";

            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                Conn.Open();
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
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
        public static CL_Veiculos verificaVeiculos(CL_Veiculos objVeiculos, string con)
        {
            string sql = "SELECT * FROM veiculo WHERE ve_chassi =('" + objVeiculos.ve_chassi + "')";

            DB_Funcoes.DesmontaConexao(con);
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
                        objVeiculos.ve_chassi = dr["ve_chassi"].ToString().Trim();
                        objVeiculos.ve_placa = dr["ve_placa"].ToString().Trim();
                        objVeiculos.ve_serie = dr["ve_serie"].ToString().Trim();
                        objVeiculos.ve_rrae = dr["ve_rrae"].ToString().Trim();
                        objVeiculos.ve_nomprop = dr["ve_nompro"].ToString().Trim();
                        objVeiculos.ve_prop = dr["ve_prop"].ToString().Trim();
                        objVeiculos.ve_linha = dr["ve_linha"].ToString().Trim();
                        objVeiculos.ve_marca = dr["ve_marca"].ToString().Trim();
                        objVeiculos.ve_anofab = dr["ve_anofab"].ToString().Trim();
                        objVeiculos.ve_modelo = dr["ve_modelo"].ToString().Trim();
                        objVeiculos.ve_anomod = dr["ve_anomod"].ToString().Trim();
                        objVeiculos.ve_cup1 = dr["ve_cup1"].ToString().Trim();
                        objVeiculos.ve_cup2 = dr["ve_cup2"].ToString().Trim();
                        objVeiculos.ve_cup3 = dr["ve_cup3"].ToString().Trim();
                        objVeiculos.ve_cup4 = dr["ve_cup4"].ToString().Trim();
                        objVeiculos.ve_cup5 = dr["ve_cup5"].ToString().Trim();
                        objVeiculos.ve_estmaq = dr["ve_estmaq"].ToString().Trim();
                        return objVeiculos;
                    }
                }
                objVeiculos.ve_chassi = null;
                return objVeiculos;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return objVeiculos;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        public static bool incluiVeiculo(CL_Veiculos objVeiculos, string con)
        {
            string sql = "INSERT INTO veiculo (ve_chassi, ve_placa, ve_serie, ve_rrae, ve_nompro, ve_prop, ve_linha, ve_marca, ve_anofab, ve_modelo, ve_anomod, ve_cup1, ve_cup2, ve_cup3, ve_cup4, ve_cup5, ve_estmaq)" +
                "VALUES (@ve_chassi, @ve_placa, @ve_serie, @ve_rrae, @ve_nompro, @ve_prop, @ve_linha, @ve_marca, @ve_anofab, @ve_modelo, @ve_anomod, @ve_cup1, @ve_cup2, @ve_cup3, @ve_cup4, @ve_cup5, @ve_estmaq)";

            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("ve_chassi", objVeiculos.ve_chassi);
                comand.Parameters.AddWithValue("ve_placa", objVeiculos.ve_placa);
                comand.Parameters.AddWithValue("ve_serie", objVeiculos.ve_serie);
                comand.Parameters.AddWithValue("ve_rrae", objVeiculos.ve_rrae);
                comand.Parameters.AddWithValue("ve_nompro", objVeiculos.ve_nomprop);
                comand.Parameters.AddWithValue("ve_prop", objVeiculos.ve_prop);
                comand.Parameters.AddWithValue("ve_linha", objVeiculos.ve_linha);
                comand.Parameters.AddWithValue("ve_marca", objVeiculos.ve_marca);
                comand.Parameters.AddWithValue("ve_anofab", objVeiculos.ve_anofab);
                comand.Parameters.AddWithValue("ve_modelo", objVeiculos.ve_modelo);
                comand.Parameters.AddWithValue("ve_anomod", objVeiculos.ve_anomod);
                comand.Parameters.AddWithValue("ve_cup1", objVeiculos.ve_cup1);
                comand.Parameters.AddWithValue("ve_cup2", objVeiculos.ve_cup2);
                comand.Parameters.AddWithValue("ve_cup3", objVeiculos.ve_cup3);
                comand.Parameters.AddWithValue("ve_cup4", objVeiculos.ve_cup4);
                comand.Parameters.AddWithValue("ve_cup5", objVeiculos.ve_cup5);
                comand.Parameters.AddWithValue("ve_estmaq", objVeiculos.ve_estmaq);

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
        public static bool alteraVeiculo(CL_Veiculos objVeiculos, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                String sql = "UPDATE veiculo SET ve_chassi='" + objVeiculos.ve_chassi + "', ve_placa='" + objVeiculos.ve_placa + "', ve_serie='" + objVeiculos.ve_serie +
                "', ve_rrae='" + objVeiculos.ve_rrae + "', ve_nompro='" + objVeiculos.ve_nomprop + "', ve_prop='" + objVeiculos.ve_prop + "', ve_linha='" + objVeiculos.ve_linha +
                "', ve_marca='" + objVeiculos.ve_marca + "', ve_anofab='" + objVeiculos.ve_anofab + "', ve_modelo='" + objVeiculos.ve_modelo + "', ve_anomod='" + objVeiculos.ve_anomod +
                "', ve_cup1='" + objVeiculos.ve_cup1 + "', ve_cup2='" + objVeiculos.ve_cup2 + "', ve_cup3='" + objVeiculos.ve_cup3 + "', ve_cup4='" + objVeiculos.ve_cup4 +
                "', ve_cup5='" + objVeiculos.ve_cup5 + "', ve_estmaq='" + objVeiculos.ve_estmaq + "' WHERE ve_chassi= '" + objVeiculos.ve_chassi + "'";

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
                {
                    Conn.Close();
                }
            }
        }

        public List<CL_Veiculos> listar(string pesquisa, string con, string filtroPesq)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            List<CL_Veiculos> objList = new List<CL_Veiculos>();
            CL_Veiculos obj = null;

            if (filtroPesq == "1")
            {
                sql = "SELECT ve_chassi, ve_prop, ve_nompro, ve_marca, ve_modelo, ve_serie FROM veiculo WHERE ve_chassi LIKE '%" + pesquisa + "%'";
            }
            else if (filtroPesq == "2")
            {
                sql = "SELECT ve_chassi, ve_prop, ve_nompro, ve_marca, ve_modelo, ve_serie FROM veiculo WHERE ve_prop LIKE '%" + pesquisa + "%' ORDER BY ve_chassi";
            }
            else if (filtroPesq == "3")
            {
                sql = "SELECT ve_chassi, ve_prop, ve_nompro, ve_marca, ve_modelo, ve_serie FROM veiculo WHERE ve_nompro LIKE '%" + pesquisa + "%' ORDER BY ve_chassi";
            }
            else if (filtroPesq == "4")
            {
                sql = "SELECT ve_chassi, ve_prop, ve_nompro, ve_marca, ve_modelo, ve_serie FROM veiculo WHERE ve_serie LIKE '%" + pesquisa + "%' ORDER BY ve_chassi";
            }
            else
            {
                sql = "SELECT ve_chassi, ve_prop, ve_nompro, ve_marca, ve_modelo, ve_serie FROM veiculo ORDER BY ve_chassi";
            }

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
                        obj = new CL_Veiculos();
                        //leio as informações dos campos e jogo para o objeto
                        obj.ve_chassi = dr["ve_chassi"].ToString().Trim();
                        obj.ve_prop = dr["ve_prop"].ToString().Trim();
                        obj.ve_nomprop = dr["ve_nompro"].ToString().Trim();
                        obj.ve_marca = dr["ve_marca"].ToString().Trim();
                        obj.ve_modelo = dr["ve_modelo"].ToString().Trim();
                        obj.ve_serie = dr["ve_serie"].ToString().Trim();

                        objList.Add(obj);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
            return objList;
        }

        public static CL_Veiculos buscaParticip(CL_Veiculos objVeiculos, string con)
        {
            string sql = "SELECT p_cod, p_nome FROM particip WHERE p_cod =" + objVeiculos.ve_prop;

            DB_Funcoes.DesmontaConexao(con);
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
                        objVeiculos.ve_prop = dr["p_cod"].ToString().Trim();
                        objVeiculos.ve_nomprop = dr["p_nome"].ToString().Trim();
                        return objVeiculos;
                    }
                }
                objVeiculos.ve_chassi = null;
                return objVeiculos;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return objVeiculos;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
    }
}