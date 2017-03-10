using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_ComlMarca
    {
        public static int buscaCod(int m_cod, string con)
        {
            return DB_ComlMarca.buscaCod(m_cod, con);
        }

        public static CL_ComlMarca buscaMarca(CL_ComlMarca objComlMarca, string con)
        {
            if (objComlMarca.m_codigo == 0)
            {
                objComlMarca = null;
                return objComlMarca;
            }
            else
            {
                return DB_ComlMarca.buscaMarca(objComlMarca, con);
            }
        }

        public static bool cadMarca(CL_ComlMarca objComlMarca, string con)
        {
            if (objComlMarca.m_codigo > 0 && objComlMarca.m_nome != "")
            {
                return DB_ComlMarca.cadMarca(objComlMarca, con);
            }
            else
            {
                return false;
            }
        }

        public static bool alteraMarca(CL_ComlMarca objComlMarca, string con)
        {
            if (objComlMarca.m_codigo > 0 && objComlMarca.m_nome != "")
            {
                return DB_ComlMarca.alteraMarca(objComlMarca, con);
            }
            else
            {
                return false;
            }
        }

        public static bool excluiMarca(CL_ComlMarca objComlMarca, string con)
        {
            if (objComlMarca.m_codigo > 0 && objComlMarca.m_nome != "")
            {
                return DB_ComlMarca.excluiMarca(objComlMarca, con);
            }
            else
            {
                return false;
            }
        }

        public List<CL_ComlMarca> listar(string con)
        {
            return DB_ComlMarca.listar(con);
        }
    }
}