using BANCO;
using CLASSES;
using System.Collections.Generic;
using System;

namespace NEGOCIO
{
    public class NG_Requis
    {
        public static int buscaCodRequis(int req_cod, string con)
        {
            return DB_Requis.buscaCodRequis(req_cod, con);
        }
        public List<CL_Requis> buscaRequis(CL_Requis objRequis, string con)
        {
            return DB_Requis.buscaRequis(objRequis, con);
        }
        public List<CL_Requis> listar(string pesq, string con, string filtroPesq)
        {
            return DB_Requis.listar(pesq, con, filtroPesq);
        }
        public static bool incluiRequis(CL_Requis objRequis, string con)
        {
            return DB_Requis.incluiRequis(objRequis, con);
        }
        public static int buscaRecno(string con)
        {
            return DB_Requis.buscaRecno(con);
        }
        public static bool excluiRequis(int req_cod, string con)
        {
            return DB_Requis.excluiRequis(req_cod, con);
        }
        public static bool alteraRequis(List<CL_Requis> objListRequisC, string con)
        {
            return DB_Requis.alteraRequis(objListRequisC, con);
        }
        public static List<CL_Requis> BuscaRequis(int os_cod, string con)
        {
            CL_Requis objRequis = new CL_Requis();
            objRequis.req_cod = os_cod;
            return DB_Requis.buscaRequis(objRequis, con);
        }
        public static bool encerraRequis(CL_Requis objRequis, string con)
        {
            if (objRequis.req_cod > 0)
            {
                return DB_Requis.encerraRequis(objRequis, con);
            }
            else
            {
                return false;
            }
        }

        public static bool excluiItemRequis(CL_Requis objRemoveRequis, string con)
        {
            return DB_Requis.excluiItemRequis(objRemoveRequis, con);
        }

        public static List<CL_Requis> buscaRequisOserv(int o_serv, int particip, string con)
        {
            return DB_Requis.buscaRequisOserv(o_serv, particip, con);
        }

        public static bool attDadosRequis(List<CL_Requis> objListRequisRetorno, string con)
        {
            return DB_Requis.attDadosRequis(objListRequisRetorno, con);
        }

        public static double totalPecas(int o_cod, string con)
        {
            return DB_Requis.totalPecas(o_cod, con);
        }

        public static double qtdFat(int o_cod, string con)
        {
            return DB_Requis.qtdFat(o_cod, con);
        }

        public static double valorservico(int o_cod, string con)
        {
            return DB_Requis.valorservico(o_cod, con);
        }
    }
}