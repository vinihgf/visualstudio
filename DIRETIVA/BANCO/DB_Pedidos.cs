using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BANCO
{
    public class DB_Pedidos : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static NpgsqlConnection Conn2 { get; set; }

        public static bool cadPedidos(List<CL_Pedidos> objListPed, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int recno = 0, ped_cod = 0;
            recno = DB_Funcoes.buscaRecno(recno, "pedidos", con);
            if (recno > 0)
            {
                try
                {
                    Conn.Open();
                    foreach (CL_Pedidos obj in objListPed)
                    {
                        string sql = "INSERT INTO pedidos (ped_cod, ped_ctrl, ped_codcli, ped_data, est_cod, est_nome, est_nome2, est_nome3, est_nome4, est_tpprod, " +
                            "est_ngrupo, est_nsgrup, est_famil, ped_qtdade, ped_preco, ped_desc, ped_vldesc, ped_tipo, ped_vend, ped_descnt, " +
                            "ped_pz1, ped_pz2, ped_pz3, ped_pz4, ped_pz5, ped_pz6, ped_pz7, ped_pz8, ped_pz9, ped_pz10, ped_pz11, ped_pz12, " +
                            "ped_dt1, ped_dt2, ped_dt3, ped_dt4, ped_dt5, ped_dt6, ped_dt7, ped_dt8, ped_dt9, ped_dt10, ped_dt11, ped_dt12, " +
                            "ped_vlr1, ped_vlr2, ped_vlr3, ped_vlr4, ped_vlr5, ped_vlr6, ped_vlr7, ped_vlr8, ped_vlr9, ped_vlr10, ped_vlr11, ped_vlr12, " +
                            "ped_codtra, ped_transp, ped_placa1, ped_obs1, ped_obs2, ped_obs3, ped_obs4, ped_obs5, ped_cfisc, ped_tribut, " +
                            "ped_comis, ped_voltag, ped_frete, ped_tpfret, ped_espec, ped_qtdvol, ped_marca, ped_peso, ped_condic, ped_ccondi, ped_local, " +
                            "ped_usudac, ped_movdig, ped_clinom, ped_cliend, ped_clicid, ped_clicpf, ped_trauf, ped_ufveic, ped_basest, ped_icmst, ped_vlripi, " +
                            "ped_obped1, ped_obped2, ped_ean, ped_eantri, ped_obsfn1, ped_obsfn2, ped_unid, ped_iest, ped_vlcust, ped_seguro, ped_indpre, ped_pesolq, " +
                            "ped_desesp, ped_trcida, ped_entrad, ped_cnpj, ped_vltabe, ped_lin1, ped_lin2, ped_lin3, ped_lin4, ped_lin5, ped_lin6, ped_lin7, ped_lin8, " +
                            "ped_lin9, ped_lin10, ped_lin11, ped_lin12, ped_idumov, ped_assina) " +
                            " VALUES " +
                            "(@ped_cod, @ped_ctrl, @ped_codcli, @ped_data, @est_cod, @est_nome, @est_nome2, @est_nome3, @est_nome4, @est_tpprod, " +
                            "@est_ngrupo, @est_nsgrup, @est_famil, @ped_qtdade, @ped_preco, @ped_desc, @ped_vldesc, @ped_tipo, @ped_vend, @ped_descnt, " +
                            "@ped_pz1, @ped_pz2, @ped_pz3, @ped_pz4, @ped_pz5, @ped_pz6, @ped_pz7, @ped_pz8, @ped_pz9, @ped_pz10, @ped_pz11, @ped_pz12, " +
                            "@ped_dt1, @ped_dt2, @ped_dt3, @ped_dt4, @ped_dt5, @ped_dt6, @ped_dt7, @ped_dt8, @ped_dt9, @ped_dt10, @ped_dt11, @ped_dt12, " +
                            "@ped_vlr1, @ped_vlr2, @ped_vlr3, @ped_vlr4, @ped_vlr5, @ped_vlr6, @ped_vlr7, @ped_vlr8, @ped_vlr9, @ped_vlr10, @ped_vlr11, @ped_vlr12, " +
                            "@ped_codtra, @ped_transp, @ped_placa1, @ped_obs1, @ped_obs2, @ped_obs3, @ped_obs4, @ped_obs5, @ped_cfisc, @ped_tribut, " +
                            "@ped_comis, @ped_voltag, @ped_frete, @ped_tpfret, @ped_espec, @ped_qtdvol, @ped_marca, @ped_peso, @ped_condic, @ped_ccondi, @ped_local, " +
                            "@ped_usudac, @ped_movdig, @ped_clinom, @ped_cliend, @ped_clicid, @ped_clicpf, @ped_trauf, @ped_ufveic, @ped_basest, @ped_icmst, @ped_vlripi, " +
                            "@ped_obped1, @ped_obped2, @ped_ean, @ped_eantri, @ped_obsfn1, @ped_obsfn2, @ped_unid, @ped_iest, @ped_vlcust, @ped_seguro, @ped_indpre, @ped_pesolq, " +
                            "@ped_desesp, @ped_trcida, @ped_entrad, @ped_cnpj, @ped_vltabe, @ped_lin1, @ped_lin2, @ped_lin3, @ped_lin4, @ped_lin5, @ped_lin6, @ped_lin7, @ped_lin8, " +
                            "@ped_lin9, @ped_lin10, @ped_lin11, @ped_lin12, @ped_idumov, @ped_assina)";
                        recno++;
                        NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                        cmd.Parameters.AddWithValue("ped_cod", obj.ped_cod);
                        ped_cod = obj.ped_cod;
                        cmd.Parameters.AddWithValue("ped_ctrl", recno + " - " + obj.ped_ctrl);
                        cmd.Parameters.AddWithValue("ped_codcli", obj.ped_codcli);
                        cmd.Parameters.AddWithValue("ped_data", obj.ped_data.ToShortDateString());
                        cmd.Parameters.AddWithValue("est_cod", obj.est_cod);
                        cmd.Parameters.AddWithValue("est_nome", obj.est_nome);
                        cmd.Parameters.AddWithValue("est_nome2", obj.est_nome2);
                        cmd.Parameters.AddWithValue("est_nome3", obj.est_nome3);
                        cmd.Parameters.AddWithValue("est_nome4", obj.est_nome4);
                        cmd.Parameters.AddWithValue("est_tpprod", obj.est_tpprod);
                        cmd.Parameters.AddWithValue("est_ngrupo", obj.est_ngrupo);
                        cmd.Parameters.AddWithValue("est_nsgrup", obj.est_nsgrup);
                        cmd.Parameters.AddWithValue("est_famil", obj.est_famil);
                        cmd.Parameters.AddWithValue("ped_qtdade", obj.ped_qtdade);
                        cmd.Parameters.AddWithValue("ped_preco", obj.ped_preco);
                        cmd.Parameters.AddWithValue("ped_desc", obj.ped_desc);
                        cmd.Parameters.AddWithValue("ped_vldesc", obj.ped_vldesc);
                        cmd.Parameters.AddWithValue("ped_tipo", obj.ped_tipo);
                        cmd.Parameters.AddWithValue("ped_vend", obj.ped_vend);
                        cmd.Parameters.AddWithValue("ped_descnt", obj.ped_descnt);
                        cmd.Parameters.AddWithValue("ped_pz1", obj.ped_pz1);
                        cmd.Parameters.AddWithValue("ped_pz2", obj.ped_pz2);
                        cmd.Parameters.AddWithValue("ped_pz3", obj.ped_pz3);
                        cmd.Parameters.AddWithValue("ped_pz4", obj.ped_pz4);
                        cmd.Parameters.AddWithValue("ped_pz5", obj.ped_pz5);
                        cmd.Parameters.AddWithValue("ped_pz6", obj.ped_pz6);
                        cmd.Parameters.AddWithValue("ped_pz7", obj.ped_pz7);
                        cmd.Parameters.AddWithValue("ped_pz8", obj.ped_pz8);
                        cmd.Parameters.AddWithValue("ped_pz9", obj.ped_pz9);
                        cmd.Parameters.AddWithValue("ped_pz10", obj.ped_pz10);
                        cmd.Parameters.AddWithValue("ped_pz11", obj.ped_pz11);
                        cmd.Parameters.AddWithValue("ped_pz12", obj.ped_pz12);
                        cmd.Parameters.AddWithValue("ped_dt1", obj.ped_dt1);
                        cmd.Parameters.AddWithValue("ped_dt2", obj.ped_dt2);
                        cmd.Parameters.AddWithValue("ped_dt3", obj.ped_dt3);
                        cmd.Parameters.AddWithValue("ped_dt4", obj.ped_dt4);
                        cmd.Parameters.AddWithValue("ped_dt5", obj.ped_dt5);
                        cmd.Parameters.AddWithValue("ped_dt6", obj.ped_dt6);
                        cmd.Parameters.AddWithValue("ped_dt7", obj.ped_dt7);
                        cmd.Parameters.AddWithValue("ped_dt8", obj.ped_dt8);
                        cmd.Parameters.AddWithValue("ped_dt9", obj.ped_dt9);
                        cmd.Parameters.AddWithValue("ped_dt10", obj.ped_dt10);
                        cmd.Parameters.AddWithValue("ped_dt11", obj.ped_dt11);
                        cmd.Parameters.AddWithValue("ped_dt12", obj.ped_dt12);
                        cmd.Parameters.AddWithValue("ped_vlr1", obj.ped_vlr1);
                        cmd.Parameters.AddWithValue("ped_vlr2", obj.ped_vlr2);
                        cmd.Parameters.AddWithValue("ped_vlr3", obj.ped_vlr3);
                        cmd.Parameters.AddWithValue("ped_vlr4", obj.ped_vlr4);
                        cmd.Parameters.AddWithValue("ped_vlr5", obj.ped_vlr5);
                        cmd.Parameters.AddWithValue("ped_vlr6", obj.ped_vlr6);
                        cmd.Parameters.AddWithValue("ped_vlr7", obj.ped_vlr7);
                        cmd.Parameters.AddWithValue("ped_vlr8", obj.ped_vlr8);
                        cmd.Parameters.AddWithValue("ped_vlr9", obj.ped_vlr9);
                        cmd.Parameters.AddWithValue("ped_vlr10", obj.ped_vlr10);
                        cmd.Parameters.AddWithValue("ped_vlr11", obj.ped_vlr11);
                        cmd.Parameters.AddWithValue("ped_vlr12", obj.ped_vlr12);
                        cmd.Parameters.AddWithValue("ped_codtra", obj.ped_codtra);
                        cmd.Parameters.AddWithValue("ped_transp", obj.ped_transp);
                        cmd.Parameters.AddWithValue("ped_placa1", obj.ped_placa1);
                        cmd.Parameters.AddWithValue("ped_obs1", obj.ped_obs1);
                        cmd.Parameters.AddWithValue("ped_obs2", obj.ped_obs2);
                        cmd.Parameters.AddWithValue("ped_obs3", obj.ped_obs3);
                        cmd.Parameters.AddWithValue("ped_obs4", obj.ped_obs4);
                        cmd.Parameters.AddWithValue("ped_obs5", obj.ped_obs5);
                        cmd.Parameters.AddWithValue("ped_cfisc", obj.ped_cfisc);
                        cmd.Parameters.AddWithValue("ped_tribut", obj.ped_tribut);
                        cmd.Parameters.AddWithValue("ped_comis", obj.ped_comis);
                        cmd.Parameters.AddWithValue("ped_voltag", obj.ped_voltag);
                        cmd.Parameters.AddWithValue("ped_frete", obj.ped_frete);
                        cmd.Parameters.AddWithValue("ped_tpfret", obj.ped_tpfret);
                        cmd.Parameters.AddWithValue("ped_espec", obj.ped_espec);
                        cmd.Parameters.AddWithValue("ped_qtdvol", obj.ped_qtdvol);
                        cmd.Parameters.AddWithValue("ped_marca", obj.ped_marca);
                        cmd.Parameters.AddWithValue("ped_peso", obj.ped_peso);
                        cmd.Parameters.AddWithValue("ped_condic", obj.ped_condic);
                        cmd.Parameters.AddWithValue("ped_ccondi", obj.ped_ccondic);
                        cmd.Parameters.AddWithValue("ped_local", obj.ped_local);
                        cmd.Parameters.AddWithValue("ped_usudac", obj.ped_usudac);
                        cmd.Parameters.AddWithValue("ped_movdig", DateTime.Now.ToShortDateString());
                        cmd.Parameters.AddWithValue("ped_clinom", obj.ped_clinom);
                        cmd.Parameters.AddWithValue("ped_cliend", obj.ped_cliend);
                        cmd.Parameters.AddWithValue("ped_clicid", obj.ped_clicid);
                        cmd.Parameters.AddWithValue("ped_clicpf", obj.ped_clicpf);
                        cmd.Parameters.AddWithValue("ped_trauf", obj.ped_trauf);
                        cmd.Parameters.AddWithValue("ped_ufveic", obj.ped_ufveic);
                        cmd.Parameters.AddWithValue("ped_basest", obj.ped_basest);
                        cmd.Parameters.AddWithValue("ped_icmst", obj.ped_icmst);
                        cmd.Parameters.AddWithValue("ped_vlripi", obj.ped_vlripi);
                        cmd.Parameters.AddWithValue("ped_obped1", obj.ped_obped1);
                        cmd.Parameters.AddWithValue("ped_obped2", obj.ped_obped2);
                        cmd.Parameters.AddWithValue("ped_ean", obj.ped_ean);
                        cmd.Parameters.AddWithValue("ped_eantri", obj.ped_eantri);
                        cmd.Parameters.AddWithValue("ped_obsfn1", obj.ped_obsfn1);
                        cmd.Parameters.AddWithValue("ped_obsfn2", obj.ped_obsfn2);
                        cmd.Parameters.AddWithValue("ped_unid", obj.ped_unid);
                        cmd.Parameters.AddWithValue("ped_iest", obj.ped_iest);
                        cmd.Parameters.AddWithValue("ped_vlcust", obj.ped_vlcust);
                        cmd.Parameters.AddWithValue("ped_seguro", obj.ped_seguro);
                        cmd.Parameters.AddWithValue("ped_indpre", obj.ped_indpre);
                        cmd.Parameters.AddWithValue("ped_pesolq", obj.ped_pesolq);
                        cmd.Parameters.AddWithValue("ped_desesp", obj.ped_desesp);
                        cmd.Parameters.AddWithValue("ped_trcida", obj.ped_trcida);
                        cmd.Parameters.AddWithValue("ped_entrad", obj.ped_entrad);
                        cmd.Parameters.AddWithValue("ped_cnpj", obj.ped_cnpj);
                        cmd.Parameters.AddWithValue("ped_vltabe", obj.ped_vltabe);
                        cmd.Parameters.AddWithValue("ped_lin1", obj.ped_lin1);
                        cmd.Parameters.AddWithValue("ped_lin2", obj.ped_lin2);
                        cmd.Parameters.AddWithValue("ped_lin3", obj.ped_lin3);
                        cmd.Parameters.AddWithValue("ped_lin4", obj.ped_lin4);
                        cmd.Parameters.AddWithValue("ped_lin5", obj.ped_lin5);
                        cmd.Parameters.AddWithValue("ped_lin6", obj.ped_lin6);
                        cmd.Parameters.AddWithValue("ped_lin7", obj.ped_lin7);
                        cmd.Parameters.AddWithValue("ped_lin8", obj.ped_lin8);
                        cmd.Parameters.AddWithValue("ped_lin9", obj.ped_lin9);
                        cmd.Parameters.AddWithValue("ped_lin10", obj.ped_lin10);
                        cmd.Parameters.AddWithValue("ped_lin11", obj.ped_lin11);
                        cmd.Parameters.AddWithValue("ped_lin12", obj.ped_lin12);
                        cmd.Parameters.AddWithValue("ped_idumov", obj.ped_idumov);
                        cmd.Parameters.AddWithValue("ped_assina", obj.ped_assina);

                        cmd.ExecuteScalar();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    excluiPedErro(ped_cod, con);
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
            else
            {
                return false;
            }
        }

        public static bool attCondicPed(CL_Pedidos obj, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE pedido SET p_condic=@p_condic, p_ccondi=@p_ccondi WHERE p_cod=@p_cod; UPDATE pedidos SET " +
                            "ped_condic=@p_condic, ped_ccondi=@p_ccondi, " +
                            "ped_pz1=@ped_pz1, ped_pz2=@ped_pz2, ped_pz3=@ped_pz3, ped_pz4=@ped_pz4, ped_pz5=@ped_pz5, ped_pz6=@ped_pz6, ped_pz7=@ped_pz7, " +
                            "ped_pz8=@ped_pz8, ped_pz9=@ped_pz9, ped_pz10=@ped_pz10, ped_pz11=@ped_pz11, ped_pz12=@ped_pz12, " +
                            "ped_dt1=@ped_dt1, ped_dt2=@ped_dt2, ped_dt3=@ped_dt3, ped_dt4=@ped_dt4, ped_dt5=@ped_dt5, ped_dt6=@ped_dt6, ped_dt7=@ped_dt7, " +
                            "ped_dt8=@ped_dt8, ped_dt9=@ped_dt9, ped_dt10=@ped_dt10, ped_dt11=@ped_dt11, ped_dt12=@ped_dt12, " +
                            "ped_vlr1=@ped_vlr1, ped_vlr2=@ped_vlr2, ped_vlr3=@ped_vlr3, ped_vlr4=@ped_vlr4, ped_vlr5=@ped_vlr5, ped_vlr6=@ped_vlr6, ped_vlr7=@ped_vlr7, " +
                            "ped_vlr8=@ped_vlr8, ped_vlr9=@ped_vlr9, ped_vlr10=@ped_vlr10, ped_vlr11=@ped_vlr11, ped_vlr12=@ped_vlr12 WHERE ped_cod=@p_cod";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("p_condic", obj.ped_condic);
                cmd.Parameters.AddWithValue("p_ccondi", obj.ped_ccondic);
                cmd.Parameters.AddWithValue("p_cod", obj.ped_cod);
                cmd.Parameters.AddWithValue("ped_pz1", obj.ped_pz1);
                cmd.Parameters.AddWithValue("ped_pz2", obj.ped_pz2);
                cmd.Parameters.AddWithValue("ped_pz3", obj.ped_pz3);
                cmd.Parameters.AddWithValue("ped_pz4", obj.ped_pz4);
                cmd.Parameters.AddWithValue("ped_pz5", obj.ped_pz5);
                cmd.Parameters.AddWithValue("ped_pz6", obj.ped_pz6);
                cmd.Parameters.AddWithValue("ped_pz7", obj.ped_pz7);
                cmd.Parameters.AddWithValue("ped_pz8", obj.ped_pz8);
                cmd.Parameters.AddWithValue("ped_pz9", obj.ped_pz9);
                cmd.Parameters.AddWithValue("ped_pz10", obj.ped_pz10);
                cmd.Parameters.AddWithValue("ped_pz11", obj.ped_pz11);
                cmd.Parameters.AddWithValue("ped_pz12", obj.ped_pz12);
                cmd.Parameters.AddWithValue("ped_dt1", obj.ped_dt1);
                cmd.Parameters.AddWithValue("ped_dt2", obj.ped_dt2);
                cmd.Parameters.AddWithValue("ped_dt3", obj.ped_dt3);
                cmd.Parameters.AddWithValue("ped_dt4", obj.ped_dt4);
                cmd.Parameters.AddWithValue("ped_dt5", obj.ped_dt5);
                cmd.Parameters.AddWithValue("ped_dt6", obj.ped_dt6);
                cmd.Parameters.AddWithValue("ped_dt7", obj.ped_dt7);
                cmd.Parameters.AddWithValue("ped_dt8", obj.ped_dt8);
                cmd.Parameters.AddWithValue("ped_dt9", obj.ped_dt9);
                cmd.Parameters.AddWithValue("ped_dt10", obj.ped_dt10);
                cmd.Parameters.AddWithValue("ped_dt11", obj.ped_dt11);
                cmd.Parameters.AddWithValue("ped_dt12", obj.ped_dt12);
                cmd.Parameters.AddWithValue("ped_vlr1", obj.ped_vlr1);
                cmd.Parameters.AddWithValue("ped_vlr2", obj.ped_vlr2);
                cmd.Parameters.AddWithValue("ped_vlr3", obj.ped_vlr3);
                cmd.Parameters.AddWithValue("ped_vlr4", obj.ped_vlr4);
                cmd.Parameters.AddWithValue("ped_vlr5", obj.ped_vlr5);
                cmd.Parameters.AddWithValue("ped_vlr6", obj.ped_vlr6);
                cmd.Parameters.AddWithValue("ped_vlr7", obj.ped_vlr7);
                cmd.Parameters.AddWithValue("ped_vlr8", obj.ped_vlr8);
                cmd.Parameters.AddWithValue("ped_vlr9", obj.ped_vlr9);
                cmd.Parameters.AddWithValue("ped_vlr10", obj.ped_vlr10);
                cmd.Parameters.AddWithValue("ped_vlr11", obj.ped_vlr11);
                cmd.Parameters.AddWithValue("ped_vlr12", obj.ped_vlr12);

                Conn.Open();
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

        public static List<CL_Pedidos> buscaPedidos(int p_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_Pedidos obj = null;
            List<CL_Pedidos> objListPedido = new List<CL_Pedidos>();


            string sql = "SELECT ped_cod, est_cod, est_nome, ped_qtdade, ped_preco, ped_vldesc, ped_desc, ped_idumov FROM pedidos WHERE ped_cod=@p_cod";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            comand.Parameters.AddWithValue("p_cod", p_cod);

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        obj = new CL_Pedidos();
                        obj.ped_cod = p_cod;
                        obj.est_cod = dr["est_cod"].ToString().Trim();
                        obj.est_nome = dr["est_nome"].ToString().Trim();
                        obj.ped_qtdade = Convert.ToDouble(dr["ped_qtdade"]);
                        obj.ped_preco = Convert.ToDouble(dr["ped_preco"]);
                        obj.ped_vldesc = Convert.ToDouble(dr["ped_vldesc"]);
                        obj.ped_desc = Convert.ToDouble(dr["ped_desc"]);
                        obj.ped_idumov = Convert.ToInt32(dr["ped_idumov"]);
                        objListPedido.Add(obj);
                    }
                    return objListPedido;
                }
                else
                {
                    objListPedido = null;
                    return objListPedido;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objListPedido = null;
                return objListPedido;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        private static void excluiPedErro(int ped_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn2 = new NpgsqlConnection(CONEXAO);

            string sql2 = "DELETE FROM pedidos WHERE ped_cod=" + ped_cod;
            NpgsqlCommand comand = new NpgsqlCommand(sql2, Conn2);
            try
            {
                Conn2.Open();
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
                    Conn2.Close();
                }
            }
        }
    }
}