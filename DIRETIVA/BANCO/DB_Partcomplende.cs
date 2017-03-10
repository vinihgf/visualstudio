using CLASSES;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BANCO
{
    public class DB_Partcomplende : Conexao
    {
        public static NpgsqlConnection Conn { get; set; }
        public static int buscaCod(string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);
            int pc_codigo = 0;

            string sql = "SELECT pc_codigo FROM part_compl ORDER BY pc_codigo DESC LIMIT 1";

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
                        pc_codigo = Convert.ToInt16(dr["pc_codigo"]);
                        pc_codigo = pc_codigo + 1;

                        return pc_codigo;
                    }
                    else
                    {
                        pc_codigo = 0;
                        return pc_codigo;
                    }
                }
                else
                {
                    pc_codigo = 1;
                    return pc_codigo;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                pc_codigo = 0;
                return pc_codigo;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool excluiPartComplende(CL_Partcomplende objPartComplende, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "DELETE FROM part_compl WHERE pc_codigo=@pc_codigo";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("pc_codigo", objPartComplende.pc_codigo);

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

        public static bool alteraPartComplende(CL_Partcomplende objPartComplende, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "UPDATE part_compl SET pc_codpart=@pc_codpart, pc_nome=@pc_nome, pc_cnpj=@pc_cnpj, pc_ende=@pc_ende, " +
                    "pc_nr=@pc_nr, pc_compl=@pc_compl, pc_bairro=@pc_bairro, pc_ibge=@pc_ibge, pc_cida=@pc_cida, pc_uf=@pc_uf, pc_respons=@pc_respons, " +
                    "pc_matric=@pc_matric, pc_email=@pc_email, pc_fone=@pc_fone, pc_cep=@pc_cep, pc_iest=@pc_iest, pc_ativo=@pc_ativo, pc_situac=@pc_situac " +
                    "WHERE pc_codigo=@pc_codigo";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("pc_codigo", objPartComplende.pc_codigo);
                comand.Parameters.AddWithValue("pc_codpart", objPartComplende.pc_codpart);
                comand.Parameters.AddWithValue("pc_nome", objPartComplende.pc_nome);
                comand.Parameters.AddWithValue("pc_cnpj", objPartComplende.pc_cnpj);
                comand.Parameters.AddWithValue("pc_ende", objPartComplende.pc_ende);
                comand.Parameters.AddWithValue("pc_nr", objPartComplende.pc_nr);
                comand.Parameters.AddWithValue("pc_compl", objPartComplende.pc_compl);
                comand.Parameters.AddWithValue("pc_ibge", objPartComplende.pc_ibge);
                comand.Parameters.AddWithValue("pc_cida", objPartComplende.pc_cida);
                comand.Parameters.AddWithValue("pc_uf", objPartComplende.pc_uf);
                comand.Parameters.AddWithValue("pc_matric", objPartComplende.pc_matric);
                comand.Parameters.AddWithValue("pc_email", objPartComplende.pc_email);
                comand.Parameters.AddWithValue("pc_bairro", objPartComplende.pc_bairro);
                comand.Parameters.AddWithValue("pc_fone", objPartComplende.pc_fone);
                comand.Parameters.AddWithValue("pc_cep", objPartComplende.pc_cep);
                comand.Parameters.AddWithValue("pc_iest", objPartComplende.pc_iest);
                comand.Parameters.AddWithValue("pc_ativo", objPartComplende.pc_ativo);
                comand.Parameters.AddWithValue("pc_respons", objPartComplende.pc_respons);
                comand.Parameters.AddWithValue("pc_situac", objPartComplende.pc_situac);

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

        public static bool cadPartComplende(CL_Partcomplende objPartComplende, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            try
            {
                string sql = "INSERT INTO part_compl (pc_codigo, pc_codpart, pc_nome, pc_cnpj, pc_ende, pc_nr, pc_compl, pc_bairro, pc_ibge, " +
                    "pc_cida, pc_uf, pc_respons, pc_matric, pc_email, pc_fone, pc_cep, pc_iest, pc_ativo, pc_situac) VALUES (" +
                    "@pc_codigo, @pc_codpart, @pc_nome, @pc_cnpj, @pc_ende, @pc_nr, @pc_compl, @pc_bairro, @pc_ibge, " +
                    "@pc_cida, @pc_uf, @pc_respons, @pc_matric, @pc_email, @pc_fone, @pc_cep, @pc_iest, @pc_ativo, @pc_situac)";

                NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
                comand.Parameters.AddWithValue("pc_codigo", objPartComplende.pc_codigo);
                comand.Parameters.AddWithValue("pc_codpart", objPartComplende.pc_codpart);
                comand.Parameters.AddWithValue("pc_nome", objPartComplende.pc_nome);
                comand.Parameters.AddWithValue("pc_cnpj", objPartComplende.pc_cnpj);
                comand.Parameters.AddWithValue("pc_ende", objPartComplende.pc_ende);
                comand.Parameters.AddWithValue("pc_nr", objPartComplende.pc_nr);
                comand.Parameters.AddWithValue("pc_compl", objPartComplende.pc_compl);
                comand.Parameters.AddWithValue("pc_ibge", objPartComplende.pc_ibge);
                comand.Parameters.AddWithValue("pc_cida", objPartComplende.pc_cida);
                comand.Parameters.AddWithValue("pc_uf", objPartComplende.pc_uf);
                comand.Parameters.AddWithValue("pc_matric", objPartComplende.pc_matric);
                comand.Parameters.AddWithValue("pc_bairro", objPartComplende.pc_bairro);
                comand.Parameters.AddWithValue("pc_email", objPartComplende.pc_email);
                comand.Parameters.AddWithValue("pc_fone", objPartComplende.pc_fone);
                comand.Parameters.AddWithValue("pc_cep", objPartComplende.pc_cep);
                comand.Parameters.AddWithValue("pc_iest", objPartComplende.pc_iest);
                comand.Parameters.AddWithValue("pc_ativo", objPartComplende.pc_ativo);
                comand.Parameters.AddWithValue("pc_respons", objPartComplende.pc_respons);
                comand.Parameters.AddWithValue("pc_situac", objPartComplende.pc_situac);

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

        public static List<CL_Partcomplende> buscaComplendes(int p_clicod, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM part_compl WHERE pc_codpart=@codigo";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            comand.Parameters.AddWithValue("codigo", p_clicod);
            CL_Partcomplende objPartComplende = null;
            List<CL_Partcomplende> objList = new List<CL_Partcomplende>();

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objPartComplende = new CL_Partcomplende();
                        objPartComplende.pc_codigo = Convert.ToInt32(dr["pc_codigo"]);
                        objPartComplende.pc_codpart = p_clicod;
                        objPartComplende.pc_nome = dr["pc_nome"].ToString().Trim();
                        objPartComplende.pc_cnpj = dr["pc_cnpj"].ToString().Trim();
                        objPartComplende.pc_ende = dr["pc_ende"].ToString().Trim();
                        objPartComplende.pc_nr = dr["pc_nr"].ToString().Trim();
                        objPartComplende.pc_compl = dr["pc_compl"].ToString().Trim();
                        objPartComplende.pc_bairro = dr["pc_bairro"].ToString().Trim();
                        objPartComplende.pc_ibge = dr["pc_ibge"].ToString().Trim();
                        objPartComplende.pc_cida = dr["pc_cida"].ToString().Trim();
                        objPartComplende.pc_uf = dr["pc_uf"].ToString().Trim();
                        objPartComplende.pc_respons = dr["pc_respons"].ToString().Trim();
                        objPartComplende.pc_matric = dr["pc_matric"].ToString().Trim();
                        objPartComplende.pc_email = dr["pc_email"].ToString().Trim();
                        objPartComplende.pc_fone = dr["pc_fone"].ToString().Trim();
                        objPartComplende.pc_cep = dr["pc_cep"].ToString().Trim();
                        objPartComplende.pc_iest = dr["pc_iest"].ToString().Trim();
                        objPartComplende.pc_ativo = dr["pc_ativo"].ToString().Trim();
                        objPartComplende.pc_situac = dr["pc_situac"].ToString().Trim();
                        objList.Add(objPartComplende);
                    }
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

        public static CL_Partcomplende buscaPartComplende(string codigo, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT * FROM part_compl WHERE pc_codigo=@codigo";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            comand.Parameters.AddWithValue("codigo", codigo);
            CL_Partcomplende objPartComplende = new CL_Partcomplende();

            try
            {
                Conn.Open();
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        objPartComplende.pc_codigo = Convert.ToInt32(codigo);
                        objPartComplende.pc_codpart = Convert.ToInt32(dr["pc_codpart"]);
                        objPartComplende.pc_nome = dr["pc_nome"].ToString().Trim();
                        objPartComplende.pc_cnpj = dr["pc_cnpj"].ToString().Trim();
                        objPartComplende.pc_ende = dr["pc_ende"].ToString().Trim();
                        objPartComplende.pc_nr = dr["pc_nr"].ToString().Trim();
                        objPartComplende.pc_compl = dr["pc_compl"].ToString().Trim();
                        objPartComplende.pc_bairro = dr["pc_bairro"].ToString().Trim();
                        objPartComplende.pc_ibge = dr["pc_ibge"].ToString().Trim();
                        objPartComplende.pc_cida = dr["pc_cida"].ToString().Trim();
                        objPartComplende.pc_uf = dr["pc_uf"].ToString().Trim();
                        objPartComplende.pc_respons = dr["pc_respons"].ToString().Trim();
                        objPartComplende.pc_matric = dr["pc_matric"].ToString().Trim();
                        objPartComplende.pc_email = dr["pc_email"].ToString().Trim();
                        objPartComplende.pc_fone = dr["pc_fone"].ToString().Trim();
                        objPartComplende.pc_cep = dr["pc_cep"].ToString().Trim();
                        objPartComplende.pc_iest = dr["pc_iest"].ToString().Trim();
                        objPartComplende.pc_ativo = dr["pc_ativo"].ToString().Trim();
                        objPartComplende.pc_situac = dr["pc_situac"].ToString().Trim();
                        return objPartComplende;
                    }
                    else
                    {
                        objPartComplende = null;
                        return objPartComplende;
                    }
                }
                else
                {
                    objPartComplende = null;
                    return objPartComplende;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                objPartComplende = null;
                return objPartComplende;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static bool confereIE(string iest, string con)
        {
            DB_Funcoes.DesmontaConexao(con);
            CONEXAO = montaDAO(CONEXAO);
            Conn = new NpgsqlConnection(CONEXAO);

            string sql = "SELECT pc_codigo FROM part_compl WHERE REPLACE(pc_iest,'/','') =@iest";
            NpgsqlCommand comand = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr;
            comand.Parameters.AddWithValue("iest", iest.Replace("/", ""));

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
    }
}