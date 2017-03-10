using BANCO;
using CLASSES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEGOCIO
{
    public class NG_Locacao
    {
        public static int buscaCod(string con)
        {
            return DB_Locacao.buscaCod(con);
        }
        public static string buscaContr(string con)
        {
            return DB_Locacao.buscaContr(con);
        }
        public static bool incluiLocacao(CL_Locacao objLocacao, string con)
        {
            return DB_Locacao.incluiLocacao(objLocacao, con);
        }
        public List<CL_Locacao> listar(string con)
        {
            return DB_Locacao.listar(con);
        }
        public static List<CL_Locacao> buscaLocacao(int l_cod, string con)
        {
            return DB_Locacao.buscaLocacao(l_cod, con);
        }
        public static bool excluiLocacao(CL_Locacao objExcluiEquip, string con)
        {
            return DB_Locacao.excluiLocacao(objExcluiEquip, con);
        }
        public List<CL_Locacao> getRelatorio(int l_codigo, int l_contr, string con)
        {
            return DB_Locacao.getRelatorio(l_codigo, l_contr, con);
        }
        public static List<CL_Locacao> buscaLocacaoContr(int l_contr, string con)
        {
            return DB_Locacao.buscaLocacaoContr(l_contr, con);
        }
        public static bool encerraLocacao(int l_contr, string con, string l_ocor)
        {
            return DB_Locacao.encerraLocacao(l_contr, con, l_ocor);
        }
    }
}