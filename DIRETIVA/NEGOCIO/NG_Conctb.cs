using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Conctb
    {
        public static CL_Conctb buscaConctb(string con_cod, string con)
        {
            return DB_Conctb.buscaConctb(con_cod, con);
        }
    }
}