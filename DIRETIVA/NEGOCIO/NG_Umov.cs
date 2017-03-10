using BANCO;
using CLASSES;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace NEGOCIO
{
    public class NG_Umov
    {
        public static bool sincronizaApp(string token, string arquivo, string postData)
        {
            return DB_Umov.sincronizaApp(token, arquivo, postData);
        }

        public static bool attDadosApp(string token, string arquivo, string post, string id)
        {
            return DB_Umov.attDadosApp(token, arquivo, post, id);
        }

        public static bool attSituac(string sql, string con)
        {
            return DB_Umov.attSituac(sql, con);
        }

        public static int buscaIDAgentType(int id, string token)
        {
            return DB_Umov.buscaIDAgentType(id, token);
        }

        public static string buscaDados(string token, string arquivo, int id, string retorno)
        {
            return DB_Umov.buscaDados(token, arquivo, id, retorno);
        }

        public static bool deleteDados(string token, string arquivo)
        {
            return DB_Umov.deleteDados(token, arquivo);
        }

        public static bool confereParticip(int clicod, int codend, string token, string con)
        {
            try
            {
                string cgc = "", sql = "";
                if (codend != 0)
                {
                    CL_Partcomplende objPartComplend = new CL_Partcomplende();
                    objPartComplend = NG_Partcomplende.buscaPartComplende(codend.ToString(), con);
                    if (objPartComplend != null)
                    {
                        cgc = cgc = objPartComplend.pc_cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                        cgc = cgc + "-" + objPartComplend.pc_codigo;
                        sql = "UPDATE part_compl SET pc_situac='S' WHERE pc_codigo=" + objPartComplend.pc_codigo;
                        if (objPartComplend.pc_situac == "S")
                            return true;
                        else if (objPartComplend.pc_situac == "A")
                        {
                            string post = acertaXmlAlterar(objPartComplend.pc_nome, objPartComplend.pc_nome, objPartComplend.pc_uf, "BRASIL", objPartComplend.pc_cida, objPartComplend.pc_bairro, objPartComplend.pc_ende, objPartComplend.pc_nr, objPartComplend.pc_compl, objPartComplend.pc_cep, objPartComplend.pc_fone.Trim(), objPartComplend.pc_fone.Trim(), objPartComplend.pc_email);
                            if (attDadosApp(token, "serviceLocal", post, cgc))
                            {

                                if (attSituac(sql, con))
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
                        else if (objPartComplend.pc_situac == "I" || objPartComplend.pc_situac == "")
                        {
                            string post = acertaXmlNovo(cgc, objPartComplend.pc_nome, objPartComplend.pc_nome, objPartComplend.pc_uf, "BRASIL", objPartComplend.pc_cida, objPartComplend.pc_bairro, objPartComplend.pc_ende, objPartComplend.pc_nr, objPartComplend.pc_compl, objPartComplend.pc_cep, objPartComplend.pc_fone.Trim(), objPartComplend.pc_fone.Trim(), objPartComplend.pc_email);
                            if (sincronizaApp(token, "serviceLocal", post))
                            {
                                if (attSituac(sql, con))
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
                else
                {
                    //PARTICIP
                    CL_Particip objParticip = new CL_Particip();
                    objParticip = NG_Particip.buscaParticip(clicod.ToString(), con);
                    if (objParticip != null)
                    {
                        cgc = cgc = objParticip.p_cgc.Replace(".", "").Replace("/", "").Replace("-", "");
                        sql = "UPDATE particip SET p_situac='S' WHERE p_cod=" + objParticip.p_clicod;
                        if (objParticip.p_situac == "S")
                            return true;
                        else if (objParticip.p_situac == "A")
                        {
                            string post = acertaXmlAlterar(objParticip.p_nome, objParticip.p_fantas, objParticip.p_est, objParticip.p_pais, objParticip.p_cida, objParticip.p_bairro, objParticip.p_ende, objParticip.p_nr, objParticip.p_comend, objParticip.p_cep, objParticip.p_celul, objParticip.p_fone, objParticip.p_email);
                            if (attDadosApp(token, "serviceLocal", post, cgc))
                            {

                                if (attSituac(sql, con))
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
                        else if (objParticip.p_situac == "I" || objParticip.p_situac == "")
                        {
                            string post = acertaXmlNovo(cgc, objParticip.p_nome, objParticip.p_fantas, objParticip.p_est, objParticip.p_pais, objParticip.p_cida, objParticip.p_bairro, objParticip.p_ende, objParticip.p_nr, objParticip.p_comend, objParticip.p_cep, objParticip.p_celul, objParticip.p_fone, objParticip.p_email);
                            if (sincronizaApp(token, "serviceLocal", post))
                            {
                                if (attSituac(sql, con))
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
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        private static string acertaXmlNovo(string cgc, string nome, string fantas, string uf, string pais, string cida, string bairro, string ende, string num, string compl, string cep, string celular, string fone, string email)
        {
            return "data=<serviceLocal>" +
                            "<description>" + nome.Trim() + "</description>" +
                            "<active>true</active>" +
                            "<alternativeIdentifier>" + cgc + "</alternativeIdentifier>" +
                            "<corporateName>" + fantas.Trim() + "</corporateName>" +
                            "<country>" + pais + "</country>" +
                            "<state>" + uf.Trim() + "</state>" +
                            "<city>" + cida.Trim() + "</city>" +
                            "<cityNeighborhood>" + bairro.Trim() + "</cityNeighborhood>" +
                            "<street>" + ende.Trim() + "</street>" +
                            "<streetNumber>" + trocaNum(num) + "</streetNumber>" +
                            "<streetComplement>" + compl.Trim() + "</streetComplement>" +
                            "<zipCode>" + cep + "</zipCode>" +
                            "<cellphoneNumber>" + acertaTel(celular) + "</cellphoneNumber>" +
                            "<phoneNumber>" + acertaTel(fone) + "</phoneNumber>" +
                            "<email>" + email + "</email>" +
                         "</serviceLocal>";
        }

        private static string acertaXmlAlterar(string nome, string fantas, string uf, string pais, string cida, string bairro, string ende, string num, string compl, string cep, string celular, string fone, string email)
        {
            return "data=<serviceLocal>" +
                            "<description>" + nome.Trim() + "</description>" +
                            "<active>true</active>" +
                            "<corporateName>" + fantas.Trim() + "</corporateName>" +
                            "<country>"+ pais +"</country>" +
                            "<state>" + uf.Trim() + "</state>" +
                            "<city>" + cida.Trim() + "</city>" +
                            "<cityNeighborhood>" + bairro.Trim() + "</cityNeighborhood>" +
                            "<street>" + ende.Trim() + "</street>" +
                            "<streetNumber>" + trocaNum(num) + "</streetNumber>" +
                            "<streetComplement>" + compl.Trim() + "</streetComplement>" +
                            "<zipCode>" + cep + "</zipCode>" +
                            "<cellphoneNumber>" + acertaTel(celular) + "</cellphoneNumber>" +
                            "<phoneNumber>" + acertaTel(fone) + "</phoneNumber>" +
                            "<email>" + email + "</email>" +
                         "</serviceLocal>";
        }

        private static object acertaTel(string tel)
        {
            tel = tel.Replace(" ", "");
            tel = tel.Replace("(", "");
            tel = tel.Replace(")", "");
            tel = tel.Replace("-", "");
            return tel;
        }

        private static object trocaNum(string nr)
        {
            if (nr.Trim() == "SN" || nr.Trim() == "S/N")
            {
                return "0";
            }
            else
            {
                return nr;
            }
        }
    }
}