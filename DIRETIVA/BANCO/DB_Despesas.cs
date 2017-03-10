using System;
using CLASSES;
using Npgsql;
using System.Data;

namespace BANCO
{
    public class DB_Despesas : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            int d_id = 0;

            string sql = "SELECT d_id FROM despesa_lavoura ORDER BY d_id DESC LIMIT 1";

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
                        d_id = Convert.ToInt32(dr["d_id"]);
                        d_id = d_id + 1;
                        return d_id;
                    }
                    else
                    {
                        d_id = 0;
                        return d_id;
                    }
                }
                else
                {
                    d_id = 1;
                    return d_id;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                d_id = 0;
                return d_id;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadDespesa(CL_Despesas objDespesa, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO despesa_lavoura (d_data, d_serie, d_maquina, d_maquina2, d_estcod, d_caixa, d_contas, d_id, d_nota, d_fornec, d_lavoura, d_produto, " +
                    "d_valor, d_ccusto, d_qtdade)" +
                "VALUES (@d_data, @d_serie, @d_maquina, @d_maquina2, @d_estcod, @d_caixa, @d_contas, @d_id, @d_nota, @d_fornec, @d_lavoura, @d_produto," +
                    "@d_valor, @d_ccusto, @d_qtdade)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("d_data", objDespesa.d_data.ToShortDateString());
                comand.Parameters.AddWithValue("d_serie", objDespesa.d_serie);
                comand.Parameters.AddWithValue("d_maquina", objDespesa.d_maquina);
                comand.Parameters.AddWithValue("d_maquina2", objDespesa.d_maquina2);
                comand.Parameters.AddWithValue("d_estcod", objDespesa.d_estcod);
                comand.Parameters.AddWithValue("d_caixa", objDespesa.d_caixa);
                comand.Parameters.AddWithValue("d_contas", objDespesa.d_contas);
                comand.Parameters.AddWithValue("d_id", objDespesa.d_id);
                comand.Parameters.AddWithValue("d_nota", objDespesa.d_nota);
                comand.Parameters.AddWithValue("d_fornec", objDespesa.d_fornec);
                comand.Parameters.AddWithValue("d_lavoura", objDespesa.d_lavoura);
                comand.Parameters.AddWithValue("d_produto", objDespesa.d_produto);
                comand.Parameters.AddWithValue("d_valor", objDespesa.d_valor);
                comand.Parameters.AddWithValue("d_ccusto", objDespesa.d_ccusto);
                comand.Parameters.AddWithValue("d_qtdade", objDespesa.d_qtdade);
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

        public static bool alterarDespesa(CL_Despesas objDespesa, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE despesa_lavoura SET d_data=@d_data, d_serie=@d_serie, d_maquina=@d_maquina, d_maquina2=@d_maquina2, d_estcod=@d_estcod, d_caixa=@d_caixa, " +
                    "d_contas=@d_contas, d_nota=@d_nota, d_fornec=@d_fornec, d_lavoura=@d_lavoura, d_produto=@d_produto, " +
                    "d_valor, d_ccusto, d_qtdade WHERE d_id=@d_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("d_data", objDespesa.d_data.ToShortDateString());
                comand.Parameters.AddWithValue("d_serie", objDespesa.d_serie);
                comand.Parameters.AddWithValue("d_maquina", objDespesa.d_maquina);
                comand.Parameters.AddWithValue("d_maquina2", objDespesa.d_maquina2);
                comand.Parameters.AddWithValue("d_estcod", objDespesa.d_estcod);
                comand.Parameters.AddWithValue("d_caixa", objDespesa.d_caixa);
                comand.Parameters.AddWithValue("d_contas", objDespesa.d_contas);
                comand.Parameters.AddWithValue("d_id", objDespesa.d_id);
                comand.Parameters.AddWithValue("d_nota", objDespesa.d_nota);
                comand.Parameters.AddWithValue("d_fornec", objDespesa.d_fornec);
                comand.Parameters.AddWithValue("d_lavoura", objDespesa.d_lavoura);
                comand.Parameters.AddWithValue("d_produto", objDespesa.d_produto);
                comand.Parameters.AddWithValue("d_valor", objDespesa.d_valor);
                comand.Parameters.AddWithValue("d_ccusto", objDespesa.d_ccusto);
                comand.Parameters.AddWithValue("d_qtdade", objDespesa.d_qtdade);
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

        public static bool excluirDespesa(CL_Despesas objDespesa, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "DELETE FROM despesa_lavoura WHERE d_id=@d_id";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("d_id", objDespesa.d_id);
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

        public static CL_Despesas buscaDespesa(string d_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_Despesas objDespesa = new CL_Despesas();

            string sql = "SELECT * FROM despesa_lavoura WHERE d_id=" + d_id;

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
                        objDespesa.d_id = Convert.ToInt32(dr["d_id"]);
                        objDespesa.d_data = Convert.ToDateTime(dr["d_data"]);
                        objDespesa.d_serie = dr["d_serie"].ToString().Trim();
                        objDespesa.d_maquina = dr["d_maquina"].ToString().Trim();
                        objDespesa.d_maquina2 = dr["d_maquina2"].ToString().Trim();
                        objDespesa.d_estcod = dr["d_estcod"].ToString().Trim();
                        objDespesa.d_nota = Convert.ToInt32(dr["d_nota"]);
                        objDespesa.d_fornec = Convert.ToInt32(dr["d_fornec"]);
                        objDespesa.d_lavoura = Convert.ToInt32(dr["d_lavoura"]);
                        objDespesa.d_produto = Convert.ToInt32(dr["d_produto"]);
                        objDespesa.d_valor = Convert.ToDouble(dr["d_valor"]);
                        objDespesa.d_ccusto = Convert.ToInt32(dr["d_ccusto"]);
                        objDespesa.d_qtdade = Convert.ToDouble(dr["d_qtdade"]);


                        return objDespesa;
                    }
                    else
                    {
                        objDespesa = null;
                        return objDespesa;
                    }
                }
                else
                {
                    objDespesa = null;
                    return objDespesa;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objDespesa = null;
                return objDespesa;
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