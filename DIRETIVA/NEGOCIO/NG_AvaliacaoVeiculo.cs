using System;
using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_AvaliacaoVeiculo
    {
        public static bool confereAvaliacao(int idUmov, string con)
        {
            return DB_AvaliacaoVeiculo.confereAvaliacao(idUmov, con);
        }

        public static int buscaCod(string con)
        {
            return DB_AvaliacaoVeiculo.buscaCod(con);
        }

        public static CL_AvaliacaoVeiculo buscaAvaliacaoIDUmov(int idUmov, string con)
        {
            return DB_AvaliacaoVeiculo.buscaAvaliacaoIDUmov(idUmov, con);
        }

        public static bool cadAvaliacao(CL_AvaliacaoVeiculo objAvaliacao, string con)
        {
            return DB_AvaliacaoVeiculo.cadAvaliacao(objAvaliacao, con);
        }
    }
}