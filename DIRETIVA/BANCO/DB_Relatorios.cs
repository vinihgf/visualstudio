using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Relatorios : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static List<REL_Contrato> getRelatorioContrato(string contr, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT l.l_codigo, l.l_clicod, l.l_emis, l.l_dev, l.l_patrimon, l.l_descri, l.l_dmy, l.l_tempo, l.l_valor, l.l_situac" +
                ", e.e_cod, e.e_nserie, e.e_modelo, e.e_marca" +
                ", p.p_nome, p.p_ende, p.p_cida, p.p_est, p.p_bairro, p.p_cep, p.p_cgc, p.p_iest, p.p_fone, p.p_email, p.p_comend, p.p_nr" +
                ", emp_fantas, emp_end, emp_nr, emp_bairro, emp_cida, emp_cep, emp_est, emp_fone, emp_cgc, emp_iscest, emp_email, emp_site, emp_comend" +
                " FROM locacao l, equipamento e, particip p, empresa" +
                " WHERE l.l_patrimon = e.e_patrimon AND l.l_clicod = p.p_cod AND l.l_contr=" + contr;


            List<REL_Contrato> objList = new List<REL_Contrato>();
            REL_Contrato obj = null;

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
                        obj = new REL_Contrato();
                        obj.l_cod = Convert.ToInt32(dr["l_codigo"]);
                        obj.l_clicod = Convert.ToInt32(dr["l_clicod"]);
                        obj.l_emis = Convert.ToDateTime(dr["l_emis"]);
                        obj.l_dev = Convert.ToDateTime(dr["l_dev"]);
                        obj.l_contr = Convert.ToInt32(contr);
                        obj.l_patrimon = dr["l_patrimon"].ToString().Trim();
                        obj.l_descri = dr["l_descri"].ToString().Trim();
                        obj.l_dmy = dr["l_dmy"].ToString().Trim();
                        obj.l_tempo = Convert.ToInt32(dr["l_tempo"]);
                        obj.l_valor = Convert.ToDouble(dr["l_valor"]);
                        obj.l_situac = dr["l_situac"].ToString().Trim();

                        obj.e_cod = Convert.ToInt32(dr["e_cod"]);
                        obj.e_nserie = dr["e_nserie"].ToString().Trim();
                        obj.e_modelo = dr["e_modelo"].ToString().Trim();
                        obj.e_marca = dr["e_marca"].ToString().Trim();

                        obj.p_nome = dr["p_nome"].ToString().Trim();
                        obj.p_ende = dr["p_ende"].ToString().Trim();
                        obj.p_cida = dr["p_cida"].ToString().Trim();
                        obj.p_est = dr["p_est"].ToString().Trim();
                        obj.p_bairro = dr["p_bairro"].ToString().Trim();
                        obj.p_cep = dr["p_cep"].ToString().Trim();
                        obj.p_cgc = dr["p_cgc"].ToString().Trim();
                        obj.p_iest = dr["p_iest"].ToString().Trim();
                        obj.p_fone = dr["p_fone"].ToString().Trim();
                        obj.p_email = dr["p_email"].ToString().Trim();
                        obj.p_comend = dr["p_comend"].ToString().Trim();
                        obj.p_nr = dr["p_nr"].ToString().Trim();

                        obj.emp_fantas = dr["emp_fantas"].ToString().Trim();
                        obj.emp_ende = dr["emp_end"].ToString().Trim();
                        obj.emp_comend = dr["emp_comend"].ToString().Trim();
                        obj.emp_nr = Convert.ToInt32(dr["emp_nr"]);
                        obj.emp_bairro = dr["emp_bairro"].ToString().Trim();
                        obj.emp_cida = dr["emp_cida"].ToString().Trim();
                        obj.emp_cep = dr["emp_cep"].ToString().Trim();
                        obj.emp_est = dr["emp_est"].ToString().Trim();
                        obj.emp_fone = dr["emp_fone"].ToString().Trim();
                        obj.emp_cgc = dr["emp_cgc"].ToString().Trim();
                        obj.emp_iscest = dr["emp_iscest"].ToString().Trim();
                        obj.emp_email = dr["emp_email"].ToString().Trim();
                        obj.emp_site = dr["emp_site"].ToString().Trim();


                        objList.Add(obj);
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

        public static List<REL_Sinistro> getRelSinistro(DateTime dataI, DateTime dataF, int particip, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (particip == 0)
            {
                sql = "SELECT s_cod, s_proposta, s_comunica, s_resumo, s_evento, s_eventof, s_deferido, s_deferimento,"+
                        "p_clicod, p_nome, p_apolice, p_seguradora, p_cultura, p_proposta, p_area, p_talhoes, p_municip, p_agronomo  "+
                        "FROM sinistro, proposta, particip "+
                        "WHERE s_proposta = p_id "+
                        "AND p_clicod = p_cod "+
                        "AND s_comunica>= @dataI " +
                        "AND s_comunica<= @dataF";
            }
            else
            {
                sql = "SELECT s_cod, s_proposta, s_comunica, s_resumo, s_evento, s_eventof, s_deferido, s_deferimento," +
                        "p_clicod, p_nome, p_apolice, p_cultura, p_seguradora, p_proposta, p_area, p_talhoes, p_municip, p_agronomo  " +
                        "FROM sinistro, proposta, particip " +
                        "WHERE s_proposta = p_id " +
                        "AND p_clicod = p_cod " +
                        "AND p_clicod = @p_clicod " +
                        "AND s_comunica>= @dataI " +
                        "AND s_comunica<= @dataF";
            }

            List<REL_Sinistro> objList = new List<REL_Sinistro>();
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_clicod", particip);
            comand.Parameters.AddWithValue("dataI", dataI);
            comand.Parameters.AddWithValue("dataF", dataF);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new REL_Sinistro()
                        {
                            s_cod = Convert.ToInt32(dr["s_cod"]),
                            s_proposta = dr["p_proposta"].ToString().Trim(),
                            s_comunica = dr["s_comunica"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_comunica"]),
                            s_resumo = dr["s_resumo"].ToString().Trim(),
                            s_evento = dr["s_evento"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_evento"]),
                            s_eventof = dr["s_eventof"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["s_eventof"]),
                            s_deferido = dr["s_deferido"].ToString().Trim(),
                            s_deferimento = dr["s_deferimento"].ToString().Trim(),
                            s_cliente = dr["p_clicod"].ToString().Trim() + " - " + dr["p_nome"].ToString().Trim(),
                            s_apolice = dr["p_apolice"].ToString().Trim(),
                            s_area = Convert.ToDouble(dr["p_area"]),
                            s_talhoes = Convert.ToDouble(dr["p_talhoes"]),
                            s_agronomo = dr["p_agronomo"].ToString().Trim(),
                            s_cultura = dr["p_cultura"].ToString().Trim(),
                            s_seguradora = dr["p_seguradora"].ToString().Trim(),
                            s_municip = dr["p_municip"].ToString().Trim(),
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

        public static List<CL_Proposta> getRelProposta(DateTime dataInicial, DateTime dataFinal, int particip, int parceiro, string con)
        {

            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT p_id, p_clicod, p_nome, p_apolice, p_proposta, p_cultura, p_data, p_financiador, p_previsao, p_seguradora, p_area, p_talhoes, "+
                        "p_premio, p_impseg, p_premseg, p_municip, p_agronomo, p_semente, p_parceiro, con_nome, con_comis, con_corretor "+
                        "FROM proposta RIGTH JOIN particip ON p_clicod = p_cod "+
                        "LEFT JOIN convenio ON p_parceiro = con_cod ORDER BY p_id";
            if(particip == 0)
            {
                if(parceiro == 0)
                {
                    sql = "SELECT p_id, p_clicod, p_nome, p_apolice, p_proposta, p_cultura, p_data, p_financiador, p_previsao, p_seguradora, p_area, p_talhoes, " +
                        "p_premio, p_impseg, p_premseg, p_municip, p_agronomo, p_semente, p_parceiro, con_nome, con_comis, con_corretor " +
                        "FROM proposta RIGTH JOIN particip ON p_clicod = p_cod " +
                        "LEFT JOIN convenio ON p_parceiro = con_cod ORDER BY p_id";
                }
                else
                {
                    sql = "SELECT p_id, p_clicod, p_nome, p_apolice, p_proposta, p_cultura, p_data, p_financiador, p_previsao, p_seguradora, p_area, p_talhoes, " +
                        "p_premio, p_impseg, p_premseg, p_municip, p_agronomo, p_semente, p_parceiro, con_nome, con_comis, con_corretor " +
                        "FROM proposta RIGTH JOIN particip ON p_clicod = p_cod " +
                        "LEFT JOIN convenio ON p_parceiro = con_cod WHERE p_parceiro=@p_parceiro ORDER BY p_id";
                }
            }
            else
            {
                if(parceiro == 0)
                {
                    sql = "SELECT p_id, p_clicod, p_nome, p_apolice, p_proposta, p_cultura, p_data, p_financiador, p_previsao, p_seguradora, p_area, p_talhoes, " +
                        "p_premio, p_impseg, p_premseg, p_municip, p_agronomo, p_semente, p_parceiro, con_nome, con_comis, con_corretor " +
                        "FROM proposta RIGTH JOIN particip ON p_clicod = p_cod " +
                        "LEFT JOIN convenio ON p_parceiro = con_cod WHERE p_clicod=@p_clicod ORDER BY p_id";
                }
                else
                {
                    sql = "SELECT p_id, p_clicod, p_nome, p_apolice, p_proposta, p_cultura, p_data, p_financiador, p_previsao, p_seguradora, p_area, p_talhoes, " +
                        "p_premio, p_impseg, p_premseg, p_municip, p_agronomo, p_semente, p_parceiro, con_nome, con_comis, con_corretor " +
                        "FROM proposta RIGTH JOIN particip ON p_clicod = p_cod " +
                        "LEFT JOIN convenio ON p_parceiro = con_cod WHERE p_clicod=@p_clicod AND p_parceiro=@p_parceiro ORDER BY p_id";
                }
            }

            List<CL_Proposta> objList = new List<CL_Proposta>();
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_parceiro", parceiro);
            comand.Parameters.AddWithValue("p_clicod", particip);
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
                            p_data = Convert.ToDateTime(dr["p_data"]),
                            p_financiador = dr["p_financiador"].ToString().Trim(),
                            p_previsao = dr["p_previsao"].ToString().Trim(),
                            p_seguradora = dr["p_seguradora"].ToString().Trim(),
                            p_area = Convert.ToDouble(dr["p_area"]),
                            p_talhoes = Convert.ToDouble(dr["p_talhoes"]),
                            p_premioTotal = Convert.ToDouble(dr["p_premio"]),
                            p_impSegurado = Convert.ToDouble(dr["p_impseg"]),
                            p_premioSegurado = Convert.ToDouble(dr["p_premseg"]),
                            p_municip = dr["p_municip"].ToString().Trim(),
                            p_semente = dr["p_semente"].ToString().Trim(),
                            p_agronomo = dr["p_agronomo"].ToString().Trim(),
                            p_parcnome = dr["con_nome"].ToString().Trim(),
                            p_corretor = dr["con_corretor"] is DBNull ? "N" : dr["con_corretor"].ToString().Trim(),
                            p_parccomis = dr["con_comis"] is DBNull ? 0 : Convert.ToDouble(dr["con_comis"]),
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

        public static List<REL_Patrimon> getPatrimon(DateTime dataI, DateTime dataF, string patrimon, string servico, string situac, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            List<REL_Patrimon> objList = new List<REL_Patrimon>();
            REL_Patrimon obj = null;
            string sql = "SELECT emp_nome, emp_end, emp_nr, emp_comend, emp_bairro, emp_cida, emp_est, emp_cep, emp_foto," +
                    " emp_fone, p_cod, p_nome, o_cod, o_emis, o_setor, o_valor, o_serv, o_servexec, e_patrimon, e_modelo" +
                    " FROM empresa, particip, oserv, equipamento" +
                    " WHERE e_cod=o_equipcod";
            if (dataI.ToShortDateString() != "01/01/0001")
                sql += " AND o_emis>=@dataI AND o_emis<=@dataF";

            if (situac != "A")
                sql += " AND o_situac=@situac";

            sql += " AND o_clicod=p_cod" +
                        " AND e_patrimon=@patrimon" +
                        " ORDER BY o_cod";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dataI", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataF", dataF.ToShortDateString());
            comand.Parameters.AddWithValue("situac", situac);
            comand.Parameters.AddWithValue("patrimon", patrimon);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        obj = new REL_Patrimon();
                        obj.emp_nome = dr["emp_nome"].ToString().Trim();
                        obj.emp_ende = dr["emp_end"].ToString().Trim();
                        obj.emp_nr = dr["emp_nr"].ToString().Trim();
                        obj.emp_bairro = dr["emp_bairro"].ToString().Trim();
                        obj.emp_cida = dr["emp_cida"].ToString().Trim();
                        obj.emp_est = dr["emp_est"].ToString().Trim();
                        obj.emp_cep = dr["emp_cep"].ToString().Trim();
                        obj.emp_comend = dr["emp_comend"].ToString().Trim();
                        obj.emp_fone = dr["emp_fone"].ToString().Trim();
                        obj.emp_foto = dr["emp_foto"].ToString().Trim();

                        obj.p_nome = dr["p_nome"].ToString().Trim();
                        obj.p_clicod = Convert.ToInt32(dr["p_cod"]);

                        obj.o_cod = Convert.ToInt32(dr["o_cod"]);
                        obj.o_emis = dr["o_emis"].ToString().Trim().Replace("00:00:00", "");
                        obj.o_setor = dr["o_setor"].ToString().Trim();
                        obj.o_defeito = dr["o_serv"].ToString().Trim();
                        obj.o_servExec = dr["o_servexec"].ToString().Trim();
                        obj.o_valor = Convert.ToDouble(dr["o_valor"]);
                        obj.o_patrimon = dr["e_patrimon"].ToString().Trim();
                        obj.o_modelo = dr["e_modelo"].ToString().Trim();

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

        public static List<CL_RelOsCli> getPesqOsCli(int clicod, int codend, string situac, DateTime dataI, DateTime dataF, string setor, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            List<CL_RelOsCli> objList = new List<CL_RelOsCli>();
            CL_RelOsCli obj = null;
            string sql = "";
            if (setor != "")
            {
                sql = "SELECT emp_nome, emp_end, emp_nr, emp_comend, emp_bairro, emp_cida, emp_est, emp_cep, emp_foto," +
                    " emp_fone, p_cod, p_nome, p_cgc, p_fone, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est," +
                    " p_cep, o_cod, o_emis, o_setor, o_valor, o_codend, e_patrimon, e_modelo" +
                    " FROM empresa, particip, oserv, equipamento" +
                    " WHERE e_cod = o_equipcod" +
                    " AND o_emis >= @dataI AND o_emis <= @dataF" +
                    " AND o_situac = @situac" +
                    " AND o_clicod = p_cod" +
                    " AND p_cod = @clicod" +
                    " AND o_setor = @setor" +
                    " AND o_codend = @codend" +
                    " ORDER BY o_cod";
            }
            else
            {
                sql = "SELECT emp_nome, emp_end, emp_nr, emp_comend, emp_bairro, emp_cida, emp_est, emp_cep, emp_foto," +
                    " emp_fone, p_cod, p_nome, p_cgc, p_fone, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est," +
                    " p_cep, o_cod, o_emis, o_setor, o_valor, e_patrimon, e_modelo" +
                    " FROM empresa, particip, oserv, equipamento" +
                    " WHERE e_cod = o_equipcod" +
                    " AND o_emis >= @dataI AND o_emis <= @dataF" +
                    " AND o_situac = @situac" +
                    " AND o_clicod = p_cod" +
                    " AND p_cod = @clicod" +
                    " AND o_codend = @codend" +
                    " ORDER BY o_cod";
            }

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dataI", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataF", dataF.ToShortDateString());
            comand.Parameters.AddWithValue("situac", situac);
            comand.Parameters.AddWithValue("clicod", clicod);
            comand.Parameters.AddWithValue("setor", setor);
            comand.Parameters.AddWithValue("codend", codend);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        obj = new CL_RelOsCli();
                        obj.emp_nome = dr["emp_nome"].ToString().Trim();
                        obj.emp_ende = dr["emp_end"].ToString().Trim();
                        obj.emp_nr = dr["emp_nr"].ToString().Trim();
                        obj.emp_bairro = dr["emp_bairro"].ToString().Trim();
                        obj.emp_cida = dr["emp_cida"].ToString().Trim();
                        obj.emp_est = dr["emp_est"].ToString().Trim();
                        obj.emp_cep = dr["emp_cep"].ToString().Trim();
                        obj.emp_comend = dr["emp_comend"].ToString().Trim();
                        obj.emp_fone = dr["emp_fone"].ToString().Trim();
                        obj.emp_foto = dr["emp_foto"].ToString().Trim();

                        obj.p_nome = dr["p_nome"].ToString().Trim();
                        obj.p_ende = dr["p_ende"].ToString().Trim();
                        obj.p_nr = dr["p_nr"].ToString().Trim();
                        obj.p_comend = dr["p_comend"].ToString().Trim();
                        obj.p_bairro = dr["p_bairro"].ToString().Trim();
                        obj.p_cida = dr["p_cida"].ToString().Trim();
                        obj.p_est = dr["p_est"].ToString().Trim();
                        obj.p_cep = dr["p_cep"].ToString().Trim();
                        obj.p_cgc = dr["p_cgc"].ToString().Trim();
                        obj.p_fone = dr["p_fone"].ToString().Trim();
                        obj.p_clicod = Convert.ToInt32(dr["p_cod"]);

                        obj.o_cod = Convert.ToInt32(dr["o_cod"]);
                        obj.o_emis = dr["o_emis"].ToString().Trim().Replace("00:00:00", "");
                        obj.o_setor = dr["o_setor"].ToString().Trim();
                        obj.o_valor = dr["o_valor"] is DBNull ? 0 : Convert.ToDouble(dr["o_valor"]);
                        obj.o_patrimon = dr["e_patrimon"].ToString().Trim();
                        obj.o_modelo = dr["e_modelo"].ToString().Trim();

                        objList.Add(obj);
                    }
                    dr.Close();
                    return objList;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static List<REL_encerraRequis> getEncerraRequis(int req_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT req_cod, req_data, req_qtdade, req_qtfat, est_nome, est_cod, p_cod, p_nome, mec_nome, mec_cod, os_mecanic FROM requis, particip, mecanico, servico WHERE req_cod=" + req_cod +
                " AND os_cod=" + req_cod + " AND os_mecanic=mec_cod AND req_codcli=p_cod";

            List<REL_encerraRequis> objList = new List<REL_encerraRequis>();
            REL_encerraRequis obj = null;

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
                        obj = new REL_encerraRequis();
                        obj.req_cod = Convert.ToInt32(dr["req_cod"]);
                        obj.req_data = Convert.ToDateTime(dr["req_data"]);
                        obj.p_clicod = Convert.ToInt32(dr["p_cod"]);
                        obj.p_clinome = dr["p_nome"].ToString().Trim();
                        obj.u_codigo = Convert.ToInt32(dr["u_codigo"]);
                        obj.mec_nome = dr["mec_nome"].ToString().Trim();
                        obj.est_cod = dr["est_cod"].ToString().Trim();
                        obj.est_nome = dr["est_nome"].ToString().Trim();
                        obj.req_qtd = Convert.ToDouble(dr["req_qtdade"]);
                        obj.req_qtdFat = Convert.ToDouble(dr["req_qtfat"]);
                        obj.req_devolver = obj.req_qtd - obj.req_qtdFat;
                        objList.Add(obj);
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

        public static REL_Protocolo getProtocolo(REL_Protocolo objRelProt, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT emp_nome, emp_end, emp_nr, emp_bairro, emp_cida, emp_est, emp_cep, emp_comend, emp_cgc, emp_fone, emp_foto," +
                " p_nome, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est, p_cep, p_cgc, p_fone," +
                " o_cod, o_emis, o_obs, e_marca, e_modelo, e_nserie, e_descri, e_patrimon, mec_nome, mec_cod " +
                " FROM empresa, particip, oserv, mecanico, equipamento WHERE p_cod=o_clicod AND o_mecanic=mec_cod AND o_equipcod=e_cod AND o_cod=" + objRelProt.o_cod;
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
                        objRelProt.emp_nome = dr["emp_nome"].ToString().Trim();
                        objRelProt.emp_ende = dr["emp_end"].ToString().Trim();
                        objRelProt.emp_nr = Convert.ToInt32(dr["emp_nr"]);
                        objRelProt.emp_bairro = dr["emp_bairro"].ToString().Trim();
                        objRelProt.emp_cida = dr["emp_cida"].ToString().Trim();
                        objRelProt.emp_est = dr["emp_est"].ToString().Trim();
                        objRelProt.emp_cep = dr["emp_cep"].ToString().Trim();
                        objRelProt.emp_comend = dr["emp_comend"].ToString().Trim();
                        objRelProt.emp_cgc = dr["emp_cgc"].ToString().Trim();
                        objRelProt.emp_fone = dr["emp_fone"].ToString().Trim();
                        objRelProt.emp_foto = dr["emp_foto"].ToString().Trim();

                        objRelProt.p_nome = dr["p_nome"].ToString().Trim();
                        objRelProt.p_ende = dr["p_ende"].ToString().Trim();
                        objRelProt.p_nr = dr["p_nr"].ToString().Trim();
                        objRelProt.p_comend = dr["p_comend"].ToString().Trim();
                        objRelProt.p_bairro = dr["p_bairro"].ToString().Trim();
                        objRelProt.p_cida = dr["p_cida"].ToString().Trim();
                        objRelProt.p_est = dr["p_est"].ToString().Trim();
                        objRelProt.p_cep = dr["p_cep"].ToString().Trim();
                        objRelProt.p_cgc = dr["p_cgc"].ToString().Trim();
                        objRelProt.p_fone = dr["p_fone"].ToString().Trim();

                        objRelProt.o_emis = Convert.ToDateTime(dr["o_emis"]);
                        objRelProt.o_obs = dr["o_obs"].ToString().Trim();

                        objRelProt.e_marca = dr["e_marca"].ToString().Trim();
                        objRelProt.e_modelo = dr["e_modelo"].ToString().Trim();
                        objRelProt.e_serie = dr["e_nserie"].ToString().Trim();
                        objRelProt.e_descri = dr["e_descri"].ToString().Trim();
                        objRelProt.e_patrimon = dr["e_patrimon"].ToString().Trim();

                        objRelProt.mec_nome = dr["mec_nome"].ToString().Trim();

                        return objRelProt;
                    }
                    else
                    {
                        objRelProt = null;
                        return objRelProt;
                    }
                }
                else
                {
                    objRelProt = null;
                    return objRelProt;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objRelProt = null;
                return objRelProt;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static REL_OS7 getOS7(REL_OS7 objRelOS, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT emp_nome, emp_end, emp_nr, emp_bairro, emp_cida, emp_est, emp_cep, emp_comend, emp_fone, emp_foto," +
                " p_nome, p_ende, p_nr, p_comend, p_bairro, p_cida, p_est, p_cep, p_cgc, p_fone," +
                " o_cod, o_emis, o_servexec, o_aces, o_valor, o_serv, o_obs, e_marca, e_modelo, e_nserie, e_descri, e_patrimon, mec_nome, mec_cod " +
                " FROM empresa, particip, oserv, mecanico, equipamento WHERE p_cod=o_clicod AND o_mecanic=mec_cod AND o_equipcod=e_cod AND o_cod=" + objRelOS.o_cod;
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
                        objRelOS.emp_nome = dr["emp_nome"].ToString().Trim();
                        objRelOS.emp_ende = dr["emp_end"].ToString().Trim();
                        objRelOS.emp_nr = dr["emp_nr"].ToString().Trim();
                        objRelOS.emp_bairro = dr["emp_bairro"].ToString().Trim();
                        objRelOS.emp_cida = dr["emp_cida"].ToString().Trim();
                        objRelOS.emp_est = dr["emp_est"].ToString().Trim();
                        objRelOS.emp_cep = dr["emp_cep"].ToString().Trim();
                        objRelOS.emp_comend = dr["emp_comend"].ToString().Trim();
                        objRelOS.emp_fone = dr["emp_fone"].ToString().Trim();
                        objRelOS.emp_foto = dr["emp_foto"].ToString().Trim();

                        objRelOS.p_nome = dr["p_nome"].ToString().Trim();
                        objRelOS.p_ende = dr["p_ende"].ToString().Trim();
                        objRelOS.p_nr = dr["p_nr"].ToString().Trim();
                        objRelOS.p_comend = dr["p_comend"].ToString().Trim();
                        objRelOS.p_bairro = dr["p_bairro"].ToString().Trim();
                        objRelOS.p_cida = dr["p_cida"].ToString().Trim();
                        objRelOS.p_est = dr["p_est"].ToString().Trim();
                        objRelOS.p_cep = dr["p_cep"].ToString().Trim();
                        objRelOS.p_cgc = dr["p_cgc"].ToString().Trim();
                        objRelOS.p_fone = dr["p_fone"].ToString().Trim();

                        objRelOS.o_emis = Convert.ToDateTime(dr["o_emis"]);
                        objRelOS.o_cod = Convert.ToInt32(dr["o_cod"]);
                        objRelOS.o_serv = dr["o_serv"].ToString().Trim();
                        objRelOS.o_tot = Convert.ToDouble(dr["o_valor"]);
                        objRelOS.o_servexec = dr["o_servexec"].ToString().Trim();
                        objRelOS.o_obs = dr["o_obs"].ToString().Trim();
                        objRelOS.o_aces = dr["o_aces"].ToString().Trim();

                        objRelOS.e_marca = dr["e_marca"].ToString().Trim();
                        objRelOS.e_modelo = dr["e_modelo"].ToString().Trim();
                        objRelOS.e_serie = dr["e_nserie"].ToString().Trim();
                        objRelOS.e_descri = dr["e_descri"].ToString().Trim();
                        objRelOS.e_patrimon = dr["e_patrimon"].ToString().Trim();

                        objRelOS.mec_nome = dr["mec_nome"].ToString().Trim();

                        return objRelOS;
                    }
                    else
                    {
                        objRelOS = null;
                        return objRelOS;
                    }
                }
                else
                {
                    objRelOS = null;
                    return objRelOS;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objRelOS = null;
                return objRelOS;
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