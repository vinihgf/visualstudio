using System.Collections.Generic;
using CLASSES;
using BANCO;
using System;

namespace NEGOCIO
{
    public class NG_Movduprec
    {
        public List<CL_Movduprec> buscaMov(int dupcod, int dupparc, string con)
        {
            return DB_Movduprec.buscaMov(dupcod, dupparc, con);
        }

        public static bool recebeTotalMov(CL_Duprec objDuprec, CL_Movduprec objMovDuprec, CL_Movduprec objMovDupJuros, CL_ReciboDupl objRecibo, string con)
        {
            return DB_Movduprec.recebeTotalMov(objDuprec, objMovDuprec, objMovDupJuros, objRecibo, con);
        }

        public static int buscaCodigoRecibo(string con)
        {
            return DB_Movduprec.buscaCodigoRecibo(con);
        }
    }
}