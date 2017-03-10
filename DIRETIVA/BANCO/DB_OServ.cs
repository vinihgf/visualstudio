using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_OServ : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static long buscaCod(long o_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT o_cod FROM oserv ORDER BY o_cod DESC LIMIT 1";

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
                        o_cod = Convert.ToInt64(dr["o_cod"]);
                        o_cod = o_cod + 1;

                        return o_cod;
                    }
                    else
                    {
                        o_cod = 0;
                        return o_cod;
                    }
                }
                else
                {
                    o_cod = 1;
                    return o_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                o_cod = 0;
                return o_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Oserv> listarPeriodo(DateTime dataI, DateTime dataF, string situac, string clicod, int mecanico, int codend, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            List<CL_Oserv> objList = new List<CL_Oserv>();
            CL_Oserv obj = null;

            if (clicod == "")
            {
                if (mecanico == 0)
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_setor, o_serv, o_servexec, o_valor, o_hora, o_situac, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon, o_codend FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                        " WHERE o_codend=@codend AND o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac=@o_situac AND o_emis>=@dataI AND o_emis<=@dataF ORDER BY o_cod";
                }
                else
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_setor, o_serv, o_servexec, o_valor, o_hora, o_situac, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon, o_codend FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                        " WHERE o_mecanic=@mecanico AND o_codend=@codend AND o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac=@o_situac AND o_emis>=@dataI AND o_emis<=@dataF ORDER BY o_cod";
                }
            }
            else
            {
                if (mecanico == 0)
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_setor, o_serv, o_servexec, o_valor, o_hora, o_situac, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon, o_codend FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                        " WHERE o_codend=@codend AND o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac=@o_situac AND o_emis>=@dataI AND o_emis<=@dataF AND p_cod=@p_cod ORDER BY o_cod";
                }
                else
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_setor, o_serv, o_servexec, o_valor, o_hora, o_situac, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon, o_codend FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                        " WHERE o_mecanic=@mecanico AND o_codend=@codend AND o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac=@o_situac AND o_emis>=@dataI AND o_emis<=@dataF AND p_cod=@p_cod ORDER BY o_cod";
                }
            }
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("o_situac", situac);
            comand.Parameters.AddWithValue("dataI", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataF", dataF.ToShortDateString());
            comand.Parameters.AddWithValue("p_cod", clicod);
            comand.Parameters.AddWithValue("mecanico", mecanico);
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
                        obj = new CL_Oserv();
                        obj.o_cod = Convert.ToInt32(dr["o_cod"]);
                        obj.o_emis = Convert.ToDateTime(dr["o_emis"]);
                        obj.o_hora = Convert.ToDateTime(dr["o_hora"]);
                        obj.o_clicod = Convert.ToInt32(dr["o_clicod"]);
                        obj.o_clinome = dr["p_nome"].ToString().Trim();
                        obj.o_descri = dr["m_infor"].ToString().Trim();
                        obj.o_mecanic = Convert.ToInt32(dr["o_mecanic"]);
                        obj.o_nomeMecanic = dr["mec_nome"].ToString().Trim();
                        obj.o_situac = dr["o_situac"].ToString().Trim();
                        obj.o_valor = Convert.ToDouble(dr["o_valor"]);
                        obj.o_obs = dr["o_obs"].ToString().Trim();
                        obj.o_codEquip = Convert.ToInt32(dr["o_equipcod"]);
                        obj.o_serie = dr["e_nserie"].ToString().Trim();
                        obj.o_patrimon = dr["e_patrimon"].ToString().Trim();
                        obj.o_modelo = Convert.ToInt32(dr["o_modelo"]);
                        obj.o_situac = dr["o_situac"].ToString().Trim();
                        obj.o_nomeModelo = dr["e_modelo"].ToString().Trim();
                        obj.o_aces = dr["o_aces"].ToString().Trim();
                        obj.o_serv = dr["o_serv"].ToString().Trim();
                        obj.o_servexec = dr["o_servexec"].ToString().Trim();
                        obj.o_setor = dr["o_setor"].ToString().Trim();

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

        public static List<CL_Oserv> relProdutividade(DateTime dataI, DateTime dataF, string tecnico, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT o_cod, o_emis, o_serv, o_servexec, o_mintrab, o_mecanic, mec_nome "+
                         "FROM oserv, mecanico "+
                         "WHERE o_mecanic = mec_cod " +
                         "AND o_emis>= @datai "+
                         "AND o_emis<= @dataf "+
                         "AND o_mecanic = @tecnico "+
                         "ORDER BY o_cod";

            List<CL_Oserv> objList = new List<CL_Oserv>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("datai", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataf", dataF.ToShortDateString());
            comand.Parameters.AddWithValue("tecnico", tecnico);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Oserv()
                        {
                            o_cod = Convert.ToInt32(dr["o_cod"]),
                            o_emis = Convert.ToDateTime(dr["o_emis"]),
                            o_serv = dr["o_serv"].ToString().Trim(),
                            o_servexec = dr["o_servexec"].ToString().Trim(),
                            o_mintrab = dr["o_mintrab"] is DBNull ? 0 : Convert.ToInt32(dr["o_mintrab"]),
                            o_mecanic = Convert.ToInt32(dr["o_mecanic"]),
                            o_nomeMecanic = dr["mec_nome"].ToString().Trim(),
                        });
                    }
                    dr.Close();
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

        public static bool alterarOservEApp(CL_Oserv objOserv, string postData, string token, CL_Requis objRequis, string con, string situac)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "UPDATE oserv SET o_emis=@o_emis, o_hora=@o_hora, o_clicod=@o_clicod, o_marca=@o_marca, o_modelo=@o_modelo, o_equipcod=@o_equipcod, o_serv=@o_serv, o_orca=@o_orca," +
                    " o_garantia=@o_garantia, o_tpserv=@o_tpserv, o_local=@o_local, o_obs=@o_obs, o_setor=@o_setor, o_contat=@o_contat, o_env=@o_env, o_apr=@o_apr, o_sai=@o_sai," +
                    " o_fim=@o_fim, o_situac=@o_situac, o_mecanic=@o_mecanic, o_valor=@o_valor, o_servexec=@o_servexec, o_sincr=@o_sincr, o_aces=@o_aces WHERE o_cod=@o_cod";
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("o_cod", objOserv.o_cod);
                comand.Parameters.AddWithValue("o_emis", objOserv.o_emis.ToShortDateString());
                comand.Parameters.AddWithValue("o_hora", objOserv.o_hora.ToShortTimeString());
                comand.Parameters.AddWithValue("o_clicod", objOserv.o_clicod);
                comand.Parameters.AddWithValue("o_marca", objOserv.o_marca);
                comand.Parameters.AddWithValue("o_modelo", objOserv.o_modelo);
                comand.Parameters.AddWithValue("o_equipcod", objOserv.o_codEquip);
                comand.Parameters.AddWithValue("o_serv", objOserv.o_serv);
                comand.Parameters.AddWithValue("o_orca", objOserv.o_orca.ToShortDateString());
                comand.Parameters.AddWithValue("o_garantia", objOserv.o_garantia);
                comand.Parameters.AddWithValue("o_tpserv", objOserv.o_tpServ);
                comand.Parameters.AddWithValue("o_local", objOserv.o_local);
                comand.Parameters.AddWithValue("o_setor", objOserv.o_setor);
                comand.Parameters.AddWithValue("o_env", objOserv.o_env.ToShortDateString());
                comand.Parameters.AddWithValue("o_apr", objOserv.o_apr.ToShortDateString());
                comand.Parameters.AddWithValue("o_sai", objOserv.o_sai.ToShortDateString());
                comand.Parameters.AddWithValue("o_fim", objOserv.o_fim.ToShortDateString());
                comand.Parameters.AddWithValue("o_situac", objOserv.o_situac);
                comand.Parameters.AddWithValue("o_mecanic", objOserv.o_mecanic);
                comand.Parameters.AddWithValue("o_valor", objOserv.o_valor);
                comand.Parameters.AddWithValue("o_obs", objOserv.o_obs);
                comand.Parameters.AddWithValue("o_servexec", objOserv.o_servexec);
                comand.Parameters.AddWithValue("o_sincr", objOserv.o_sincr);
                comand.Parameters.AddWithValue("o_contat", objOserv.o_contat);
                comand.Parameters.AddWithValue("o_aces", objOserv.o_aces);
                comand.ExecuteScalar();

                sql = "UPDATE requis SET req_vend=@req_vend, req_codcli=@req_codcli, req_oserv=@req_oserv, req_data=@req_data, est_cod=@est_cod, est_nome=@est_nome, est_tpprod=@est_tpprod, est_ngrupo=@est_ngrupo, est_nsgrup=@est_nsgrup, "+
                    "est_famil=@est_famil, req_qtdade=@req_qtdade, req_preco=@req_preco, req_desc=@req_desc, req_custo=@req_custo, req_vended=@req_vended, req_tribut=@req_tribut, req_issqn=@req_issqn, req_vldesc=@req_vldesc, req_situac=@req_situac, "+
                    "req_pcfixo=@req_pcfixo, req_tpserv=@req_tpserv, req_cnpj=@req_cnpj, req_iest=@req_iest, req_codend=@req_codend WHERE req_cod=@req_cod";
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("req_cod", objRequis.req_cod);
                comand.Parameters.AddWithValue("req_vend", objRequis.req_vend);
                comand.Parameters.AddWithValue("req_codcli", objRequis.req_codcli);
                comand.Parameters.AddWithValue("req_oserv", objRequis.req_oserv);
                comand.Parameters.AddWithValue("req_data", objRequis.req_data.ToShortDateString());
                comand.Parameters.AddWithValue("est_cod", objRequis.req_est.est_cod.Trim());
                comand.Parameters.AddWithValue("est_nome", objRequis.req_est.est_nome.Trim());
                comand.Parameters.AddWithValue("est_tpprod", objRequis.req_est.est_tpprod.Trim());
                comand.Parameters.AddWithValue("est_ngrupo", objRequis.req_est.est_ngrupo);
                comand.Parameters.AddWithValue("est_nsgrup", objRequis.req_est.est_nsgrup);
                comand.Parameters.AddWithValue("est_famil", objRequis.req_est.est_famil);
                comand.Parameters.AddWithValue("req_qtdade", objRequis.req_qtdade);
                comand.Parameters.AddWithValue("req_preco", objRequis.req_preco);
                comand.Parameters.AddWithValue("req_desc", objRequis.req_desc);
                comand.Parameters.AddWithValue("req_custo", objRequis.req_custo);
                comand.Parameters.AddWithValue("req_vended", objRequis.req_vended);
                comand.Parameters.AddWithValue("req_tribut", objRequis.req_tribut);
                comand.Parameters.AddWithValue("req_issqn", objRequis.req_issqn);
                comand.Parameters.AddWithValue("req_vldesc", objRequis.req_vldesc);
                comand.Parameters.AddWithValue("req_situac", objRequis.req_situac);
                comand.Parameters.AddWithValue("req_pcfixo", objRequis.req_pcfixo);
                comand.Parameters.AddWithValue("req_tpserv", objRequis.req_tpserv);
                comand.Parameters.AddWithValue("req_qtfat", objRequis.req_qtfat);
                comand.Parameters.AddWithValue("req_cnpj", objRequis.req_cnpj);
                comand.Parameters.AddWithValue("req_iest", objRequis.req_iest);
                comand.Parameters.AddWithValue("req_codend", objRequis.req_codend);

                comand.ExecuteScalar();
                if (situac == "I")
                {
                    if (DB_Umov.sincronizaApp(token, "schedule", postData))
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                else
                {
                    if (DB_Umov.attDadosApp(token, "schedule", postData, objOserv.o_cod.ToString()))
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
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

        public static bool cadOservEApp(CL_Oserv objOserv, string postData, string token, CL_Requis objRequis, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "INSERT INTO oserv (o_cod, o_emis, o_clicod, o_marca, o_modelo, o_equipcod, o_serv, o_orca, o_garantia, o_tpserv, o_local, o_setor, o_env, o_sai, o_apr, o_fim, o_situac, o_mecanic, o_valor, o_obs, o_servexec, o_sincr, o_contat, o_hora, o_codend, o_aces)" +
                    " VALUES (@o_cod, @o_emis, @o_clicod, @o_marca, @o_modelo, @o_equipcod, @o_serv, @o_orca, @o_garantia, @o_tpserv, @o_local, @o_setor, @o_env, @o_sai, @o_apr, @o_fim, @o_situac, @o_mecanic, @o_valor, @o_obs, @o_servexec, @o_sincr, @o_contat, @o_hora, @o_codend, @o_aces)";
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("o_cod", objOserv.o_cod);
                comand.Parameters.AddWithValue("o_emis", objOserv.o_emis.ToShortDateString());
                comand.Parameters.AddWithValue("o_hora", objOserv.o_hora.ToShortTimeString());
                comand.Parameters.AddWithValue("o_clicod", objOserv.o_clicod);
                comand.Parameters.AddWithValue("o_marca", objOserv.o_marca);
                comand.Parameters.AddWithValue("o_modelo", objOserv.o_modelo);
                comand.Parameters.AddWithValue("o_equipcod", objOserv.o_codEquip);
                comand.Parameters.AddWithValue("o_serv", objOserv.o_serv);
                comand.Parameters.AddWithValue("o_orca", objOserv.o_orca.ToShortDateString());
                comand.Parameters.AddWithValue("o_garantia", objOserv.o_garantia);
                comand.Parameters.AddWithValue("o_tpserv", objOserv.o_tpServ);
                comand.Parameters.AddWithValue("o_local", objOserv.o_local);
                comand.Parameters.AddWithValue("o_setor", objOserv.o_setor);
                comand.Parameters.AddWithValue("o_env", objOserv.o_env.ToShortDateString());
                comand.Parameters.AddWithValue("o_apr", objOserv.o_apr.ToShortDateString());
                comand.Parameters.AddWithValue("o_sai", objOserv.o_sai.ToShortDateString());
                comand.Parameters.AddWithValue("o_fim", objOserv.o_fim.ToShortDateString());
                comand.Parameters.AddWithValue("o_situac", objOserv.o_situac);
                comand.Parameters.AddWithValue("o_mecanic", objOserv.o_mecanic);
                comand.Parameters.AddWithValue("o_valor", objOserv.o_valor);
                comand.Parameters.AddWithValue("o_obs", objOserv.o_obs);
                comand.Parameters.AddWithValue("o_servexec", objOserv.o_servexec);
                comand.Parameters.AddWithValue("o_sincr", objOserv.o_sincr);
                comand.Parameters.AddWithValue("o_contat", objOserv.o_contat);
                comand.Parameters.AddWithValue("o_codend", objOserv.o_codend);
                comand.Parameters.AddWithValue("o_aces", objOserv.o_aces);

                comand.ExecuteScalar();

                if (objOserv.o_valor > 0)
                {
                    sql = "INSERT INTO requis (req_lcto, req_cod, req_vend, req_codcli, req_oserv, req_data, est_cod, est_nome, est_tpprod, est_ngrupo, est_nsgrup, est_famil, req_qtdade, req_preco, req_desc, req_custo, req_vended, req_tribut, req_issqn, req_vldesc, req_situac, req_pcfixo, req_tpserv, req_qtfat, req_ctrreg, req_cnpj, req_iest, req_codend)" +
                        "VALUES (@req_lcto, @req_cod, @req_vend, @req_codcli, @req_oserv, @req_data, @est_cod, @est_nome, @est_tpprod, @est_ngrupo, @est_nsgrup, @est_famil, @req_qtdade, @req_preco, @req_desc, @req_custo, @req_vended, @req_tribut, @req_issqn, @req_vldesc, @req_situac, @req_pcfixo, @req_tpserv, @req_qtfat, @req_ctrreg, @req_cnpj, @req_iest, @req_codend)";
                    comand = new NpgsqlCommand(sql, Conn, transaction);
                    objRequis.req_ctrreg = objRequis.req_cod + " " + objRequis.req_recno + " " + objRequis.req_est.est_cod;
                    comand.Parameters.AddWithValue("req_lcto", objRequis.req_lcto);
                    comand.Parameters.AddWithValue("req_cod", objRequis.req_cod);
                    comand.Parameters.AddWithValue("req_vend", objRequis.req_vend);
                    comand.Parameters.AddWithValue("req_codcli", objRequis.req_codcli);
                    comand.Parameters.AddWithValue("req_oserv", objRequis.req_oserv);
                    comand.Parameters.AddWithValue("req_data", objRequis.req_data.ToShortDateString());
                    comand.Parameters.AddWithValue("est_cod", objRequis.req_est.est_cod.Trim());
                    comand.Parameters.AddWithValue("est_nome", objRequis.req_est.est_nome.Trim());
                    comand.Parameters.AddWithValue("est_tpprod", objRequis.req_est.est_tpprod.Trim());
                    comand.Parameters.AddWithValue("est_ngrupo", objRequis.req_est.est_ngrupo);
                    comand.Parameters.AddWithValue("est_nsgrup", objRequis.req_est.est_nsgrup);
                    comand.Parameters.AddWithValue("est_famil", objRequis.req_est.est_famil);
                    comand.Parameters.AddWithValue("req_qtdade", objRequis.req_qtdade);
                    comand.Parameters.AddWithValue("req_preco", objRequis.req_preco);
                    comand.Parameters.AddWithValue("req_desc", objRequis.req_desc);
                    comand.Parameters.AddWithValue("req_custo", objRequis.req_custo);
                    comand.Parameters.AddWithValue("req_vended", objRequis.req_vended);
                    comand.Parameters.AddWithValue("req_tribut", objRequis.req_tribut);
                    comand.Parameters.AddWithValue("req_issqn", objRequis.req_issqn);
                    comand.Parameters.AddWithValue("req_vldesc", objRequis.req_vldesc);
                    comand.Parameters.AddWithValue("req_situac", objRequis.req_situac);
                    comand.Parameters.AddWithValue("req_pcfixo", objRequis.req_pcfixo);
                    comand.Parameters.AddWithValue("req_tpserv", objRequis.req_tpserv);
                    comand.Parameters.AddWithValue("req_qtfat", objRequis.req_qtfat);
                    comand.Parameters.AddWithValue("req_ctrreg", objRequis.req_ctrreg);
                    comand.Parameters.AddWithValue("req_cnpj", objRequis.req_cnpj);
                    comand.Parameters.AddWithValue("req_iest", objRequis.req_iest);
                    comand.Parameters.AddWithValue("req_codend", objRequis.req_codend);

                    comand.ExecuteScalar();
                }

                if (DB_Umov.sincronizaApp(token, "schedule", postData))
                {
                    transaction.Commit();
                    return true;
                }
                else
                    return false;
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

        public static bool cadOserv(CL_Oserv objOserv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO oserv (o_cod, o_emis, o_clicod, o_marca, o_modelo, o_equipcod, o_serv, o_orca, o_garantia, o_tpserv, o_local, o_setor, o_env, o_sai, o_apr, o_fim, o_situac, o_mecanic, o_valor, o_obs, o_servexec, o_sincr, o_contat, o_hora, o_codend, o_aces)" +
                    " VALUES (@o_cod, @o_emis, @o_clicod, @o_marca, @o_modelo, @o_equipcod, @o_serv, @o_orca, @o_garantia, @o_tpserv, @o_local, @o_setor, @o_env, @o_sai, @o_apr, @o_fim, @o_situac, @o_mecanic, @o_valor, @o_obs, @o_servexec, @o_sincr, @o_contat, @o_hora, @o_codend, @o_aces)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("o_cod", objOserv.o_cod);
                comand.Parameters.AddWithValue("o_emis", objOserv.o_emis.ToShortDateString());
                comand.Parameters.AddWithValue("o_hora", objOserv.o_hora.ToShortTimeString());
                comand.Parameters.AddWithValue("o_clicod", objOserv.o_clicod);
                comand.Parameters.AddWithValue("o_marca", objOserv.o_marca);
                comand.Parameters.AddWithValue("o_modelo", objOserv.o_modelo);
                comand.Parameters.AddWithValue("o_equipcod", objOserv.o_codEquip);
                comand.Parameters.AddWithValue("o_serv", objOserv.o_serv);
                comand.Parameters.AddWithValue("o_orca", objOserv.o_orca.ToShortDateString());
                comand.Parameters.AddWithValue("o_garantia", objOserv.o_garantia);
                comand.Parameters.AddWithValue("o_tpserv", objOserv.o_tpServ);
                comand.Parameters.AddWithValue("o_local", objOserv.o_local);
                comand.Parameters.AddWithValue("o_setor", objOserv.o_setor);
                comand.Parameters.AddWithValue("o_env", objOserv.o_env.ToShortDateString());
                comand.Parameters.AddWithValue("o_apr", objOserv.o_apr.ToShortDateString());
                comand.Parameters.AddWithValue("o_sai", objOserv.o_sai.ToShortDateString());
                comand.Parameters.AddWithValue("o_fim", objOserv.o_fim.ToShortDateString());
                comand.Parameters.AddWithValue("o_situac", objOserv.o_situac);
                comand.Parameters.AddWithValue("o_mecanic", objOserv.o_mecanic);
                comand.Parameters.AddWithValue("o_valor", objOserv.o_valor);
                comand.Parameters.AddWithValue("o_obs", objOserv.o_obs);
                comand.Parameters.AddWithValue("o_servexec", objOserv.o_servexec);
                comand.Parameters.AddWithValue("o_sincr", objOserv.o_sincr);
                comand.Parameters.AddWithValue("o_contat", objOserv.o_contat);
                comand.Parameters.AddWithValue("o_codend", objOserv.o_codend);
                comand.Parameters.AddWithValue("o_aces", objOserv.o_aces);

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
                    Conn.Close();
            }
        }

        public static CL_Oserv buscaOserv(CL_Oserv objOserv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p.p_cod, p.p_nome, p.p_fone, p.p_ende, p.p_comend, p.p_nr, p.p_est, p.p_cida, ma.m_codigo, mo.m_codigo, mo.m_infor, e.e_nserie, e.e_cod, o.o_marca, o.o_modelo, o.o_clicod, o.o_equipcod, o.o_cod, o.o_emis, o.o_serv, o.o_orca, o.o_garantia, o.o_tpserv, o.o_local, o.o_setor, o.o_env, o.o_apr, o.o_sai, o.o_fim, o.o_situac, o.o_mecanic, o_valor, o_obs, o_servexec, o_sincr, o_assina, o_contat, o_hora, o_codend, o_aces, mec.mec_nome" +
                " FROM particip p, coml_marca ma, coml_modelo mo, equipamento e, oserv o, mecanico mec" +
                " WHERE p.p_cod = o.o_clicod AND ma.m_codigo = o.o_marca AND mo.m_codigo = o.o_modelo AND e.e_cod = o.o_equipcod AND mec.mec_cod = o.o_mecanic AND o.o_cod =" + objOserv.o_cod;

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
                        objOserv.o_cida = dr["p_cida"].ToString().Trim();
                        objOserv.o_clicod = Convert.ToInt32(dr["p_cod"]);
                        objOserv.o_clinome = dr["p_nome"].ToString().Trim();

                        objOserv.o_ende = dr["p_ende"].ToString().Trim();
                        objOserv.o_ende = objOserv.o_ende + ", " + dr["p_nr"].ToString().Trim();
                        if (dr["p_comend"].ToString().Trim() != "")
                        {
                            string p_comend = dr["p_comend"].ToString().Trim();
                            objOserv.o_ende = objOserv.o_ende + " - " + p_comend;
                        }
                        objOserv.o_descri = dr["m_infor"].ToString().Trim();
                        objOserv.o_fone = dr["p_fone"].ToString().Trim();
                        objOserv.o_est = dr["p_est"].ToString().Trim();
                        objOserv.o_serie = dr["e_nserie"].ToString().Trim();
                        objOserv.o_apr = Convert.ToDateTime(dr["o_apr"]);
                        objOserv.o_env = Convert.ToDateTime(dr["o_env"]);
                        objOserv.o_codEquip = Convert.ToInt32(dr["o_equipcod"]);
                        objOserv.o_emis = Convert.ToDateTime(dr["o_emis"]);
                        objOserv.o_hora = Convert.ToDateTime(dr["o_hora"]);
                        objOserv.o_fim = Convert.ToDateTime(dr["o_fim"]);
                        objOserv.o_garantia = dr["o_garantia"].ToString().Trim();
                        objOserv.o_local = dr["o_local"].ToString().Trim();
                        objOserv.o_marca = Convert.ToInt32(dr["o_marca"]);
                        objOserv.o_mecanic = Convert.ToInt32(dr["o_mecanic"]);
                        objOserv.o_modelo = Convert.ToInt32(dr["o_modelo"]);
                        objOserv.o_nomeMecanic = dr["mec_nome"].ToString().Trim();
                        objOserv.o_orca = Convert.ToDateTime(dr["o_orca"]);
                        objOserv.o_sai = Convert.ToDateTime(dr["o_sai"]);
                        objOserv.o_serv = dr["o_serv"].ToString().Trim();
                        objOserv.o_servexec = dr["o_servexec"].ToString().Trim();
                        objOserv.o_setor = dr["o_setor"].ToString().Trim();
                        objOserv.o_contat = dr["o_contat"].ToString().Trim();
                        objOserv.o_situac = dr["o_situac"].ToString().Trim();
                        objOserv.o_tpServ = dr["o_tpserv"].ToString().Trim();
                        objOserv.o_valor = Convert.ToDouble(dr["o_valor"]);
                        objOserv.o_obs = dr["o_obs"].ToString().Trim();
                        objOserv.o_sincr = dr["o_sincr"].ToString().Trim();
                        objOserv.o_codend = Convert.ToInt32(dr["o_codend"]);
                        objOserv.o_aces = dr["o_aces"].ToString().Trim();
                        objOserv.o_assina = dr["o_assina"].ToString().Trim();
                        return objOserv;
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

        public static bool alterarOserv(CL_Oserv objOserv, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE oserv SET o_emis=@o_emis, o_hora=@o_hora,  o_clicod=@o_clicod, o_marca=@o_marca, o_modelo=@o_modelo, o_equipcod=@o_equipcod, o_serv=@o_serv, o_orca=@o_orca," +
                    " o_garantia=@o_garantia, o_tpserv=@o_tpserv, o_local=@o_local, o_obs=@o_obs, o_setor=@o_setor, o_contat=@o_contat, o_env=@o_env, o_apr=@o_apr, o_sai=@o_sai," +
                    " o_fim=@o_fim, o_situac=@o_situac, o_mecanic=@o_mecanic, o_valor=@o_valor, o_servexec=@o_servexec, o_sincr=@o_sincr, o_aces=@o_aces WHERE o_cod=@o_cod";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("o_cod", objOserv.o_cod);
                comand.Parameters.AddWithValue("o_emis", objOserv.o_emis.ToShortDateString());
                comand.Parameters.AddWithValue("o_hora", objOserv.o_hora.ToShortTimeString());
                comand.Parameters.AddWithValue("o_clicod", objOserv.o_clicod);
                comand.Parameters.AddWithValue("o_marca", objOserv.o_marca);
                comand.Parameters.AddWithValue("o_modelo", objOserv.o_modelo);
                comand.Parameters.AddWithValue("o_equipcod", objOserv.o_codEquip);
                comand.Parameters.AddWithValue("o_serv", objOserv.o_serv);
                comand.Parameters.AddWithValue("o_orca", objOserv.o_orca.ToShortDateString());
                comand.Parameters.AddWithValue("o_garantia", objOserv.o_garantia);
                comand.Parameters.AddWithValue("o_tpserv", objOserv.o_tpServ);
                comand.Parameters.AddWithValue("o_local", objOserv.o_local);
                comand.Parameters.AddWithValue("o_setor", objOserv.o_setor);
                comand.Parameters.AddWithValue("o_env", objOserv.o_env.ToShortDateString());
                comand.Parameters.AddWithValue("o_apr", objOserv.o_apr.ToShortDateString());
                comand.Parameters.AddWithValue("o_sai", objOserv.o_sai.ToShortDateString());
                comand.Parameters.AddWithValue("o_fim", objOserv.o_fim.ToShortDateString());
                comand.Parameters.AddWithValue("o_situac", objOserv.o_situac);
                comand.Parameters.AddWithValue("o_mecanic", objOserv.o_mecanic);
                comand.Parameters.AddWithValue("o_valor", objOserv.o_valor);
                comand.Parameters.AddWithValue("o_obs", objOserv.o_obs);
                comand.Parameters.AddWithValue("o_servexec", objOserv.o_servexec);
                comand.Parameters.AddWithValue("o_sincr", objOserv.o_sincr);
                comand.Parameters.AddWithValue("o_contat", objOserv.o_contat);
                comand.Parameters.AddWithValue("o_aces", objOserv.o_aces);

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

        public static bool excluiOserv(CL_Oserv objOserv, string token, string arquivo, string post, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction trans = Conn.BeginTransaction();
            string sql = "DELETE FROM oserv WHERE o_cod=@o_cod; DELETE FROM requis WHERE req_cod=@o_cod";
            try
            {
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, trans);
                comand.Parameters.AddWithValue("o_cod", objOserv.o_cod);
                comand.ExecuteScalar();
                if(objOserv.o_sincr == "S")
                {
                    if (DB_Umov.attDadosApp(token, arquivo, post, objOserv.o_cod.ToString()))
                        trans.Commit();
                    else
                        trans.Commit();
                }
                else
                    trans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                trans.Rollback();
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public static List<CL_Oserv> listar(string pesq, string con, string filtroPesq, string situac)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            List<CL_Oserv> objList = new List<CL_Oserv>();
            CL_Oserv obj = null;

            if (pesq == "")
            {
                sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_serv, o_servexec, o_valor, o_hora, o_situac, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                    " WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac='"+ situac +"' ORDER BY o_cod";
            }
            else
            {
                if (filtroPesq == "1")//COD EQUIP
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_serv, o_servexec, o_valor, o_hora, o_situac, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                    " WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac='" + situac + "' AND o_equipcod=" + pesq + " ORDER BY o_cod";
                }
                else if (filtroPesq == "2")//COD CLIENTE
                {
                      sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_serv, o_servexec, o_valor, o_hora, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                    " WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND p_nome LIKE '%" + pesq + "%' AND o_situac='" + situac + "'  AND o_clicod = p_cod ORDER BY o_cod";
                }
                else if(filtroPesq == "3")//MODELO
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_serv, o_servexec, o_valor, o_hora, e_modelo, o_obs, o_aces, e_nserie, e_cod, e_patrimon FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                    " WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac='" + situac + "' AND e_modelo LIKE '%" + pesq + "%' ORDER BY o_cod";
                }
                else if(filtroPesq == "4") //TECNICO
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_serv, o_servexec, o_valor, o_hora, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                    " WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_situac='" + situac + "' AND o_mecanic=" + pesq + " ORDER BY o_cod";
                }
                else
                {
                    sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_serv, o_servexec, o_valor, o_hora, o_obs, o_aces, e_nserie, e_modelo, e_cod, e_patrimon FROM oserv, particip, coml_modelo, mecanico, equipamento" +
                    " WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod ORDER BY o_cod";
                }
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
                        obj = new CL_Oserv();
                        obj.o_cod = Convert.ToInt32(dr["o_cod"]);
                        obj.o_emis = Convert.ToDateTime(dr["o_emis"]);
                        obj.o_hora = Convert.ToDateTime(dr["o_hora"]);
                        obj.o_clicod = Convert.ToInt32(dr["o_clicod"]);
                        obj.o_clinome = dr["p_nome"].ToString().Trim();
                        obj.o_descri = dr["m_infor"].ToString().Trim();
                        obj.o_mecanic = Convert.ToInt32(dr["o_mecanic"]);
                        obj.o_nomeMecanic = dr["mec_nome"].ToString().Trim();
                        obj.o_situac = dr["o_situac"].ToString().Trim();
                        obj.o_valor = Convert.ToDouble(dr["o_valor"]);
                        obj.o_obs = dr["o_obs"].ToString().Trim();
                        obj.o_codEquip = Convert.ToInt32(dr["o_equipcod"]);
                        obj.o_serie = dr["e_nserie"].ToString().Trim();
                        obj.o_patrimon = dr["e_patrimon"].ToString().Trim();
                        obj.o_modelo = Convert.ToInt32(dr["o_modelo"]);
                        obj.o_situac = dr["o_situac"].ToString().Trim();
                        obj.o_nomeModelo = dr["e_modelo"].ToString().Trim();
                        obj.o_aces = dr["o_aces"].ToString().Trim();
                        obj.o_serv = dr["o_serv"].ToString().Trim();
                        obj.o_servexec = dr["o_servexec"].ToString().Trim();

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

        public static List<CL_Oserv> pesqOservSincr(int sincr_cod, string con, string situac)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            List<CL_Oserv> objList = new List<CL_Oserv>();
            CL_Oserv obj = null;

            if (sincr_cod == 0)
            {
                sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_valor, o_obs, o_aces, o_situac, o_codend, o_sincr, o_setor, o_hora, e_nserie, e_cod, e_patrimon " +
                        "FROM oserv, particip, coml_modelo, mecanico, equipamento " +
                        "WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_sincr='S' AND o_situac='" + situac + "'" +
                        "ORDER BY o_cod";
            }
            else
            {
                sql = "SELECT o_cod, o_emis, o_clicod, o_equipcod, p_nome, o_modelo, m_infor, o_mecanic, mec_nome, o_situac, o_valor, o_obs, o_aces, o_situac, o_codend, o_sincr, o_setor, o_hora, e_nserie, e_cod, e_patrimon " +
                           "FROM oserv, particip, coml_modelo, mecanico, equipamento " +
                           "WHERE o_mecanic = mec_cod AND e_cod = o_equipcod AND o_modelo = m_codigo AND o_clicod = p_cod AND o_sincr='S' AND o_situac='" + situac + "' AND o_cod=" + sincr_cod +
                           " ORDER BY o_cod";
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
                        obj = new CL_Oserv();
                        obj.o_cod = Convert.ToInt32(dr["o_cod"]);
                        obj.o_emis = Convert.ToDateTime(dr["o_emis"]);
                        obj.o_hora = Convert.ToDateTime(dr["o_hora"]);
                        obj.o_clicod = Convert.ToInt32(dr["o_clicod"]);
                        obj.o_clinome = dr["p_nome"].ToString().Trim();
                        obj.o_descri = dr["m_infor"].ToString().Trim();
                        obj.o_mecanic = Convert.ToInt32(dr["o_mecanic"]);
                        obj.o_nomeMecanic = dr["mec_nome"].ToString().Trim();
                        obj.o_situac = dr["o_situac"].ToString().Trim();
                        obj.o_valor = Convert.ToDouble(dr["o_valor"]);
                        obj.o_obs = dr["o_obs"].ToString().Trim();
                        obj.o_codEquip = Convert.ToInt32(dr["o_equipcod"]);
                        obj.o_serie = dr["e_nserie"].ToString().Trim();
                        obj.o_patrimon = dr["e_patrimon"].ToString().Trim();
                        obj.o_codend = Convert.ToInt32(dr["o_codend"]);
                        obj.o_setor = dr["o_setor"].ToString().Trim();
                        obj.o_aces = dr["o_aces"].ToString().Trim();

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

        public static bool attDadosOS(CL_SincrOserv objSincr, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE oserv SET o_fim=@o_fim, o_situac=@o_situac, o_valor=@o_valor, o_servexec=@o_servexec, o_obs=@o_obs, o_mintrab=@o_mintrab, o_assina=@o_assina " +
                    "WHERE o_cod=@o_cod";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("o_fim", objSincr.sincr_dataExec.ToShortDateString());
                comand.Parameters.AddWithValue("o_situac", "R");
                comand.Parameters.AddWithValue("o_valor", objSincr.sincr_totOS);
                comand.Parameters.AddWithValue("o_servexec", objSincr.sincr_servExec);
                comand.Parameters.AddWithValue("o_cod", objSincr.sincr_os);
                comand.Parameters.AddWithValue("o_obs", objSincr.sincr_obs);
                comand.Parameters.AddWithValue("o_mintrab", objSincr.sincr_hrsTrab);
                comand.Parameters.AddWithValue("o_assina", objSincr.sincr_urlAss);
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