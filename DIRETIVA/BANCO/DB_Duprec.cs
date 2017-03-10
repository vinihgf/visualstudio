using System;
using System.Collections.Generic;
using CLASSES;
using System.Data;
using Npgsql;
using System.Linq;

namespace BANCO
{
    public class DB_Duprec : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaCodigo(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT dup_cod FROM duprec ORDER BY dup_cod DESC LIMIT 1";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                        return Convert.ToInt32(dr["dup_cod"]) + 1;
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

        public static bool conferePermissao(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_mctarec FROM usudac WHERE u_email=@u_email";

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
                        string ret = dr["u_mctarec"].ToString().Trim();
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

        public static CL_Duprec buscaTituloTotMov(int dupcod, int dupparc, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT dup_cod, dup_parc, dup_nome, dup_nota, dup_bco, bco_nome, dup_vend, con_nome, dup_comis, dup_emis, dup_vcto, dup_valor, dup_vlnota, dup_nrbco, dup_stit, s_descri "+
                        "FROM duprec LEFT JOIN bancos ON dup_bco = bco_cod "+
                        "LEFT JOIN convenio ON dup_vend = con_cod "+
                        "JOIN sittitulo ON dup_stit = s_codigo "+
                        "WHERE dup_cod = @dupcod AND dup_parc = @dupparc";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dupcod", dupcod);
            comand.Parameters.AddWithValue("dupparc", dupparc);
            NpgsqlDataReader dr;
            CL_Duprec obj = new CL_Duprec();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        obj.dup_cod = dupcod;
                        obj.dup_parc = dupparc;
                        obj.dup_nota = Convert.ToInt32(dr["dup_nota"]);
                        obj.dup_emis = Convert.ToDateTime(dr["dup_emis"]);
                        obj.dup_vcto = Convert.ToDateTime(dr["dup_vcto"]);
                        obj.dup_valor = Convert.ToDouble(dr["dup_valor"]);
                        obj.dup_nome = dr["dup_nome"].ToString().Trim();
                        obj.dup_bco = Convert.ToInt32(dr["dup_bco"]);
                        obj.dup_banco = dr["bco_nome"].ToString().Trim();
                        obj.dup_vend = Convert.ToInt32(dr["dup_vend"]);
                        obj.dup_vendnome = dr["con_nome"].ToString().Trim();
                        obj.dup_comis = Convert.ToDouble(dr["dup_comis"]);
                        obj.dup_vlnota = Convert.ToDouble(dr["dup_vlnota"]);
                        obj.dup_nrbco = dr["dup_nrbco"].ToString().Trim();
                        obj.dup_stit = Convert.ToInt32(dr["dup_stit"]);
                        obj.dup_situac = dr["s_descri"].ToString().Trim();
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

        public static List<CL_Duprec> buscaReciboImprimir(List<CL_Duprec> objListDuprec, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT dup_emis, dup_nota, dup_vcto, dup_vlpgto FROM duprec WHERE ";
            CL_Duprec obj = null;
            for(int x =0; x < objListDuprec.Count; x++)
            {
                obj = new CL_Duprec();
                obj = objListDuprec.ElementAt(x);
                sql += "dup_cod=" + obj.dup_cod + " AND dup_parc=" + obj.dup_parc;
                if (x < objListDuprec.Count - 1)
                    sql = " OR";
            }
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            List<CL_Duprec> objList = new List<CL_Duprec>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Duprec()
                        {
                            dup_nota = Convert.ToInt32(dr["dup_nota"]),
                            dup_emis = Convert.ToDateTime(dr["dup_emis"]),
                            dup_vcto = Convert.ToDateTime(dr["dup_vcto"]),
                            dup_vlpgto = Convert.ToDouble(dr["dup_vlpgto"]),
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

        public static List<CL_Duprec> buscaDupCliente(int p_clicod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM duprec WHERE dup_codcli=@dup_codcli AND dup_pgto IS NULL";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dup_codcli", p_clicod);
            NpgsqlDataReader dr;
            List<CL_Duprec> objList = new List<CL_Duprec>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Duprec()
                        {
                            dup_cod = Convert.ToInt32(dr["dup_cod"]),
                            dup_parc = Convert.ToInt32(dr["dup_parc"]),
                            dup_nota = Convert.ToInt32(dr["dup_nota"]),
                            dup_emis = Convert.ToDateTime(dr["dup_emis"]),
                            dup_vcto = Convert.ToDateTime(dr["dup_vcto"]),
                            dup_valor = Convert.ToDouble(dr["dup_valor"]),
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

        public static CL_Duprec buscaTitulo(int dupcod, int dupparc, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_Duprec objDuprec = new CL_Duprec();

            string sql = "SELECT * FROM duprec WHERE dup_cod=@dup_cod AND dup_parc=@dup_parc";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dup_cod", dupcod);
            comand.Parameters.AddWithValue("dup_parc", dupparc);
            NpgsqlDataReader dr;
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objDuprec.dup_cod = dupcod;
                        objDuprec.dup_parc = dupparc;
                        objDuprec.dup_codcli = dr["dup_codcli"] is DBNull ? 0 : Convert.ToInt32(dr["dup_codcli"]);
                        objDuprec.dup_nome = dr["dup_nome"].ToString().Trim();
                        objDuprec.dup_bco = dr["dup_bco"] is DBNull ? 0 : Convert.ToInt32(dr["dup_bco"]);
                        objDuprec.dup_vend = dr["dup_vend"] is DBNull ? 0 : Convert.ToInt32(dr["dup_vend"]);
                        objDuprec.dup_nomven = dr["dup_nomven"].ToString().Trim();
                        objDuprec.dup_comis = dr["dup_comis"] is DBNull ? 0 : Convert.ToDouble(dr["dup_comis"]);
                        objDuprec.dup_emis = Convert.ToDateTime(dr["dup_emis"]);
                        objDuprec.dup_vcto = Convert.ToDateTime(dr["dup_vcto"]);
                        objDuprec.dup_vlnota = dr["dup_vlnota"] is DBNull ? 0 : Convert.ToDouble(dr["dup_vlnota"]);
                        objDuprec.dup_valor = dr["dup_valor"] is DBNull ? 0 : Convert.ToDouble(dr["dup_valor"]);
                        objDuprec.dup_nota = dr["dup_nota"] is DBNull ? 0 : Convert.ToInt32(dr["dup_nota"]);
                        objDuprec.dup_nrbco = dr["dup_nrbco"].ToString().Trim();
                        objDuprec.dup_vcomis = dr["dup_vcomis"] is DBNull ? 0 : Convert.ToDouble(dr["dup_vcomis"]);
                        objDuprec.dup_stit = dr["dup_stit"] is DBNull ? 0 : Convert.ToInt32(dr["dup_stit"]);
                        return objDuprec;
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

        public static bool recebeDupCliente(List<CL_Duprec> objListDuprec, List<CL_Movduprec> objListMovDup, List<CL_Movduprec> objListMovJuros, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();

            try
            {
                NpgsqlCommand comand = null;
                string sql = "UPDATE duprec SET dup_pgto=@dup_pgto, dup_vlpgto=@dup_vlpgto, dup_juros=dup_juros+@dup_juros, dup_vlrrec=dup_vlrrec+@dup_saldo, dup_diapgt=@diapgt WHERE dup_cod=@dup_cod AND dup_parc=@dup_parc";
                foreach (CL_Duprec obj in objListDuprec)
                {
                    if (obj.dup_receber)
                    {
                        comand = new NpgsqlCommand(sql, Conn, transaction);
                        comand.Parameters.AddWithValue("dup_pgto", obj.dup_pgto);
                        comand.Parameters.AddWithValue("dup_vlpgto", obj.dup_valor);
                        comand.Parameters.AddWithValue("dup_cod", obj.dup_cod);
                        comand.Parameters.AddWithValue("dup_parc", obj.dup_parc);
                        comand.Parameters.AddWithValue("dup_juros", obj.dup_juros);
                        comand.Parameters.AddWithValue("dup_saldo", obj.dup_saldo);
                        comand.Parameters.AddWithValue("diapgt", DateTime.Now.ToShortDateString());
                        comand.ExecuteScalar();
                    }
                }
                foreach(var obj in objListMovDup)
                {
                    sql = "INSERT INTO movduprec (mr_duplic, mr_parc, mr_data, mr_tipo, mr_codcli, mr_cliente, mr_bco, mr_stit, mr_vend, mr_nomeven, mr_comis, " +
                      "mr_valor, mr_hist1, mr_acumul) VALUES (@mr_duplic, @mr_parc, @mr_data, @mr_tipo, @mr_codcli, @mr_cliente, @mr_bco, @mr_stit, @mr_vend, " +
                      "@mr_nomeven, @mr_comis, @mr_valor, @mr_hist1, @mr_acumul)";
                    comand = new NpgsqlCommand(sql, Conn, transaction);
                    comand.Parameters.AddWithValue("mr_duplic", obj.mr_duplic);
                    comand.Parameters.AddWithValue("mr_parc", obj.mr_parc);
                    comand.Parameters.AddWithValue("mr_data", obj.mr_data);
                    comand.Parameters.AddWithValue("mr_tipo", obj.mr_tipo);
                    comand.Parameters.AddWithValue("mr_codcli", obj.mr_codcli);
                    comand.Parameters.AddWithValue("mr_cliente", obj.mr_cliente);
                    comand.Parameters.AddWithValue("mr_bco", obj.mr_bco);
                    comand.Parameters.AddWithValue("mr_stit", obj.mr_stit);
                    comand.Parameters.AddWithValue("mr_vend", obj.mr_vend);
                    comand.Parameters.AddWithValue("mr_nomeven", obj.mr_nomeven);
                    comand.Parameters.AddWithValue("mr_comis", obj.mr_comis);
                    comand.Parameters.AddWithValue("mr_valor", obj.mr_valor);
                    comand.Parameters.AddWithValue("mr_hist1", obj.mr_hist1);
                    comand.Parameters.AddWithValue("mr_acumul", obj.mr_acumul);
                    comand.ExecuteScalar();
                }
                foreach (var obj in objListMovJuros)
                {
                    sql = "INSERT INTO movduprec (mr_duplic, mr_parc, mr_data, mr_tipo, mr_codcli, mr_cliente, mr_bco, mr_stit, mr_vend, mr_nomeven, mr_comis, " +
                      "mr_valor, mr_hist1, mr_acumul) VALUES (@mr_duplic, @mr_parc, @mr_data, @mr_tipo, @mr_codcli, @mr_cliente, @mr_bco, @mr_stit, @mr_vend, " +
                      "@mr_nomeven, @mr_comis, @mr_valor, @mr_hist1, @mr_acumul)";
                    comand = new NpgsqlCommand(sql, Conn, transaction);
                    comand.Parameters.AddWithValue("mr_duplic", obj.mr_duplic);
                    comand.Parameters.AddWithValue("mr_parc", obj.mr_parc);
                    comand.Parameters.AddWithValue("mr_data", obj.mr_data);
                    comand.Parameters.AddWithValue("mr_tipo", obj.mr_tipo);
                    comand.Parameters.AddWithValue("mr_codcli", obj.mr_codcli);
                    comand.Parameters.AddWithValue("mr_cliente", obj.mr_cliente);
                    comand.Parameters.AddWithValue("mr_bco", obj.mr_bco);
                    comand.Parameters.AddWithValue("mr_stit", obj.mr_stit);
                    comand.Parameters.AddWithValue("mr_vend", obj.mr_vend);
                    comand.Parameters.AddWithValue("mr_nomeven", obj.mr_nomeven);
                    comand.Parameters.AddWithValue("mr_comis", obj.mr_comis);
                    comand.Parameters.AddWithValue("mr_valor", obj.mr_valor);
                    comand.Parameters.AddWithValue("mr_hist1", obj.mr_hist1);
                    comand.Parameters.AddWithValue("mr_acumul", obj.mr_acumul);
                    comand.ExecuteScalar();
                }
                //transaction.Commit();
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

        public static List<CL_Duprec> listaPendencias(DateTime data, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT dup_cod, dup_parc, dup_nome, dup_nota, dup_emis, dup_vcto, dup_valor "+
                         "FROM duprec WHERE dup_pgto IS NULL AND dup_vcto<=@data ORDER BY dup_cod";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("data", data.ToShortDateString());
            NpgsqlDataReader dr;
            List<CL_Duprec> objList = new List<CL_Duprec>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Duprec()
                        {
                            dup_cod = Convert.ToInt32(dr["dup_cod"]),
                            dup_parc = Convert.ToInt32(dr["dup_parc"]),
                            dup_nome = dr["dup_nome"].ToString().Trim(),
                            dup_nota = Convert.ToInt32(dr["dup_nota"]),
                            dup_emis = Convert.ToDateTime(dr["dup_emis"]),
                            dup_vcto = Convert.ToDateTime(dr["dup_vcto"]),
                            dup_valor = Convert.ToDouble(dr["dup_valor"]),
                            dup_receber = false,
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

        public static List<CL_Duprec> listaDuprec(string situac, int p_clicod, DateTime dataI, DateTime dataF, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "";
            if (situac == "N")
                if (dataI.ToShortDateString() == "01/01/0001")
                    sql = "SELECT dup_cod, dup_parc, dup_codcli, dup_nome, dup_nota, dup_vend, dup_nomven, dup_comis, " +
                      "dup_emis, dup_vcto, dup_pgto, dup_valor, dup_vlpgto " +
                      "FROM duprec WHERE dup_pgto IS NULL AND dup_codcli=@p_clicod ORDER BY dup_cod, dup_parc";
                else
                    sql = "SELECT dup_cod, dup_parc, dup_codcli, dup_nome, dup_nota, dup_vend, dup_nomven, dup_comis, " +
                      "dup_emis, dup_vcto, dup_pgto, dup_valor, dup_vlpgto " +
                      "FROM duprec WHERE dup_pgto IS NULL AND dup_codcli=@p_clicod " +
                      "AND dup_emis >= @dataI AND dup_emis <= @dataF ORDER BY dup_cod, dup_parc";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_clicod", p_clicod);
            comand.Parameters.AddWithValue("dataI", dataI.ToShortDateString());
            comand.Parameters.AddWithValue("dataF", dataF.ToShortDateString());
            NpgsqlDataReader dr;
            List<CL_Duprec> objList = new List<CL_Duprec>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Duprec()
                        {
                            dup_cod = Convert.ToInt32(dr["dup_cod"]),
                            dup_parc = Convert.ToInt32(dr["dup_parc"]),
                            dup_codcli = Convert.ToInt32(dr["dup_codcli"]),
                            dup_nome = dr["dup_nome"].ToString().Trim(),
                            dup_nota = Convert.ToInt32(dr["dup_nota"]),
                            dup_vend = Convert.ToInt32(dr["dup_vend"]),
                            dup_nomven = dr["dup_nomven"].ToString().Trim(),
                            dup_comis = Convert.ToDouble(dr["dup_comis"]),
                            dup_emis = Convert.ToDateTime(dr["dup_emis"]),
                            dup_vcto = Convert.ToDateTime(dr["dup_vcto"]),
                            dup_pgto = dr["dup_pgto"].ToString().Trim(),
                            dup_valor = Convert.ToDouble(dr["dup_valor"]),
                            dup_vlpgto = Convert.ToDouble(dr["dup_vlpgto"]),
                            dup_saldo = Convert.ToDouble(dr["dup_valor"]) - Convert.ToDouble(dr["dup_vlpgto"]),
                            dup_receber = false,
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

        public static List<CL_Duprec> buscaDuprec(int p_id, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            string sql = "SELECT * FROM duprec WHERE dup_nota=@p_id";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("p_id", p_id);
            NpgsqlDataReader dr;
            List<CL_Duprec> objList = new List<CL_Duprec>();
            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objList.Add(new CL_Duprec()
                        {
                            dup_cod = Convert.ToInt32(dr["dup_cod"]),
                            dup_codcli = Convert.ToInt32(dr["dup_codcli"]),
                            dup_comis = dr["dup_cod"] is DBNull ? 0 : Convert.ToDouble(dr["dup_comis"]),
                            dup_emis = Convert.ToDateTime(dr["dup_emis"]),
                            dup_nome = dr["dup_nome"].ToString().Trim(),
                            dup_nomven = dr["dup_nomven"].ToString().Trim(),
                            dup_nota = p_id,
                            dup_parc = dr["dup_parc"] is DBNull ? 0 : Convert.ToInt32(dr["dup_parc"]),
                            dup_valor = dr["dup_valor"] is DBNull ? 0 : Convert.ToDouble(dr["dup_valor"]),
                            dup_vcto =  Convert.ToDateTime(dr["dup_vcto"]),
                            dup_vend = dr["dup_vend"] is DBNull ? 0 : Convert.ToInt32(dr["dup_vend"]),
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

        public static bool alteraDuprec(CL_Duprec objDuprec, string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();

            try
            {
                string sql = "UPDATE duprec SET dup_codcli=@dup_codcli, dup_nome=@dup_nome, dup_bco=@dup_bco, dup_vend=@dup_vend, dup_nomven=@dup_nomven," +
                             "dup_comis=@dup_comis, dup_emis=@dup_emis, dup_vcto=@dup_vcto, dup_vlnota=@dup_vlnota, dup_valor=@dup_valor, dup_nota=@dup_nota," +
                             "dup_nrbco=@dup_nrbco, dup_vcomis=@dup_vcomis, dup_stit=@dup_stit, dup_pgto=@dup_pgto WHERE dup_cod=@dup_cod and dup_parc=@dup_parc";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("dup_codcli", objDuprec.dup_codcli);
                comand.Parameters.AddWithValue("dup_nome", objDuprec.dup_nome);
                comand.Parameters.AddWithValue("dup_bco", objDuprec.dup_bco);
                comand.Parameters.AddWithValue("dup_vend", objDuprec.dup_vend);
                comand.Parameters.AddWithValue("dup_nomven", objDuprec.dup_nomven);
                comand.Parameters.AddWithValue("dup_comis", objDuprec.dup_comis);
                comand.Parameters.AddWithValue("dup_emis", objDuprec.dup_emis.ToShortDateString());
                comand.Parameters.AddWithValue("dup_vcto", objDuprec.dup_vcto.ToShortDateString());
                comand.Parameters.AddWithValue("dup_vlnota", objDuprec.dup_vlnota);
                comand.Parameters.AddWithValue("dup_valor", objDuprec.dup_valor);
                comand.Parameters.AddWithValue("dup_nota", objDuprec.dup_nota);
                comand.Parameters.AddWithValue("dup_nrbco", objDuprec.dup_nrbco);
                comand.Parameters.AddWithValue("dup_vcomis", objDuprec.dup_vcomis);
                comand.Parameters.AddWithValue("dup_stit", objDuprec.dup_stit);
                comand.Parameters.AddWithValue("dup_pgto", objDuprec.dup_pgto);
                comand.Parameters.AddWithValue("dup_cod", objDuprec.dup_cod);
                comand.Parameters.AddWithValue("dup_parc", objDuprec.dup_parc);
                comand.ExecuteScalar();

                sql = "UPDATE movduprec SET mr_data=@mr_data, mr_bco=@mr_bco, mr_stit=@mr_stit, mr_vend=@mr_vend, mr_nomeven=@mr_nomeven, " +
                      "mr_comis=@mr_comis, mr_valor=@mr_valor where mr_duplic=@mr_duplic and mr_parc=@mr_parc and mr_codcli=@mr_codcli and mr_tipo=1";
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("mr_data", objDuprec.dup_emis);
                comand.Parameters.AddWithValue("mr_bco", objDuprec.dup_bco);
                comand.Parameters.AddWithValue("mr_stit", objDuprec.dup_stit);
                comand.Parameters.AddWithValue("mr_vend", objDuprec.dup_vend);
                comand.Parameters.AddWithValue("mr_nomeven", objDuprec.dup_nomven);
                comand.Parameters.AddWithValue("mr_comis", objDuprec.dup_comis);
                comand.Parameters.AddWithValue("mr_valor", objDuprec.dup_valor);
                comand.Parameters.AddWithValue("mr_duplic", objDuprec.dup_cod);
                comand.Parameters.AddWithValue("mr_parc", objDuprec.dup_parc);
                comand.Parameters.AddWithValue("mr_codcli", objDuprec.dup_codcli);
                comand.ExecuteScalar();

                sql = "INSERT INTO hist_usuario (h_usuario, h_tabela, h_hist1, h_hist2) VALUES (@email, 'duprec, movduprec', 'ALTERACAO DO TITULO "+objDuprec.dup_cod+" - "+objDuprec.dup_parc+"', '')";
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("email", email);
                comand.ExecuteScalar();
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

        public static bool excluiDuprec(CL_Duprec objDuprec, string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();


            try
            {
                string sql = "DELETE FROM duprec WHERE dup_cod=" + objDuprec.dup_cod + " and dup_parc= " + objDuprec.dup_parc;
                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.ExecuteScalar();

                sql = "DELETE FROM movduprec where mr_duplic=" + objDuprec.dup_cod + " and mr_parc= " + objDuprec.dup_parc + " and mr_codcli= " + objDuprec.dup_codcli;
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.ExecuteScalar();

                sql = "INSERT INTO hist_usuario (h_usuario, h_tabela, h_hist1, h_hist2) VALUES (@email, 'duprec, movduprec', 'EXCLUSAO DO TITULO " + objDuprec.dup_cod + " - " + objDuprec.dup_parc + "', '')";
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("email", email);
                comand.ExecuteScalar();

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
                {
                    Conn.Close();
                }
            }
        }

        public static bool cadDuprec(CL_Duprec objDuprec, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            Conn.Open();
            NpgsqlTransaction transaction = Conn.BeginTransaction();

            try
            {
                string sql = "INSERT INTO duprec (dup_cod, dup_parc, dup_codcli, dup_nome, dup_bco, dup_vend, dup_nomven, dup_comis, dup_emis, dup_vcto, " +
                "dup_vlnota, dup_valor, dup_nota, dup_nrbco, dup_vcomis, dup_stit) VALUES (@dup_cod, @dup_parc, @dup_codcli, @dup_nome, @dup_bco, " +
                "@dup_vend, @dup_nomven, @dup_comis, @dup_emis, @dup_vcto, @dup_vlnota, @dup_valor, @dup_nota, @dup_nrbco, @dup_vcomis, @dup_stit)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("dup_cod", objDuprec.dup_cod);
                comand.Parameters.AddWithValue("dup_parc", objDuprec.dup_parc);
                comand.Parameters.AddWithValue("dup_codcli", objDuprec.dup_codcli);
                comand.Parameters.AddWithValue("dup_nome", objDuprec.dup_nome);
                comand.Parameters.AddWithValue("dup_bco", objDuprec.dup_bco);
                comand.Parameters.AddWithValue("dup_vend", objDuprec.dup_vend);
                comand.Parameters.AddWithValue("dup_nomven", objDuprec.dup_nomven);
                comand.Parameters.AddWithValue("dup_comis", objDuprec.dup_comis);
                comand.Parameters.AddWithValue("dup_emis", objDuprec.dup_emis);
                comand.Parameters.AddWithValue("dup_vcto", objDuprec.dup_vcto);
                comand.Parameters.AddWithValue("dup_vlnota", objDuprec.dup_vlnota);
                comand.Parameters.AddWithValue("dup_valor", objDuprec.dup_valor);
                comand.Parameters.AddWithValue("dup_nota", objDuprec.dup_nota);
                comand.Parameters.AddWithValue("dup_nrbco", objDuprec.dup_nrbco);
                comand.Parameters.AddWithValue("dup_vcomis", objDuprec.dup_vcomis);
                comand.Parameters.AddWithValue("dup_stit", objDuprec.dup_stit);
                comand.ExecuteScalar();

                sql = "INSERT INTO movduprec (mr_duplic, mr_parc, mr_data, mr_tipo, mr_codcli, mr_cliente, mr_bco, mr_stit, mr_vend, mr_nomeven, mr_comis, " +
                      "mr_valor, mr_hist1, mr_acumul) VALUES (@mr_duplic, @mr_parc, @mr_data, @mr_tipo, @mr_codcli, @mr_cliente, @mr_bco, @mr_stit, @mr_vend, " +
                      "@mr_nomeven, @mr_comis, @mr_valor, @mr_hist1, @mr_acumul)";
                comand = new NpgsqlCommand(sql, Conn, transaction);
                comand.Parameters.AddWithValue("mr_duplic", objDuprec.dup_cod);
                comand.Parameters.AddWithValue("mr_parc", objDuprec.dup_parc);
                comand.Parameters.AddWithValue("mr_data", objDuprec.dup_emis);
                comand.Parameters.AddWithValue("mr_tipo", 1);
                comand.Parameters.AddWithValue("mr_codcli", objDuprec.dup_codcli);
                comand.Parameters.AddWithValue("mr_cliente", objDuprec.dup_nome);
                comand.Parameters.AddWithValue("mr_bco", objDuprec.dup_bco);
                comand.Parameters.AddWithValue("mr_stit", objDuprec.dup_stit);
                comand.Parameters.AddWithValue("mr_vend", objDuprec.dup_vend);
                comand.Parameters.AddWithValue("mr_nomeven", objDuprec.dup_nomven);
                comand.Parameters.AddWithValue("mr_comis", objDuprec.dup_comis);
                comand.Parameters.AddWithValue("mr_valor", objDuprec.dup_valor);
                comand.Parameters.AddWithValue("mr_hist1", "Valor do titulo");
                comand.Parameters.AddWithValue("mr_acumul", "D");
                comand.ExecuteScalar();

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
                {
                    Conn.Close();
                }
            }
        }

        public static bool confereTitulo(int dupcod, int dupparc, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT dup_cod FROM duprec where dup_cod=@dupcod and dup_parc=@dupparc";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("dupcod", dupcod);
            comand.Parameters.AddWithValue("dupparc", dupparc);
            NpgsqlDataReader dr;

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
                    return false;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return true;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

        }
    }
}