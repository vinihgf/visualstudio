using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Pedido : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static NpgsqlConnection Conn2 { get; set; }
        public static int buscaCod(int p_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_cod FROM pedido ORDER BY p_cod DESC LIMIT 1";

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
                        p_cod = Convert.ToInt16(dr["p_cod"]) + 1;
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
                    p_cod = 1;
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

        public static CL_Pedido buscaPedidoIDUmov(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_cod, p_ctrl, p_codcli, p_clinom, p_data, p_total, p_vend, con_nome, p_condic, p_ccondi, p_assina " +
                "FROM pedido, convenio WHERE p_vend=con_cod AND p_idumov=" + idUmov;
            CL_Pedido objPedido = new CL_Pedido();
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
                        objPedido.p_cod = Convert.ToInt32(dr["p_cod"]);
                        objPedido.p_ctrl = dr["p_ctrl"].ToString().Trim();
                        objPedido.p_codcli = Convert.ToInt32(dr["p_codcli"]);
                        objPedido.p_clinom = dr["p_clinom"].ToString().Trim();
                        objPedido.p_data = Convert.ToDateTime(dr["p_data"]);
                        objPedido.p_total = Convert.ToDouble(dr["p_total"]);
                        objPedido.p_vend = Convert.ToInt32(dr["p_vend"]);
                        objPedido.p_vendnom = dr["con_nome"].ToString().Trim();
                        objPedido.p_condic = dr["p_condic"].ToString().Trim();
                        objPedido.p_ccondi = Convert.ToInt32(dr["p_ccondi"]);
                        objPedido.p_idumov = idUmov;
                        objPedido.p_assina = dr["p_assina"].ToString().Trim();
                        return objPedido;
                    }
                    else
                    {
                        objPedido = null;
                        return objPedido;
                    }
                }
                else
                {
                    objPedido = null;
                    return objPedido;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                objPedido = null;
                return objPedido;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool conferePedidoApp(long p_idumov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT p_cod FROM pedido WHERE p_idumov=" + p_idumov;

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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public static bool cad_Pedido(CL_Pedido objPedido, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int recno = 0;
            recno = DB_Funcoes.buscaRecno(recno, "pedido", con);
            if (recno > 0)
            {
                try
                {
                    Conn.Open();
                    string sql = "INSERT INTO pedido (p_cod, p_ctrl, p_codcli, p_data, p_total, p_vend, p_condic, p_ccondi, " +
                        "p_usudac, p_movdig, p_clinom, p_transp, p_fonetra, p_idumov, p_assina) " +
                        "VALUES " +
                        "(@p_cod, @p_ctrl, @p_codcli, @p_data, @p_total, @p_vend, @p_condic, @p_ccondi, " +
                        "@p_usudac, @p_movdig, @p_clinom, @p_transp, @p_fonetra, @p_idumov, @p_assina)";
                    recno++;
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, Conn);
                    cmd.Parameters.AddWithValue("p_cod", objPedido.p_cod);
                    cmd.Parameters.AddWithValue("p_ctrl", recno + " - " + objPedido.p_ctrl);
                    cmd.Parameters.AddWithValue("p_codcli", objPedido.p_codcli);
                    cmd.Parameters.AddWithValue("p_data", objPedido.p_data);
                    cmd.Parameters.AddWithValue("p_total", objPedido.p_total);
                    cmd.Parameters.AddWithValue("p_vend", objPedido.p_vend);
                    cmd.Parameters.AddWithValue("p_condic", objPedido.p_condic);
                    cmd.Parameters.AddWithValue("p_ccondi", objPedido.p_ccondi);
                    cmd.Parameters.AddWithValue("p_usudac", objPedido.p_usudac);
                    cmd.Parameters.AddWithValue("p_movdig", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("p_clinom", objPedido.p_clinom);
                    cmd.Parameters.AddWithValue("p_transp", objPedido.p_transp);
                    cmd.Parameters.AddWithValue("p_fonetra", objPedido.p_fonetra);
                    cmd.Parameters.AddWithValue("p_idumov", objPedido.p_idumov);
                    cmd.Parameters.AddWithValue("p_assina", objPedido.p_assina);
                    cmd.ExecuteScalar();
                    return true;
                }
                catch (Exception ex)
                {
                    excluiPedErro(objPedido.p_cod, con);
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

        private static void excluiPedErro(int p_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn2 = new NpgsqlConnection(CONEXAO);

            string sql2 = "DELETE FROM pedidos WHERE ped_cod=" + p_cod + "; DELETE FROM pedido WHERE p_cod=" + p_cod;
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