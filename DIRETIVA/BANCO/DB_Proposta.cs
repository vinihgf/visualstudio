using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Proposta : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCodigo(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_id FROM proposta ORDER BY p_id DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["p_id"]) + 1;
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
                    Conn.Close();
            }
        }

        public static bool conferePermissaoRelatorio(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_icontrc FROM usudac WHERE u_email=@u_email";

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
                        string ret = dr["u_icontrc"].ToString().Trim();
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

        public static bool conferePermissao(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_arma FROM usudac WHERE u_email=@u_email";

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
                        string ret = dr["u_arma"].ToString().Trim();
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

        public static List<CL_Proposta> pesquisaProposta(string pesquisa, string filtro, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (pesquisa == "")
                sql = "SELECT p_id, p_clicod, p_nome as p_nome, p_data, p_apolice, p_proposta, p_municip, p_area, p_talhoes " +
                        "FROM proposta p, particip " +
                        "WHERE p_clicod = p_cod";
            else
            {
                if (filtro == "1")//NOME
                    sql = "SELECT p_id, p_clicod, p_nome as p_nome, p_data, p_apolice, p_proposta, p_municip, p_area, p_talhoes " +
                            "FROM proposta p, particip " +
                            "WHERE p_clicod = p_cod " +
                            "AND p_nome LIKE '%" + pesquisa + "%'";
                else if (filtro == "2")//PROPOSTA
                    sql = "SELECT p_id, p_clicod, p_nome as p_nome, p_data, p_apolice, p_proposta, p_municip, p_area, p_talhoes " +
                        "FROM proposta p, particip " +
                        "WHERE p_clicod = p_cod " +
                        "AND p_proposta = '" + pesquisa + "'";

                else if (filtro == "3")//APOLICE
                    sql = "SELECT p_id, p_clicod, p_nome as p_nome, p_data, p_apolice, p_proposta, p_municip, p_area, p_talhoes " +
                        "FROM proposta p, particip " +
                        "WHERE p_clicod = p_cod " +
                        "AND p_apolice = '" + pesquisa + "'";
            }


            List<CL_Proposta> objList = new List<CL_Proposta>();

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
                        objList.Add(new CL_Proposta()
                        {
                            p_id = Convert.ToInt32(dr["p_id"]),
                            p_data = Convert.ToDateTime(dr["p_data"]),
                            p_clinome = Convert.ToInt32(dr["p_clicod"]) + " - " + dr["p_nome"].ToString(),
                            p_apolice = dr["p_apolice"].ToString().Trim(),
                            p_proposta = dr["p_proposta"].ToString().Trim(),
                            p_municip = dr["p_municip"].ToString().Trim(),
                            p_area = Convert.ToDouble(dr["p_area"]),
                            p_talhoes = Convert.ToDouble(dr["p_talhoes"]),
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

        public static List<CL_Proposta> listarPendencias(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_id, p_clicod, p_nome as p_nome, p_apolice, p_proposta, p_cultura, p_seguradora, p_municip, p.p_situac "+
                        "FROM proposta p, particip "+
                        "WHERE p_clicod = p_cod "+
                        "AND p.p_situac = 'PD'";

            List<CL_Proposta> objList = new List<CL_Proposta>();

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
                        objList.Add(new CL_Proposta()
                        {
                            p_id = Convert.ToInt32(dr["p_id"]),
                            p_clinome = Convert.ToInt32(dr["p_clicod"]) + " - " + dr["p_nome"].ToString(),
                            p_apolice = dr["p_apolice"].ToString().Trim(),
                            p_proposta = dr["p_proposta"].ToString().Trim(),
                            p_cultura = dr["p_cultura"].ToString().Trim(),
                            p_seguradora = dr["p_seguradora"].ToString().Trim(),
                            p_municip = dr["p_municip"].ToString().Trim(),
                            p_situac = dr["p_situac"].ToString().Trim(),
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

        public static List<CL_Proposta> listaDDL(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_id, p_apolice, p_proposta, p_nome FROM proposta, particip WHERE p_clicod=p_cod";
            List<CL_Proposta> objList = new List<CL_Proposta>();

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
                        objList.Add(new CL_Proposta()
                        {
                            p_id = Convert.ToInt32(dr["p_id"]),
                            p_apoliceNome = dr["p_apolice"].ToString().Trim() + " - " + dr["p_nome"].ToString().Trim(),
                            p_propostaNome = dr["p_proposta"].ToString().Trim() + " - " + dr["p_nome"].ToString().Trim(),
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

        public static string cadProposta(CL_Proposta objProposta, List<CL_MovProposta> objListMovProp, List<CL_Duprec> objListDuprec, string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "INSERT INTO proposta (p_clicod, p_apolice, p_cultura, p_data, p_financiador, p_previsao, p_proposta, p_seguradora, p_area, p_premio, p_talhoes, p_parceiro, p_parcela, p_impseg, p_premseg, p_municip, p_cep, p_assina, p_situac, p_agronomo, p_tipo, p_chuva, p_tromba, p_geada, p_granizo, p_iniciop, p_fimp, p_inicioz, p_fimz, p_semente, p_replantio, p_usudac, p_vcto, p_perseg) " +
                    "VALUES " +
                    "(@p_clicod, @p_apolice, @p_cultura, @p_data, @p_financiador, @p_previsao, @p_proposta, @p_seguradora, @p_area, @p_premio, @p_talhoes, @p_parceiro, @p_parcela, @p_impseg, @p_premseg, @p_municip, @p_cep, @p_assina, @p_situac, @p_agronomo, @p_tipo, @p_chuva, @p_tromba, @p_geada, @p_granizo, @p_iniciop, @p_fimp, @p_inicioz, @p_fimz, @p_semente, @p_replantio, @p_usudac, @p_vcto, @p_perseg)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("p_clicod", objProposta.p_clicod);
                cmd.Parameters.AddWithValue("p_apolice", objProposta.p_apolice);
                cmd.Parameters.AddWithValue("p_cultura", objProposta.p_cultura);
                cmd.Parameters.AddWithValue("p_data", objProposta.p_data.ToShortDateString());
                cmd.Parameters.AddWithValue("p_financiador", objProposta.p_financiador);
                cmd.Parameters.AddWithValue("p_previsao", objProposta.p_previsao);
                cmd.Parameters.AddWithValue("p_proposta", objProposta.p_proposta);
                cmd.Parameters.AddWithValue("p_seguradora", objProposta.p_seguradora);
                cmd.Parameters.AddWithValue("p_area", objProposta.p_area);
                cmd.Parameters.AddWithValue("p_premio", objProposta.p_premioTotal);
                cmd.Parameters.AddWithValue("p_talhoes", objProposta.p_talhoes);
                cmd.Parameters.AddWithValue("p_impseg", objProposta.p_impSegurado);
                cmd.Parameters.AddWithValue("p_premseg", objProposta.p_premioSegurado);
                cmd.Parameters.AddWithValue("p_parceiro", objProposta.p_parceiro);
                cmd.Parameters.AddWithValue("p_parcela", objProposta.p_parcela);
                cmd.Parameters.AddWithValue("p_municip", objProposta.p_municip);
                cmd.Parameters.AddWithValue("p_cep", objProposta.p_cep);
                cmd.Parameters.AddWithValue("p_assina", objProposta.p_assina);
                cmd.Parameters.AddWithValue("p_situac", objProposta.p_situac);
                cmd.Parameters.AddWithValue("p_agronomo", objProposta.p_agronomo);
                cmd.Parameters.AddWithValue("p_tipo", objProposta.p_tipo);
                cmd.Parameters.AddWithValue("p_chuva", objProposta.p_chuva);
                cmd.Parameters.AddWithValue("p_tromba", objProposta.p_tromba);
                cmd.Parameters.AddWithValue("p_geada", objProposta.p_geada);
                cmd.Parameters.AddWithValue("p_granizo", objProposta.p_granizo);
                cmd.Parameters.AddWithValue("p_iniciop", objProposta.p_inicioPlantio);
                cmd.Parameters.AddWithValue("p_fimp", objProposta.p_fimPlantio);
                cmd.Parameters.AddWithValue("p_inicioz", objProposta.p_inicioZon);
                cmd.Parameters.AddWithValue("p_fimz", objProposta.p_fimZon);
                cmd.Parameters.AddWithValue("p_semente", objProposta.p_semente);
                cmd.Parameters.AddWithValue("p_replantio", objProposta.p_replantio);
                cmd.Parameters.AddWithValue("p_usudac", email);
                cmd.Parameters.AddWithValue("p_vcto", objProposta.p_vcto.ToShortDateString());
                cmd.Parameters.AddWithValue("p_perseg", objProposta.p_perseguradora);
                cmd.ExecuteScalar();
                transaction.Commit();

                if (objProposta.p_cultura != "PR")
                {
                    objProposta.p_id = buscaUltimoAdd(con, email);
                    if (objProposta.p_id > 0)
                    {
                        if (Conn.State == ConnectionState.Closed)
                            Conn.Open();
                        transaction = Conn.BeginTransaction();
                        foreach (var obj in objListMovProp)
                        {
                            sql = "INSERT INTO mov_proposta (mp_proposta, mp_area, mp_nome) " +
                                "VALUES " +
                                "(@mp_proposta, @mp_area, @mp_nome)";
                            cmd = new NpgsqlCommand(sql, Conn, transaction);
                            cmd.Parameters.AddWithValue("@mp_proposta", objProposta.p_id);
                            cmd.Parameters.AddWithValue("@mp_area", obj.mp_area);
                            cmd.Parameters.AddWithValue("@mp_nome", obj.mp_nome);
                            cmd.ExecuteScalar();
                        }

                        foreach (var obj in objListDuprec)
                        {
                            sql = "INSERT INTO duprec (dup_cod, dup_codcli, dup_comis, dup_emis, dup_nome, dup_nomven, dup_nota, dup_parc, dup_valor, dup_vcto, dup_vend, dup_pgto) " +
                                "VALUES " +
                                "(@dup_cod, @dup_codcli, @dup_comis, @dup_emis, @dup_nome, @dup_nomven, @dup_nota, @dup_parc, @dup_valor, @dup_vcto, @dup_vend, @dup_pgto)";
                            cmd = new NpgsqlCommand(sql, Conn, transaction);
                            cmd.Parameters.AddWithValue("dup_cod", obj.dup_cod);
                            cmd.Parameters.AddWithValue("dup_codcli", obj.dup_codcli);
                            cmd.Parameters.AddWithValue("dup_comis", obj.dup_comis);
                            cmd.Parameters.AddWithValue("dup_emis", obj.dup_emis.ToShortDateString());
                            cmd.Parameters.AddWithValue("dup_nome", obj.dup_nome);
                            cmd.Parameters.AddWithValue("dup_nomven", obj.dup_nomven);
                            cmd.Parameters.AddWithValue("dup_nota", objProposta.p_id);
                            cmd.Parameters.AddWithValue("dup_parc", obj.dup_parc);
                            cmd.Parameters.AddWithValue("dup_valor", obj.dup_valor);
                            cmd.Parameters.AddWithValue("dup_vcto", obj.dup_vcto);
                            cmd.Parameters.AddWithValue("dup_vend", obj.dup_vend);
                            cmd.Parameters.AddWithValue("dup_pgto", obj.dup_pgto);
                            cmd.ExecuteScalar();

                            sql = "INSERT INTO movduprec (mr_duplic, mr_parc, mr_data, mr_tipo, mr_codcli, mr_cliente, mr_bco, mr_stit, mr_vend, mr_nomeven, mr_comis, " +
                          "mr_valor, mr_hist1, mr_acumul) VALUES (@mr_duplic, @mr_parc, @mr_data, @mr_tipo, @mr_codcli, @mr_cliente, @mr_bco, @mr_stit, @mr_vend, " +
                          "@mr_nomeven, @mr_comis, @mr_valor, @mr_hist1, @mr_acumul)";
                            cmd = new NpgsqlCommand(sql, Conn, transaction);
                            cmd.Parameters.AddWithValue("mr_duplic", obj.dup_cod);
                            cmd.Parameters.AddWithValue("mr_parc", obj.dup_parc);
                            cmd.Parameters.AddWithValue("mr_data", obj.dup_emis);
                            cmd.Parameters.AddWithValue("mr_tipo", 2);
                            cmd.Parameters.AddWithValue("mr_codcli", obj.dup_codcli);
                            cmd.Parameters.AddWithValue("mr_cliente", obj.dup_nome);
                            cmd.Parameters.AddWithValue("mr_bco", obj.dup_bco);
                            cmd.Parameters.AddWithValue("mr_stit", obj.dup_stit);
                            cmd.Parameters.AddWithValue("mr_vend", obj.dup_vend);
                            cmd.Parameters.AddWithValue("mr_nomeven", obj.dup_nomven);
                            cmd.Parameters.AddWithValue("mr_comis", obj.dup_comis);
                            cmd.Parameters.AddWithValue("mr_valor", obj.dup_valor);
                            cmd.Parameters.AddWithValue("mr_hist1", "Valor do titulo");
                            cmd.Parameters.AddWithValue("mr_acumul", "D");
                            cmd.ExecuteScalar();
                        }
                        transaction.Commit();
                        return "OK";
                    }
                    else
                    {
                        transaction.Rollback();
                        return "ERRO AO GRAVAR AS ÁREAS REFERENTE A PROPOSTA";
                    }
                }
                else
                    return "OK";
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return ex.ToString();
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        private static int buscaUltimoAdd(string con, string email)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_id FROM proposta WHERE p_usudac=@p_usudac ORDER BY p_id DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_usudac", email);
            NpgsqlDataReader dr;
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["p_id"]);
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
                    Conn.Close();
            }
        }

        public static CL_Proposta buscaProposta(int codigo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM proposta WHERE p_id=@p_id";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_id", codigo);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        CL_Proposta obj = new CL_Proposta();
                        obj.p_id = codigo;
                        obj.p_proposta = dr["p_proposta"].ToString().Trim();
                        obj.p_apolice = dr["p_apolice"].ToString().Trim();
                        obj.p_area = Convert.ToDouble(dr["p_area"]);
                        obj.p_clicod = Convert.ToInt32(dr["p_clicod"]);
                        obj.p_cultura = dr["p_cultura"].ToString().Trim();
                        obj.p_data = Convert.ToDateTime(dr["p_data"]);
                        obj.p_financiador = dr["p_financiador"].ToString().Trim();
                        obj.p_parceiro = Convert.ToInt32(dr["p_parceiro"]);
                        obj.p_previsao = dr["p_previsao"].ToString().Trim();
                        obj.p_seguradora = dr["p_seguradora"].ToString().Trim();
                        obj.p_talhoes = Convert.ToDouble(dr["p_talhoes"]);
                        obj.p_premioSegurado = Convert.ToDouble(dr["p_premseg"]);
                        obj.p_parcela = Convert.ToInt32(dr["p_parcela"]);
                        obj.p_premioTotal = Convert.ToDouble(dr["p_premio"]);
                        obj.p_impSegurado = Convert.ToDouble(dr["p_impseg"]);
                        obj.p_premioSegurado = Convert.ToDouble(dr["p_premseg"]);
                        obj.p_municip = dr["p_municip"].ToString().Trim();
                        obj.p_cep= dr["p_cep"].ToString().Trim();
                        obj.p_assina = dr["p_assina"].ToString().Trim();
                        obj.p_situac = dr["p_situac"].ToString().Trim();
                        obj.p_agronomo = dr["p_agronomo"].ToString().Trim();
                        obj.p_tipo = dr["p_tipo"].ToString().Trim();
                        obj.p_chuva = dr["p_chuva"].ToString().Trim();
                        obj.p_tromba = dr["p_tromba"].ToString().Trim();
                        obj.p_geada = dr["p_geada"].ToString().Trim();
                        obj.p_granizo = dr["p_granizo"].ToString().Trim();
                        obj.p_inicioPlantio = dr["p_iniciop"].ToString().Trim();
                        obj.p_fimPlantio = dr["p_fimp"].ToString().Trim();
                        obj.p_inicioZon = dr["p_inicioz"].ToString().Trim();
                        obj.p_fimZon = dr["p_fimz"].ToString().Trim();
                        obj.p_semente = dr["p_semente"].ToString().Trim();
                        obj.p_replantio = dr["p_replantio"].ToString().Trim();
                        obj.p_vcto = dr["p_vcto"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["p_vcto"]);
                        obj.p_perseguradora = dr["p_perseg"] is DBNull ? 0 : Convert.ToDouble(dr["p_perseg"]);
                        return obj;
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

        public static bool alteraProposta(CL_Proposta objProposta, List<CL_MovProposta> objListMovProp, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "DELETE FROM mov_proposta WHERE mp_proposta=@p_id;";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("p_id", objProposta.p_id);
                cmd.ExecuteScalar();

                sql = "UPDATE proposta SET p_clicod=@p_clicod, p_apolice=@p_apolice, p_cultura=@p_cultura, p_data=@p_data, p_financiador=@p_financiador, p_previsao=@p_previsao, "+
                    "p_proposta=@p_proposta, p_seguradora=@p_seguradora, p_area=@p_area, p_premio=@p_premio, p_talhoes=@p_talhoes, p_parceiro=@p_parceiro, p_parcela=@p_parcela, "+
                    "p_impseg=@p_impseg, p_premseg=@p_premseg, p_municip=@p_municip, p_cep=@p_cep, p_assina=@p_assina, p_situac=@p_situac, p_agronomo=@p_agronomo, p_tipo=@p_tipo, "+
                    "p_chuva=@p_chuva, p_tromba=@p_tromba, p_geada=@p_geada, p_granizo=@p_granizo, p_iniciop=@p_iniciop, p_fimp=@p_fimp, p_inicioz=@p_inicioz, p_fimz=@p_fimz, "+
                    "p_semente=@p_semente, p_replantio=@p_replantio, p_vcto=@p_vcto, p_perseg=@p_perseg WHERE p_id=@p_id";
                cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("p_id", objProposta.p_id);
                cmd.Parameters.AddWithValue("p_clicod", objProposta.p_clicod);
                cmd.Parameters.AddWithValue("p_apolice", objProposta.p_apolice);
                cmd.Parameters.AddWithValue("p_cultura", objProposta.p_cultura);
                cmd.Parameters.AddWithValue("p_data", objProposta.p_data.ToShortDateString());
                cmd.Parameters.AddWithValue("p_financiador", objProposta.p_financiador);
                cmd.Parameters.AddWithValue("p_previsao", objProposta.p_previsao);
                cmd.Parameters.AddWithValue("p_proposta", objProposta.p_proposta);
                cmd.Parameters.AddWithValue("p_seguradora", objProposta.p_seguradora);
                cmd.Parameters.AddWithValue("p_area", objProposta.p_area);
                cmd.Parameters.AddWithValue("p_premio", objProposta.p_premioTotal);
                cmd.Parameters.AddWithValue("p_talhoes", objProposta.p_talhoes);
                cmd.Parameters.AddWithValue("p_talhoes", objProposta.p_talhoes);
                cmd.Parameters.AddWithValue("p_impseg", objProposta.p_impSegurado);
                cmd.Parameters.AddWithValue("p_premseg", objProposta.p_premioSegurado);
                cmd.Parameters.AddWithValue("p_parceiro", objProposta.p_parceiro);
                cmd.Parameters.AddWithValue("p_parcela", objProposta.p_parcela);
                cmd.Parameters.AddWithValue("p_municip", objProposta.p_municip);
                cmd.Parameters.AddWithValue("p_cep", objProposta.p_cep);
                cmd.Parameters.AddWithValue("p_assina", objProposta.p_assina);
                cmd.Parameters.AddWithValue("p_situac", objProposta.p_situac);
                cmd.Parameters.AddWithValue("p_agronomo", objProposta.p_agronomo);
                cmd.Parameters.AddWithValue("p_tipo", objProposta.p_tipo);
                cmd.Parameters.AddWithValue("p_chuva", objProposta.p_chuva);
                cmd.Parameters.AddWithValue("p_tromba", objProposta.p_tromba);
                cmd.Parameters.AddWithValue("p_geada", objProposta.p_geada);
                cmd.Parameters.AddWithValue("p_granizo", objProposta.p_granizo);
                cmd.Parameters.AddWithValue("p_iniciop", objProposta.p_inicioPlantio);
                cmd.Parameters.AddWithValue("p_fimp", objProposta.p_fimPlantio);
                cmd.Parameters.AddWithValue("p_inicioz", objProposta.p_inicioZon);
                cmd.Parameters.AddWithValue("p_fimz", objProposta.p_fimZon);
                cmd.Parameters.AddWithValue("p_semente", objProposta.p_semente);
                cmd.Parameters.AddWithValue("p_replantio", objProposta.p_replantio);
                cmd.Parameters.AddWithValue("p_vcto", objProposta.p_vcto.ToShortDateString());
                cmd.Parameters.AddWithValue("p_perseg", objProposta.p_perseguradora);
                cmd.ExecuteScalar();

                foreach (var obj in objListMovProp)
                {
                    sql = "INSERT INTO mov_proposta (mp_proposta, mp_area, mp_nome) " +
                        "VALUES " +
                        "(@mp_proposta, @mp_area, @mp_nome)";
                    cmd = new NpgsqlCommand(sql, Conn, transaction);
                    cmd.Parameters.AddWithValue("@mp_proposta", objProposta.p_id);
                    cmd.Parameters.AddWithValue("@mp_area", obj.mp_area);
                    cmd.Parameters.AddWithValue("@mp_nome", obj.mp_nome);
                    cmd.ExecuteScalar();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static bool excluiProposta(CL_Proposta objProposta, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "DELETE FROM duprec WHERE dup_nota=@p_id; DELETE FROM mov_proposta WHERE mp_proposta=@p_id; DELETE FROM proposta WHERE p_id=@p_id;";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("p_id", objProposta.p_id);
                cmd.ExecuteScalar();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
    }
}