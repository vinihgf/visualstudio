using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_OS
    {
        public static int buscaOsCod(int os_cod, string con)
        {
            return DB_OS.buscaOsCod(os_cod, con);
        }

        public static CL_OS buscaOS(int os_cod, string con)
        {
            return DB_OS.buscaOS(os_cod, con);
        }

        public List<CL_OS> listar(string pesquisa, string con, string filtro)
        {
            return DB_OS.listar(pesquisa, con, filtro);
        }

        public static bool cadOs(CL_OS objOS, string con)
        {
            return DB_OS.cadOs(objOS, con);
        }

        public static bool alteraOS(CL_OS objOS, string con)
        {
            return DB_OS.alteraOS(objOS, con);
        }

        public static bool excluiOS(CL_OS objOs, string con)
        {
            return DB_OS.excluiOS(objOs, con);
        }

        public static List<CL_OS> sincroOfic(List<CL_OS> objListOS, string con)
        {
            return DB_OS.sincroOfic(objListOS, con);
        }
        public static bool encerraOS(CL_OS objOS, string con)
        {
            if (objOS.os_cod > 0)
            {
                return DB_OS.encerraOS(objOS, con);
            }
            else
            {
                return false;
            }
        }
    }
}