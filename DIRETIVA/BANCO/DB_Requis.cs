using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Requis : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaCodRequis(int req_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT req_cod FROM requis ORDER BY req_cod DESC limit 1";

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
                        req_cod = Convert.ToInt32(dr["req_cod"]);
                        req_cod = req_cod + 1;
                        return req_cod;
                    }
                    else
                    {
                        req_cod = 0;
                        return req_cod;
                    }

                }
                else
                {
                    req_cod = 1;
                    return req_cod;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                req_cod = 0;
                return req_cod;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static List<CL_Requis> buscaRequis(CL_Requis objRequis, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM requis WHERE req_cod=" + objRequis.req_cod;

            List<CL_Requis> objList = new List<CL_Requis>();
            CL_Requis obj = null;

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
                        obj = new CL_Requis();
                        //leio as informações dos campos e jogo para o objeto
                        obj.req_cod = Convert.ToInt32(dr["req_cod"]);

                        if (dr["req_codcli"] != null)
                        {
                            obj.req_vend = Convert.ToInt32(dr["req_vend"]);
                        }
                        else
                        {
                            obj.req_vend = 0;
                        }
                        obj.req_codcli = Convert.ToInt32(dr["req_codcli"]);
                        obj.req_oserv = Convert.ToInt32(dr["req_oserv"]);
                        obj.req_data = Convert.ToDateTime(dr["req_data"]);
                        obj.req_est.est_cod = dr["est_cod"].ToString().Trim();
                        obj.req_est.est_nome = dr["est_nome"].ToString().Trim();
                        obj.req_estcod = dr["est_cod"].ToString().Trim();
                        obj.req_estnome = dr["est_nome"].ToString().Trim();
                        obj.req_est.est_tpprod = dr["est_tpprod"].ToString().Trim();
                        obj.req_est.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                        obj.req_est.est_nsgrup = Convert.ToInt32(dr["est_nsgrup"]);
                        obj.req_est.est_famil = dr["est_famil"].ToString().Trim();
                        obj.req_qtdade = Convert.ToInt32(dr["req_qtdade"]);
                        obj.req_preco = Convert.ToDouble(dr["req_preco"]);
                        obj.req_nota = Convert.ToInt32(dr["req_nota"]);
                        obj.req_desc = Convert.ToDouble(dr["req_desc"]);
                        obj.req_custo = Convert.ToDouble(dr["req_custo"]);
                        obj.req_ntoper = dr["req_ntoper"].ToString().Trim();
                        obj.req_lcto = Convert.ToInt32(dr["req_lcto"]);
                        obj.req_vended = Convert.ToInt32(dr["req_vended"]);
                        obj.req_tpserv = dr["req_tpserv"].ToString().Trim();
                        obj.req_impr = dr["req_impr"].ToString().Trim();
                        obj.req_local = dr["req_local"].ToString().Trim();
                        obj.req_condic = Convert.ToInt32(dr["req_condic"]);
                        obj.req_vldesc = Convert.ToDouble(dr["req_vldesc"]);
                        obj.req_cnpj = dr["req_cnpj"].ToString().Trim();
                        obj.req_iest = dr["req_iest"].ToString().Trim();
                        obj.req_situac = dr["req_situac"].ToString().Trim();
                        obj.req_tribut = Convert.ToInt32(dr["req_tribut"]);
                        obj.req_issqn = dr["req_issqn"].ToString().Trim();
                        obj.req_vlrTot = obj.req_qtdade * obj.req_preco - obj.req_vldesc;
                        objList.Add(obj);
                    }
                }
                dr.Close();
                return objList;
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

        public static double valorservico(int o_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT SUM(req_qtfat) as total FROM requis WHERE req_issqn='S' AND req_oserv=" + o_cod;
            double qtdFat = 0;
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
                        string aux = dr["total"].ToString().Trim();
                        if (aux != "")
                            qtdFat = Convert.ToDouble(dr["total"]);
                        else
                            qtdFat = 0;

                        dr.Close();
                        return qtdFat;
                    }
                    else
                    {
                        qtdFat = 0;
                        return qtdFat;
                    }
                }
                else
                {
                    qtdFat = 0;
                    return qtdFat;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                qtdFat = 0;
                return qtdFat;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static double qtdFat(int o_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT SUM(req_qtfat) as total FROM requis WHERE req_oserv=" + o_cod;
            double qtdFat = 0;
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
                        qtdFat = Convert.ToDouble(dr["total"]);
                        dr.Close();
                        return qtdFat;
                    }
                    else
                    {
                        qtdFat = 0;
                        return qtdFat;
                    }
                }
                else
                {
                    qtdFat = 0;
                    return qtdFat;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                qtdFat = 0;
                return qtdFat;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static double totalPecas(int o_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT SUM(req_qtdade * req_preco) as total FROM requis WHERE est_cod <> 'MO' AND req_oserv=" + o_cod;
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return dr["total"] is DBNull ? 0 : Convert.ToDouble(dr["total"]);
                    else
                        return 0;
                }
                else
                    return 0;

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
        public static List<CL_Requis> listar(string pesq, string con, string filtroPesq)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "";

            if (filtroPesq == "1")
            {
                sql = "SELECT * FROM requis WHERE req_oserv=" + pesq + " ORDER BY req_cod ASC";
            }
            else if (filtroPesq == "2")
            {
                sql = "SELECT * FROM requis WHERE req_codcli=" + pesq + " ORDER BY req_cod ASC";
            }

            List<CL_Requis> objList = new List<CL_Requis>();
            CL_Requis obj = null;

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
                        obj = new CL_Requis();
                        //leio as informações dos campos e jogo para o objeto
                        obj.req_cod = Convert.ToInt32(dr["req_cod"]);

                        if (dr["req_codcli"] != null)
                        {
                            obj.req_vend = Convert.ToInt32(dr["req_vend"]);
                        }
                        else
                        {
                            obj.req_vend = 0;
                        }
                        obj.req_codcli = Convert.ToInt32(dr["req_codcli"]);
                        obj.req_oserv = Convert.ToInt32(dr["req_oserv"]);
                        obj.req_data = Convert.ToDateTime(dr["req_data"]);
                        obj.req_est.est_cod = dr["est_cod"].ToString().Trim();
                        obj.req_est.est_nome = dr["est_nome"].ToString().Trim();
                        obj.req_est.est_tpprod = dr["est_tpprod"].ToString().Trim();
                        obj.req_est.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                        obj.req_est.est_nsgrup = Convert.ToInt32(dr["est_nsgrup"]);
                        obj.req_est.est_famil = dr["est_famil"].ToString().Trim();
                        obj.req_qtdade = Convert.ToInt32(dr["req_qtdade"]);
                        obj.req_preco = Convert.ToDouble(dr["req_preco"]);
                        obj.req_nota = Convert.ToInt32(dr["req_nota"]);
                        obj.req_desc = Convert.ToDouble(dr["req_desc"].ToString().Trim());
                        obj.req_custo = Convert.ToDouble(dr["req_custo"]);
                        obj.req_ntoper = dr["req_ntoper"].ToString().Trim();
                        obj.req_lcto = Convert.ToInt32(dr["req_lcto"]);
                        obj.req_vended = Convert.ToInt32(dr["req_vended"]);
                        obj.req_tpserv = dr["req_tpserv"].ToString().Trim();
                        obj.req_impr = dr["req_impr"].ToString().Trim();
                        obj.req_local = dr["req_local"].ToString().Trim();
                        obj.req_condic = Convert.ToInt32(dr["req_condic"]);
                        obj.req_vldesc = Convert.ToDouble(dr["req_vldesc"]);
                        obj.req_cnpj = dr["req_cnpj"].ToString().Trim();
                        obj.req_iest = dr["req_iest"].ToString().Trim();
                        obj.req_situac = dr["req_situac"].ToString().Trim();
                        obj.req_tribut = Convert.ToInt32(dr["req_tribut"]);
                        obj.req_issqn = dr["req_issqn"].ToString().Trim();
                        obj.req_vlrTot = obj.req_qtdade * obj.req_preco;
                        objList.Add(obj);
                    }
                }
                dr.Close();
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static bool incluiRequis(CL_Requis obj, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            int sr_recno = buscaRecno(con);
            if (sr_recno == 0)
                return false;
            else
            {
                try
                {
                    string sql = "INSERT INTO requis (req_lcto, req_cod, req_vend, req_codcli, req_oserv, req_data, est_cod, est_nome, est_tpprod, est_ngrupo, est_nsgrup, est_famil, req_qtdade, req_preco, req_desc, req_custo, req_vended, req_tribut, req_issqn, req_vldesc, req_situac, req_pcfixo, req_tpserv, req_qtfat, req_ctrreg, req_cnpj, req_iest, req_codend)" +
                    "VALUES (@req_lcto, @req_cod, @req_vend, @req_codcli, @req_oserv, @req_data, @est_cod, @est_nome, @est_tpprod, @est_ngrupo, @est_nsgrup, @est_famil, @req_qtdade, @req_preco, @req_desc, @req_custo, @req_vended, @req_tribut, @req_issqn, @req_vldesc, @req_situac, @req_pcfixo, @req_tpserv, @req_qtfat, @req_ctrreg, @req_cnpj, @req_iest, @req_codend)";
                    Conn.Open();
                    NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                    obj.req_ctrreg = obj.req_cod + " " + sr_recno + " " + obj.req_est.est_cod;
                    comand.Parameters.AddWithValue("req_lcto", obj.req_lcto);
                    comand.Parameters.AddWithValue("req_cod", obj.req_cod);
                    comand.Parameters.AddWithValue("req_vend", obj.req_vend);
                    comand.Parameters.AddWithValue("req_codcli", obj.req_codcli);
                    comand.Parameters.AddWithValue("req_oserv", obj.req_oserv);
                    comand.Parameters.AddWithValue("req_data", obj.req_data.ToShortDateString());
                    comand.Parameters.AddWithValue("est_cod", obj.req_est.est_cod.Trim());
                    comand.Parameters.AddWithValue("est_nome", obj.req_est.est_nome.Trim());
                    comand.Parameters.AddWithValue("est_tpprod", obj.req_est.est_tpprod.Trim());
                    comand.Parameters.AddWithValue("est_ngrupo", obj.req_est.est_ngrupo);
                    comand.Parameters.AddWithValue("est_nsgrup", obj.req_est.est_nsgrup);
                    comand.Parameters.AddWithValue("est_famil", obj.req_est.est_famil);
                    comand.Parameters.AddWithValue("req_qtdade", obj.req_qtdade);
                    comand.Parameters.AddWithValue("req_preco", obj.req_preco);
                    comand.Parameters.AddWithValue("req_desc", obj.req_desc);
                    comand.Parameters.AddWithValue("req_custo", obj.req_custo);
                    comand.Parameters.AddWithValue("req_vended", obj.req_vended);
                    comand.Parameters.AddWithValue("req_tribut", obj.req_tribut);
                    comand.Parameters.AddWithValue("req_issqn", obj.req_issqn);
                    comand.Parameters.AddWithValue("req_vldesc", obj.req_vldesc);
                    comand.Parameters.AddWithValue("req_situac", obj.req_situac);
                    comand.Parameters.AddWithValue("req_pcfixo", obj.req_pcfixo);
                    comand.Parameters.AddWithValue("req_tpserv", obj.req_tpserv);
                    comand.Parameters.AddWithValue("req_qtfat", obj.req_qtfat);
                    comand.Parameters.AddWithValue("req_ctrreg", obj.req_ctrreg);
                    comand.Parameters.AddWithValue("req_cnpj", obj.req_cnpj);
                    comand.Parameters.AddWithValue("req_iest", obj.req_iest);
                    comand.Parameters.AddWithValue("req_codend", obj.req_codend);

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
        }
        public static int buscaRecno(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT sr_recno FROM requis ORDER BY sr_recno DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["sr_recno"]) + 1;
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
        public static bool excluiRequis(int req_cod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM requis WHERE req_cod=" + req_cod;

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
        public static bool alteraRequis(List<CL_Requis> objListRequisC, string con)
        {
            return false;
        }
        public static bool encerraRequis(CL_Requis objRequis, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = " UPDATE requis SET req_situac='Z' WHERE req_cod= " + objRequis.req_cod;

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
        public static bool excluiItemRequis(CL_Requis objRemoveRequis, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "DELETE FROM requis WHERE est_cod='" + objRemoveRequis.req_est.est_cod + "' AND req_cod=" + objRemoveRequis.req_cod;

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
        public static List<CL_Requis> buscaRequisOserv(int o_serv, int particip, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM requis WHERE req_codcli=" + particip + " AND req_oserv=" + o_serv;

            List<CL_Requis> objList = new List<CL_Requis>();
            CL_Requis obj = null;

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
                        obj = new CL_Requis();
                        //leio as informações dos campos e jogo para o objeto
                        obj.req_cod = Convert.ToInt32(dr["req_cod"]);

                        if (dr["req_codcli"] != null)
                        {
                            obj.req_vend = Convert.ToInt32(dr["req_vend"]);
                        }
                        else
                        {
                            obj.req_vend = 0;
                        }
                        obj.req_codcli = Convert.ToInt32(dr["req_codcli"]);
                        obj.req_oserv = Convert.ToInt32(dr["req_oserv"]);
                        obj.req_data = Convert.ToDateTime(dr["req_data"]);
                        obj.req_est.est_cod = dr["est_cod"].ToString().Trim();
                        obj.req_est.est_nome = dr["est_nome"].ToString().Trim();
                        obj.req_estcod = dr["est_cod"].ToString().Trim();
                        obj.req_estnome = dr["est_nome"].ToString().Trim();
                        obj.req_est.est_tpprod = dr["est_tpprod"].ToString().Trim();
                        obj.req_est.est_ngrupo = Convert.ToInt32(dr["est_ngrupo"]);
                        obj.req_est.est_nsgrup = Convert.ToInt32(dr["est_nsgrup"]);
                        obj.req_est.est_famil = dr["est_famil"].ToString().Trim();
                        obj.req_qtdade = Convert.ToInt32(dr["req_qtdade"]);
                        obj.req_preco = Convert.ToDouble(dr["req_preco"]);
                        obj.req_custo = Convert.ToDouble(dr["req_custo"]);
                        obj.req_ntoper = dr["req_ntoper"].ToString().Trim();
                        obj.req_lcto = Convert.ToInt32(dr["req_lcto"]);
                        obj.req_vended = Convert.ToInt32(dr["req_vended"]);
                        obj.req_tpserv = dr["req_tpserv"].ToString().Trim();
                        obj.req_impr = dr["req_impr"].ToString().Trim();
                        obj.req_local = dr["req_local"].ToString().Trim();
                        obj.req_vldesc = Convert.ToDouble(dr["req_vldesc"]);
                        obj.req_cnpj = dr["req_cnpj"].ToString().Trim();
                        obj.req_iest = dr["req_iest"].ToString().Trim();
                        obj.req_situac = dr["req_situac"].ToString().Trim();
                        obj.req_tribut = Convert.ToInt32(dr["req_tribut"]);
                        obj.req_issqn = dr["req_issqn"].ToString().Trim();
                        obj.req_vlrTot = obj.req_qtdade * obj.req_preco - obj.req_vldesc;
                        objList.Add(obj);
                    }
                }
                dr.Close();
                return objList;
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
        public static bool attDadosRequis(List<CL_Requis> objListRequisRetorno, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int req_cod = 0, req_codcli = 0, req_oserv = 0;
            NpgsqlCommand comand = null;
            try
            {
                Conn.Open();
                string sql = "";
                foreach (CL_Requis obj in objListRequisRetorno)
                {
                    if (obj.req_qtdade > obj.req_qtdDev)
                    {
                        sql = "UPDATE requis SET req_qtdade=@req_qtdade WHERE req_oserv=@req_oserv AND req_codcli=@req_codcli AND req_cod=@req_cod";
                        comand = new NpgsqlCommand(sql, Conn);
                        comand.Parameters.AddWithValue("req_qtdade", obj.req_qtdade);
                        comand.Parameters.AddWithValue("req_oserv", obj.req_oserv);
                        comand.Parameters.AddWithValue("req_codcli", obj.req_codcli);
                        comand.Parameters.AddWithValue("req_cod", obj.req_cod);
                        comand.ExecuteScalar();
                        req_cod = obj.req_cod;
                        req_oserv = obj.req_oserv;
                        req_codcli = obj.req_codcli;
                    }
                }
                sql = "UPDATE requis SET req_situac='R' WHERE req_oserv=@req_oserv AND req_codcli=@req_codcli AND req_cod=@req_cod";
                comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("req_oserv", req_oserv);
                comand.Parameters.AddWithValue("req_codcli", req_codcli);
                comand.Parameters.AddWithValue("req_cod", req_cod);
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