using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Est : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static CL_Est buscaProd(string est_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            bool pv = true;

            CL_Est objEst = new CL_Est();

            string sql = "SELECT est_cod, est_grupo, est_ngrupo, est_nsgrup, est_sgrupo, est_tpprod, est_nome, est_loccid, est_locend, " +
                "est_locbai, est_locnr, est_local, est_ctadeb, con_nome AS ctadeb, est_ctacre, con_nome AS ctacre, est_locdep, est_codant, " +
                "est_unid, u_nome, est_bloq, est_unest, est_unmov, est_clades, est_peso, est_comis, est_compro, est_fornec, p_nome, " +
                "est_nome2, est_nome3, est_nome4, est_cdpesq, est_mensa, est_dmensa, est_obs, est_inicio, est_medioi, est_minimo, est_maximo, est_qtsegu, " +
                "est_oficin, est_campo, est_pratel, est_qtde, est_medio, est_vlqtd, est_qtdped, est_dtpedi, est_penden, est_vulten, est_ulten, est_lucro, " +
                "est_pcven, est_pcven2, est_pcven3, est_pcven4, est_vdanet, est_ean, est_eantri, est_dimens, est_pcfixo,est_cfisc, est_tribut, tri_nome AS tribut, " +
                "est_trbent, tri_nome AS trbent, est_strib, est_tbisen, est_tboutr, est_pis, est_tabpis, est_cofins, est_ipivlr, est_origem, est_subtri, est_tpitem, " +
                "est_tipi, est_codgen, est_codser, est_anp, est_tanque, est_gia, est_lote, est_qfraci, est_situac " +
                "FROM est " +
                "LEFT JOIN particip ON est_fornec = p_cod " +
                "LEFT JOIN conctb ON (est_ctadeb = con_cod OR est_ctacre = con_cod) " +
                "LEFT JOIN tribut ON (est_tribut = tri_cod OR est_trbent = tri_cod) " +
                "LEFT JOIN unmedida ON est_unid = u_unid " +
                "WHERE est_cod='" + est_cod + "'";

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
                        if (pv)
                        {
                            objEst.est_cod = dr["est_cod"].ToString().Trim();
                            objEst.est_nome = dr["est_nome"].ToString().Trim();
                            objEst.est_bloq = dr["est_bloq"].ToString().Trim();
                            objEst.est_famil = dr["est_grupo"].ToString().Trim();
                            objEst.est_ngrupo = dr["est_ngrupo"] is DBNull ? 0 : Convert.ToInt32(dr["est_ngrupo"]);
                            objEst.est_nsgrup = dr["est_nsgrup"] is DBNull ? 0 : Convert.ToInt32(dr["est_nsgrup"]);
                            objEst.est_tpprod = dr["est_tpprod"].ToString().Trim();
                            objEst.est_unid = dr["est_unid"].ToString().Trim();
                            objEst.est_ctaDeb = dr["est_ctadeb"].ToString().Trim();
                            objEst.est_ctaCre = dr["est_ctacre"].ToString().Trim();
                            objEst.est_nomeCtaDeb = dr["ctadeb"].ToString().Trim();
                            objEst.est_locnr = dr["est_locnr"].ToString().Trim();
                            objEst.est_loccid = dr["est_loccid"].ToString().Trim();
                            objEst.est_locbai = dr["est_locbai"].ToString().Trim();
                            objEst.est_locend = dr["est_locend"].ToString().Trim();
                            objEst.est_local = dr["est_local"].ToString().Trim();
                            objEst.est_locdep = dr["est_locdep"].ToString().Trim();
                            objEst.est_codant = dr["est_codant"].ToString().Trim();
                            objEst.est_unest = dr["est_unest"].ToString().Trim();
                            objEst.est_unmov = dr["est_unmov"] is DBNull ? 0 : Convert.ToDouble(dr["est_unmov"]);
                            objEst.est_clades = dr["est_clades"] is DBNull ? 0 : Convert.ToInt32(dr["est_clades"]);
                            objEst.est_peso = dr["est_peso"] is DBNull ? 0 : Convert.ToDouble(dr["est_peso"]);
                            objEst.est_comis = dr["est_comis"] is DBNull ? 0 : Convert.ToDouble(dr["est_comis"]);
                            objEst.est_compr = dr["est_compro"] is DBNull ? 0 : Convert.ToDouble(dr["est_compro"]);
                            objEst.est_fornec = dr["est_fornec"] is DBNull ? 0 : Convert.ToInt32(dr["est_fornec"]);
                            objEst.est_nomeFornec = dr["p_nome"].ToString().Trim();
                            objEst.est_nome2 = dr["est_nome2"].ToString().Trim();
                            objEst.est_nome3 = dr["est_nome3"].ToString().Trim();
                            objEst.est_nome4 = dr["est_nome4"].ToString().Trim();
                            objEst.est_mensa = dr["est_mensa"] is DBNull ? 0 : Convert.ToInt32(dr["est_mensa"]);
                            objEst.est_dmensa = dr["est_dmensa"].ToString().Trim();
                            objEst.est_obs = dr["est_obs"].ToString().Trim();

                            objEst.est_inicio = dr["est_inicio"] is DBNull ? 0 : Convert.ToDouble(dr["est_inicio"]);
                            objEst.est_medioi = dr["est_medioi"] is DBNull ? 0 : Convert.ToDouble(dr["est_medioi"]);
                            objEst.est_minimo = dr["est_minimo"] is DBNull ? 0 : Convert.ToDouble(dr["est_minimo"]);
                            objEst.est_maximo = dr["est_maximo"] is DBNull ? 0 : Convert.ToDouble(dr["est_maximo"]);
                            objEst.est_qtseg = dr["est_qtsegu"] is DBNull ? 0 : Convert.ToDouble(dr["est_qtsegu"]);
                            objEst.est_oficin = dr["est_oficin"] is DBNull ? 0 : Convert.ToDouble(dr["est_oficin"]);
                            objEst.est_campo = dr["est_campo"] is DBNull ? 0 : Convert.ToDouble(dr["est_campo"]);
                            objEst.est_pratel = dr["est_pratel"] is DBNull ? 0 : Convert.ToDouble(dr["est_pratel"]);
                            objEst.est_qtde = dr["est_qtde"] is DBNull ? 0 : Convert.ToDouble(dr["est_qtde"]);
                            objEst.est_medio = dr["est_medio"] is DBNull ? 0 : Convert.ToDouble(dr["est_medio"]);
                            objEst.est_vlqtd = dr["est_vlqtd"] is DBNull ? 0 : Convert.ToDouble(dr["est_vlqtd"]);
                            objEst.est_qtdped = dr["est_qtdped"] is DBNull ? 0 : Convert.ToDouble(dr["est_qtdped"]);
                            if (dr["est_dtpedi"].ToString() != "")
                            {
                                objEst.est_dtped = Convert.ToDateTime(dr["est_dtpedi"]);
                            }
                            objEst.est_penden = dr["est_penden"] is DBNull ? 0 : Convert.ToDouble(dr["est_penden"]);
                            objEst.est_vulten = dr["est_vulten"] is DBNull ? 0 : Convert.ToDouble(dr["est_vulten"]);
                            if (dr["est_ulten"].ToString() != "")
                            {
                                objEst.est_ulten = Convert.ToDateTime(dr["est_ulten"]);
                            }
                            objEst.est_lucro = dr["est_lucro"] is DBNull ? 0 : Convert.ToDouble(dr["est_lucro"]);
                            objEst.est_pcven = dr["est_pcven"] is DBNull ? 0 : Convert.ToDouble(dr["est_pcven"]);
                            objEst.est_pcven2 = dr["est_pcven2"] is DBNull ? 0 : Convert.ToDouble(dr["est_pcven2"]);
                            objEst.est_pcven3 = dr["est_pcven3"] is DBNull ? 0 : Convert.ToDouble(dr["est_pcven3"]);
                            objEst.est_pcven4 = dr["est_pcven4"] is DBNull ? 0 : Convert.ToDouble(dr["est_pcven4"]);
                            objEst.est_vdanet = dr["est_vdanet"] is DBNull ? 0 : Convert.ToDouble(dr["est_vdanet"]);
                            objEst.est_ean = dr["est_ean"].ToString().Trim();
                            objEst.est_eanTri = dr["est_eantri"].ToString().Trim();
                            objEst.est_dimens = dr["est_dimens"].ToString().Trim();
                            objEst.est_pcfixo = dr["est_pcfixo"].ToString().Trim();

                            objEst.est_cfisc = dr["est_cfisc"].ToString().Trim();
                            objEst.est_tribut = dr["est_tribut"] is DBNull ? 0 : Convert.ToInt32(dr["est_tribut"]);
                            objEst.est_trbent = dr["est_trbent"] is DBNull ? 0 : Convert.ToInt32(dr["est_trbent"]);
                            objEst.est_nomeTribut = dr["tribut"].ToString().Trim();
                            objEst.est_strib = dr["est_strib"].ToString().Trim();
                            objEst.est_tbisen = dr["est_tbisen"] is DBNull ? 0 : Convert.ToInt32(dr["est_tbisen"]);
                            objEst.est_tboutr = dr["est_tboutr"] is DBNull ? 0 : Convert.ToInt32(dr["est_tboutr"]);
                            objEst.est_pis = dr["est_pis"] is DBNull ? 0 : Convert.ToDecimal(dr["est_pis"]);
                            objEst.est_tabpis = dr["est_tabpis"].ToString().Trim();
                            objEst.est_cofins = dr["est_cofins"] is DBNull ? 0 : Convert.ToDouble(dr["est_cofins"]);
                            objEst.est_ipivlr = dr["est_ipivlr"] is DBNull ? 0 : Convert.ToDouble(dr["est_ipivlr"]);
                            objEst.est_origem = dr["est_origem"].ToString().Trim();
                            objEst.est_subtri = dr["est_subtri"].ToString().Trim();
                            objEst.est_tipitem = dr["est_tpitem"] is DBNull ? 0 : Convert.ToInt32(dr["est_tpitem"]);
                            objEst.est_tipi = dr["est_tipi"].ToString().Trim();
                            objEst.est_codgen = dr["est_codgen"].ToString().Trim();
                            objEst.est_codser = dr["est_codser"].ToString().Trim();
                            objEst.est_anp = dr["est_anp"].ToString().Trim();
                            objEst.est_tanque = dr["est_tanque"] is DBNull ? 0 : Convert.ToInt32(dr["est_tanque"]);
                            objEst.est_gia = dr["est_gia"].ToString().Trim();
                            objEst.est_lote = dr["est_lote"].ToString().Trim();
                            objEst.est_qfraci = dr["est_qfraci"].ToString().Trim();
                            objEst.est_situac = dr["est_situac"].ToString().Trim();
                        }
                        else
                        {
                            objEst.est_nomeCtaCre = dr["ctacre"].ToString().Trim();//ELSE
                            objEst.est_nomeTrbent = dr["trbent"].ToString().Trim();//ELSE
                        }
                        pv = false;
                    }
                    return objEst;
                }
                else
                {
                    objEst = null;
                    return objEst;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objEst = null;
                return objEst;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static CL_Est buscaProdUmov(string est_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Est objEst = new CL_Est();

            string sql = "SELECT est_cod, est_nome, est_bloq, est_grupo, est_ngrupo, est_nsgrup, est_unid, est_nome2, " +
                "est_qtde, est_pcven, est_situac " +
                "FROM est WHERE est_cod='" + est_cod + "'";

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
                        objEst.est_cod = dr["est_cod"].ToString().Trim();
                        objEst.est_nome = dr["est_nome"].ToString().Trim();
                        objEst.est_bloq = dr["est_bloq"].ToString().Trim();
                        objEst.est_famil = dr["est_grupo"].ToString().Trim();
                        objEst.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                        objEst.est_nsgrup = Convert.ToInt32(dr["est_nsgrup"]);
                        objEst.est_unid = dr["est_unid"].ToString().Trim();
                        objEst.est_nome2 = dr["est_nome2"].ToString().Trim();
                        objEst.est_qtde = dr["est_qtde"] is DBNull ? 0 : Convert.ToInt32(dr["est_qtde"]);
                        objEst.est_pcven = dr["est_pcven"] is DBNull ? 0 : Convert.ToInt32(dr["est_pcven"]);
                        objEst.est_situac = dr["est_situac"].ToString().Trim();
                        return objEst;
                    }
                    else
                    {
                        objEst = null;
                        return objEst;
                    }
                }
                else
                {
                    objEst = null;
                    return objEst;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objEst = null;
                return objEst;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool updateCodEst(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE empresa SET emp_prxite=emp_prxite+1";

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

        public static bool alteraEstDGA(CL_Est objEst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE est SET est_ngrupo=@est_ngrupo, est_grupo=@est_grupo, est_nsgrup=@est_nsgrup, est_sgrupo=@est_sgrupo," +
                    "est_nome=@est_nome, est_nome2=@est_nome2, est_nome3=@est_nome3, est_nome4=@est_nome4, est_unid=@est_unid, " +
                    "est_pcven=@est_pcven, est_cfisc=@est_cfisc, est_tribut=@est_tribut, est_lucro=@est_lucro," +
                    "est_local=@est_local, est_origem=@est_origem, est_trbent=@est_trbent, " +
                    "est_situac=@est_situac WHERE est_cod=@est_cod";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("est_ngrupo", objEst.est_ngrupo);
                comand.Parameters.AddWithValue("est_grupo", objEst.est_grupo);
                comand.Parameters.AddWithValue("est_nsgrup", objEst.est_nsgrup);
                comand.Parameters.AddWithValue("est_sgrupo", objEst.est_sgrupo);
                comand.Parameters.AddWithValue("est_nome", objEst.est_nome);
                comand.Parameters.AddWithValue("est_nome2", objEst.est_nome2);
                comand.Parameters.AddWithValue("est_nome3", objEst.est_nome3);
                comand.Parameters.AddWithValue("est_nome4", objEst.est_nome4);
                comand.Parameters.AddWithValue("est_unid", objEst.est_unid);
                comand.Parameters.AddWithValue("est_pcven", objEst.est_pcven);
                comand.Parameters.AddWithValue("est_cfisc", objEst.est_cfisc);
                comand.Parameters.AddWithValue("est_tribut", objEst.est_tribut);
                comand.Parameters.AddWithValue("est_lucro", objEst.est_lucro);
                comand.Parameters.AddWithValue("est_local", objEst.est_local);
                comand.Parameters.AddWithValue("est_origem", objEst.est_origem);
                comand.Parameters.AddWithValue("est_trbent", objEst.est_trbent);
                comand.Parameters.AddWithValue("est_cod", objEst.est_cod);
                comand.Parameters.AddWithValue("est_situac", "A");

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

        public static bool cadEstDGA(CL_Est objEst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO est " +
                    "(est_cod, est_ngrupo, est_grupo, est_nsgrup, est_sgrupo, est_nome, est_nome2, est_nome3, est_nome4, " +
                    "est_unid, est_pcven, est_cfisc, est_tribut, est_lucro, est_local, " +
                    "est_origem, est_trbent, est_situac) " +
                    "VALUES " +
                    "(@est_cod, @est_ngrupo, @est_grupo, @est_nsgrup, @est_sgrupo, @est_nome, @est_nome2, @est_nome3, @est_nome4, " +
                    "@est_unid, @est_pcven, @est_cfisc, @est_tribut, @est_lucro, @est_local, " +
                    "@est_origem, @est_trbent, @est_situac)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("est_cod", objEst.est_cod);
                comand.Parameters.AddWithValue("est_ngrupo", objEst.est_ngrupo);
                comand.Parameters.AddWithValue("est_grupo", objEst.est_grupo);
                comand.Parameters.AddWithValue("est_nsgrup", objEst.est_nsgrup);
                comand.Parameters.AddWithValue("est_sgrupo", objEst.est_sgrupo);
                comand.Parameters.AddWithValue("est_nome", objEst.est_nome);
                comand.Parameters.AddWithValue("est_nome2", objEst.est_nome2);
                comand.Parameters.AddWithValue("est_nome3", objEst.est_nome3);
                comand.Parameters.AddWithValue("est_nome4", objEst.est_nome4);
                comand.Parameters.AddWithValue("est_unid", objEst.est_unid);
                comand.Parameters.AddWithValue("est_pcven", objEst.est_pcven);
                comand.Parameters.AddWithValue("est_cfisc", objEst.est_cfisc);
                comand.Parameters.AddWithValue("est_tribut", objEst.est_tribut);
                comand.Parameters.AddWithValue("est_lucro", objEst.est_lucro);
                comand.Parameters.AddWithValue("est_local", objEst.est_local);
                comand.Parameters.AddWithValue("est_origem", objEst.est_origem);
                comand.Parameters.AddWithValue("est_trbent", objEst.est_trbent);
                comand.Parameters.AddWithValue("est_situac", "I");

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

        public static CL_Est buscaProdDGA(string est_cod, string con)
        {
            string sql = "SELECT est_cod, est_ngrupo, est_grupo, est_nsgrup, est_sgrupo, est_nome, est_unid," +
                        " est_local, est_lucro, est_pcven, est_tribut, tri_nome, est_trbent, tri_nome, est_cfisc, est_origem," +
                        " est_nome2, est_nome3, est_nome4" +
                        " FROM EST, TRIBUT" +
                        " WHERE(est_tribut = tri_cod OR est_trbent = tri_cod)" +
                        " AND est_cod = '" + est_cod + "'";
            bool pv = true;
            CL_Est objEst = new CL_Est();

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
                        if (pv)
                        {
                            objEst.est_cod = dr["est_cod"].ToString().Trim();
                            objEst.est_nome = dr["est_nome"].ToString().Trim();
                            objEst.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                            objEst.est_nsgrup = Convert.ToInt32(dr["est_nsgrup"]);
                            objEst.est_unid = dr["est_unid"].ToString().Trim();
                            objEst.est_local = dr["est_local"].ToString().Trim();
                            objEst.est_nome2 = dr["est_nome2"].ToString().Trim();
                            objEst.est_nome3 = dr["est_nome3"].ToString().Trim();
                            objEst.est_nome4 = dr["est_nome4"].ToString().Trim();
                            objEst.est_lucro = Convert.ToDouble(dr["est_lucro"]);
                            objEst.est_pcven = Convert.ToDouble(dr["est_pcven"]);
                            objEst.est_cfisc = dr["est_cfisc"].ToString().Trim();
                            objEst.est_tribut = Convert.ToInt32(dr["est_tribut"]);
                            objEst.est_trbent = Convert.ToInt32(dr["est_trbent"]);
                            objEst.est_nomeTribut = dr["tri_nome"].ToString().Trim();
                            objEst.est_nomeTrbent = dr["tri_nome"].ToString().Trim();
                            objEst.est_origem = dr["est_origem"].ToString().Trim();
                        }
                        else
                        {
                            objEst.est_nomeTrbent = dr["tri_nome"].ToString().Trim();
                        }
                        pv = false;
                    }
                    return objEst;
                }
                else
                {
                    objEst = null;
                    return objEst;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objEst = null;
                return objEst;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static List<CL_Est> listar(string pesq, string filtro, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";
            if (pesq == "")
            {
                sql = "SELECT * FROM est";
                if (filtro == "APP")//LISTAR PARA ENVIAR EST PARA O APP
                {
                    sql = "SELECT * FROM est WHERE est_vdanet<>0 AND est_ngrupo=2";
                }
            }
            else
            {
                if (filtro == "1")//NOME
                {
                    sql = "SELECT * FROM est WHERE est_nome LIKE '%" + pesq + "%'";
                }
                else if (filtro == "2")//COD
                {
                    sql = "SELECT * FROM est WHERE est_cod='" + pesq + "'";
                }
            }

            List<CL_Est> objList = new List<CL_Est>();
            CL_Est obj = null;

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
                        obj = new CL_Est();
                        //leio as informações dos campos e jogo para o objeto
                        obj.est_cod = dr["est_cod"].ToString().Trim();
                        obj.est_nome = dr["est_nome"].ToString().Trim();
                        obj.est_nome2 = dr["est_nome2"].ToString().Trim();
                        obj.est_nome3 = dr["est_nome3"].ToString().Trim();
                        obj.est_nome4 = dr["est_nome4"].ToString().Trim();
                        obj.est_tpprod = dr["est_tpprod"].ToString().Trim();
                        obj.est_oficin = Convert.ToDouble(dr["est_oficin"]);
                        obj.est_campo = Convert.ToDouble(dr["est_campo"]);
                        obj.est_qtde = Convert.ToDouble(dr["est_qtde"]);
                        obj.est_qtde = obj.est_qtde - obj.est_campo - obj.est_oficin;
                        obj.est_tribut = Convert.ToInt32(dr["est_tribut"]);
                        obj.est_pcven = Convert.ToDouble(dr["est_pcven"]);
                        obj.est_pcven2 = Convert.ToDouble(dr["est_pcven2"]);
                        obj.est_pcven3 = Convert.ToDouble(dr["est_pcven3"]);
                        obj.est_pcven4 = Convert.ToDouble(dr["est_pcven4"]);
                        obj.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                        obj.est_nsgrup = Convert.ToInt32(dr["est_nsgrup"]);
                        obj.est_grupo = dr["est_grupo"].ToString().Trim();
                        obj.est_cfisc = dr["est_cfisc"].ToString().Trim();
                        obj.est_famil = dr["est_sgrupo"].ToString().Trim();
                        obj.est_bloq = dr["est_bloq"].ToString().Trim();
                        obj.est_unid = dr["est_unid"].ToString().Trim();
                        obj.est_peso = Convert.ToDouble(dr["est_peso"]);
                        obj.est_comis = Convert.ToDouble(dr["est_comis"]);
                        obj.est_ipivlr = Convert.ToDouble(dr["est_ipivlr"]);
                        obj.est_strib = dr["est_strib"].ToString().Trim();
                        obj.est_subtri = dr["est_subtri"].ToString().Trim();
                        obj.est_qfraci = dr["est_qfraci"].ToString().Trim();
                        obj.est_situac = dr["est_situac"].ToString().Trim();
                        obj.est_vdanet = Convert.ToDouble(dr["est_vdanet"]);

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

        public static bool somaEstOficin(string est_cod, double req_qtdade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            double est_oficin = 0;
            string sql = "SELECT est_oficin FROM est WHERE est_cod='" + est_cod + "'";

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
                        est_oficin = Convert.ToDouble(dr["est_oficin"]);
                        est_oficin = est_oficin + req_qtdade;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }

            try
            {
                string sql2 = "UPDATE est SET est_oficin=" + est_oficin + "WHERE est_cod='" + est_cod + "'";
                NpgsqlCommand comand2 = new NpgsqlCommand(sql2, Conn);
                Conn.Open();
                comand2.ExecuteScalar();
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

        public static bool subtEstOficin(string est_cod, double req_qtdade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            double est_oficin = 0;
            string sql = "SELECT est_oficin FROM est WHERE est_cod='" + est_cod + "'";

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
                        est_oficin = Convert.ToDouble(dr["est_oficin"]);
                        est_oficin = est_oficin - req_qtdade;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }

            try
            {
                string sql2 = "UPDATE est SET est_oficin=" + est_oficin + "WHERE est_cod='" + est_cod + "'";
                NpgsqlCommand comand2 = new NpgsqlCommand(sql2, Conn);
                Conn.Open();
                comand2.ExecuteScalar();
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

        public static int buscaCod(int est_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT emp_prxite FROM EMPRESA";

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
                        est_cod = Convert.ToInt32(dr["emp_prxite"]);

                        return est_cod;
                    }
                    else
                    {
                        est_cod = 0;
                        return est_cod;
                    }
                }
                else
                {
                    est_cod = 1;
                    return est_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                est_cod = 0;
                return est_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadEst(CL_Est objEst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO est " +
                    "(est_cod, est_codant, est_ngrupo, est_grupo, est_nsgrup, est_sgrupo, est_nome, est_nome2, est_nome3, est_nome4, " +
                    "est_unid, est_fornec, est_minimo, est_maximo, est_pratel, est_oficin, est_campo, est_qtde, est_vlqtd, " +
                    "est_pcven, est_pcven2, est_pcven3, est_pcven4, est_medio, est_ulten, est_vulten, est_cfisc, est_inicio, est_medioi, " +
                    "est_ipivlr, est_tribut, est_peso, est_strib, est_unmov, est_tpprod, est_bloq, est_dtpedi, est_qtdped, est_penden, " +
                    "est_clades, est_lucro, est_ctacre, est_ctadeb, est_comis, est_compro, est_local, est_lote, " +
                    "est_tbisen, est_tboutr, est_pis, est_cofins, est_tanque, est_subtri, est_tpitem, est_tipi, est_codgen, " +
                    "est_codser, est_anp, est_origem, est_mensa, est_dmensa, est_unest, est_gia, est_cdpesq, est_ean, est_eantri, est_tabpis, " +
                    "est_obs, est_dimens, est_trbent, est_pcfixo, est_qtsegu, est_qfraci, est_locdep, est_vdanet, est_loccid, est_locbai, est_locend, est_locnr, est_situac) " +
                    "VALUES " +
                    "(@est_cod, @est_codant, @est_ngrupo, @est_grupo, @est_nsgrup, @est_sgrupo, @est_nome, @est_nome2, @est_nome3, @est_nome4, " +
                    "@est_unid, @est_fornec, @est_minimo, @est_maximo, @est_pratel, @est_oficin, @est_campo, @est_qtde, @est_vlqtd, " +
                    "@est_pcven, @est_pcven2, @est_pcven3, @est_pcven4, @est_medio, @est_ulten, @est_vulten, @est_cfisc, @est_inicio, @est_medioi, " +
                    "@est_ipivlr, @est_tribut, @est_peso, @est_strib, @est_unmov, @est_tpprod, @est_bloq, @est_dtpedi, @est_qtdped, @est_penden, " +
                    "@est_clades, @est_lucro, @est_ctacre, @est_ctadeb, @est_comis, @est_compro, @est_local, @est_lote, " +
                    "@est_tbisen, @est_tboutr, @est_pis, @est_cofins, @est_tanque, @est_subtri, @est_tpitem, @est_tipi, @est_codgen, " +
                    "@est_codser, @est_anp, @est_origem, @est_mensa, @est_dmensa, @est_unest, @est_gia, @est_cdpesq, @est_ean, @est_eantri, @est_tabpis, " +
                    "@est_obs, @est_dimens, @est_trbent, @est_pcfixo, @est_qtsegu, @est_qfraci, @est_locdep, @est_vdanet, @est_loccid, @est_locbai, @est_locend, @est_locnr, @est_situac)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("est_cod", objEst.est_cod);
                comand.Parameters.AddWithValue("est_codant", objEst.est_codant);
                comand.Parameters.AddWithValue("est_ngrupo", objEst.est_ngrupo);
                comand.Parameters.AddWithValue("est_grupo", objEst.est_grupo);
                comand.Parameters.AddWithValue("est_nsgrup", objEst.est_nsgrup);
                comand.Parameters.AddWithValue("est_sgrupo", objEst.est_sgrupo);
                comand.Parameters.AddWithValue("est_nome", objEst.est_nome);
                comand.Parameters.AddWithValue("est_nome2", objEst.est_nome2);
                comand.Parameters.AddWithValue("est_nome3", objEst.est_nome3);
                comand.Parameters.AddWithValue("est_nome4", objEst.est_nome4);
                comand.Parameters.AddWithValue("est_unid", objEst.est_unid);
                comand.Parameters.AddWithValue("est_fornec", objEst.est_fornec);
                comand.Parameters.AddWithValue("est_minimo", objEst.est_minimo);
                comand.Parameters.AddWithValue("est_maximo", objEst.est_maximo);
                comand.Parameters.AddWithValue("est_pratel", objEst.est_pratel);
                comand.Parameters.AddWithValue("est_oficin", objEst.est_oficin);
                comand.Parameters.AddWithValue("est_campo", objEst.est_campo);
                comand.Parameters.AddWithValue("est_qtde", objEst.est_qtde);
                comand.Parameters.AddWithValue("est_vlqtd", objEst.est_vlqtd);
                comand.Parameters.AddWithValue("est_pcven", objEst.est_pcven);
                comand.Parameters.AddWithValue("est_pcven2", objEst.est_pcven2);
                comand.Parameters.AddWithValue("est_pcven3", objEst.est_pcven3);
                comand.Parameters.AddWithValue("est_pcven4", objEst.est_pcven4);
                comand.Parameters.AddWithValue("est_medio", objEst.est_medio);
                comand.Parameters.AddWithValue("est_medioi", objEst.est_medioi);
                comand.Parameters.AddWithValue("est_ulten", objEst.est_ulten.ToShortDateString());
                comand.Parameters.AddWithValue("est_vulten", objEst.est_vulten);
                comand.Parameters.AddWithValue("est_cfisc", objEst.est_cfisc);
                comand.Parameters.AddWithValue("est_inicio", objEst.est_inicio);
                comand.Parameters.AddWithValue("est_ipivlr", objEst.est_ipivlr);
                comand.Parameters.AddWithValue("est_tribut", objEst.est_tribut);
                comand.Parameters.AddWithValue("est_peso", objEst.est_peso);
                comand.Parameters.AddWithValue("est_strib", objEst.est_strib);
                comand.Parameters.AddWithValue("est_unmov", objEst.est_unmov);
                comand.Parameters.AddWithValue("est_tpprod", objEst.est_tpprod);
                comand.Parameters.AddWithValue("est_bloq", objEst.est_bloq);
                comand.Parameters.AddWithValue("est_dtpedi", objEst.est_dtped.ToShortDateString());
                comand.Parameters.AddWithValue("est_qtdped", objEst.est_qtdped);
                comand.Parameters.AddWithValue("est_penden", objEst.est_penden);
                comand.Parameters.AddWithValue("est_clades", objEst.est_clades);
                comand.Parameters.AddWithValue("est_lucro", objEst.est_lucro);
                comand.Parameters.AddWithValue("est_ctacre", objEst.est_ctaCre);
                comand.Parameters.AddWithValue("est_ctadeb", objEst.est_ctaDeb);
                comand.Parameters.AddWithValue("est_comis", objEst.est_comis);
                comand.Parameters.AddWithValue("est_compro", objEst.est_compr);
                comand.Parameters.AddWithValue("est_local", objEst.est_local);
                comand.Parameters.AddWithValue("est_lote", objEst.est_lote);
                comand.Parameters.AddWithValue("est_tbisen", objEst.est_tbisen);
                comand.Parameters.AddWithValue("est_tboutr", objEst.est_tboutr);
                comand.Parameters.AddWithValue("est_pis", objEst.est_pis);
                comand.Parameters.AddWithValue("est_cofins", objEst.est_cofins);
                comand.Parameters.AddWithValue("est_tanque", objEst.est_tanque);
                comand.Parameters.AddWithValue("est_subtri", objEst.est_subtri);
                comand.Parameters.AddWithValue("est_tpitem", objEst.est_tipitem);
                comand.Parameters.AddWithValue("est_tipi", objEst.est_tipi);
                comand.Parameters.AddWithValue("est_codgen", objEst.est_codgen);
                comand.Parameters.AddWithValue("est_codser", objEst.est_codser);
                comand.Parameters.AddWithValue("est_anp", objEst.est_anp);
                comand.Parameters.AddWithValue("est_origem", objEst.est_origem);
                comand.Parameters.AddWithValue("est_mensa", objEst.est_mensa);
                comand.Parameters.AddWithValue("est_dmensa", objEst.est_dmensa);
                comand.Parameters.AddWithValue("est_unest", objEst.est_unest);
                comand.Parameters.AddWithValue("est_gia", objEst.est_gia);
                comand.Parameters.AddWithValue("est_cdpesq", objEst.est_cdPesq);
                comand.Parameters.AddWithValue("est_ean", objEst.est_ean);
                comand.Parameters.AddWithValue("est_eantri", objEst.est_eanTri);
                comand.Parameters.AddWithValue("est_tabpis", objEst.est_tabpis);
                comand.Parameters.AddWithValue("est_obs", objEst.est_obs);
                comand.Parameters.AddWithValue("est_dimens", objEst.est_dimens);
                comand.Parameters.AddWithValue("est_trbent", objEst.est_trbent);
                comand.Parameters.AddWithValue("est_pcfixo", objEst.est_pcfixo);
                comand.Parameters.AddWithValue("est_qtsegu", objEst.est_qtseg);
                comand.Parameters.AddWithValue("est_qfraci", objEst.est_qfraci);
                comand.Parameters.AddWithValue("est_locdep", objEst.est_locdep);
                comand.Parameters.AddWithValue("est_loccid", objEst.est_loccid);
                comand.Parameters.AddWithValue("est_locbai", objEst.est_locbai);
                comand.Parameters.AddWithValue("est_locend", objEst.est_locend);
                comand.Parameters.AddWithValue("est_locnr", objEst.est_locnr);
                comand.Parameters.AddWithValue("est_vdanet", objEst.est_vdanet);
                comand.Parameters.AddWithValue("est_situac", "I");

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

        public static bool alteraEst(CL_Est objEst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE est SET est_codant=@est_codant, est_ngrupo=@est_ngrupo, est_grupo=@est_grupo, est_nsgrup=@est_nsgrup, est_sgrupo=@est_sgrupo," +
                    "est_nome=@est_nome, est_nome2=@est_nome2, est_nome3=@est_nome3, est_nome4=@est_nome4, est_unid=@est_unid, est_fornec=@est_fornec, est_minimo=@est_minimo," +
                    "est_maximo=@est_maximo, est_pratel=@est_pratel, est_oficin=@est_oficin, est_campo=@est_campo, est_qtde=@est_qtde, est_vlqtd=@est_vlqtd," +
                    "est_pcven=@est_pcven, est_pcven2=@est_pcven2, est_pcven3=@est_pcven3, est_pcven4=@est_pcven4, est_medio=@est_medio, est_ulten=@est_ulten," +
                    "est_vulten=@est_vulten, est_cfisc=@est_cfisc, est_inicio=@est_inicio, est_medioi=@est_medioi, est_ipivlr=@est_ipivlr, est_tribut=@est_tribut, est_peso=@est_peso," +
                    "est_strib=@est_strib, est_unmov=@est_unmov, est_tpprod=@est_tpprod, est_bloq=@est_bloq, est_dtpedi=@est_dtpedi, est_qtdped=@est_qtdped," +
                    "est_penden=@est_penden, est_clades=@est_clades, est_ctacre=@est_ctacre, est_ctadeb=@est_ctadeb, est_comis=@est_comis, est_lucro=@est_lucro," +
                    "est_compro=@est_compro, est_local=@est_local, est_lote=@est_lote, est_tbisen=@est_tbisen, est_tboutr=@est_tboutr, est_pis=@est_pis," +
                    "est_cofins=@est_cofins, est_tanque=@est_tanque, est_subtri=@est_subtri, est_tpitem=@est_tpitem, est_tipi=@est_tipi, est_codgen=@est_codgen," +
                    "est_codser=@est_codser, est_anp=@est_anp, est_origem=@est_origem, est_mensa=@est_mensa, est_dmensa=@est_dmensa, est_unest=@est_unest," +
                    "est_gia=@est_gia, est_cdpesq=@est_cdpesq, est_ean=@est_ean, est_eantri=@est_eantri, est_tabpis=@est_tabpis, est_obs=@est_obs," +
                    "est_dimens=@est_dimens, est_trbent=@est_trbent, est_pcfixo=@est_pcfixo, est_qtsegu=@est_qtsegu, est_qfraci=@est_qfraci, est_locdep=@est_locdep," +
                    "est_vdanet=@est_vdanet, est_loccid=@est_loccid, est_locbai=@est_locbai, est_locend=@est_locend, est_locnr=@est_locnr, est_situac=@est_situac " +
                    "WHERE est_cod=@est_cod";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("est_codant", objEst.est_codant);
                comand.Parameters.AddWithValue("est_ngrupo", objEst.est_ngrupo);
                comand.Parameters.AddWithValue("est_grupo", objEst.est_grupo);
                comand.Parameters.AddWithValue("est_nsgrup", objEst.est_nsgrup);
                comand.Parameters.AddWithValue("est_sgrupo", objEst.est_sgrupo);
                comand.Parameters.AddWithValue("est_nome", objEst.est_nome);
                comand.Parameters.AddWithValue("est_nome2", objEst.est_nome2);
                comand.Parameters.AddWithValue("est_nome3", objEst.est_nome3);
                comand.Parameters.AddWithValue("est_nome4", objEst.est_nome4);
                comand.Parameters.AddWithValue("est_unid", objEst.est_unid);
                comand.Parameters.AddWithValue("est_fornec", objEst.est_fornec);
                comand.Parameters.AddWithValue("est_minimo", objEst.est_minimo);
                comand.Parameters.AddWithValue("est_maximo", objEst.est_maximo);
                comand.Parameters.AddWithValue("est_pratel", objEst.est_pratel);
                comand.Parameters.AddWithValue("est_oficin", objEst.est_oficin);
                comand.Parameters.AddWithValue("est_campo", objEst.est_campo);
                comand.Parameters.AddWithValue("est_qtde", objEst.est_qtde);
                comand.Parameters.AddWithValue("est_vlqtd", objEst.est_vlqtd);
                comand.Parameters.AddWithValue("est_pcven", objEst.est_pcven);
                comand.Parameters.AddWithValue("est_pcven2", objEst.est_pcven2);
                comand.Parameters.AddWithValue("est_pcven3", objEst.est_pcven3);
                comand.Parameters.AddWithValue("est_pcven4", objEst.est_pcven4);
                comand.Parameters.AddWithValue("est_medio", objEst.est_medio);
                comand.Parameters.AddWithValue("est_ulten", objEst.est_ulten.ToShortDateString());
                comand.Parameters.AddWithValue("est_vulten", objEst.est_vulten);
                comand.Parameters.AddWithValue("est_cfisc", objEst.est_cfisc);
                comand.Parameters.AddWithValue("est_inicio", objEst.est_inicio);
                comand.Parameters.AddWithValue("est_medioi", objEst.est_medioi);
                comand.Parameters.AddWithValue("est_ipivlr", objEst.est_ipivlr);
                comand.Parameters.AddWithValue("est_tribut", objEst.est_tribut);
                comand.Parameters.AddWithValue("est_peso", objEst.est_peso);
                comand.Parameters.AddWithValue("est_strib", objEst.est_strib);
                comand.Parameters.AddWithValue("est_unmov", objEst.est_unmov);
                comand.Parameters.AddWithValue("est_tpprod", objEst.est_tpprod);
                comand.Parameters.AddWithValue("est_bloq", objEst.est_bloq);
                comand.Parameters.AddWithValue("est_dtpedi", objEst.est_dtped.ToShortDateString());
                comand.Parameters.AddWithValue("est_qtdped", objEst.est_qtdped);
                comand.Parameters.AddWithValue("est_penden", objEst.est_penden);
                comand.Parameters.AddWithValue("est_clades", objEst.est_clades);
                comand.Parameters.AddWithValue("est_ctacre", objEst.est_ctaCre);
                comand.Parameters.AddWithValue("est_ctadeb", objEst.est_ctaDeb);
                comand.Parameters.AddWithValue("est_comis", objEst.est_comis);
                comand.Parameters.AddWithValue("est_lucro", objEst.est_lucro);
                comand.Parameters.AddWithValue("est_compro", objEst.est_compr);
                comand.Parameters.AddWithValue("est_local", objEst.est_local);
                comand.Parameters.AddWithValue("est_lote", objEst.est_lote);
                comand.Parameters.AddWithValue("est_tbisen", objEst.est_tbisen);
                comand.Parameters.AddWithValue("est_tboutr", objEst.est_tboutr);
                comand.Parameters.AddWithValue("est_pis", objEst.est_pis);
                comand.Parameters.AddWithValue("est_cofins", objEst.est_cofins);
                comand.Parameters.AddWithValue("est_tanque", objEst.est_tanque);
                comand.Parameters.AddWithValue("est_subtri", objEst.est_subtri);
                comand.Parameters.AddWithValue("est_tpitem", objEst.est_tipitem);
                comand.Parameters.AddWithValue("est_tipi", objEst.est_tipi);
                comand.Parameters.AddWithValue("est_codgen", objEst.est_codgen);
                comand.Parameters.AddWithValue("est_codser", objEst.est_codser);
                comand.Parameters.AddWithValue("est_anp", objEst.est_anp);
                comand.Parameters.AddWithValue("est_origem", objEst.est_origem);
                comand.Parameters.AddWithValue("est_mensa", objEst.est_mensa);
                comand.Parameters.AddWithValue("est_dmensa", objEst.est_dmensa);
                comand.Parameters.AddWithValue("est_unest", objEst.est_unest);
                comand.Parameters.AddWithValue("est_gia", objEst.est_gia);
                comand.Parameters.AddWithValue("est_cdpesq", objEst.est_cdPesq);
                comand.Parameters.AddWithValue("est_ean", objEst.est_ean);
                comand.Parameters.AddWithValue("est_eantri", objEst.est_eanTri);
                comand.Parameters.AddWithValue("est_tabpis", objEst.est_tabpis);
                comand.Parameters.AddWithValue("est_obs", objEst.est_obs);
                comand.Parameters.AddWithValue("est_dimens", objEst.est_dimens);
                comand.Parameters.AddWithValue("est_trbent", objEst.est_trbent);
                comand.Parameters.AddWithValue("est_pcfixo", objEst.est_pcfixo);
                comand.Parameters.AddWithValue("est_qtsegu", objEst.est_qtseg);
                comand.Parameters.AddWithValue("est_qfraci", objEst.est_qfraci);
                comand.Parameters.AddWithValue("est_locdep", objEst.est_locdep);
                comand.Parameters.AddWithValue("est_vdanet", objEst.est_vdanet);
                comand.Parameters.AddWithValue("est_loccid", objEst.est_loccid);
                comand.Parameters.AddWithValue("est_locbai", objEst.est_locbai);
                comand.Parameters.AddWithValue("est_locend", objEst.est_locend);
                comand.Parameters.AddWithValue("est_locnr", objEst.est_locnr);
                comand.Parameters.AddWithValue("est_cod", objEst.est_cod);
                comand.Parameters.AddWithValue("est_situac", "A");

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

        public static bool excluiEst(CL_Est objEst, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM est WHERE est_cod='" + objEst.est_cod + "'";

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
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Est> listarApp(string situac, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT est_cod, est_nome, est_ngrupo, est_situac, est_vdanet FROM est WHERE est_situac='" + situac + "'";
            List<CL_Est> objList = new List<CL_Est>();
            CL_Est obj = null;

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
                        obj = new CL_Est();
                        //leio as informações dos campos e jogo para o objeto
                        obj.est_cod = dr["est_cod"].ToString().Trim();
                        obj.est_nome = dr["est_nome"].ToString().Trim();
                        obj.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                        obj.est_situac = dr["est_situac"].ToString().Trim();
                        obj.est_vdanet = Convert.ToDouble(dr["est_vdanet"]);

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