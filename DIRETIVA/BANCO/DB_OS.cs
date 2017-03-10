using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BANCO
{
    public class DB_OS : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaOsCod(int os_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            os_cod = 0;

            string sql = "SELECT os_cod FROM servico ORDER BY os_cod DESC limit 1";

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
                        os_cod = Convert.ToInt32(dr["os_cod"]);
                        os_cod = os_cod + 1;
                        return os_cod;
                    }
                    return os_cod;
                }
                return os_cod;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return os_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        public static CL_OS buscaOS(int os_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_OS objOs = new CL_OS();

            string sql = "SELECT * FROM servico WHERE os_cod=" + os_cod;

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
                        objOs.os_cod = Convert.ToInt32(dr["os_cod"]);
                        objOs.os_emis = Convert.ToDateTime(dr["os_emis"]);
                        objOs.os_codCliente = Convert.ToInt32(dr["os_cliente"]);
                        objOs.os_nomCliente = dr["os_clinome"].ToString().Trim();
                        objOs.os_codCliFat = Convert.ToInt32(dr["os_clifat"]);
                        objOs.os_mecanic = Convert.ToInt32(dr["os_mecanic"]);
                        objOs.os_veiculo = dr["os_veiculo"].ToString().Trim();
                        objOs.os_vemode = dr["os_vemode"].ToString().Trim();
                        objOs.os_veser = dr["os_veser"].ToString().Trim();
                        objOs.os_cupom = dr["os_cupom"].ToString().Trim();
                        objOs.os_combust = dr["os_combust"].ToString().Trim();
                        objOs.os_km = dr["os_km"].ToString().Trim();
                        objOs.os_local = dr["os_local"].ToString().Trim();
                        objOs.os_tipo = dr["os_tipo"].ToString().Trim();
                        objOs.os_horimetro = dr["os_horim"].ToString().Trim();
                        objOs.os_consTec = dr["os_mecanic"].ToString().Trim();
                        objOs.os_recepc = dr["os_recepc"].ToString().Trim();
                        objOs.os_dataProm = Convert.ToDateTime(dr["os_entrega"]);
                        objOs.os_hProm = Convert.ToDateTime(dr["os_hprom"]);
                        objOs.os_dataEntr = Convert.ToDateTime(dr["os_termino"]);
                        objOs.os_nota = dr["os_nota"].ToString().Trim();
                        objOs.os_obs = dr["os_obs1"].ToString().Trim();
                        objOs.os_cod1 = dr["os_cod1"].ToString().Trim();
                        objOs.os_cod2 = dr["os_cod2"].ToString().Trim();
                        objOs.os_cod3 = dr["os_cod3"].ToString().Trim();
                        objOs.os_cod4 = dr["os_cod4"].ToString().Trim();
                        objOs.os_cod5 = dr["os_cod5"].ToString().Trim();
                        objOs.os_cod6 = dr["os_cod6"].ToString().Trim();
                        objOs.os_cod7 = dr["os_cod7"].ToString().Trim();
                        objOs.os_cod8 = dr["os_cod8"].ToString().Trim();
                        objOs.os_cod9 = dr["os_cod9"].ToString().Trim();
                        objOs.os_cod10 = dr["os_cod10"].ToString().Trim();
                        objOs.os_cod11 = dr["os_cod11"].ToString().Trim();
                        objOs.os_cod12 = dr["os_cod12"].ToString().Trim();
                        objOs.os_cod13 = dr["os_cod13"].ToString().Trim();
                        objOs.os_cod14 = dr["os_cod14"].ToString().Trim();
                        objOs.os_cod15 = dr["os_cod15"].ToString().Trim();
                        objOs.os_cod16 = dr["os_cod16"].ToString().Trim();
                        objOs.os_cod17 = dr["os_cod17"].ToString().Trim();
                        objOs.os_cod18 = dr["os_cod18"].ToString().Trim();
                        objOs.os_cod19 = dr["os_cod19"].ToString().Trim();
                        objOs.os_cod20 = dr["os_cod20"].ToString().Trim();
                        objOs.os_cod21 = dr["os_cod21"].ToString().Trim();
                        objOs.os_cod22 = dr["os_cod22"].ToString().Trim();
                        objOs.os_situac = dr["os_situac"].ToString().Trim();

                        return objOs;
                    }
                    else
                    {
                        objOs = null;
                        return objOs;
                    }
                }
                else
                {
                    objOs = null;
                    return objOs;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objOs = null;
                return objOs;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        public static List<CL_OS> listar(string pesquisa, string con, string filtro)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            List<CL_OS> objList = new List<CL_OS>();
            CL_OS obj = null;

            if (pesquisa == "")
            {
                sql = "SELECT os_cod, os_cliente, os_clifat, os_emis, os_entrega, os_veiculo, os_hemis, os_hprom, os_combust, os_km, os_cod1, os_cod2, os_cod3, os_cod4, os_cod5, os_cod6, os_cod7, os_cod8, os_cod9, os_cod10, os_cod11, os_cod12, os_cod13, os_cod14, os_cod15, os_cod16, os_cod17, os_cod18, os_cod19, os_cod20, os_cod21, os_cod22, os_clinome, os_vemode, os_mecanic, os_horim FROM servico";
            }
            else if (filtro == "1")
            {
                sql = "SELECT os_cod, os_cliente, os_clifat, os_emis, os_entrega, os_veiculo, os_hemis, os_hprom, os_combust, os_km, os_cod1, os_cod2, os_cod3, os_cod4, os_cod5, os_cod6, os_cod7, os_cod8, os_cod9, os_cod10, os_cod11, os_cod12, os_cod13, os_cod14, os_cod15, os_cod16, os_cod17, os_cod18, os_cod19, os_cod20, os_cod21, os_cod22, os_clinome, os_vemode, os_mecanic, os_horim FROM servico WHERE os_clinome LIKE '%" + pesquisa + "%'";
            }
            else if (filtro == "2")
            {
                sql = "SELECT os_cod, os_cliente, os_clifat, os_emis, os_entrega, os_veiculo, os_hemis, os_hprom, os_combust, os_km, os_cod1, os_cod2, os_cod3, os_cod4, os_cod5, os_cod6, os_cod7, os_cod8, os_cod9, os_cod10, os_cod11, os_cod12, os_cod13, os_cod14, os_cod15, os_cod16, os_cod17, os_cod18, os_cod19, os_cod20, os_cod21, os_cod22, os_clinome, os_vemode FROM servico WHERE os_veiculo LIKE '%" + pesquisa + "%'";
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
                        obj = new CL_OS();
                        //leio as informações dos campos e jogo para o objeto
                        obj.os_cod = Convert.ToInt32(dr["os_cod"].ToString());
                        obj.os_codCliente = Convert.ToInt32(dr["os_cliente"].ToString());
                        obj.os_codCliFat = Convert.ToInt32(dr["os_clifat"].ToString());
                        obj.os_combust = dr["os_combust"].ToString().Trim();
                        obj.os_consTec = dr["os_mecanic"].ToString().Trim();
                        obj.os_horimetro = dr["os_horim"].ToString().Trim();
                        obj.os_km = dr["os_km"].ToString().Trim();
                        obj.os_nomCliente = dr["os_clinome"].ToString().Trim();
                        obj.os_veiculo = dr["os_veiculo"].ToString().Trim();
                        obj.os_servExec = dr["os_cod1"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod1"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod2"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod3"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod4"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod5"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod6"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod7"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod8"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod9"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod10"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod11"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod12"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod13"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod14"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod15"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod16"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod17"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod18"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod19"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod20"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod21"].ToString().Trim();
                        obj.os_servExec = obj.os_servExec + " \n" + dr["os_cod22"].ToString().Trim();
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
        public static bool cadOs(CL_OS objOS, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO servico (os_cod, os_emis, os_cliente, os_clinome, os_clifat, os_veiculo, os_vemode, os_cupom, os_combust, os_km, os_local, os_tipo, os_horim, os_mecanic, os_entrega, os_hprom, os_termino, os_obs1, os_cliende, os_clifone, os_vemarca, os_veser, os_cod1, os_cod2, os_cod3, os_cod4, os_cod5, os_cod6, os_cod7, os_cod8, os_cod9, os_cod10, os_cod11, os_cod12, os_cod13, os_cod14, os_cod15, os_cod16, os_cod17, os_cod18, os_cod19, os_cod20, os_cod21, os_cod22, os_recepc, os_c_cnpj, os_c_iest, os_f_cnpj, os_f_iest, os_situac) " +
                "VALUES (@os_cod, @os_emis, @os_cliente, @os_clinome, @os_clifat, @os_veiculo, @os_vemode, @os_cupom, @os_combust, @os_km, @os_local, @os_tipo, @os_horim, @os_consTec, @os_dataProm, @os_hProm, @os_dataEntr, @os_obs1, @os_cliende, @os_clifone, @os_vemarca, @os_veser, @os_cod1, @os_cod2, @os_cod3, @os_cod4, @os_cod5, @os_cod6, @os_cod7, @os_cod8, @os_cod9, @os_cod10, @os_cod11, @os_cod12, @os_cod13, @os_cod14, @os_cod15, @os_cod16, @os_cod17, @os_cod18, @os_cod19, @os_cod20, @os_cod21, @os_cod22, @os_recepc, @os_c_cnpj, @os_c_iest, @os_f_cnpj, @os_f_iest, @os_situac)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("os_cod", objOS.os_cod);
                comand.Parameters.AddWithValue("os_emis", objOS.os_emis.ToShortDateString());
                comand.Parameters.AddWithValue("os_cliente", objOS.os_codCliente);
                comand.Parameters.AddWithValue("os_clinome", objOS.os_nomCliente);
                comand.Parameters.AddWithValue("os_clifat", objOS.os_codCliFat);
                comand.Parameters.AddWithValue("os_veiculo", objOS.os_veiculo);
                comand.Parameters.AddWithValue("os_vemode", objOS.os_vemode);
                comand.Parameters.AddWithValue("os_cupom", objOS.os_cupom);
                comand.Parameters.AddWithValue("os_combust", objOS.os_combust);
                comand.Parameters.AddWithValue("os_km", objOS.os_km);
                comand.Parameters.AddWithValue("os_local", objOS.os_local);
                comand.Parameters.AddWithValue("os_tipo", objOS.os_tipo);
                comand.Parameters.AddWithValue("os_horim", objOS.os_horimetro);
                comand.Parameters.AddWithValue("os_consTec", objOS.os_consTec);
                comand.Parameters.AddWithValue("os_dataProm", objOS.os_dataProm.ToShortDateString());
                comand.Parameters.AddWithValue("os_hprom", objOS.os_hProm.ToShortTimeString());
                comand.Parameters.AddWithValue("os_dataEntr", objOS.os_dataEntr.ToShortDateString());
                comand.Parameters.AddWithValue("os_obs1", objOS.os_obs);
                comand.Parameters.AddWithValue("os_cliende", objOS.os_cliende);
                comand.Parameters.AddWithValue("os_clifone", objOS.os_clifone);
                comand.Parameters.AddWithValue("os_vemarca", objOS.os_vemarca);
                comand.Parameters.AddWithValue("os_veser", objOS.os_veser);
                comand.Parameters.AddWithValue("os_cod1", objOS.os_cod1);
                comand.Parameters.AddWithValue("os_cod2", objOS.os_cod2);
                comand.Parameters.AddWithValue("os_cod3", objOS.os_cod3);
                comand.Parameters.AddWithValue("os_cod4", objOS.os_cod4);
                comand.Parameters.AddWithValue("os_cod5", objOS.os_cod5);
                comand.Parameters.AddWithValue("os_cod6", objOS.os_cod6);
                comand.Parameters.AddWithValue("os_cod7", objOS.os_cod7);
                comand.Parameters.AddWithValue("os_cod8", objOS.os_cod8);
                comand.Parameters.AddWithValue("os_cod9", objOS.os_cod9);
                comand.Parameters.AddWithValue("os_cod10", objOS.os_cod10);
                comand.Parameters.AddWithValue("os_cod11", objOS.os_cod11);
                comand.Parameters.AddWithValue("os_cod12", objOS.os_cod12);
                comand.Parameters.AddWithValue("os_cod13", objOS.os_cod13);
                comand.Parameters.AddWithValue("os_cod14", objOS.os_cod14);
                comand.Parameters.AddWithValue("os_cod15", objOS.os_cod15);
                comand.Parameters.AddWithValue("os_cod16", objOS.os_cod16);
                comand.Parameters.AddWithValue("os_cod17", objOS.os_cod17);
                comand.Parameters.AddWithValue("os_cod18", objOS.os_cod18);
                comand.Parameters.AddWithValue("os_cod19", objOS.os_cod19);
                comand.Parameters.AddWithValue("os_cod20", objOS.os_cod20);
                comand.Parameters.AddWithValue("os_cod21", objOS.os_cod21);
                comand.Parameters.AddWithValue("os_cod22", objOS.os_cod22);
                comand.Parameters.AddWithValue("os_recepc", objOS.os_recepc);
                comand.Parameters.AddWithValue("os_c_cnpj", objOS.os_c_cnpj);
                comand.Parameters.AddWithValue("os_c_iest", objOS.os_c_iest);
                comand.Parameters.AddWithValue("os_f_cnpj", objOS.os_f_cnpj);
                comand.Parameters.AddWithValue("os_f_iest", objOS.os_f_iest);
                comand.Parameters.AddWithValue("os_situac", objOS.os_situac);
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
        public static bool alteraOS(CL_OS objOS, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE servico SET os_emis='" + objOS.os_emis.ToShortDateString() + "', os_cliente='" + objOS.os_codCliente + "', os_clinome='" + objOS.os_nomCliente +
                "', os_clifat='" + objOS.os_codCliFat + "', os_veiculo='" + objOS.os_veiculo + "', os_vemode='" + objOS.os_vemode + "', os_cupom='" + objOS.os_cupom +
                "', os_c_cnpj='" + objOS.os_c_cnpj + "', os_c_iest='" + objOS.os_c_iest + "', os_f_cnpj='" + objOS.os_f_cnpj + "', os_f_iest='" + objOS.os_f_iest +
                "', os_combust='" + objOS.os_combust + "', os_km='" + objOS.os_km + "', os_local='" + objOS.os_local + "', os_tipo='" + objOS.os_tipo +
                "', os_horim='" + objOS.os_horimetro + "', os_mecanic='" + objOS.os_consTec + "', os_entrega='" + objOS.os_dataProm.ToShortDateString() + "', os_hprom='" + objOS.os_hProm.ToShortTimeString() +
                "', os_termino='" + objOS.os_dataEntr + "', os_obs1='" + objOS.os_obs + "', os_cliende='" + objOS.os_cliende + "', os_clifone='" + objOS.os_clifone +
                "', os_cod1='" + objOS.os_cod1 + "', os_cod2='" + objOS.os_cod2 + "', os_cod3='" + objOS.os_cod3 + "', os_cod4='" + objOS.os_cod4 +
                "', os_cod5='" + objOS.os_cod5 + "', os_cod6='" + objOS.os_cod6 + "', os_cod7='" + objOS.os_cod7 + "', os_cod8='" + objOS.os_cod8 +
                "', os_cod9='" + objOS.os_cod9 + "', os_cod10='" + objOS.os_cod10 + "', os_cod11='" + objOS.os_cod11 + "', os_cod12='" + objOS.os_cod12 +
                "', os_cod13='" + objOS.os_cod13 + "', os_cod14='" + objOS.os_cod14 + "', os_cod15='" + objOS.os_cod15 + "', os_cod16='" + objOS.os_cod16 +
                "', os_cod17='" + objOS.os_cod17 + "', os_cod18='" + objOS.os_cod18 + "', os_cod19='" + objOS.os_cod19 + "', os_cod20='" + objOS.os_cod20 +
                "', os_cod21='" + objOS.os_cod21 + "', os_cod22='" + objOS.os_cod22 + "', os_situac='" + objOS.os_situac + "' WHERE os_cod= '" + objOS.os_cod + "'";

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
        public static bool excluiOS(CL_OS objOs, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM servico WHERE os_cod='" + objOs.os_cod + "'";

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
        public static List<CL_OS> sincroOfic(List<CL_OS> objListOS, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT os_cod, os_clinome, u_nome, os_veiculo, os_situac, os_mecanic, os_emis FROM servico JOIN usudac ON u_codigo=os_mecanic WHERE os_situac='F' ORDER BY OS_COD";

            CL_OS obj = null;

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
                        obj = new CL_OS();
                        //leio as informações dos campos e jogo para o objeto
                        obj.os_cod = Convert.ToInt32(dr["os_cod"]);
                        obj.os_nomCliente = dr["os_clinome"].ToString().Trim();
                        obj.os_consTec = dr["os_mecanic"].ToString().Trim();
                        obj.os_nomConsTec = dr["u_nome"].ToString().Trim();
                        obj.os_veiculo = dr["os_veiculo"].ToString().Trim();
                        obj.os_situac = dr["os_situac"].ToString().Trim();

                        objListOS.Add(obj);
                    }
                    dr.Close();
                    return objListOS;
                }
                else
                {
                    objListOS = null;
                    return objListOS;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListOS = null;
                return objListOS;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static bool encerraOS(CL_OS objOS, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = " UPDATE servico SET os_situac='Z' WHERE os_cod= " + objOS.os_cod + "; UPDATE requis SET req_situac='Z' WHERE req_cod=" + objOS.os_cod;

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
    }
}