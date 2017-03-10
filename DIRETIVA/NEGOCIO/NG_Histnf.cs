using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Histnf
    {
        public static CL_Histnf buscaHist(int cod, string con)
        {
            return DB_Histnf.buscaHist(cod, con);
        }
    }
}