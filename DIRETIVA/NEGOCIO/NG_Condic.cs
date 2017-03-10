using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Condic
    {
        public static CL_Condic buscaCondic(CL_Condic objCondic, string con)
        {
            if (objCondic.c_cod > 0)
            {
                return DB_Condic.buscaCondic(objCondic, con);
            }
            else
            {
                objCondic = null;
                return objCondic;
            }
        }

        public static List<CL_Condic> listar(List<CL_Condic> objListCondic, string con)
        {
            return DB_Condic.listar(objListCondic, con);
        }

        public static int buscaCod(int c_cod, string con)
        {
            return DB_Condic.buscaCod(c_cod, con);
        }

        public static bool cadCondic(CL_Condic objCondic, string con)
        {
            return DB_Condic.cadCondic(objCondic, con);
        }

        public static bool alterarCondic(CL_Condic objCondic, string con)
        {
            return DB_Condic.alterarCondic(objCondic, con);
        }

        public static bool excluirCondic(CL_Condic objCondic, string con)
        {
            return DB_Condic.excluirCondic(objCondic, con);
        }
    }
}