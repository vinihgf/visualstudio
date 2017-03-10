using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_MovProposta
    {
        public List<CL_MovProposta> buscaProposta(int p_id, string con)
        {
            return DB_MovProposta.buscaProposta(p_id, con);
        }
    }
}