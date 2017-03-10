using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Lavoura
    {
        public static List<CL_Lavoura> listarLavoura(string con)
        {
            return DB_Lavoura.listarLavoura(con);
        }

        public static int buscaCodigo(int l_id, string con)
        {
            return DB_Lavoura.buscaCodigo(l_id, con);
        }

        public static CL_Lavoura buscaLavoura(CL_Lavoura objLavoura, string con)
        {
            return DB_Lavoura.buscaLavoura(objLavoura, con);
        }

        public static bool cadLavoura(CL_Lavoura objLavoura, string con)
        {
            return DB_Lavoura.cadLavoura(objLavoura, con);
        }

        public static bool alteraLavoura(CL_Lavoura objLavoura, string con)
        {
            return DB_Lavoura.alteraLavoura(objLavoura, con);
        }

        public static bool excluiLavoura(CL_Lavoura objLavoura, string con)
        {
            return DB_Lavoura.excluiLavoura(objLavoura, con);
        }
    }
}