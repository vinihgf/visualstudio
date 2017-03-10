using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Veiculos
    {
        public static bool excluiVeiculo(CL_Veiculos objVeiculos, string con)
        {
            return DB_Veiculos.excluiVeiculo(objVeiculos, con);
        }

        public static bool incluiVeiculo(CL_Veiculos objVeiculos, string con)
        {
            return DB_Veiculos.incluiVeiculo(objVeiculos, con);
        }

        public static bool alteraVeiculo(CL_Veiculos objVeiculos, string con)
        {
            return DB_Veiculos.alteraVeiculo(objVeiculos, con);
        }
        public static CL_Veiculos verificaVeiculo(CL_Veiculos objVeiculos, string con)
        {
            return DB_Veiculos.verificaVeiculos(objVeiculos, con);
        }
        public List<CL_Veiculos> listar(string pesquisa, string con, string filtroPesq)
        {
            return new DB_Veiculos().listar(pesquisa, con, filtroPesq);
        }

        public static CL_Veiculos buscaParticip(CL_Veiculos objVeiculos, string con)
        {
            return DB_Veiculos.buscaParticip(objVeiculos, con);
        }
    }
}