using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_TipoMov
    {
        public static CL_TipoMov buscaTipo(int cod, string con)
        {
            return DB_TipoMov.buscaTipo(cod, con);
        }

        public List<CL_TipoMov> buscaTipos(string con)
        {
            return DB_TipoMov.buscaTipos(con);
        }

        public static int buscaCod(string con)
        {
            return DB_TipoMov.buscaCod(con);
        }

        public static bool cadTipo(CL_TipoMov objTipo, string con)
        {
            return DB_TipoMov.cadTipo(objTipo, con);
        }

        public static bool alterarTipo(CL_TipoMov objTipo, string con)
        {
            return DB_TipoMov.alterar(objTipo, con);
        }

        public static bool excluirTipo(CL_TipoMov objTipo, string con)
        {
            return DB_TipoMov.excluitTipo(objTipo, con);
        }
    }
}