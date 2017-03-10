using System;
using System.Collections.Generic;
using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Sinistro
    {
        public static int buscaCodigo(string con)
        {
            return DB_Sinistro.buscaCodigo(con);
        }

        public static CL_Sinistro buscaSinistro(int codigo, string con)
        {
            return DB_Sinistro.buscaSinistro(codigo, con);
        }

        public static bool cadastraSinistro(CL_Sinistro objSinistro, string atualizaPerda, string con)
        {
            return DB_Sinistro.cadastraSinistro(objSinistro, atualizaPerda, con);
        }

        public static bool alteraSinistro(CL_Sinistro objSinistro, string atualizaPerda, string con)
        {
            return DB_Sinistro.alteraSinistro(objSinistro, atualizaPerda, con);
        }

        public static bool conferePermissaoConsulta(string email, string con)
        {
            return DB_Sinistro.conferePermissaoConsulta(email, con);
        }

        public static bool excluiSinistro(CL_Sinistro objSinistro, string atualizaPerda, string con)
        {
            return DB_Sinistro.excluiSinistro(objSinistro, atualizaPerda, con);
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Sinistro.conferePermissao(email, con);
        }

        public List<CL_Sinistro> listaPendencias(string con)
        {
            return DB_Sinistro.listaPendencias(con);
        }

        public List<CL_Sinistro> listar(string pesquisa, string filtro, string con)
        {
            return DB_Sinistro.listar(pesquisa, filtro, con);
        }
    }
}