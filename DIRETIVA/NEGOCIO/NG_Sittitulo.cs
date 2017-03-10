using System;
using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Sittitulo
    {
        public static int buscaCod(string con)
        {
            return DB_Sittitulo.buscaCod(con);
        }

        public List<CL_Sittitulo> buscaSituacoes(string con)
        {
            return DB_Sittitulo.buscaSituacoes(con);
        }

        public static bool cadSit(CL_Sittitulo objSit, string con)
        {
            return DB_Sittitulo.cadSit(objSit, con);
        }

        public static bool alteraSit(CL_Sittitulo objSit, string con)
        {
            return DB_Sittitulo.alteraSit(objSit, con);
        }

        public static bool excluiSit(CL_Sittitulo objSit, string con)
        {
            return DB_Sittitulo.excluiSit(objSit, con);
        }

        public static CL_Sittitulo buscaSit(int cod, string con)
        {
            return DB_Sittitulo.buscaSit(cod, con);
        }
        public List<CL_Sittitulo> listagemSimples(string con)
        {
            return DB_Sittitulo.listagemSimples(con);
        }
    }
}