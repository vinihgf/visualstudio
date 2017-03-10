using BANCO;
using CLASSES;
using System.Collections.Generic;
using System;

namespace NEGOCIO
{
    public class NG_Relatorios
    {
        public List<REL_Contrato> getRelatorioContrato(string contr, string con)
        {
            return DB_Relatorios.getRelatorioContrato(contr, con);
        }

        public List<REL_encerraRequis> getEncerraRequis(int req_cod, string con)
        {
            return DB_Relatorios.getEncerraRequis(req_cod, con);
        }

        public static REL_Protocolo getProtocolo(REL_Protocolo objRelProtocolo, string con)
        {
            return DB_Relatorios.getProtocolo(objRelProtocolo, con);
        }

        public static REL_OS7 getOS7(REL_OS7 objRelOS, string con)
        {
            return DB_Relatorios.getOS7(objRelOS, con);
        }

        public static List<CL_RelOsCli> getPesqOsCli(int clicod, int codend, string situac, DateTime dataI, DateTime dataF, string setor, string con)
        {
            return DB_Relatorios.getPesqOsCli(clicod, codend, situac, dataI, dataF, setor, con);
        }

        public List<CL_Proposta> getRelProposta(DateTime dataInicial, DateTime dataFinal, int particip, int parceiro, string con)
        {
            return DB_Relatorios.getRelProposta(dataInicial, dataFinal, particip, parceiro, con);
        }

        public static List<REL_Patrimon> getPatrimon(DateTime dataI, DateTime dataF, string patrimon, string servico, string situac, string con)
        {
            return DB_Relatorios.getPatrimon(dataI, dataF, patrimon, servico, situac, con);
        }

        public List<REL_Sinistro> getRelSinistro(DateTime dataI, DateTime dataF, int particip, string con)
        {
            return DB_Relatorios.getRelSinistro(dataI, dataF, particip, con);
        }
    }
}