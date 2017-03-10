using System;
using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Entrega
    {
        public List<CL_Entrega> buscaEntregaPeriodo(DateTime dataI, DateTime dataF, string coluna, string ordem, string cidade, string con)
        {
            return DB_Entrega.buscaEntregaPeriodo(dataI, dataF, coluna, ordem, cidade, con);
        }

        public static bool attEntregador(int e_id, int e_idEntregador, string con)
        {
            return DB_Entrega.attEntregador(e_id, e_idEntregador, con);
        }

        public static bool confereMovEntrega(int idUmov, string con)
        {
            return DB_Entrega.confereMovEntrega(idUmov, con);
        }

        public static CL_SincrEntrega buscaMovEntrega(int idUmov, string con)
        {
            return DB_Entrega.buscaMovEntrega(idUmov, con);
        }

        public static bool gravaMovEntrega(CL_SincrEntrega objSincrEntrega, string con)
        {
            return DB_Entrega.gravaMovEntrega(objSincrEntrega, con);
        }

        public static bool attStatus(long e_id, string e_status, DateTime e_data, string e_situac, string con)
        {
            return DB_Entrega.attStatus(e_id, e_status, e_data, e_situac, con);
        }

        public static int buscaNrTentativas(long e_identreg, string con)
        {
            return DB_Entrega.buscaNrTentativas(e_identreg, con);
        }

        public List<CL_Entrega> consultaPgtoAwb(DateTime dataI, DateTime dataF, string modelo, string status, string entregador, string con)
        {
            return DB_Entrega.consultaPgtoAwb(dataI, dataF, modelo, status, entregador, con);
        }

        public List<CL_SincrEntrega> buscaMovEntregasPeriodo(DateTime dataI, DateTime dataF, string coluna, string con)
        {
            return DB_Entrega.buscaMovEntregaPeriodo(dataI, dataF, coluna, con);
        }

        public static List<CL_Entrega> buscaLocalizParticip(DateTime dataI, DateTime dataF, string con)
        {
            return DB_Entrega.buscaLocalizParticip(dataI, dataF, con);
        }

        public static long buscaIDUltimaSincr(string con)
        {
            return DB_Entrega.buscaIDUltimaSincr(con);
        }

        public List<CL_Entrega> consultaPontualidade(DateTime dataI, DateTime dataF, string cidade, string situac, string entregador, string con)
        {
            return DB_Entrega.consultaPontualidade(dataI, dataF, cidade, situac, entregador, con);
        }

        public static int buscaID(string con)
        {
            return DB_Entrega.buscaID(con);
        }

        public List<CL_Entrega> buscaRoteirizacao(string entregador, DateTime dataI, DateTime dataF, string con)
        {
            return DB_Entrega.buscaRoteirizacao(entregador, dataI, dataF, con);
        }

        public static bool cadEntrega(CL_Entrega objEntrega, string token, string post, string sql, string con)
        {
            return DB_Entrega.cadEntrega(objEntrega, token, post, sql, con);
        }
    }
}