using Npgsql;
using System;
using System.Data;
using CLASSES;

namespace BANCO
{
    public class DB_AvaliacaoVeiculo : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static bool confereAvaliacao(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT a_id FROM avaliacao_veiculo WHERE a_idapp=@idUmov";

            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            comand.Parameters.AddWithValue("idUmov", idUmov);
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

        public static bool cadAvaliacao(CL_AvaliacaoVeiculo objAvaliacao, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO avaliacao_veiculo (a_id, a_idapp, a_tecnico, a_data, a_cliente, a_clinome, a_cliende, a_clicida, a_marca, a_modelo, a_serie, a_chassi, a_anomod, a_horasuso, " +
                    "a_tipoveic, a_tipotrac, a_motor, a_ftmotor, a_hidrau, a_fthidrau, a_terpto, a_tomfor, a_transm, a_embreag, a_freio, a_tracao, a_eixodian, a_sisdir, a_obsmec, a_alterna, " +
                    "a_arranq, a_bateria, a_farois, a_painel, a_ftpainel, a_obseletr, a_lata, a_pintura, a_ftpintu, a_obslata, a_arcond, a_cabine, a_plm, a_obsacess, a_ftacess, a_pneumedd, a_pneumard, " + 
                    "a_pneudd, a_ftpneudd, a_pneude, a_ftpneude, a_pneumedt, a_pneumart, a_pneutd, a_ftpneutd, a_pneute, a_ftpneute, a_obspneu, a_plataf, a_ftplataf, a_sislimp, a_sisdebu, " +
                    "a_sissepar, a_armazen, a_descarga, a_obscolhe, a_vlrreceb, a_ftadc1, a_ftadc2, a_ftadc3, a_ftadc4)" +
                    "VALUES (@a_id, @a_idapp, @a_tecnico, @a_data, @a_cliente, @a_clinome, @a_cliende, @a_clicida, @a_marca, @a_modelo, @a_serie, @a_chassi, @a_anomod, @a_horasuso, " +
                    "@a_tipoveic, @a_tipotrac, @a_motor, @a_ftmotor, @a_hidrau, @a_fthidrau, @a_terpto, @a_tomfor, @a_transm, @a_embreag, @a_freio, @a_tracao, @a_eixodian, @a_sisdir, @a_obsmec, @a_alterna, " +
                    "@a_arranq, @a_bateria, @a_farois, @a_painel, @a_ftpainel, @a_obseletr, @a_lata, @a_pintura, @a_ftpintu, @a_obslata, @a_arcond, @a_cabine, @a_plm, @a_obsacess, @a_ftacess, @a_pneumedd, @a_pneumard, " +
                    "@a_pneudd, @a_ftpneudd, @a_pneude, @a_ftpneude, @a_pneumedt, @a_pneumart, @a_pneutd, @a_ftpneutd, @a_pneute, @a_ftpneute, @a_obspneu, @a_plataf, @a_ftplataf, @a_sislimp, @a_sisdebu, " +
                    "@a_sissepar, @a_armazen, @a_descarga, @a_obscolhe, @a_vlrreceb, @a_ftadc1, @a_ftadc2, @a_ftadc3, @a_ftadc4)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("a_id", objAvaliacao.a_id);
                comand.Parameters.AddWithValue("a_idapp", objAvaliacao.a_idapp);
                comand.Parameters.AddWithValue("a_tecnico", objAvaliacao.a_tecnico);
                comand.Parameters.AddWithValue("a_data", objAvaliacao.a_data.ToShortDateString());
                comand.Parameters.AddWithValue("a_cliente", objAvaliacao.a_cliente);
                comand.Parameters.AddWithValue("a_clinome", objAvaliacao.a_clinome);
                comand.Parameters.AddWithValue("a_cliende", objAvaliacao.a_cliende);
                comand.Parameters.AddWithValue("a_clicida", objAvaliacao.a_clicida);
                comand.Parameters.AddWithValue("a_marca", objAvaliacao.a_marca);
                comand.Parameters.AddWithValue("a_modelo", objAvaliacao.a_modelo);
                comand.Parameters.AddWithValue("a_serie", objAvaliacao.a_serie);
                comand.Parameters.AddWithValue("a_chassi", objAvaliacao.a_chassi);
                comand.Parameters.AddWithValue("a_anomod", objAvaliacao.a_anomod);
                comand.Parameters.AddWithValue("a_horasuso", objAvaliacao.a_horasuso);
                comand.Parameters.AddWithValue("a_tipoveic", objAvaliacao.a_tipoveic);
                comand.Parameters.AddWithValue("a_tipotrac", objAvaliacao.a_tipotrac);
                comand.Parameters.AddWithValue("a_motor", objAvaliacao.a_motor);
                comand.Parameters.AddWithValue("a_ftmotor", objAvaliacao.a_ftmotor);
                comand.Parameters.AddWithValue("a_hidrau", objAvaliacao.a_hidrau);
                comand.Parameters.AddWithValue("a_fthidrau", objAvaliacao.a_fthidrau);
                comand.Parameters.AddWithValue("a_terpto", objAvaliacao.a_terpto);
                comand.Parameters.AddWithValue("a_tomfor", objAvaliacao.a_tomfor);
                comand.Parameters.AddWithValue("a_transm", objAvaliacao.a_transm);
                comand.Parameters.AddWithValue("a_embreag", objAvaliacao.a_embreag);
                comand.Parameters.AddWithValue("a_freio", objAvaliacao.a_freio);
                comand.Parameters.AddWithValue("a_tracao", objAvaliacao.a_tracao);
                comand.Parameters.AddWithValue("a_eixodian", objAvaliacao.a_eixodian);
                comand.Parameters.AddWithValue("a_sisdir", objAvaliacao.a_sisdir);
                comand.Parameters.AddWithValue("a_obsmec", objAvaliacao.a_obsmec);
                comand.Parameters.AddWithValue("a_alterna", objAvaliacao.a_alterna);
                comand.Parameters.AddWithValue("a_arranq", objAvaliacao.a_arranq);
                comand.Parameters.AddWithValue("a_bateria", objAvaliacao.a_bateria);
                comand.Parameters.AddWithValue("a_farois", objAvaliacao.a_farois);
                comand.Parameters.AddWithValue("a_painel", objAvaliacao.a_painel);
                comand.Parameters.AddWithValue("a_ftpainel", objAvaliacao.a_ftpainel);
                comand.Parameters.AddWithValue("a_obseletr", objAvaliacao.a_obseletr);
                comand.Parameters.AddWithValue("a_lata", objAvaliacao.a_lata);
                comand.Parameters.AddWithValue("a_pintura", objAvaliacao.a_pintura);
                comand.Parameters.AddWithValue("a_ftpintu", objAvaliacao.a_ftpintu);
                comand.Parameters.AddWithValue("a_obslata", objAvaliacao.a_obslata);
                comand.Parameters.AddWithValue("a_arcond", objAvaliacao.a_arcond);
                comand.Parameters.AddWithValue("a_cabine", objAvaliacao.a_cabine);
                comand.Parameters.AddWithValue("a_plm", objAvaliacao.a_plm);
                comand.Parameters.AddWithValue("a_obsacess", objAvaliacao.a_obsAcess);
                comand.Parameters.AddWithValue("a_ftacess", objAvaliacao.a_ftacess);
                comand.Parameters.AddWithValue("a_pneumedd", objAvaliacao.a_pneumedd);
                comand.Parameters.AddWithValue("a_pneumard", objAvaliacao.a_pneumard);
                comand.Parameters.AddWithValue("a_pneudd", objAvaliacao.a_pneudd);
                comand.Parameters.AddWithValue("a_ftpneudd", objAvaliacao.a_ftpneudd);
                comand.Parameters.AddWithValue("a_pneude", objAvaliacao.a_pneude);
                comand.Parameters.AddWithValue("a_ftpneude", objAvaliacao.a_ftpneude);
                comand.Parameters.AddWithValue("a_pneumedt", objAvaliacao.a_pneumedt);
                comand.Parameters.AddWithValue("a_pneumart", objAvaliacao.a_pneumart);
                comand.Parameters.AddWithValue("a_pneutd", objAvaliacao.a_pneutd);
                comand.Parameters.AddWithValue("a_ftpneutd", objAvaliacao.a_ftpneutd);
                comand.Parameters.AddWithValue("a_pneute", objAvaliacao.a_pneute);
                comand.Parameters.AddWithValue("a_ftpneute", objAvaliacao.a_ftpneute);
                comand.Parameters.AddWithValue("a_obspneu", objAvaliacao.a_obspneu);
                comand.Parameters.AddWithValue("a_plataf", objAvaliacao.a_plataf);
                comand.Parameters.AddWithValue("a_ftplataf", objAvaliacao.a_ftplataf);
                comand.Parameters.AddWithValue("a_sislimp", objAvaliacao.a_sislimp);
                comand.Parameters.AddWithValue("a_sisdebu", objAvaliacao.a_sisdebu);
                comand.Parameters.AddWithValue("a_sissepar", objAvaliacao.a_sissepar);
                comand.Parameters.AddWithValue("a_armazen", objAvaliacao.a_armazen);
                comand.Parameters.AddWithValue("a_descarga", objAvaliacao.a_descarga);
                comand.Parameters.AddWithValue("a_obscolhe", objAvaliacao.a_obscolhe);
                comand.Parameters.AddWithValue("a_vlrreceb", objAvaliacao.a_vlrreceb);
                comand.Parameters.AddWithValue("a_ftadc1", objAvaliacao.a_ftadc1);
                comand.Parameters.AddWithValue("a_ftadc2", objAvaliacao.a_ftadc2);
                comand.Parameters.AddWithValue("a_ftadc3", objAvaliacao.a_ftadc3);
                comand.Parameters.AddWithValue("a_ftadc4", objAvaliacao.a_ftadc4);

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

        public static CL_AvaliacaoVeiculo buscaAvaliacaoIDUmov(int idUmov, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            CL_AvaliacaoVeiculo objAvaliacao = new CL_AvaliacaoVeiculo();

            string sql = "SELECT * FROM avaliacao_veiculo WHERE a_idapp=" + idUmov;

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
                        objAvaliacao.a_id = dr["a_id"] is DBNull ? 0 : Convert.ToInt32(dr["a_id"]);
                        objAvaliacao.a_idapp = idUmov;
                        objAvaliacao.a_data = dr["a_data"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["a_data"]);
                        objAvaliacao.a_cliente = dr["a_cliente"] is DBNull ? 0 : Convert.ToInt32(dr["a_cliente"]);
                        objAvaliacao.a_tecnico = dr["a_tecnico"] is DBNull ? 0 : Convert.ToInt32(dr["a_tecnico"]);
                        objAvaliacao.a_clinome = dr["a_clinome"].ToString().Trim();
                        objAvaliacao.a_cliende = dr["a_cliende"].ToString().Trim();
                        objAvaliacao.a_clicida = dr["a_clicida"].ToString().Trim();
                        objAvaliacao.a_marca = dr["a_marca"].ToString().Trim();
                        objAvaliacao.a_modelo = dr["a_modelo"].ToString().Trim();
                        objAvaliacao.a_serie = dr["a_serie"].ToString().Trim();
                        objAvaliacao.a_chassi = dr["a_chassi"].ToString().Trim();
                        objAvaliacao.a_anomod = dr["a_anomod"].ToString().Trim();
                        objAvaliacao.a_horasuso = dr["a_horasuso"].ToString().Trim();
                        objAvaliacao.a_tipoveic = dr["a_tipoveic"].ToString().Trim();
                        objAvaliacao.a_tipotrac = dr["a_tipotrac"].ToString().Trim();
                        objAvaliacao.a_motor = Convert.ToInt32(dr["a_motor"]);
                        objAvaliacao.a_ftmotor = dr["a_ftmotor"].ToString().Trim();
                        objAvaliacao.a_hidrau = Convert.ToInt32(dr["a_hidrau"]);
                        objAvaliacao.a_fthidrau = dr["a_fthidrau"].ToString().Trim();
                        objAvaliacao.a_terpto = Convert.ToInt32(dr["a_terpto"]);
                        objAvaliacao.a_tomfor = Convert.ToInt32(dr["a_tomfor"]);
                        objAvaliacao.a_transm = Convert.ToInt32(dr["a_transm"]);
                        objAvaliacao.a_embreag = Convert.ToInt32(dr["a_embreag"]);
                        objAvaliacao.a_freio = Convert.ToInt32(dr["a_freio"]);
                        objAvaliacao.a_tracao = Convert.ToInt32(dr["a_tracao"]);
                        objAvaliacao.a_eixodian = Convert.ToInt32(dr["a_eixodian"]);
                        objAvaliacao.a_sisdir = Convert.ToInt32(dr["a_sisdir"]);
                        objAvaliacao.a_obsmec = dr["a_obsmec"].ToString().Trim();
                        objAvaliacao.a_alterna = Convert.ToInt32(dr["a_alterna"]);
                        objAvaliacao.a_arranq = Convert.ToInt32(dr["a_arranq"]);
                        objAvaliacao.a_bateria = Convert.ToInt32(dr["a_bateria"]);
                        objAvaliacao.a_farois = Convert.ToInt32(dr["a_farois"]);
                        objAvaliacao.a_painel = Convert.ToInt32(dr["a_painel"]);
                        objAvaliacao.a_ftpainel = dr["a_ftpainel"].ToString().Trim();
                        objAvaliacao.a_obseletr = dr["a_obseletr"].ToString().Trim();
                        objAvaliacao.a_lata = Convert.ToInt32(dr["a_lata"]);
                        objAvaliacao.a_pintura = Convert.ToInt32(dr["a_pintura"]);
                        objAvaliacao.a_ftpintu = dr["a_ftpintu"].ToString().Trim();
                        objAvaliacao.a_obslata = dr["a_obslata"].ToString().Trim();
                        objAvaliacao.a_arcond = Convert.ToInt32(dr["a_arcond"]);
                        objAvaliacao.a_cabine = Convert.ToInt32(dr["a_cabine"]);
                        objAvaliacao.a_plm = Convert.ToInt32(dr["a_plm"]);
                        objAvaliacao.a_obsAcess = dr["a_obsacess"].ToString().Trim();
                        objAvaliacao.a_ftacess = dr["a_ftacess"].ToString().Trim();
                        objAvaliacao.a_pneumedd = dr["a_pneumedd"].ToString().Trim();
                        objAvaliacao.a_pneumard = dr["a_pneumard"].ToString().Trim();
                        objAvaliacao.a_pneudd = Convert.ToInt32(dr["a_pneudd"]);
                        objAvaliacao.a_ftpneudd = dr["a_ftpneudd"].ToString().Trim();
                        objAvaliacao.a_pneude = Convert.ToInt32(dr["a_pneude"]);
                        objAvaliacao.a_ftpneude = dr["a_ftpneude"].ToString().Trim();
                        objAvaliacao.a_pneumedt = dr["a_pneumedt"].ToString().Trim();
                        objAvaliacao.a_pneumart = dr["a_pneumart"].ToString().Trim();
                        objAvaliacao.a_pneutd = Convert.ToInt32(dr["a_pneutd"]);
                        objAvaliacao.a_ftpneutd = dr["a_ftpneutd"].ToString().Trim();
                        objAvaliacao.a_pneute = Convert.ToInt32(dr["a_pneute"]);
                        objAvaliacao.a_ftpneute = dr["a_ftpneute"].ToString().Trim();
                        objAvaliacao.a_obspneu = dr["a_obspneu"].ToString().Trim();
                        objAvaliacao.a_plataf = Convert.ToInt32(dr["a_plataf"]);
                        objAvaliacao.a_ftplataf = dr["a_ftplataf"].ToString().Trim();
                        objAvaliacao.a_sislimp = Convert.ToInt32(dr["a_sislimp"]);
                        objAvaliacao.a_sisdebu = Convert.ToInt32(dr["a_sisdebu"]);
                        objAvaliacao.a_sissepar = Convert.ToInt32(dr["a_sissepar"]);
                        objAvaliacao.a_armazen = Convert.ToInt32(dr["a_armazen"]);
                        objAvaliacao.a_descarga = Convert.ToInt32(dr["a_descarga"]);
                        objAvaliacao.a_obscolhe = dr["a_obscolhe"].ToString().Trim();
                        objAvaliacao.a_vlrreceb = Convert.ToDouble(dr["a_vlrreceb"]);
                        objAvaliacao.a_ftadc1 = dr["a_ftadc1"].ToString().Trim();
                        objAvaliacao.a_ftadc2 = dr["a_ftadc2"].ToString().Trim();
                        objAvaliacao.a_ftadc3 = dr["a_ftadc3"].ToString().Trim();
                        objAvaliacao.a_ftadc4 = dr["a_ftadc4"].ToString().Trim();
                        return objAvaliacao;
                    }
                    else
                    {
                        objAvaliacao = null;
                        return objAvaliacao;
                    }
                }
                else
                {
                    objAvaliacao = null;
                    return objAvaliacao;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objAvaliacao = null;
                return objAvaliacao;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int a_id = 0;
            string sql = "SELECT a_id FROM avaliacao_veiculo ORDER BY a_id DESC LIMIT 1";

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
                        a_id = Convert.ToInt32(dr["a_id"]);
                        a_id = a_id + 1;
                        return a_id;
                    }
                    else
                    {
                        a_id = 0;
                        return a_id;
                    }

                }
                else
                {
                    a_id = 1;
                    return a_id;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                a_id = 0;
                return a_id;
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