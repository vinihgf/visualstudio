using BANCO;
using CLASSES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace NEGOCIO
{
    public class NG_Particip
    {
        public static int buscaCodigoParticip(string con)
        {
            return DB_Particip.buscaCodigoParticip(con);
        }

        public static bool incluiParticip(CL_Particip objParticip, string con)
        {
            return DB_Particip.incluiParticip(objParticip, con);
        }

        public static bool excluiParticip(CL_Particip objParticip, string con)
        {
            return DB_Particip.excluiParticip(objParticip, con);
        }

        public static CL_Particip buscaParticip(string p_clicod, string con)
        {
            return DB_Particip.buscarParticip(p_clicod, con);
        }

        public static bool alteraParticip(CL_Particip objParticip, string con)
        {
            return DB_Particip.alteraParticip(objParticip, con);
        }

        public static int buscaCodigoIBGE(string cidade, int estado, string con)
        {
            return DB_Particip.buscaCodigoIBGE(cidade, estado, con);
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Particip.conferePermissao(email, con);
        }

        public List<CL_Particip> listar(string pesquisa, string con, string filtroPesq)
        {
            return new DB_Particip().listar(pesquisa, con, filtroPesq);
        }

        public static int verificaParticip(string p_cgc, string con)
        {
            return DB_Particip.verificaParticip(p_cgc, con);
        }

        public static bool attIDumov(string id, int p_clicod, string con)
        {
            return DB_Particip.attIDumov(id, p_clicod, con);
        }

        public static bool cadParticipUmov(CL_Particip objParticip, CL_Empresa objEmpresa, string con)
        {
            WebRequest request = WebRequest.Create("https://api.umov.me/CenterWeb/api/" + objEmpresa.emp_token + "/serviceLocal.xml");
            request.Method = "POST";
            string postData = "data=" +
                                    "<serviceLocal>" +
                                        "<description>" + objParticip.p_nome + "</description>" +
                                        "<active>true</active>" +
                                        "<alternativeIdentifier>" + objParticip.p_cgc.Replace(".", "").Replace("/", "").Replace("-", "") + "</alternativeIdentifier>" +
                                        "<corporateName>" + objParticip.p_fantas + "</corporateName>" +
                                        "<country>" + objParticip.trocaPais(objParticip.p_pais) + "</country>" +
                                        "<state>" + objParticip.p_est + "</state>" +
                                        "<city>" + objParticip.p_cida + "</city>" +
                                        "<cityNeighborhood>" + objParticip.p_bairro + "</cityNeighborhood>" +
                                        "<streetType></streetType>" +
                                        "<street>" + objParticip.p_ende + "</street>" +
                                        "<streetNumber>" + objParticip.trocaNum(objParticip.p_nr).Trim() + "</streetNumber>" +
                                        "<streetComplement>" + objParticip.p_comend + "</streetComplement>" +
                                        "<zipCode>" + objParticip.p_cep.Trim() + "</zipCode>" +
                                        "<cellphoneStd></cellphoneStd>" +
                                        "<cellphoneNumber>" + objParticip.acertaTel(objParticip.p_celul) + "</cellphoneNumber>" +
                                        "<phoneStd></phoneStd>" +
                                        "<phoneNumber>" + objParticip.acertaTel(objParticip.p_fone) + "</phoneNumber>" +
                                        "<email>" + objParticip.p_email + "</email>" +
                                        "<observation></observation>";
            if (objParticip.p_localiz != "")
            {
                postData = postData + "<geoCoordinate>" + objParticip.p_localiz + "</geoCoordinate>";
            }
            postData = postData + "<exportStatus>0</exportStatus></serviceLocal>";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            if ((((HttpWebResponse)response).StatusDescription) == "Created")
            {
                try
                {
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<CL_Particip> listagemSimples(string con)
        {
            return DB_Particip.listagemSimples(con);
        }

        public List<CL_Particip> buscaEntregaManual(string particip, string cida, string tipo, string con)
        {
            return DB_Particip.buscaEntregaManual(particip, cida, tipo, con);
        }

        public static bool confereIE(string iest, string con)
        {
            return DB_Particip.confereIE(iest, con);
        }

        public List<CL_Particip> listarParticipCidada(string cidade, string con)
        {
            return DB_Particip.listarParticipCidade(cidade, con);
        }

        public static void attLocaliz(string latitude, string longitude, string clicod, string con)
        {
            DB_Particip.attLocaliz(latitude, longitude, clicod, con);
        }
    }
}