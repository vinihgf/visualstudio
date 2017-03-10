using System;
using System.Collections.Generic;
using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Stribest
    {
        public static CL_Stribest buscaStribest(int tribut, string estorg, string estdst, string con)
        {
            return DB_Stribest.buscaStribest(tribut, estorg, estdst, con);
        }

        public static int buscaCod(string con)
        {
            return DB_Stribest.buscaCod(con);
        }

        public static List<CL_Stribest> listar(string con)
        {
            return DB_Stribest.listar(con);
        }

        public static CL_Stribest buscaStribestCod(int s_cod, string con)
        {
            return DB_Stribest.buscaStribestCod(s_cod, con);
        }

        public static bool cadParametro(CL_Stribest objStribest, string con)
        {
            return DB_Stribest.cadParametro(objStribest, con);
        }

        public static bool alteraParametro(CL_Stribest objStribest, string con)
        {
            return DB_Stribest.alteraParametro(objStribest, con);
        }

        public static bool excluiParametro(CL_Stribest objStribest, string con)
        {
            return DB_Stribest.excluiParametro(objStribest, con);
        }
    }
}