using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Tribut
    {
        public static CL_Tribut buscaTribut(int tri_cod, string con)
        {
            return DB_Tribut.buscaTribut(tri_cod, con);
        }

        public List<CL_Tribut> listar(string tribut, string filtroPesq, string con)
        {
            return DB_Tribut.listar(tribut, filtroPesq, con);
        }

        public static int buscaCod(int tri_cod, string con)
        {
            return DB_Tribut.buscaCod(tri_cod, con);
        }

        public static bool cadTribut(CL_Tribut objTribut, string con)
        {
            return DB_Tribut.cadTribut(objTribut, con);
        }

        public static bool alteraTribut(CL_Tribut objTribut, string con)
        {
            return DB_Tribut.alteraTribut(objTribut, con);
        }

        public static bool excluiTribut(CL_Tribut objTribut, string con)
        {
            return DB_Tribut.excluiTribut(objTribut, con);
        }
    }
}