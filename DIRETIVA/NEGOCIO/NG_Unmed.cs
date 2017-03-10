using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Unmed
    {
        public static bool cadUnmed(CL_Unmed objUnmed, string con)
        {
            return DB_Unmed.cadUnmed(objUnmed, con);
        }

        public static bool alteraUnmed(CL_Unmed objUnmed, string con)
        {
            return DB_Unmed.alteraUnmed(objUnmed, con);
        }

        public static bool excUnmed(CL_Unmed objUnmed, string con)
        {
            return DB_Unmed.excUnmed(objUnmed, con);
        }

        public static CL_Unmed buscaUnmed(CL_Unmed objUnmed, string con)
        {
            return DB_Unmed.buscaUnmed(objUnmed, con);
        }

        public static List<CL_Unmed> listar(List<CL_Unmed> objListUnmed, string con)
        {
            return DB_Unmed.listar(objListUnmed, con);
        }
    }
}