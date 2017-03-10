using System;
using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Duprec
    {
        public List<CL_Duprec> buscaDuprec(int p_id, string con)
        {
            return DB_Duprec.buscaDuprec(p_id, con);
        }

        public static int buscaCodigo(string con)
        {
            return DB_Duprec.buscaCodigo(con);
        }

        public List<CL_Duprec> listaDuprec(string situac, int p_clicod, DateTime dataI, DateTime dataF, string con)
        {
            return DB_Duprec.listaDuprec(situac, p_clicod, dataI, dataF, con);
        }

        public static bool recebeDupCliente(List<CL_Duprec> objListDuprec, List<CL_Movduprec> objListMovDup, List<CL_Movduprec> objListMovJuros, string con)
        {
            return DB_Duprec.recebeDupCliente(objListDuprec, objListMovDup, objListMovJuros, con);
        }
        public static bool confereTitulo(int dupcod, int dupparc, string con)
        {
            return DB_Duprec.confereTitulo(dupcod, dupparc, con);
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Duprec.conferePermissao(email, con);
        }

        public static bool cadDuprec(CL_Duprec objDuprec, string con)
        {
            if (objDuprec.dup_cod > 0 && objDuprec.dup_parc > 0)
            {
                return DB_Duprec.cadDuprec(objDuprec, con);
            }
            else
            {
                return false;
            }
        }

        public static CL_Duprec buscaTituloTotMov(int dupcod, int dupparc, string con)
        {
            return DB_Duprec.buscaTituloTotMov(dupcod, dupparc, con);
        }

        public List<CL_Duprec> listaPendencias(DateTime data, string con)
        {
            return DB_Duprec.listaPendencias(data, con);
        }

        public static CL_Duprec buscaTitulo(int dupcod, int dupparc, string con)
        {
            return DB_Duprec.buscaTitulo(dupcod, dupparc, con);
        }

        public static bool alteraDuprec(CL_Duprec objDuprec, string email, string con)
        {
            if (objDuprec.dup_cod > 0 && objDuprec.dup_parc > 0)
            {
                return DB_Duprec.alteraDuprec(objDuprec, email, con);
            }
            else
            {
                return false;
            }
        }

        public static bool excluiDuprec(CL_Duprec objDuprec, string email, string con)
        {
            if (objDuprec.dup_cod > 0 && objDuprec.dup_parc > 0)
                return DB_Duprec.excluiDuprec(objDuprec, email, con);
            else
                return false;
        }

        public List<CL_Duprec> buscaDupCliente(int p_clicod, string con)
        {
            return DB_Duprec.buscaDupCliente(p_clicod, con);
        }

        public static List<CL_Duprec> buscaReciboImprimir(List<CL_Duprec> objListDuprec, string con)
        {
            return DB_Duprec.buscaReciboImprimir(objListDuprec, con);
        }
    }
}