using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_CicloProducao
    {
        public static int buscaCod(string con)
        {
            return DB_CicloProducao.buscaCod(con);
        }

        public static List<CL_CicloProducao> listar(string con)
        {
            return DB_CicloProducao.listar(con);
        }

        public static bool cadProducao(CL_CicloProducao objProducao, string con)
        {
            return DB_CicloProducao.cadProducao(objProducao, con);
        }

        public static bool alterarProducao(CL_CicloProducao objProducao, string con)
        {
            return DB_CicloProducao.alteraProducao(objProducao, con);
        }

        public static bool excluirProducao(CL_CicloProducao objProducao, string con)
        {
            return DB_CicloProducao.excluirProducao(objProducao, con);
        }

        public static CL_CicloProducao buscaProducao(int p_id, string con)
        {
            return DB_CicloProducao.buscaProducao(p_id ,con);
        }
    }
}