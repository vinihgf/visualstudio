using CLASSES;
using Npgsql;
using System;
using System.Data;

namespace BANCO
{
    public class DB_Empresa : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }

        public static CL_Empresa buscaEmpresa(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            CL_Empresa objEmpresa = new CL_Empresa();

            string sql = "SELECT * FROM empresa";

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
                        objEmpresa.emp_bairro = dr["emp_bairro"].ToString().Trim();
                        objEmpresa.emp_cep = dr["emp_cep"].ToString().Trim();
                        objEmpresa.emp_cgc = dr["emp_cgc"].ToString().Trim();
                        objEmpresa.emp_cida = dr["emp_cida"].ToString().Trim();
                        objEmpresa.emp_dirt = dr["emp_dirt"].ToString().Trim();
                        objEmpresa.emp_email = dr["emp_email"].ToString().Trim();
                        objEmpresa.emp_ende = dr["emp_end"].ToString().Trim();
                        objEmpresa.emp_est = dr["emp_est"].ToString().Trim();
                        objEmpresa.emp_fantas = dr["emp_fantas"].ToString().Trim();
                        objEmpresa.emp_fone = dr["emp_fone"].ToString().Trim();
                        objEmpresa.emp_ibge = dr["emp_ibge"].ToString().Trim();
                        objEmpresa.emp_imunic = dr["emp_imunic"].ToString().Trim();
                        objEmpresa.emp_iscest = dr["emp_iscest"].ToString().Trim();
                        objEmpresa.emp_nome = dr["emp_nome"].ToString().Trim();
                        objEmpresa.emp_comend = dr["emp_comend"].ToString().Trim();
                        objEmpresa.emp_site = dr["emp_site"].ToString().Trim();
                        objEmpresa.emp_foto = dr["emp_foto"].ToString().Trim();
                        objEmpresa.emp_nr = dr["emp_nr"] is DBNull ? 0 : Convert.ToInt32(dr["emp_nr"]);
                        objEmpresa.emp_receit = dr["emp_receit"] is DBNull ? 0 : Convert.ToDouble(dr["emp_receit"]);
                        objEmpresa.emp_nota = dr["emp_nota"] is DBNull ? 0 : Convert.ToInt32(dr["emp_nota"]);
                        objEmpresa.emp_serie = dr["emp_serie"].ToString().Trim();
                        objEmpresa.emp_dupl = dr["emp_dupl"] is DBNull ? 0 : Convert.ToInt32(dr["emp_dupl"]);
                        objEmpresa.emp_series = dr["emp_series"].ToString().Trim();
                        objEmpresa.emp_notas = dr["emp_notas"] is DBNull ? 0 : Convert.ToInt32(dr["emp_notas"]);
                        objEmpresa.emp_cusipi = dr["emp_cusipi"].ToString().Trim();
                        objEmpresa.emp_cusicm = dr["emp_cusicm"].ToString().Trim();
                        objEmpresa.emp_pont1 = dr["emp_pont1"] is DBNull ? 0 : Convert.ToInt32(dr["emp_pont1"]);
                        objEmpresa.emp_pont2 = dr["emp_pont2"] is DBNull ? 0 : Convert.ToInt32(dr["emp_pont2"]);
                        objEmpresa.emp_cdicm = dr["emp_cdicm"] is DBNull ? 0 : Convert.ToDouble(dr["emp_cdicm"]);
                        objEmpresa.emp_tipo = dr["emp_tipo"].ToString().Trim();
                        objEmpresa.emp_cofins = Convert.ToDouble(dr["emp_cofins"]);
                        objEmpresa.emp_pis = dr["emp_pis"] is DBNull ? 0 : Convert.ToDouble(dr["emp_pis"]);
                        objEmpresa.emp_csoc = dr["emp_csoc"] is DBNull ? 0 : Convert.ToDouble(dr["emp_csoc"]);
                        objEmpresa.emp_ir = dr["emp_ir"] is DBNull ? 0 : Convert.ToDouble(dr["emp_ir"]);
                        objEmpresa.emp_irven = dr["emp_irven"] is DBNull ? 0 : Convert.ToDouble(dr["emp_irven"]);
                        objEmpresa.emp_ircom = dr["emp_ircom"] is DBNull ? 0 : Convert.ToDouble(dr["emp_ircom"]);
                        objEmpresa.emp_tpfed = dr["emp_tpfed"].ToString().Trim();
                        objEmpresa.emp_saida = dr["emp_saida"].ToString().Trim();
                        objEmpresa.emp_limest = dr["emp_limest"] is DBNull ? 0 : Convert.ToDouble(dr["emp_limest"]);
                        objEmpresa.emp_limfed = dr["emp_limfed"] is DBNull ? 0 : Convert.ToDouble(dr["emp_limfed"]);
                        objEmpresa.emp_tpemis = dr["emp_tpemis"].ToString().Trim();
                        objEmpresa.emp_emissor = dr["emp_emisor"].ToString().Trim();
                        objEmpresa.emp_filial = dr["emp_filial"].ToString().Trim();
                        objEmpresa.emp_linnf = dr["emp_linnf"] is DBNull ? 0 : Convert.ToInt32(dr["emp_linnf"]);
                        objEmpresa.emp_intcon = dr["emp_intcon"].ToString().Trim();
                        objEmpresa.emp_caljur = dr["emp_caljur"].ToString().Trim();
                        objEmpresa.emp_perjur = dr["emp_perjur"] is DBNull ? 0 : Convert.ToDouble(dr["emp_perjur"]);
                        objEmpresa.emp_isimp = dr["emp_isimp"] is DBNull ? 0 : Convert.ToDouble(dr["emp_isimp"]);
                        objEmpresa.emp_forger = dr["emp_forger"].ToString().Trim();
                        objEmpresa.emp_clipro = dr["emp_clipro"].ToString().Trim();
                        objEmpresa.emp_forpro = dr["emp_forpro"].ToString().Trim();
                        objEmpresa.emp_pesnom = dr["emp_pesnom"].ToString().Trim();
                        objEmpresa.emp_vlpis = dr["emp_vlpis"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlpis"]);
                        objEmpresa.emp_vlcof = dr["emp_vlcof"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlcof"]);
                        objEmpresa.emp_vlsimp = dr["emp_vlsimp"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlsimp"]);
                        objEmpresa.emp_vlcsoc = dr["emp_vlcsoc"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlcsoc"]);
                        objEmpresa.emp_vlir = dr["emp_vlir"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlir"]);
                        objEmpresa.emp_vlicm = dr["emp_vlicm"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlicm"]);
                        objEmpresa.emp_triger = dr["emp_triger"].ToString().Trim();
                        objEmpresa.emp_forcta = dr["emp_forcta"].ToString().Trim();
                        objEmpresa.emp_serie3 = dr["emp_serie3"].ToString().Trim();
                        objEmpresa.emp_nota3 = dr["emp_nota3"] is DBNull ? 0 : Convert.ToInt32(dr["emp_nota3"]);
                        objEmpresa.emp_ecf = dr["emp_ecf"].ToString().Trim();
                        objEmpresa.emp_tprodu = dr["emp_tprodu"].ToString().Trim();
                        objEmpresa.emp_nrconh = dr["emp_nrconh"] is DBNull ? 0 : Convert.ToInt32(dr["emp_nrconh"]);
                        objEmpresa.emp_ptacup = dr["emp_ptacup"].ToString().Trim();
                        objEmpresa.emp_indice = dr["emp_indice"].ToString().Trim();
                        objEmpresa.emp_vlind = dr["emp_vlind"] is DBNull ? 0 : Convert.ToDouble(dr["emp_vlind"]);
                        objEmpresa.emp_tpdupl = dr["emp_tpdupl"].ToString().Trim();
                        objEmpresa.emp_digit = dr["emp_digit"].ToString().Trim();
                        objEmpresa.emp_tplcli = dr["emp_tplcli"].ToString().Trim();
                        objEmpresa.emp_cadest = dr["emp_cadest"].ToString().Trim();
                        objEmpresa.emp_tpdigi = dr["emp_tpdigi"].ToString().Trim();
                        objEmpresa.emp_calclc = dr["emp_calclc"].ToString().Trim();
                        objEmpresa.emp_tlcecf = dr["emp_tlcecf"].ToString().Trim();
                        objEmpresa.emp_cadcli = dr["emp_cadcli"].ToString().Trim();
                        objEmpresa.emp_prxcli = dr["emp_prxcli"] is DBNull ? 0 : Convert.ToInt32(dr["emp_prxcli"]);
                        objEmpresa.emp_video = dr["emp_video"].ToString().Trim();
                        objEmpresa.emp_ntacup = dr["emp_ntacup"].ToString().Trim();
                        objEmpresa.emp_prxite = dr["emp_prxite"] is DBNull ? 0 : Convert.ToInt32(dr["emp_prxite"]);
                        objEmpresa.emp_piscof = dr["emp_piscof"].ToString().Trim();
                        objEmpresa.emp_pasta = dr["emp_pasta"].ToString().Trim();
                        objEmpresa.emp_lclvct = dr["emp_lclvct"] is DBNull ? 0 : Convert.ToInt32(dr["emp_lclvct"]);
                        objEmpresa.emp_sped = dr["emp_sped"].ToString().Trim();
                        objEmpresa.emp_tipnfe = dr["emp_tipnfe"].ToString().Trim();
                        objEmpresa.emp_nrvias = dr["emp_nrvias"].ToString().Trim();
                        objEmpresa.emp_relrcb = dr["emp_relrcb"] is DBNull ? 0 : Convert.ToInt32(dr["emp_relrcb"]);
                        objEmpresa.emp_isufra = dr["emp_isufra"].ToString().Trim();
                        objEmpresa.emp_fax = dr["emp_fax"].ToString().Trim();
                        objEmpresa.emp_cnae = dr["emp_cnae"].ToString().Trim();
                        objEmpresa.emp_pstnfe = dr["emp_pstnfe"].ToString().Trim();
                        objEmpresa.emp_crt = dr["emp_crt"].ToString().Trim();
                        objEmpresa.emp_usaean = dr["emp_usaean"].ToString().Trim();
                        objEmpresa.emp_efdpis = dr["emp_efdpis"].ToString().Trim();
                        objEmpresa.emp_tecnic = dr["emp_tecnic"].ToString().Trim();
                        objEmpresa.emp_crea = dr["emp_crea"].ToString().Trim();
                        objEmpresa.emp_cpftec = dr["emp_cpftec"].ToString().Trim();
                        objEmpresa.emp_endtec = dr["emp_endtec"].ToString().Trim();
                        objEmpresa.emp_lmcdta = dr["emp_lmcdta"] is DBNull ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["emp_lmcdta"]);
                        objEmpresa.emp_lmcpag = dr["emp_lmcpag"] is DBNull ? 0 : Convert.ToInt32(dr["emp_lmcpag"]);
                        objEmpresa.emp_eminfe = dr["emp_eminfe"].ToString().Trim();
                        objEmpresa.emp_justif = dr["emp_justif"].ToString().Trim();
                        objEmpresa.emp_versao = dr["emp_versao"].ToString().Trim();
                        objEmpresa.emp_ctacor = dr["emp_ctacor"] is DBNull ? 0 : Convert.ToInt32(dr["emp_ctacor"]);
                        objEmpresa.emp_dvda1 = dr["emp_dvda1"].ToString().Trim();
                        objEmpresa.emp_dvda2 = dr["emp_dvda2"].ToString().Trim();
                        objEmpresa.emp_dvda3 = dr["emp_dvda3"].ToString().Trim();
                        objEmpresa.emp_dvda4 = dr["emp_dvda4"].ToString().Trim();
                        objEmpresa.emp_diaped = dr["emp_diaped"] is DBNull ? 0 : Convert.ToInt32(dr["emp_diaped"]);
                        objEmpresa.emp_lucro = dr["emp_lucro"] is DBNull ? 0 : Convert.ToDouble(dr["emp_lucro"]);
                        objEmpresa.emp_conhec = dr["emp_conhec"] is DBNull ? 0 : Convert.ToInt32(dr["emp_conhec"]);
                        objEmpresa.emp_sercon = dr["emp_sercon"].ToString().Trim();
                        objEmpresa.emp_dscven = dr["emp_dscven"] is DBNull ? 0 : Convert.ToDouble(dr["emp_dscven"]);
                        objEmpresa.emp_ecfii = dr["emp_ecfii"].ToString().Trim();
                        objEmpresa.emp_pstcte = dr["emp_pstcte"].ToString().Trim();
                        objEmpresa.emp_pstecf = dr["emp_pstecf"].ToString().Trim();
                        objEmpresa.emp_pstctb = dr["emp_pstctb"].ToString().Trim();
                        objEmpresa.emp_ativid = dr["emp_ativid"].ToString().Trim();
                        objEmpresa.emp_limcpf = dr["emp_limcpf"].ToString().Trim();
                        objEmpresa.emp_horario = dr["emp_horari"].ToString().Trim();
                        objEmpresa.emp_nrmdfe = dr["emp_nrmdfe"] is DBNull ? 0 : Convert.ToInt32(dr["emp_nrmdfe"]);
                        objEmpresa.emp_srmdfe = dr["emp_srmdfe"].ToString().Trim();
                        objEmpresa.emp_dealer = dr["emp_dealer"] is DBNull ? 0 : Convert.ToInt32(dr["emp_dealer"]);
                        objEmpresa.emp_tpemnf = dr["emp_tpemnf"].ToString().Trim();
                        objEmpresa.emp_tpctas = dr["emp_tpctas"].ToString().Trim();
                        objEmpresa.emp_visnfe = dr["emp_visnfe"].ToString().Trim();
                        objEmpresa.emp_scte2 = dr["emp_scte2"].ToString().Trim();
                        objEmpresa.emp_ncte2 = dr["emp_ncte2"].ToString().Trim();
                        objEmpresa.emp_android = dr["emp_androi"].ToString().Trim();
                        objEmpresa.emp_nrart = dr["emp_nrart"] is DBNull ? 0 : Convert.ToInt32(dr["emp_nrart"]);
                        objEmpresa.emp_token = dr["emp_token"].ToString().Trim();
                        return objEmpresa;
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

        public static bool conferePermissao(string email, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT u_mempresa FROM usudac WHERE u_email=@u_email";

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
                        string ret = dr["u_mempresa"].ToString().Trim();
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

        public static bool alteraEmpresa(CL_Empresa objEmpresa, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE empresa SET emp_bairro='" + objEmpresa.emp_bairro + "', emp_cep='" + objEmpresa.emp_cep + "', emp_cgc='" + objEmpresa.emp_cgc +
                    "', emp_cida='" + objEmpresa.emp_cida + "', emp_dirt='" + objEmpresa.emp_dirt + "', emp_email='" + objEmpresa.emp_email +
                    "', emp_end='" + objEmpresa.emp_ende + "', emp_est='" + objEmpresa.emp_est + "', emp_fantas='" + objEmpresa.emp_fantas +
                    "', emp_fone='" + objEmpresa.emp_fone + "', emp_ibge='" + objEmpresa.emp_ibge + "', emp_imunic='" + objEmpresa.emp_imunic +
                    "', emp_iscest='" + objEmpresa.emp_iscest + "', emp_nome='" + objEmpresa.emp_nome + "', emp_nr=" + objEmpresa.emp_nr +
                    " , emp_comend='" + objEmpresa.emp_comend + "', emp_site='" + objEmpresa.emp_site + "', emp_foto='" + objEmpresa.emp_foto + "', emp_token='" + objEmpresa.emp_token + "'";

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
    }
}