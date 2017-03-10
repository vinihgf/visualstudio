using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Pluviometro
    {
        public static int buscaCodigo(int p_id, string con)
        {
            return DB_Pluviometro.buscaCodigo(p_id, con);
        }

        public static CL_Pluviometro buscaPluv(CL_Pluviometro objPluv, string con)
        {
            return DB_Pluviometro.buscaPluv(objPluv, con);
        }

        public static bool cadPluviometro(CL_Pluviometro objPluv, string con)
        {
            return DB_Pluviometro.cadPluviometro(objPluv, con);
        }

        public static bool alteraPluviometro(CL_Pluviometro objPluv, string con)
        {
            return DB_Pluviometro.alteraPluviometro(objPluv, con);
        }

        public static bool excluiPluviometro(CL_Pluviometro objPluv, string con)
        {
            return DB_Pluviometro.excluiPluviometro(objPluv, con);
        }
    }
}