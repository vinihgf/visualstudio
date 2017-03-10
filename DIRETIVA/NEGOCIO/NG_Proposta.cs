using System;
using CLASSES;
using BANCO;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Proposta
    {
        public static int buscaCodigo(string con)
        {
            return DB_Proposta.buscaCodigo(con);
        }

        public static CL_Proposta buscaProposta(int codigo, string con)
        {
            return DB_Proposta.buscaProposta(codigo, con);
        }

        public static string cadProposta(CL_Proposta objProposta, List<CL_MovProposta> objListMovProp, List<CL_Duprec> objListDuprec, string email, string con)
        {
            return DB_Proposta.cadProposta(objProposta, objListMovProp, objListDuprec, email, con);
        }

        public static bool alteraProposta(CL_Proposta objProposta, List<CL_MovProposta> objListMovProp, string con)
        {
            return DB_Proposta.alteraProposta(objProposta, objListMovProp, con);
        }

        public static bool excluiProposta(CL_Proposta objProposta, string con)
        {
            return DB_Proposta.excluiProposta(objProposta, con);
        }

        public static bool conferePermissaoRelatorio(string email, string con)
        {
            return DB_Proposta.conferePermissaoRelatorio(email, con);
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Proposta.conferePermissao(email, con);
        }

        public List<CL_Proposta> listarPendencias(string con)
        {
            return DB_Proposta.listarPendencias(con);
        }

        public List<CL_Proposta> listaDDL(string con)
        {
            return DB_Proposta.listaDDL(con);
        }

        public List<CL_Proposta> pesquisaProposta(string pesquisa, string filtro, string con)
        {
            return DB_Proposta.pesquisaProposta(pesquisa, filtro, con);
        }
    }
}