using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Despesas
    {
        public static int buscaCod(string con)
        {
            return DB_Despesas.buscaCod(con);
        }

        public static bool cadDespesa(CL_Despesas objDespesa, string con)
        {
            return DB_Despesas.cadDespesa(objDespesa, con);
        }

        public static bool alterarDespesa(CL_Despesas objDespesa, string con)
        {
            return DB_Despesas.alterarDespesa(objDespesa, con);
        }

        public static bool excluirDespesa(CL_Despesas objDespesa, string con)
        {
            return DB_Despesas.excluirDespesa(objDespesa, con);
        }

        public static CL_Despesas buscaDespesa(string d_id, string con)
        {
            return DB_Despesas.buscaDespesa(d_id, con);
        }
    }
}