using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_ComlModelo
    {
        public static int buscaCod(int m_cod, string con)
        {
            return DB_ComlModelo.buscaCod(m_cod, con);
        }
        public static CL_ComlModelo buscaModelo(CL_ComlModelo objComlModelo, string con)
        {
            if (objComlModelo.m_codigo > 0)
            {
                return DB_ComlModelo.buscaModelo(objComlModelo, con);
            }
            else
            {
                return objComlModelo;
            }
        }

        public static bool cadModelo(CL_ComlModelo objComlModelo, string con)
        {
            if (objComlModelo.m_codigo > 0 && objComlModelo.m_marca > 0)
            {
                return DB_ComlModelo.cadModelo(objComlModelo, con);
            }
            else
            {
                return false;
            }
        }

        public static bool alteraModelo(CL_ComlModelo objComlModelo, string con)
        {
            if (objComlModelo.m_codigo > 0 && objComlModelo.m_marca > 0)
            {
                return DB_ComlModelo.alteraModelo(objComlModelo, con);
            }
            else
            {
                return false;
            }
        }

        public static bool excluiModelo(CL_ComlModelo objComlModelo, string con)
        {
            if (objComlModelo.m_codigo > 0 && objComlModelo.m_marca > 0)
            {
                return DB_ComlModelo.excluiModelo(objComlModelo, con);
            }
            else
            {
                return false;
            }
        }

        public List<CL_ComlModelo> listar(string con, string marca)
        {
            return DB_ComlModelo.listar(con, marca);
        }
    }
}