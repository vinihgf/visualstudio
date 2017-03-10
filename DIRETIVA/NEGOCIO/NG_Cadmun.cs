using BANCO;

namespace NEGOCIO
{
    public class NG_Cadmun
    {
        public static bool verificaCidade(string cidade, string con)
        {
            return DB_Cadmun.verificaCidade(cidade, con);
        }
    }
}