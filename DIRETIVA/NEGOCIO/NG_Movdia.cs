using System;
using CLASSES;
using System.Collections.Generic;
using BANCO;

namespace NEGOCIO
{
    public class NG_Movdia
    {
        public List<CL_Movdia> buscaMov(DateTime dataI, DateTime dataF, string tipo, string con)
        {
            return DB_Movdia.buscaMov(dataI, dataF, tipo, con);
        }
    }
}