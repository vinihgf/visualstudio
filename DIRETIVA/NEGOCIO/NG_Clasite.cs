using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Clasite
    {
        public static CL_Clasite buscaClasite(string ncm, string con)
        {
            return DB_Clasite.buscaClasite(ncm, con);
        }

        public List<CL_Clasite> listar(string ncm, string con)
        {
            return DB_Clasite.listar(ncm, con);
        }
    }
}