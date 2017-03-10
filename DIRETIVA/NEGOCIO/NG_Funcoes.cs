using BANCO;
using CLASSES;
using System;

namespace NEGOCIO
{
    public class NG_Funcoes : Conexao
    {
        public static bool validaCPF(string cgc)
        {
            if (cgc == "00000000000" || cgc == "11111111111" || cgc == "22222222222" || cgc == "33333333333" || cgc == "44444444444" || cgc == "55555555555" || cgc == "66666666666" || cgc == "77777777777" || cgc == "88888888888" || cgc == "99999999999")
            {
                return false;
            }
            else
            {

                int DF1 = 0, DF2 = 0, DF3 = 0, DF4 = 0, DF5 = 0, DF6 = 0, RESTO1 = 0, RESTO2 = 0, PRIDIG = 0, SEGDIG = 0;
                int[] D1 = new int[11];

                char[] c = cgc.ToCharArray();

                D1[0] = Convert.ToInt32(c[0].ToString());
                D1[1] = Convert.ToInt32(c[1].ToString());
                D1[2] = Convert.ToInt32(c[2].ToString());
                D1[3] = Convert.ToInt32(c[3].ToString());
                D1[4] = Convert.ToInt32(c[4].ToString());
                D1[5] = Convert.ToInt32(c[5].ToString());
                D1[6] = Convert.ToInt32(c[6].ToString());
                D1[7] = Convert.ToInt32(c[7].ToString());
                D1[8] = Convert.ToInt32(c[8].ToString());
                D1[9] = Convert.ToInt32(c[9].ToString());
                D1[10] = Convert.ToInt32(c[10].ToString());

                DF1 = (D1[0] * 10) + (D1[1] * 9) + (D1[2] * 8) + (D1[3] * 7) + (D1[4] * 6) + (D1[5] * 5) + (D1[6] * 4) + (D1[7] * 3) + (D1[8] * 2);
                DF2 = DF1 / 11;
                DF3 = DF2 * 11;
                RESTO1 = DF1 - DF3;

                if (RESTO1 == 0 || RESTO1 == 1)
                {
                    PRIDIG = 0;
                }
                else
                {
                    PRIDIG = 11 - RESTO1;
                }

                DF4 = (D1[0] * 11) + (D1[1] * 10) + (D1[2] * 9) + (D1[3] * 8) + (D1[4] * 7) + (D1[5] * 6) + (D1[6] * 5) + (D1[7] * 4) + (D1[8] * 3) + (PRIDIG * 2);
                DF5 = DF4 / 11;
                DF6 = DF5 * 11;
                RESTO2 = DF4 - DF6;

                if (RESTO2 == 0 || RESTO2 == 1)
                {
                    SEGDIG = 0;
                }
                else
                {
                    SEGDIG = 11 - RESTO2;
                }

                if (PRIDIG != D1[9] || SEGDIG != D1[10])
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool validaCNPJ(string cgc)
        {

            int DF1 = 0, DF2 = 0, DF3 = 0, DF4 = 0, DF5 = 0, DF6 = 0, RESTO1 = 0, RESTO2 = 0, PRIDIG = 0, SEGDIG = 0;
            int[] D1 = new int[14];

            char[] c = cgc.ToCharArray();

            D1[0] = Convert.ToInt32(c[0].ToString());
            D1[1] = Convert.ToInt32(c[1].ToString());
            D1[2] = Convert.ToInt32(c[2].ToString());
            D1[3] = Convert.ToInt32(c[3].ToString());
            D1[4] = Convert.ToInt32(c[4].ToString());
            D1[5] = Convert.ToInt32(c[5].ToString());
            D1[6] = Convert.ToInt32(c[6].ToString());
            D1[7] = Convert.ToInt32(c[7].ToString());
            D1[8] = Convert.ToInt32(c[8].ToString());
            D1[9] = Convert.ToInt32(c[9].ToString());
            D1[10] = Convert.ToInt32(c[10].ToString());
            D1[11] = Convert.ToInt32(c[11].ToString());
            D1[12] = Convert.ToInt32(c[12].ToString());
            D1[13] = Convert.ToInt32(c[13].ToString());

            DF1 = (D1[0] * 5) + (D1[1] * 4) + (D1[2] * 3) + (D1[3] * 2) + (D1[4] * 9) + (D1[5] * 8) + (D1[6] * 7) + (D1[7] * 6) + (D1[8] * 5) + (D1[9] * 4) + (D1[10] * 3) + (D1[11] * 2);

            DF2 = DF1 / 11;
            DF3 = DF2 * 11;
            RESTO1 = DF1 - DF3;

            if (RESTO1 == 0 || RESTO1 == 1)
            {
                PRIDIG = 0;
            }
            else
            {
                PRIDIG = 11 - RESTO1;
            }

            DF4 = (D1[0] * 6) + (D1[1] * 5) + (D1[2] * 4) + (D1[3] * 3) + (D1[4] * 2) + (D1[5] * 9) + (D1[6] * 8) + (D1[7] * 7) + (D1[8] * 6) + (D1[9] * 5) + (D1[10] * 4) + (D1[11] * 3) + (PRIDIG * 2);
            DF5 = DF4 / 11;
            DF6 = DF5 * 11;
            RESTO2 = DF4 - DF6;

            if (RESTO2 == 0 || RESTO2 == 1)
            {
                SEGDIG = 0;
            }
            else
            {
                SEGDIG = 11 - RESTO2;
            }

            if (PRIDIG != D1[12] || SEGDIG != D1[13])
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int buscaCodigoEstado(string codEst, string con)
        {
            return DB_Funcoes.buscaCodigoEstado(codEst, con);
        }
        public static bool confereEmail(string email, string con)
        {
            return DB_Funcoes.confereEmail(email, con);
        }
        public static void DesmontaConexao(string con)
        {
            try
            {
                string[] vetCon = con.Split('#');
                SERVER = vetCon[0].ToString();
                PORTA = vetCon[1].ToString();
                USER = vetCon[2].ToString();
                SENHA = vetCon[3].ToString();
                BANCO = vetCon[4].ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public static bool acessoUsudac(string email, string con)
        {
            return DB_Funcoes.acessoUsudac(email, con);
        }
        public static CL_Convenio buscaConvenio(string con_cod, string con)
        {
            return DB_Funcoes.buscaConvenio(con_cod, con);
        }
    }
}