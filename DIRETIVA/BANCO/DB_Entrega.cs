using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Entrega : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static List<CL_Entrega> buscaEntregaPeriodo(DateTime dataI, DateTime dataF, string coluna, string ordem, string cidade, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (cidade == "")
                sql = "SELECT e_id, e_remetent, e_awb, e_dataenco, e_qtdvol, e_rota, e_datastat, e_status, e_diasprev, e_clicod, e_modelo, p_nome, p_cida, p_bairro, e_situac, e_identreg " +
                    "FROM entregas, particip WHERE e_dataenco >= @datai AND e_dataenco <= @dataf AND (e_situac='P' or e_situac = '') AND p_cod=e_clicod ORDER BY " + coluna + " " + ordem;
            else
                sql = "SELECT e_id, e_remetent, e_awb, e_dataenco, e_qtdvol, e_rota, e_datastat, e_status, e_diasprev, e_clicod, e_modelo, p_nome, p_cida, p_bairro, e_situac, e_identreg " +
                    "FROM entregas, particip WHERE e_dataenco >= @datai AND e_dataenco <= @dataf AND (e_situac='P' or e_situac = '') AND p_cod=e_clicod AND p_cida LIKE '%" +cidade+ "%' ORDER BY " + coluna + " " + ordem;
            List<CL_Entrega> objListEntrega = new List<CL_Entrega>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("datai", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataf", dataF.ToShortDateString());
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListEntrega.Add(new CL_Entrega()
                        {
                            e_id = dr["e_id"] is DBNull ? 0 : Convert.ToInt32(dr["e_id"]),
                            e_remetent = dr["e_remetent"].ToString().Trim(),
                            e_awb = dr["e_awb"].ToString().Trim(),
                            e_dataenco = dr["e_dataenco"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_dataenco"]),
                            e_qtdvol = dr["e_qtdvol"] is DBNull ? 0 : Convert.ToInt32(dr["e_qtdvol"]),
                            e_rota = dr["e_rota"].ToString().Trim(),
                            e_datastat = dr["e_datastat"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_datastat"]),
                            e_status = dr["e_status"].ToString().Trim(),
                            e_diasprev = dr["e_diasprev"] is DBNull ? 0 : Convert.ToInt32(dr["e_diasprev"]),
                            e_clicod = dr["e_clicod"] is DBNull ? 0 : Convert.ToInt32(dr["e_clicod"]),
                            e_clinome = dr["p_nome"].ToString().Trim(),
                            e_clicida = dr["p_cida"].ToString().Trim(),
                            e_clibairro = dr["p_bairro"].ToString().Trim(),
                            e_situac = dr["e_situac"].ToString().Trim(),
                            e_idEntregador = dr["e_identreg"] is DBNull ? 0 : Convert.ToInt32(dr["e_identreg"]),
                            e_modelo = dr["e_modelo"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListEntrega;
                }
                else
                {
                    objListEntrega = null;
                    return objListEntrega;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListEntrega = null;
                return objListEntrega;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Entrega> buscaRoteirizacao(string entregador, DateTime dataI, DateTime dataF, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (entregador == "T")
                sql = "SELECT e_id, e_identreg, mec_locali FROM entregas, mecanico WHERE e_situac='S' AND e_dataenco>=@dataI AND e_dataenco<=@dataF AND e_identreg=mec_cod ORDER BY e_identreg";
            else
                sql = "SELECT e_id, e_identreg, mec_locali FROM entregas, mecanico WHERE e_situac='S' AND e_identreg=@entregador AND e_dataenco>=@dataI AND e_dataenco<=@dataF AND e_identreg=mec_cod ORDER BY e_identreg";
            List<CL_Entrega> objListEntrega = new List<CL_Entrega>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("entregador", entregador);
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
                        objListEntrega.Add(new CL_Entrega()
                        {
                            e_id = dr["e_id"] is DBNull ? 0 : Convert.ToInt32(dr["e_id"]),
                            e_idEntregador = dr["e_identreg"] is DBNull ? 0 : Convert.ToInt32(dr["e_identreg"]),
                            e_localizEntreg = dr["mec_locali"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListEntrega;
                }
                else
                {
                    objListEntrega = null;
                    return objListEntrega;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListEntrega = null;
                return objListEntrega;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Entrega> consultaPgtoAwb(DateTime dataI, DateTime dataF, string modelo, string status, string entregador, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if(modelo == "T")
            {
                if (entregador == "T")
                {
                    sql = "SELECT e_awb, e_dataenco, e_datastat, e_status, e_clicod, e_modelo, e_vlrreceb, e_vlrpago, e_recebido, e_pago, p_nome, p_cida, mec_nome" +
                          " FROM entregas, particip, mecanico" +
                          " WHERE e_dataenco>= @datai" +
                          " AND e_dataenco<= @dataF" +
                          " AND e_situac = @situac" +
                          " AND e_clicod = p_cod" +
                          " AND e_identreg = mec_cod";
                }
                else
                {
                    sql = "SELECT e_awb, e_dataenco, e_datastat, e_status, e_clicod, e_modelo, e_vlrreceb, e_vlrpago, e_recebido, e_pago, p_nome, p_cida, mec_nome"+
                          " FROM entregas, particip, mecanico" +
                          " WHERE e_dataenco>=@datai" +
                          " AND e_dataenco<=@dataf" +
                          " AND e_situac=@situac" +
                          " AND e_identreg=@e_identreg" +
                          " AND e_identreg=mec_cod" +
                          " AND e_clicod=p_cod";
                }
            }
            else
            {
                if (entregador == "T")
                {
                    sql = "SELECT e_awb, e_dataenco, e_datastat, e_status, e_clicod, e_modelo, e_vlrreceb, e_vlrpago, e_recebido, e_pago, p_nome, p_cida, mec_nome" +
                          " FROM entregas, particip, mecanico" +
                          " WHERE e_modelo=@modelo" + 
                          " AND e_dataenco >= @datai" +
                          " AND e_dataenco<= @dataF" +
                          " AND e_situac = @situac" +
                          " AND e_clicod = p_cod" +
                          " AND e_identreg = mec_cod";
                }
                else
                {
                    sql = "SELECT e_awb, e_dataenco, e_datastat, e_status, e_clicod, e_modelo, e_vlrreceb, e_vlrpago, e_recebido, e_pago, p_nome, p_cida, mec_nome"+
                          " FROM entregas, particip, mecanico" +
                          " WHERE e_modelo=@modelo" +
                          " AND e_dataenco>=@datai" +
                          " AND e_dataenco<=@dataf" +
                          " AND e_situac=@situac" +
                          " AND e_identreg=@e_identreg" +
                          " AND e_identreg=mec_cod" +
                          " AND e_clicod=p_cod";
                }
            }

            List<CL_Entrega> objListEntrega = new List<CL_Entrega>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("datai", dataI);
            comand.Parameters.AddWithValue("dataf", dataF);
            comand.Parameters.AddWithValue("modelo", modelo);
            comand.Parameters.AddWithValue("e_identreg", entregador);
            comand.Parameters.AddWithValue("situac", status);

            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListEntrega.Add(new CL_Entrega()
                        {
                            e_awb = dr["e_awb"].ToString().Trim(),
                            e_dataenco = dr["e_dataenco"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_dataenco"]),
                            e_status = dr["e_status"].ToString().Trim(),
                            e_datastat = dr["e_datastat"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_datastat"]),
                            e_clicod = dr["e_clicod"] is DBNull ? 0 : Convert.ToInt32(dr["e_clicod"]),
                            e_modelo = dr["e_modelo"].ToString().Trim(),
                            e_vlrreceb = dr["e_vlrreceb"] is DBNull ? 0 : Convert.ToDouble(dr["e_vlrreceb"]),
                            e_vlrpago = dr["e_vlrreceb"] is DBNull ? 0 : Convert.ToDouble(dr["e_vlrpago"]),
                            e_recebido = dr["e_recebido"].ToString().Trim(),
                            e_pago = dr["e_recebido"].ToString().Trim(),
                            e_clinome = dr["p_nome"].ToString().Trim(),
                            e_nomeEntregador = dr["mec_nome"].ToString().Trim(),
                            e_clicida = dr["p_cida"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListEntrega;
                }
                else
                {
                    objListEntrega = null;
                    return objListEntrega;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListEntrega = null;
                return objListEntrega;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadEntrega(CL_Entrega objEntrega, string token, string post, string situac, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "INSERT INTO entregas (e_awb, e_dataenco, e_datastat, e_status, e_diasprev, e_clicod, e_situac, e_id, e_identreg, e_modelo) " +
                    "VALUES " +
                    "(@e_awb, @e_dataenco, @e_datastat, @e_status, @e_diasprev, @e_clicod, @e_situac, @e_id, @e_identreg, @e_modelo)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("e_awb", objEntrega.e_awb);
                cmd.Parameters.AddWithValue("e_dataenco", objEntrega.e_dataenco);
                cmd.Parameters.AddWithValue("e_datastat", objEntrega.e_datastat);
                cmd.Parameters.AddWithValue("e_status", objEntrega.e_status);
                cmd.Parameters.AddWithValue("e_diasprev", objEntrega.e_diasprev);
                cmd.Parameters.AddWithValue("e_clicod", objEntrega.e_clicod);
                cmd.Parameters.AddWithValue("e_situac", objEntrega.e_situac);
                cmd.Parameters.AddWithValue("e_id", objEntrega.e_id);
                cmd.Parameters.AddWithValue("e_identreg", objEntrega.e_idEntregador);
                cmd.Parameters.AddWithValue("e_modelo", objEntrega.e_modelo);

                cmd.ExecuteScalar();
                if(DB_Umov.sincronizaApp(token, "schedule", post))
                {
                    if(DB_Umov.attSituac(situac, con))
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
                    transaction.Rollback();
                    return false;
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
                {
                    Conn.Close();
                }
            }
        }

        public static int buscaID(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int e_id = 0;
            string sql = "SELECT e_id FROM entregas ORDER BY e_id DESC LIMIT 1";

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
                        e_id = dr["e_id"] is DBNull ? 0 : Convert.ToInt32(dr["e_id"]); 
                        e_id = e_id + 1;

                        return e_id;
                    }
                    else
                    {
                        e_id = 0;
                        return e_id;
                    }
                }
                else
                {
                    e_id = 1;
                    return e_id;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                e_id = 0;
                return e_id;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Entrega> buscaLocalizParticip(DateTime dataI, DateTime dataF, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT p_nome, p_cida, p_ende, p_nr, p_localiz, p_bairro, p_est, e_awb, e_modelo, e_clicod, e_status, e_dataenco, e_id, e_datastat, e_diasprev, e_identreg, e_situac, mec_nome FROM particip, entregas, mecanico WHERE "+
                "p_cod = e_clicod AND e_identreg = mec_cod AND e_situac <> 'P' AND e_dataenco>=@datai AND e_dataenco<=@dataf";

            List<CL_Entrega> objListEntrega = new List<CL_Entrega>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("datai", dataI);
            comand.Parameters.AddWithValue("dataf", dataF);

            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListEntrega.Add(new CL_Entrega()
                        {
                            e_clinome = dr["p_nome"].ToString().Trim(),
                            e_clicida = dr["p_cida"].ToString().Trim(),
                            e_cliende = dr["p_ende"].ToString().Trim(),
                            e_clinr = dr["p_nr"].ToString().Trim(),
                            e_clilocaliz = dr["p_localiz"].ToString().Trim(),
                            e_clibairro = dr["p_bairro"].ToString().Trim(),
                            e_cliest = dr["p_est"].ToString().Trim(),
                            e_awb = dr["e_awb"].ToString().Trim(),
                            e_modelo = dr["e_modelo"].ToString().Trim(),
                            e_clicod = dr["e_clicod"] is DBNull ? 0 : Convert.ToInt32(dr["e_clicod"]),
                            e_status = dr["e_status"].ToString().Trim(),
                            e_dataenco = dr["e_dataenco"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_dataenco"]),
                            e_diasprev = dr["e_diasprev"] is DBNull ? 0 : Convert.ToInt32(dr["e_diasprev"]),
                            e_id = dr["e_id"] is DBNull ? 0 : Convert.ToInt32(dr["e_id"]),
                            e_datastat = dr["e_datastat"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_datastat"]),
                            e_idEntregador = dr["e_identreg"] is DBNull ? 0 : Convert.ToInt32(dr["e_identreg"]),
                            e_nomeEntregador = dr["mec_nome"].ToString().Trim(),
                            e_situac = dr["e_situac"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListEntrega;
                }
                else
                {
                    objListEntrega = null;
                    return objListEntrega;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListEntrega = null;
                return objListEntrega;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_Entrega> consultaPontualidade(DateTime dataI, DateTime dataF, string cidade, string situac, string entregador, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if(entregador != "0")
            {
                if(cidade != "")
                    sql = "SELECT e_id, e_remetent, e_awb, e_dataenco, e_diasprev, e_datastat, e_status, e_situac, p_nome, p_cida, p_bairro, e_identreg, e_clicod, e_modelo FROM entregas, particip WHERE e_clicod = p_cod AND p_cida LIKE '%" + cidade + "%' AND e_identreg=@e_identreg AND e_dataenco>=@datai AND e_dataenco<=@dataf AND e_situac=@e_situac";
                else
                    sql = "SELECT e_id, e_remetent, e_awb, e_dataenco, e_diasprev, e_datastat, e_status, e_situac, p_nome, p_cida, p_bairro, e_identreg, e_clicod, e_modelo FROM entregas, particip WHERE e_clicod = p_cod AND e_identreg=@e_identreg AND e_dataenco>=@datai AND e_dataenco<=@dataf AND e_situac=@e_situac";
            }
            else
            {
                if (cidade != "")
                    sql = "SELECT e_id, e_remetent, e_awb, e_dataenco, e_diasprev, e_datastat, e_status, e_situac, p_nome, p_cida, p_bairro, e_identreg, e_clicod, e_modelo FROM entregas, particip WHERE e_clicod = p_cod AND p_cida LIKE '%" + cidade + "%' AND e_dataenco>=@datai AND e_dataenco<=@dataf AND e_situac=@e_situac";
                else
                    sql = "SELECT e_id, e_remetent, e_awb, e_dataenco, e_diasprev, e_datastat, e_status, e_situac, p_nome, p_cida, p_bairro, e_identreg, e_clicod, e_modelo FROM entregas, particip WHERE e_clicod = p_cod AND e_dataenco>=@datai AND e_dataenco<=@dataf AND e_situac=@e_situac";
            }
            List<CL_Entrega> objListEntrega = new List<CL_Entrega>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("datai", dataI);
            comand.Parameters.AddWithValue("dataf", dataF);
            comand.Parameters.AddWithValue("e_situac", situac);
            comand.Parameters.AddWithValue("e_identreg", entregador);

            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListEntrega.Add(new CL_Entrega()
                        {
                            e_id = dr["e_id"] is DBNull ? 0 : Convert.ToInt32(dr["e_id"]),
                            e_remetent = dr["e_remetent"].ToString().Trim(),
                            e_awb = dr["e_awb"].ToString().Trim(),
                            e_dataenco = dr["e_dataenco"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_dataenco"]),
                            e_datastat = dr["e_datastat"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["e_datastat"]),
                            e_status = dr["e_status"].ToString().Trim(),
                            e_diasprev = dr["e_diasprev"] is DBNull ? 0 : Convert.ToInt32(dr["e_diasprev"]),
                            e_clicod = dr["e_clicod"] is DBNull ? 0 : Convert.ToInt32(dr["e_clicod"]),
                            e_clinome = dr["p_nome"].ToString().Trim(),
                            e_clicida = dr["p_cida"].ToString().Trim(),
                            e_clibairro = dr["p_bairro"].ToString().Trim(),
                            e_situac = dr["e_situac"].ToString().Trim(),
                            e_idEntregador = dr["e_identreg"] is DBNull ? 0 : Convert.ToInt32(dr["e_identreg"]),
                            e_modelo = dr["e_modelo"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListEntrega;
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
                {
                    Conn.Close();
                }
            }
        }

        public static long buscaIDUltimaSincr(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int m_id = 0;
            string sql = "SELECT m_id FROM mov_entregas ORDER BY m_id DESC LIMIT 1";

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
                        m_id = dr["m_id"] is DBNull ? 0 : Convert.ToInt32(dr["m_id"]);
                        return m_id;
                    }
                    else
                    {
                        m_id = 0;
                        return m_id;
                    }
                }
                else
                {
                    m_id = 0;
                    return m_id;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                m_id = 0;
                return m_id;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static List<CL_SincrEntrega> buscaMovEntregaPeriodo(DateTime dataI, DateTime dataF, string coluna, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM mov_entregas WHERE m_data>=@datai AND m_data<=@dataf ORDER BY " + coluna;
            List<CL_SincrEntrega> objListEntrega = new List<CL_SincrEntrega>();

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("datai", dataI);
            comand.Parameters.AddWithValue("dataf", dataF);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objListEntrega.Add(new CL_SincrEntrega()
                        {
                            e_id = dr["m_id"] is DBNull ? 0 : Convert.ToInt32(dr["m_id"]),
                            e_identreg = dr["m_identreg"] == null ? 0 : Convert.ToInt32(dr["m_identreg"]),
                            e_idapp = dr["m_idapp"] is DBNull ? 0 : Convert.ToInt64(dr["m_idapp"]),
                            e_data = dr["m_data"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["m_data"]),
                            e_motivo = dr["m_motivo"].ToString().Trim(),
                            e_recebedor = dr["m_nreceb"].ToString().Trim(),
                            e_tpdoc = dr["m_tipodoc"].ToString().Trim(),
                            e_doc = dr["m_docum"].ToString().Trim(),
                            e_tpreceb = dr["m_tiporec"].ToString().Trim(),
                            e_obs = dr["m_obs"].ToString().Trim(),
                            e_tentat = dr["m_nrtent"] is DBNull ? 0 : Convert.ToInt32(dr["m_nrtent"]),
                            e_status = dr["m_status"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objListEntrega;
                }
                else
                {
                    objListEntrega = null;
                    return objListEntrega;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objListEntrega = null;
                return objListEntrega;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static int buscaNrTentativas(long e_identreg, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int m_nrtent = 0;
            string sql = "SELECT m_nrtent FROM mov_entregas WHERE m_identreg=@m_identreg ORDER BY m_nrtent DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("m_identreg", e_identreg);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        m_nrtent = dr["m_nrtent"] is DBNull ? 0 : Convert.ToInt32(dr["m_nrtent"]);
                        m_nrtent = m_nrtent + 1;
                        return m_nrtent;
                    }
                    else
                    {
                        m_nrtent = 1;
                        return m_nrtent;
                    }
                }
                else
                {
                    m_nrtent = 1;
                    return m_nrtent;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                m_nrtent = 0;
                return m_nrtent;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool attStatus(long e_id, string e_status, DateTime e_data, string e_situac, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE entregas SET e_datastat=@e_data, e_status=@e_status, e_situac=@e_situac WHERE e_id=@e_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("e_data", e_data);
                comand.Parameters.AddWithValue("e_status", e_status);
                comand.Parameters.AddWithValue("e_situac", e_situac);
                comand.Parameters.AddWithValue("e_id", e_id);
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

        public static bool gravaMovEntrega(CL_SincrEntrega objSincrEntrega, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO mov_entregas (m_identreg, m_idapp, m_data, m_status, m_motivo, m_nreceb, m_tipodoc, m_docum, m_tiporec, m_obs, m_foto, m_nrtent) "+
                    "VALUES (@m_identreg, @m_idapp, @m_data, @m_status, @m_motivo, @m_nreceb, @m_tipodoc, @m_docum, @m_tiporec, @m_obs, @m_foto, @m_nrtent)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("m_identreg", objSincrEntrega.e_identreg);
                comand.Parameters.AddWithValue("m_idapp", objSincrEntrega.e_idapp);
                comand.Parameters.AddWithValue("m_data", objSincrEntrega.e_data);
                comand.Parameters.AddWithValue("m_status", objSincrEntrega.e_status);
                comand.Parameters.AddWithValue("m_motivo", objSincrEntrega.e_motivo);
                comand.Parameters.AddWithValue("m_nreceb", objSincrEntrega.e_recebedor);
                comand.Parameters.AddWithValue("m_tipodoc", objSincrEntrega.e_tpdoc);
                comand.Parameters.AddWithValue("m_docum", objSincrEntrega.e_doc);
                comand.Parameters.AddWithValue("m_tiporec", objSincrEntrega.e_tpreceb);
                comand.Parameters.AddWithValue("m_obs", objSincrEntrega.e_obs);
                comand.Parameters.AddWithValue("m_foto", objSincrEntrega.e_foto);
                comand.Parameters.AddWithValue("m_nrtent", objSincrEntrega.e_tentat);

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

        public static CL_SincrEntrega buscaMovEntrega(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_SincrEntrega objSincrEntrega = new CL_SincrEntrega();
            string sql = "SELECT * FROM mov_entregas WHERE m_idapp=@m_idapp";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("m_idapp", idUmov.ToString());
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objSincrEntrega.e_idapp = idUmov;
                        objSincrEntrega.e_id = dr["m_id"] is DBNull ? 0 : Convert.ToInt64(dr["m_id"]);
                        objSincrEntrega.e_data = Convert.ToDateTime(dr["m_data"]);
                        objSincrEntrega.e_identreg = Convert.ToInt64(dr["m_identreg"]);
                        objSincrEntrega.e_motivo = dr["m_motivo"].ToString().Trim();
                        objSincrEntrega.e_status = dr["m_status"].ToString().Trim();
                        objSincrEntrega.e_recebedor = dr["m_nreceb"].ToString().Trim();
                        objSincrEntrega.e_tpdoc = dr["m_tipodoc"].ToString().Trim();
                        objSincrEntrega.e_doc = dr["m_docum"].ToString().Trim();
                        objSincrEntrega.e_tpreceb = dr["m_tiporec"].ToString().Trim();
                        objSincrEntrega.e_obs = dr["m_obs"].ToString().Trim();
                        objSincrEntrega.e_foto = dr["m_foto"].ToString().Trim();
                        objSincrEntrega.e_tentat = dr["m_nrtent"] is DBNull ? 0 : Convert.ToInt32(dr["m_nrtent"]);
                        return objSincrEntrega;
                    }
                    else
                    {
                        objSincrEntrega = null;
                        return objSincrEntrega;
                    }
                }
                else
                {
                    objSincrEntrega = null;
                    return objSincrEntrega;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objSincrEntrega = null;
                return objSincrEntrega;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool confereMovEntrega(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT m_id FROM mov_entregas WHERE m_idapp=@idapp";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("idapp", idUmov.ToString());
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

        public static bool attEntregador(int e_id, int e_idEntregador, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE entregas SET e_identreg=@idEntregador WHERE e_id=@e_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("idEntregador", e_idEntregador);
                comand.Parameters.AddWithValue("e_id", e_id);
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