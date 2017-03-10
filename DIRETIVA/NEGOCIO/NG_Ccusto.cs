using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Ccusto
    {
        public static int buscaCodigo(int c_id, string con)
        {
            return DB_Ccusto.buscaCodigo(c_id, con);
        }

        public List<CL_Ccusto> listar(string con)
        {
            return DB_Ccusto.listar(con);
        }

        public static bool cadCcusto(CL_Ccusto objCcusto, string con)
        {
            return DB_Ccusto.cadCcusto(objCcusto, con);
        }

        public static bool alteraCcusto(CL_Ccusto objCcusto, string con)
        {
            return DB_Ccusto.alteraCcusto(objCcusto, con);
        }

        public static bool excluiCcusto(CL_Ccusto objCcusto, string con)
        {
            return DB_Ccusto.excluiCcusto(objCcusto, con);
        }

        public static CL_Ccusto buscaCcusto(CL_Ccusto objCcusto, string con)
        {
            return DB_Ccusto.buscaCcusto(objCcusto, con);
        }

    }
}