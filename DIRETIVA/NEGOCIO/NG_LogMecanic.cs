using System;
using CLASSES;
using BANCO;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_LogMecanic
    {
        public static bool gravaLog(CL_LogMecanic objLogMecanic, string con)
        {
            return DB_LogMecanic.gravaLog(objLogMecanic, con);
        }

        public static bool verificaLog(int obj, string con)
        {
            return DB_LogMecanic.verificaLog(obj, con);
        }

        public static CL_LogMecanic buscaLogMecanic(int obj, string con)
        {
            return DB_LogMecanic.buscaLogMecanic(obj, con);
        }

        public static int buscaID(string con)
        {
            return DB_LogMecanic.buscaID(con);
        }

        public static List<CL_LogMecanic> buscaLog(CL_LogMecanic objLog, string con)
        {
            throw new NotImplementedException();
        }
    }
}