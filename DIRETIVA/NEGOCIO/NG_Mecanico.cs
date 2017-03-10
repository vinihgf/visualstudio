using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Mecanico
    {
        public static int buscaCod(int mec_cod, string con)
        {
            return DB_Mecanico.buscaCod(mec_cod, con);
        }

        public List<CL_Mecanico> listar(string con)
        {
            return DB_Mecanico.listar(con);
        }

        public static CL_Mecanico buscaMec(string mec_cod, string con)
        {
            return DB_Mecanico.buscaMec(mec_cod, con);
        }

        public static bool cadMec(CL_Mecanico objMecanico, string con)
        {
            return DB_Mecanico.cadMec(objMecanico, con);
        }

        public static bool alteraMec(CL_Mecanico objMecanico, string con)
        {
            return DB_Mecanico.alteraMec(objMecanico, con);
        }

        public static bool excluiMec(CL_Mecanico objMecanico, string con)
        {
            return DB_Mecanico.excluiMec(objMecanico, con);
        }

        public static bool alteraSenha(CL_Mecanico objMecanico, string con)
        {
            return DB_Mecanico.alteraSenha(objMecanico, con);
        }
    }
}