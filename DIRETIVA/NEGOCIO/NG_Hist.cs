using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Hist
    {
        public static CL_Hist buscaHist(int his_cod, string con)
        {
            return DB_Hist.buscaHist(his_cod, con);
        }
    }
}