using System;
using System.Collections.Generic;
using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Movest
    {
        public static bool ultMov(CL_Est objEst, string con)
        {
            return DB_Movest.ultMov(objEst, con);
        }

        public static List<CL_Movest> buscaMovest(string mov_nfisc, string con)
        {
            return DB_Movest.buscaMovest(mov_nfisc, con);
        }
    }
}