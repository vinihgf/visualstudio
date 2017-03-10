using System;
using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_RotaCidade
    {
        public static int buscaID(string con)
        {
            return DB_RotaCidade.buscaID(con);
        }

        public List<CL_RotaCidade> buscaRotasEntregadores(string identreg, string con)
        {
            return DB_RotaCidade.buscaRotasEntregadores(identreg, con);
        }

        public static bool cadRota(CL_RotaCidade objRotaCidade, string con)
        {
            return DB_RotaCidade.cadRota(objRotaCidade, con);
        }

        public static bool alteraRota(CL_RotaCidade objRotaCidade, string con)
        {
            return DB_RotaCidade.alteraRota(objRotaCidade, con);
        }

        public static bool excluiRota(CL_RotaCidade objRotaCidade, string con)
        {
            return DB_RotaCidade.excluirRota(objRotaCidade, con);
        }

        public static CL_RotaCidade buscaRota(int r_id, string con)
        {
            return DB_RotaCidade.buscaRota(r_id, con);
        }

        public List<CL_RotaCidade> listar(string con)
        {
            return DB_RotaCidade.listar(con);
        }
    }
}