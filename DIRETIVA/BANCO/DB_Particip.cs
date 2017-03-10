using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Particip : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaCodigoParticip(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_cod FROM particip ORDER BY sr_recno DESC limit 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["p_cod"]) + 1;
                    else
                        return 0;
                }
                else
                    return 1;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool conferePermissao(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_mcliente FROM usudac WHERE u_email=@u_email";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("u_email", email);
            NpgsqlDataReader dr;
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string ret = dr["u_mcliente"].ToString().Trim();
                        return ret == "S" ? true : false;
                    }
                    else
                        return false;
                }
                else
                    return false;

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

        public static bool incluiParticip(CL_Particip objParticip, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO particip (p_cliente, p_fornec, p_transp, p_cgc, p_nome, p_fantas, p_cep, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est, p_pais, p_ibge, p_fone, p_contat, p_iest, p_celul, p_email, p_situac, p_localiz, p_cult, p_ident, p_nasc) " +
                "VALUES (@p_cliente, @p_fornec, @p_transp, @p_cgc, @p_nome, @p_fantas, @p_cep, @p_ende, @p_nr, @p_comend, @p_bairro, @p_cida, @p_est, @p_pais, @p_ibge, @p_fone, @p_contat, @p_iest, @p_celul, @p_email, @p_situac, @p_localiz, @p_cult, @p_ident, @p_nasc)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("p_cliente", objParticip.p_cliente);
                comand.Parameters.AddWithValue("p_fornec", objParticip.p_fornec);
                comand.Parameters.AddWithValue("p_transp", objParticip.p_transp);
                comand.Parameters.AddWithValue("p_cgc", objParticip.p_cgc);
                comand.Parameters.AddWithValue("p_nome", objParticip.p_nome);
                comand.Parameters.AddWithValue("p_fantas", objParticip.p_fantas);
                comand.Parameters.AddWithValue("p_cep", objParticip.p_cep);
                comand.Parameters.AddWithValue("p_ende", objParticip.p_ende);
                comand.Parameters.AddWithValue("p_nr", objParticip.p_nr);
                comand.Parameters.AddWithValue("p_comend", objParticip.p_comend);
                comand.Parameters.AddWithValue("p_bairro", objParticip.p_bairro);
                comand.Parameters.AddWithValue("p_cida", objParticip.p_cida);
                comand.Parameters.AddWithValue("p_est", objParticip.p_est);
                comand.Parameters.AddWithValue("p_pais", objParticip.p_pais);
                comand.Parameters.AddWithValue("p_ibge", objParticip.p_ibge);
                comand.Parameters.AddWithValue("p_fone", objParticip.p_fone);
                comand.Parameters.AddWithValue("p_contat", objParticip.p_contat);
                comand.Parameters.AddWithValue("p_iest", objParticip.p_iest);
                comand.Parameters.AddWithValue("p_celul", objParticip.p_celul);
                comand.Parameters.AddWithValue("p_email", objParticip.p_email);
                comand.Parameters.AddWithValue("p_situac", objParticip.p_situac);
                comand.Parameters.AddWithValue("p_localiz", objParticip.p_localiz);
                comand.Parameters.AddWithValue("p_cult", objParticip.p_cultura);
                comand.Parameters.AddWithValue("p_ident", objParticip.p_rg);
                comand.Parameters.AddWithValue("p_nasc", objParticip.p_nasc.ToShortDateString());

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
        public static bool excluiParticip(CL_Particip objParticip, string con)
        {
            string sql = "DELETE FROM particip WHERE p_cod =" + objParticip.p_clicod;

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
        public static List<CL_Particip> listagemSimples(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT p_cod, p_nome FROM particip ORDER BY p_cod";
            List<CL_Particip> objList = new List<CL_Particip>();

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
                        objList.Add(new CL_Particip()
                        {
                            p_clicod = dr["p_cod"] is DBNull ? 0 : Convert.ToInt32(dr["p_cod"]),
                            p_codNome = dr["p_cod"] is DBNull ? "0" : dr["p_cod"].ToString().Trim() + " - " + dr["p_nome"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objList;
                }
                else
                    return objList;
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

        public static List<CL_Particip> buscaEntregaManual(string particip, string cida, string tipo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            if (particip != "")
            {
                sql = "SELECT p_cod, p_nome, p_cep, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est, p_pais, p_fone, p_iest, p_situac, p_fantas, p_pais, p_celul, p_fone, p_email" +
                " FROM particip WHERE p_cod=@p_cod AND p_ramo=@p_idecli";
            }
            else
            {
                sql = "SELECT p_cod, p_nome, p_cep, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est, p_pais, p_fone, p_iest, p_situac, p_fantas, p_pais, p_celul, p_fone, p_email" +
                " FROM particip WHERE p_cida=@p_cida AND p_ramo=@p_idecli";
            }
            List<CL_Particip> objList = new List<CL_Particip>();
            CL_Particip objParticip = null;

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_cod", particip);
            comand.Parameters.AddWithValue("p_cida", cida);
            comand.Parameters.AddWithValue("p_idecli", tipo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objParticip = new CL_Particip();
                        objParticip.p_clicod = Convert.ToInt32(dr["p_cod"]);
                        objParticip.p_nome = dr["p_nome"].ToString().Trim();
                        objParticip.p_cep = dr["p_cep"].ToString().Trim();
                        objParticip.p_ende = dr["p_ende"].ToString().Trim();
                        objParticip.p_nr = dr["p_nr"].ToString().Trim();
                        objParticip.p_comend = dr["p_comend"].ToString().Trim();
                        objParticip.p_bairro = dr["p_bairro"].ToString().Trim();
                        objParticip.p_cida = dr["p_cida"].ToString().Trim();
                        objParticip.p_est = dr["p_est"].ToString().Trim();
                        objParticip.p_pais = dr["p_pais"].ToString().Trim();
                        objParticip.p_fone = dr["p_fone"].ToString().Trim();
                        objParticip.p_situac = dr["p_situac"].ToString().Trim();
                        objParticip.p_fantas = dr["p_fantas"].ToString().Trim();
                        objParticip.p_pais = dr["p_pais"].ToString().Trim();
                        objParticip.p_celul = dr["p_celul"].ToString().Trim();
                        objParticip.p_fone = dr["p_fone"].ToString().Trim();
                        objParticip.p_email = dr["p_email"].ToString().Trim();
                        objList.Add(objParticip);
                    }
                    return objList;
                }
                else
                    return objList;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
        public static bool confereIE(string iest, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_cod FROM particip WHERE REPLACE(p_iest,'/','') =@iest";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            comand.Parameters.AddWithValue("iest", iest.Replace("/", ""));

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return true;
                    else
                        return false;
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
        public static void attLocaliz(string latitude, string longitude, string clicod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE particip SET p_localiz=" + latitude + ", " + longitude + " WHERE p_cod=" + clicod + " AND (p_localiz IS NULL or p_localiz = ''";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                Conn.Open();
                comand.ExecuteScalar();
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
        }
        public static CL_Particip buscarParticip(string p_clicod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_Particip objParticip = new CL_Particip();

            String sql = "SELECT * FROM particip WHERE p_cod =" + p_clicod;

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
                        objParticip.p_clicod = Convert.ToInt32(dr["p_cod"]);
                        objParticip.p_cliente = dr["p_cliente"].ToString().Trim();
                        objParticip.p_fornec = dr["p_fornec"].ToString().Trim();
                        objParticip.p_transp = dr["p_transp"].ToString().Trim();
                        objParticip.p_cgc = dr["p_cgc"].ToString().Trim();
                        objParticip.p_nome = dr["p_nome"].ToString().Trim();
                        objParticip.p_fantas = dr["p_fantas"].ToString().Trim();
                        objParticip.p_cep = dr["p_cep"].ToString().Trim();
                        objParticip.p_ende = dr["p_ende"].ToString().Trim();
                        objParticip.p_nr = dr["p_nr"].ToString().Trim();
                        objParticip.p_comend = dr["p_comend"].ToString().Trim();
                        objParticip.p_bairro = dr["p_bairro"].ToString().Trim();
                        objParticip.p_cida = dr["p_cida"].ToString().Trim();
                        objParticip.p_est = dr["p_est"].ToString().Trim();
                        objParticip.p_pais = dr["p_pais"].ToString().Trim();
                        objParticip.p_ibge = dr["p_ibge"].ToString().Trim();
                        objParticip.p_fone = dr["p_fone"].ToString().Trim();
                        objParticip.p_contat = dr["p_contat"].ToString().Trim();
                        objParticip.p_iest = dr["p_iest"].ToString().Trim();
                        objParticip.p_celul = dr["p_celul"].ToString().Trim();
                        objParticip.p_email = dr["p_email"].ToString().Trim();
                        objParticip.p_situac = dr["p_situac"].ToString().Trim();
                        objParticip.p_localiz = dr["p_localiz"].ToString().Trim();
                        objParticip.p_ramo = dr["p_ramo"].ToString().Trim();
                        objParticip.p_lcred = dr["p_lcred"] is DBNull ? 0 : Convert.ToDouble(dr["p_lcred"]);
                        objParticip.p_cultura = dr["p_cult"].ToString().Trim();
                        objParticip.p_rg = dr["p_ident"].ToString().Trim();
                        objParticip.p_nasc = dr["p_nasc"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["p_nasc"]);
                        objParticip.p_codNome = objParticip.p_clicod.ToString() + " - " + objParticip.p_nome;

                        return objParticip;
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
        public static List<CL_Particip> listarParticipCidade(string cidade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_cod, p_cgc, p_nome, p_fantas, p_cep, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est, p_pais, p_ibge, p_fone, p_iest, p_situac FROM particip WHERE p_cida='" + cidade + "' AND p_cgc<>'' AND p_cgc<>'.'";

            List<CL_Particip> objList = new List<CL_Particip>();
            CL_Particip objParticip = null;

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
                        objParticip = new CL_Particip();
                        //leio as informações dos campos e jogo para o objeto
                        objParticip.p_clicod = Convert.ToInt32(dr["p_cod"]);
                        objParticip.p_cgc = dr["p_cgc"].ToString().Trim();
                        objParticip.p_nome = dr["p_nome"].ToString().Trim();
                        objParticip.p_fantas = dr["p_fantas"].ToString().Trim();
                        objParticip.p_cep = dr["p_cep"].ToString().Trim();
                        objParticip.p_ende = dr["p_ende"].ToString().Trim();
                        objParticip.p_nr = dr["p_nr"].ToString().Trim();
                        objParticip.p_comend = dr["p_comend"].ToString().Trim();
                        objParticip.p_bairro = dr["p_bairro"].ToString().Trim();
                        objParticip.p_cida = dr["p_cida"].ToString().Trim();
                        objParticip.p_est = dr["p_est"].ToString().Trim();
                        objParticip.p_pais = dr["p_pais"].ToString().Trim();
                        objParticip.p_ibge = dr["p_ibge"].ToString().Trim();
                        objParticip.p_fone = dr["p_fone"].ToString().Trim();
                        objParticip.p_iest = dr["p_iest"].ToString().Trim();
                        objParticip.p_situac = dr["p_situac"].ToString().Trim();
                        objParticip.p_codNome = objParticip.p_clicod.ToString() + " - " + objParticip.p_nome;
                        objList.Add(objParticip);
                    }
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
                ex.Message.ToString();
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
        public static bool alteraParticip(CL_Particip objParticip, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {

                string sql = "UPDATE particip SET p_cliente=@p_cliente, p_fornec=@p_fornec, p_transp=@p_transp, p_cgc=@p_cgc, p_nome=@p_nome, "+
                    "p_fantas=@p_fantas, p_cep=@p_cep, p_ende=@p_ende, p_nr=@p_nr, p_comend=@p_comend, p_bairro=@p_bairro, p_cida=@p_cida, "+
                    "p_est=@p_est, p_pais=@p_pais, p_ibge=@p_ibge, p_fone=@p_fone, p_contat=@p_contat, p_iest=@p_iest, p_celul=@p_celul, "+
                    "p_email=@p_email, p_situac=@p_situac, p_localiz=@p_localiz, p_cult=@p_cult, p_nasc=@p_nasc, p_ident=@p_ident "+
                    "WHERE p_cod=@p_cod";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("p_cod", objParticip.p_clicod);
                comand.Parameters.AddWithValue("p_cliente", objParticip.p_cliente);
                comand.Parameters.AddWithValue("p_fornec", objParticip.p_fornec);
                comand.Parameters.AddWithValue("p_transp", objParticip.p_transp);
                comand.Parameters.AddWithValue("p_cgc", objParticip.p_cgc);
                comand.Parameters.AddWithValue("p_nome", objParticip.p_nome);
                comand.Parameters.AddWithValue("p_fantas", objParticip.p_fantas);
                comand.Parameters.AddWithValue("p_cep", objParticip.p_cep);
                comand.Parameters.AddWithValue("p_ende", objParticip.p_ende);
                comand.Parameters.AddWithValue("p_nr", objParticip.p_nr);
                comand.Parameters.AddWithValue("p_comend", objParticip.p_comend);
                comand.Parameters.AddWithValue("p_bairro", objParticip.p_bairro);
                comand.Parameters.AddWithValue("p_cida", objParticip.p_cida);
                comand.Parameters.AddWithValue("p_est", objParticip.p_est);
                comand.Parameters.AddWithValue("p_pais", objParticip.p_pais);
                comand.Parameters.AddWithValue("p_ibge", objParticip.p_ibge);
                comand.Parameters.AddWithValue("p_fone", objParticip.p_fone);
                comand.Parameters.AddWithValue("p_contat", objParticip.p_contat);
                comand.Parameters.AddWithValue("p_iest", objParticip.p_iest);
                comand.Parameters.AddWithValue("p_celul", objParticip.p_celul);
                comand.Parameters.AddWithValue("p_email", objParticip.p_email);
                comand.Parameters.AddWithValue("p_situac", objParticip.p_situac);
                comand.Parameters.AddWithValue("p_localiz", objParticip.p_localiz);
                comand.Parameters.AddWithValue("p_cult", objParticip.p_cultura);
                comand.Parameters.AddWithValue("p_ident", objParticip.p_rg);
                comand.Parameters.AddWithValue("p_nasc", objParticip.p_nasc.ToShortDateString());
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
        public static int buscaCodigoIBGE(string cidade, int estado, string con)
        {
            int cod_ibge = 0;
            string sql = "SELECT muncoddv FROM cadmun WHERE munnomex='" + cidade + "' and ufcod='" + estado + "'";

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
                        cod_ibge = Convert.ToInt32(dr["muncoddv"]);
                        return cod_ibge;
                    }
                    else
                    {
                        cod_ibge = 0;
                        return cod_ibge;
                    }
                }
                else
                {
                    cod_ibge = 0;
                    return cod_ibge;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return cod_ibge;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
        public List<CL_Particip> listar(string pesquisa, string con, string filtroPesq)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            List<CL_Particip> objList = new List<CL_Particip>();
            CL_Particip objParticip = null;

            if (pesquisa == "")
            {
                sql = "SELECT * FROM particip ORDER BY p_cod";
                if (filtroPesq == "APP")
                    sql = "SELECT * FROM particip ORDER BY p_cod";
            }
            else
            {
                if (filtroPesq == "1")//FILTRO PESQ PARTICIP COD
                    sql = "SELECT * FROM particip WHERE p_cod = '" + pesquisa + "' ORDER BY p_cod";
                else if (filtroPesq == "2")//FILTRO PESQ PARTICIP NOME
                    sql = "SELECT * FROM particip WHERE p_nome LIKE '%" + pesquisa + "%' ORDER BY p_cod";
                else if (filtroPesq == "3")//FILTRO PESQ PARTICIP CGC
                    sql = "SELECT * FROM particip WHERE p_cgc = '" + pesquisa + "' ORDER BY p_cod";
                else if (filtroPesq == "4")//FILTRO CIDADE
                    sql = "SELECT * FROM particip WHERE p_cida = '" + pesquisa + "' ORDER BY p_cod";
                else if (filtroPesq == "5")//FILTRO FANTASIA
                    sql = "SELECT * FROM particip WHERE p_fantas = '" + pesquisa + "' ORDER BY p_cod";
                else
                    sql = "SELECT * FROM particip ORDER BY p_cod";
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
                        objParticip = new CL_Particip();
                        //leio as informações dos campos e jogo para o objeto
                        objParticip.p_clicod = Convert.ToInt32(dr["p_cod"]);
                        objParticip.p_cliente = dr["p_cliente"].ToString().Trim();
                        objParticip.p_fornec = dr["p_fornec"].ToString().Trim();
                        objParticip.p_transp = dr["p_transp"].ToString().Trim();
                        objParticip.p_cgc = dr["p_cgc"].ToString().Trim();
                        objParticip.p_nome = dr["p_nome"].ToString().Trim();
                        objParticip.p_fantas = dr["p_fantas"].ToString().Trim();
                        objParticip.p_cep = dr["p_cep"].ToString().Trim();
                        objParticip.p_ende = dr["p_ende"].ToString().Trim();
                        objParticip.p_nr = dr["p_nr"].ToString().Trim();
                        objParticip.p_comend = dr["p_comend"].ToString().Trim();
                        objParticip.p_bairro = dr["p_bairro"].ToString().Trim();
                        objParticip.p_cida = dr["p_cida"].ToString().Trim();
                        objParticip.p_est = dr["p_est"].ToString().Trim();
                        objParticip.p_pais = dr["p_pais"].ToString().Trim();
                        objParticip.p_ibge = dr["p_ibge"].ToString().Trim();
                        objParticip.p_fone = dr["p_fone"].ToString().Trim();
                        objParticip.p_contat = dr["p_contat"].ToString().Trim();
                        objParticip.p_iest = dr["p_iest"].ToString().Trim();
                        objParticip.p_celul = dr["p_celul"].ToString().Trim();
                        objParticip.p_email = dr["p_email"].ToString().Trim();
                        objParticip.p_situac = dr["p_situac"].ToString().Trim();
                        objParticip.p_localiz = dr["p_localiz"].ToString().Trim();
                        objParticip.p_ramo = dr["p_ramo"].ToString().Trim();
                        objParticip.p_lcred = dr["p_lcred"] is DBNull ? 0 : Convert.ToDouble(dr["p_lcred"]);
                        objParticip.p_cultura = dr["p_cult"].ToString().Trim();
                        objParticip.p_nasc = dr["p_nasc"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["p_nasc"]);
                        objParticip.p_rg = dr["p_ident"].ToString().Trim();
                        objParticip.p_codNome = objParticip.p_clicod.ToString() + " - " + objParticip.p_nome;
                        objList.Add(objParticip);
                    }
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
                ex.Message.ToString();
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
        public static int verificaParticip(string p_cgc, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int p_cod;

            if (Conn.Database == "11536081000165")
            {
                p_cod = 0;
                return p_cod;
            }
            else
            {
                string sql = "SELECT p_cod FROM particip WHERE p_cgc='" + p_cgc + "'";
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
                            p_cod = Convert.ToInt32(dr["p_cod"]);
                            return p_cod;
                        }
                        else
                        {
                            p_cod = 0;
                            return p_cod;
                        }
                    }
                    else
                    {
                        p_cod = 0;
                        return p_cod;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    p_cod = 0;
                    return p_cod;
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
        public static bool attIDumov(string id, int p_clicod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {

                String sql = "UPDATE particip SET p_idumov=" + id + ", p_situac='S' WHERE p_cod=" + p_clicod;

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