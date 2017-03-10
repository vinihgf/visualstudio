using Npgsql;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace BANCO
{
    public class DB_Umov : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static bool attSituac(string sql, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
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

        public static string buscaDados(string token, string arquivo, int id, string retorno)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://api.umov.me/CenterWeb/api/" + token + "/" + arquivo + "/alternativeIdentifier/" + id + ".xml");
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                if (((HttpWebResponse)response).StatusDescription == "OK")
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    retorno = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                    return retorno;
                }
                else
                {
                    retorno = "ERRO";
                    return retorno;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                retorno = "ERRO";
                return retorno;
            }
        }

        public static bool deleteDados(string token, string arquivo)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://api.umov.me/CenterWeb/api/" + token + "/" + arquivo + ".xml");
                request.Method = "DELETE";
                request.ContentType = "application/x-www-form-urlencoded";
                Stream dataStream = request.GetRequestStream();
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                if ((((HttpWebResponse)response).StatusDescription) == "OK")
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
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static int buscaIDAgentType(int id, string token)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://api.umov.me/CenterWeb/api/14970e9f8afd1ed151001d925905481ad3a197/agentType.xml");
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                if (((HttpWebResponse)response).StatusDescription == "OK")
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string[] vetor = reader.ReadToEnd().Split('\n');
                    vetor = vetor[4].Split('/');
                    string valor = vetor[2];
                    id = Convert.ToInt32(valor.Replace(".xml\"", ""));
                    reader.Close();
                    response.Close();
                    return id;
                }
                else
                {
                    id = 0;
                    return id;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                id = 0;
                return id;
            }
        }

        public static bool sincronizaApp(string token, string arquivo, string postData)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://api.umov.me/CenterWeb/api/" + token + "/" + arquivo + ".xml");
                request.Method = "POST";
                postData = postData.Replace("\t", "");
                postData = postData.Replace(",", ".");
                postData = postData.Replace("\t", "");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                if ((((HttpWebResponse)response).StatusDescription) == "Created" || (((HttpWebResponse)response).StatusDescription) == "OK")
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
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool attDadosApp(string token, string arquivo, string post, string id)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://api.umov.me/CenterWeb/api/" + token + "/" + arquivo + "/alternativeIdentifier/" + id + ".xml");
                request.Method = "POST";
                string postData = post;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                if ((((HttpWebResponse)response).StatusDescription) == "OK")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
    }
}