using System;
using System.Collections.Generic;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Movduprec : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static List<CL_Movduprec> buscaMov(int dupcod, int dupparc, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT mr_data, mr_tipo, mr_acumul, mr_valor, t_descri, t_somatot FROM movduprec, tipomovtitulo " +
                "WHERE mr_duplic=@mr_duplic AND mr_parc=@mr_parc AND mr_tipo=t_codigo";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("mr_duplic", dupcod);
            comand.Parameters.AddWithValue("mr_parc", dupparc);
            NpgsqlDataReader dr;
            List<CL_Movduprec> objList = new List<CL_Movduprec>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Movduprec()
                        {
                            mr_data = Convert.ToDateTime(dr["mr_data"]),
                            mr_acumul = dr["mr_acumul"].ToString().Trim(),
                            mr_valor = Convert.ToDouble(dr["mr_valor"]),
                            mr_tipo = Convert.ToInt32(dr["mr_tipo"]),
                            mr_descricao = dr["t_descri"].ToString().Trim(),
                            mr_somatot = dr["t_somatot"].ToString().Trim(),
                        });
                    }
                    dr.Close();
                    return objList;
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

        public static int buscaCodigoRecibo(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT r_codigo FROM recibo_dupl ORDER BY r_codigo DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["r_codigo"]) + 1;
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

        public static bool recebeTotalMov(CL_Duprec objDuprec, CL_Movduprec objMovDuprec, CL_Movduprec objMovDupJuros, CL_ReciboDupl objRecibo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();
            try
            {
                string sql = "UPDATE duprec SET dup_juros=dup_juros + @dup_juros, dup_vlpgto=dup_vlpgto + @dup_vlpgto, dup_vlrrec=dup_vlrrec + @dup_vlrrec, " +
                    "dup_pgto=@dup_pgto, dup_hist=@dup_hist, dup_hist1=@dup_hist1, dup_hist4=@dup_hist2, dup_hist5=@dup_hist3, dup_usupgt=@dup_usuario, dup_sit='S', dup_diapgt=@dup_diapgto " +
                    "WHERE dup_cod=@dup_cod AND dup_parc=@dup_parc";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("dup_juros", objDuprec.dup_juros);
                cmd.Parameters.AddWithValue("dup_vlpgto", objDuprec.dup_vlpgto);
                cmd.Parameters.AddWithValue("dup_vlrrec", objDuprec.dup_vlrrec);
                cmd.Parameters.AddWithValue("dup_pgto", objDuprec.dup_pgto);
                cmd.Parameters.AddWithValue("dup_hist", objDuprec.dup_hist);
                cmd.Parameters.AddWithValue("dup_hist1", objDuprec.dup_hist1);
                cmd.Parameters.AddWithValue("dup_hist2", objDuprec.dup_hist4);
                cmd.Parameters.AddWithValue("dup_hist3", objDuprec.dup_hist5);
                cmd.Parameters.AddWithValue("dup_usuario", objDuprec.dup_usudac);
                cmd.Parameters.AddWithValue("dup_diapgto", objDuprec.dup_diapgt.ToShortDateString());
                cmd.Parameters.AddWithValue("dup_cod", objDuprec.dup_cod);
                cmd.Parameters.AddWithValue("dup_parc", objDuprec.dup_parc);
                cmd.ExecuteScalar();

                sql = "INSERT INTO movduprec (mr_duplic, mr_parc, mr_data, mr_tipo, mr_codcli, mr_cliente, mr_bco, mr_stit, mr_vend, mr_nomeven, mr_comis, " +
                      "mr_valor, mr_hist1, mr_acumul, mr_hist, mr_hist2, mr_hist3, mr_usudac, mr_movdig) VALUES (@mr_duplic, @mr_parc, @mr_data, @mr_tipo, @mr_codcli, @mr_cliente, @mr_bco, @mr_stit, @mr_vend, " +
                      "@mr_nomeven, @mr_comis, @mr_valor, @mr_hist1, @mr_acumul, @mr_hist, @mr_hist2, @mr_hist3, @mr_usudac, @mr_movdig)";
                cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("mr_duplic", objMovDuprec.mr_duplic);
                cmd.Parameters.AddWithValue("mr_parc", objMovDuprec.mr_parc);
                cmd.Parameters.AddWithValue("mr_data", objMovDuprec.mr_data.ToShortDateString());
                cmd.Parameters.AddWithValue("mr_tipo", objMovDuprec.mr_tipo);
                cmd.Parameters.AddWithValue("mr_codcli", objMovDuprec.mr_codcli);
                cmd.Parameters.AddWithValue("mr_cliente", objMovDuprec.mr_cliente);
                cmd.Parameters.AddWithValue("mr_bco", objMovDuprec.mr_bco);
                cmd.Parameters.AddWithValue("mr_stit", objMovDuprec.mr_stit);
                cmd.Parameters.AddWithValue("mr_vend", objMovDuprec.mr_vend);
                cmd.Parameters.AddWithValue("mr_nomeven", objMovDuprec.mr_nomeven);
                cmd.Parameters.AddWithValue("mr_comis", objMovDuprec.mr_comis);
                cmd.Parameters.AddWithValue("mr_valor", objMovDuprec.mr_valor);
                cmd.Parameters.AddWithValue("mr_hist1", objMovDuprec.mr_hist1);
                cmd.Parameters.AddWithValue("mr_acumul", objMovDuprec.mr_acumul);
                cmd.Parameters.AddWithValue("mr_hist", objMovDuprec.mr_hist);
                cmd.Parameters.AddWithValue("mr_hist2", objMovDuprec.mr_hist2);
                cmd.Parameters.AddWithValue("mr_hist3", objMovDuprec.mr_hist3);
                cmd.Parameters.AddWithValue("mr_usudac", objMovDuprec.mr_usudac);
                cmd.Parameters.AddWithValue("mr_movdig", objMovDuprec.mr_movdig.ToShortDateString());
                cmd.ExecuteScalar();

                sql = "INSERT INTO recibo_dupl (r_data, r_titulo, r_parc, r_cliente, r_vlrtot, r_vlrtit, r_vlrjur, r_vlrdesc, r_hist1, r_hist2, r_hist3) "+
                    "VALUES (@r_data, @r_titulo, @r_parc, @r_cliente, @r_vlrtot, @r_vlrtit, @r_vlrjur, @r_vlrdesc, @r_hist1, @r_hist2, @r_hist3)";
                cmd = new NpgsqlCommand(sql, Conn, transaction);
                cmd.Parameters.AddWithValue("r_data", objRecibo.r_data);
                cmd.Parameters.AddWithValue("r_titulo", objRecibo.r_titulo);
                cmd.Parameters.AddWithValue("r_parc", objRecibo.r_parc);
                cmd.Parameters.AddWithValue("r_cliente", objRecibo.r_cliente);
                cmd.Parameters.AddWithValue("r_vlrtot", objRecibo.r_vlrtot);
                cmd.Parameters.AddWithValue("r_vlrtit", objRecibo.r_vlrtit);
                cmd.Parameters.AddWithValue("r_vlrjur", objRecibo.r_vlrjur);
                cmd.Parameters.AddWithValue("r_vlrdesc", objRecibo.r_vlrdesc);
                cmd.Parameters.AddWithValue("r_hist1", objRecibo.r_hist1);
                cmd.Parameters.AddWithValue("r_hist2", objRecibo.r_hist2);
                cmd.Parameters.AddWithValue("r_hist3", objRecibo.r_hist3);
                cmd.ExecuteScalar();

                if (objMovDupJuros != null)
                {
                    sql = "INSERT INTO movduprec (mr_duplic, mr_parc, mr_data, mr_tipo, mr_codcli, mr_cliente, mr_bco, mr_stit, mr_vend, mr_nomeven, mr_comis, " +
                          "mr_valor, mr_hist1, mr_acumul, mr_hist, mr_hist2, mr_hist3, mr_usudac, mr_movdig) VALUES (@mr_duplic, @mr_parc, @mr_data, @mr_tipo, @mr_codcli, @mr_cliente, @mr_bco, @mr_stit, @mr_vend, " +
                          "@mr_nomeven, @mr_comis, @mr_valor, @mr_hist1, @mr_acumul, @mr_hist, @mr_hist2, @mr_hist3, @mr_usudac, @mr_movdig)";
                    cmd = new NpgsqlCommand(sql, Conn, transaction);
                    cmd.Parameters.AddWithValue("mr_duplic", objMovDupJuros.mr_duplic);
                    cmd.Parameters.AddWithValue("mr_parc", objMovDupJuros.mr_parc);
                    cmd.Parameters.AddWithValue("mr_data", objMovDupJuros.mr_data.ToShortDateString());
                    cmd.Parameters.AddWithValue("mr_codcli", objMovDupJuros.mr_codcli);
                    cmd.Parameters.AddWithValue("mr_cliente", objMovDupJuros.mr_cliente);
                    cmd.Parameters.AddWithValue("mr_bco", objMovDupJuros.mr_bco);
                    cmd.Parameters.AddWithValue("mr_stit", objMovDupJuros.mr_stit);
                    cmd.Parameters.AddWithValue("mr_vend", objMovDupJuros.mr_vend);
                    cmd.Parameters.AddWithValue("mr_nomeven", objMovDupJuros.mr_nomeven);
                    cmd.Parameters.AddWithValue("mr_comis", objMovDupJuros.mr_comis);
                    cmd.Parameters.AddWithValue("mr_valor", objDuprec.dup_juros);
                    cmd.Parameters.AddWithValue("mr_hist", objMovDupJuros.mr_hist);
                    cmd.Parameters.AddWithValue("mr_hist1", objMovDupJuros.mr_hist1);
                    cmd.Parameters.AddWithValue("mr_hist2", objMovDupJuros.mr_hist2);
                    cmd.Parameters.AddWithValue("mr_hist3", objMovDupJuros.mr_hist3);
                    cmd.Parameters.AddWithValue("mr_usudac", objMovDupJuros.mr_usudac);
                    cmd.Parameters.AddWithValue("mr_movdig", objMovDupJuros.mr_movdig.ToShortDateString());
                    if (objDuprec.dup_juros > 0)
                    {
                        cmd.Parameters.AddWithValue("mr_tipo", 4);
                        cmd.Parameters.AddWithValue("mr_acumul", "C");
                    }
                    else if (objDuprec.dup_juros < 0)
                    {
                        cmd.Parameters.AddWithValue("mr_tipo", 5);
                        cmd.Parameters.AddWithValue("mr_acumul", "D");
                    }
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
    }
}