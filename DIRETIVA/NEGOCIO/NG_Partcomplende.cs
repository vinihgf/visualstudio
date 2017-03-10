using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Partcomplende
    {
        public static int buscaCod(string con)
        {
            return DB_Partcomplende.buscaCod(con);
        }

        public static bool confereIE(string iest, string con)
        {
            return DB_Partcomplende.confereIE(iest, con);
        }

        public static CL_Partcomplende buscaPartComplende(string codigo, string con)
        {
            return DB_Partcomplende.buscaPartComplende(codigo, con);
        }

        public static List<CL_Partcomplende> buscaComplendes(int p_clicod, string con)
        {
            return DB_Partcomplende.buscaComplendes(p_clicod, con);
        }

        public static bool cadPartComplende(CL_Partcomplende objPartComplende, string con)
        {
            return DB_Partcomplende.cadPartComplende(objPartComplende, con);
        }

        public static bool alteraPartComplende(CL_Partcomplende objPartComplende, string con)
        {
            return DB_Partcomplende.alteraPartComplende(objPartComplende, con);
        }

        public static bool excluiPartComplende(CL_Partcomplende objPartComplende, string con)
        {
            return DB_Partcomplende.excluiPartComplende(objPartComplende, con);
        }
    }
}