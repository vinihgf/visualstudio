using BANCO;
using CLASSES;
using System.Collections.Generic;
using System;

namespace NEGOCIO
{
    public class NG_Est
    {
        public static CL_Est buscaProd(string est_cod, string con)
        {
            return DB_Est.buscaProd(est_cod, con);
        }
        public List<CL_Est> listar(string pesq, string filtro, string con)
        {
            return DB_Est.listar(pesq, filtro, con);
        }
        public static bool somaEstOficin(string est_cod, double req_qtdade, string con)
        {
            return DB_Est.somaEstOficin(est_cod, req_qtdade, con);
        }
        public static bool subtEstOficin(string est_cod, double req_qtdade, string con)
        {
            return DB_Est.subtEstOficin(est_cod, req_qtdade, con);
        }
        public static int buscaCod(int est_cod, string con)
        {
            return DB_Est.buscaCod(est_cod, con);
        }
        public static bool cadEst(CL_Est objEst, string con)
        {
            return DB_Est.cadEst(objEst, con);
        }
        public static bool alteraEst(CL_Est objEst, string con)
        {
            return DB_Est.alteraEst(objEst, con);
        }
        public static bool excluiEst(CL_Est objEst, string con)
        {
            return DB_Est.excluiEst(objEst, con);
        }

        public List<CL_Est> listarApp(string situac, string con)
        {
            return DB_Est.listarApp(situac, con);
        }

        public static CL_Est buscaProdDGA(string est_cod, string con)
        {
            return DB_Est.buscaProdDGA(est_cod, con);
        }

        public static bool cadEstDGA(CL_Est objEst, string con)
        {
            return DB_Est.cadEstDGA(objEst, con);
        }

        public static bool alteraEstDGA(CL_Est objEst, string con)
        {
            return DB_Est.alteraEstDGA(objEst, con);
        }

        public static bool updateCodEst(string con)
        {
            return DB_Est.updateCodEst(con);
        }

        public static CL_Est buscaProdUmov(string est_cod, string con)
        {
            return DB_Est.buscaProdUmov(est_cod, con);
        }
    }
}