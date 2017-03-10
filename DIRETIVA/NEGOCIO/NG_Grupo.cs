using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Grupo
    {
        public static CL_Grupo buscaGrupo(string cfop, string con)
        {
            return DB_Grupo.buscaGrupo(cfop, con);
        }
    }
}