using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Landesp
    {
        public static int buscaID(string con)
        {
            return DB_Landesp.buscaID(con);
        }

        public static bool excluiDesp(int l_id, string con)
        {
            return DB_Landesp.excluiDesp(l_id, con);
        }

        public static bool cadDesp(CL_Landesp objLandesp, string con)
        {
            return DB_Landesp.cadDesp(objLandesp, con);
        }

        public static bool alteraDesp(CL_Landesp objLandesp, string con)
        {
            return DB_Landesp.alteraDesp(objLandesp, con);
        }

        public List<CL_Landesp> pesquisa(DateTime data, string con)
        {
            return DB_Landesp.pesquisa(data, con);
        }

        public static CL_Landesp buscaDesp(int l_id, string con)
        {
            return DB_Landesp.buscaDesp(l_id, con);
        }
    }
}