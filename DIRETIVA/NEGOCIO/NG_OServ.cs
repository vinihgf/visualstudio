using BANCO;
using CLASSES;
using System;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_OServ
    {
        public static long buscaCod(long o_cod, string con)
        {
            return DB_OServ.buscaCod(o_cod, con);
        }
        public static bool cadOserv(CL_Oserv objOserv, string con)
        {
            return DB_OServ.cadOserv(objOserv, con);
        }
        public static bool alterarOserv(CL_Oserv objOserv, string con)
        {
            return DB_OServ.alterarOserv(objOserv, con);
        }
        public static bool excluiOserv(CL_Oserv objOserv, string token, string arquivo, string post, string con)
        {
            return DB_OServ.excluiOserv(objOserv, token, arquivo, post, con);
        }
        public static CL_Oserv buscaOserv(CL_Oserv objOserv, string con)
        {
            return DB_OServ.buscaOserv(objOserv, con);
        }
        public List<CL_Oserv> listar(string pesq, string con, string filtroPesq, string situac)
        {
            return DB_OServ.listar(pesq, con, filtroPesq, situac);
        }

        public List<CL_Oserv> pesqOservSincr(int sincr_cod, string con, string situac)
        {
            return DB_OServ.pesqOservSincr(sincr_cod, con, situac);
        }

        public static bool attDadosOS(CL_SincrOserv objSincr, string con)
        {
            return DB_OServ.attDadosOS(objSincr, con);
        }

        public List<CL_Oserv> listarPeriodo(DateTime dataI, DateTime dataF, string situac, string clicod, int mecanico, int codend, string con)
        {
            return DB_OServ.listarPeriodo(dataI, dataF, situac, clicod, mecanico, codend, con);
        }

        public static bool cadOservEApp(CL_Oserv objOserv, string postData, string token, CL_Requis objRequis, string con)
        {
            return DB_OServ.cadOservEApp(objOserv, postData, token, objRequis, con);
        }

        public static bool alterarOservEApp(CL_Oserv objOserv, string postData, string token, CL_Requis objRequis, string con, string situac)
        {
            return DB_OServ.alterarOservEApp(objOserv, postData, token, objRequis, con, situac);
        }

        public List<CL_Oserv> relProdutividade(DateTime dataI, DateTime dataF, string tecnico, string con)
        {
            return DB_OServ.relProdutividade(dataI, dataF, tecnico, con);
        }
    }
}