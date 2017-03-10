using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Composic
    {
        public static CL_Composic buscaCod(CL_Composic objComposic, string con)
        {
            return DB_Composic.buscaCod(objComposic, con);
        }

        public static CL_Composic buscaCodf(CL_Composic objComposic, string con)
        {
            if (objComposic.com_cod > 0)
            {
                return DB_Composic.buscaCodf(objComposic, con);
            }
            else
            {
                objComposic = null;
                return objComposic;
            }
        }

        public static CL_Composic buscaComposic(CL_Composic objComposic, string con)
        {
            if (objComposic.com_cod != 0)
            {
                return DB_Composic.buscaComposic(objComposic, con);
            }
            else
            {
                objComposic = null;
                return objComposic;
            }
        }

        public static CL_Composic buscaComposicf(CL_Composic objComposic, string con)
        {
            if (objComposic.com_codf != 0)
            {
                return DB_Composic.buscaComposicf(objComposic, con);
            }
            else
            {
                objComposic = null;
                return objComposic;
            }
        }

        public static bool cadComposic(CL_Composic objComposic, string con)
        {
            if (objComposic.com_cod > 0 && objComposic.com_codf > 0)
            {
                return DB_Composic.cadComposic(objComposic, con);
            }
            else
            {
                return false;
            }
        }

        public static bool alteraComposic(CL_Composic objComposic, string con)
        {
            if (objComposic.com_cod > 0 && objComposic.com_codf > 0)
            {
                return DB_Composic.alteraComposic(objComposic, con);
            }
            else
            {
                return false;
            }
        }

        public static bool excluiComposic(CL_Composic objComposic, string con)
        {
            if (objComposic.com_cod > 0 && objComposic.com_codf > 0)
            {
                return DB_Composic.excluiComposic(objComposic, con);
            }
            else
            {
                return false;
            }
        }

        public static List<CL_Composic> buscaGrupos(List<CL_Composic> objListGrupo, string con)
        {
            return DB_Composic.buscaGrupos(objListGrupo, con);
        }

        public static List<CL_Composic> buscaSubs(string con_grupo, string con)
        {
            if (con_grupo != "")
            {
                return DB_Composic.buscaSubs(con_grupo, con);
            }
            else
            {
                return null;
            }
        }
    }
}