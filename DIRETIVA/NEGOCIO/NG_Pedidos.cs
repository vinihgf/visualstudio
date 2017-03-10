using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Pedidos
    {
        public static bool cadPedidos(List<CL_Pedidos> objListPed, string con)
        {
            return DB_Pedidos.cadPedidos(objListPed, con);
        }

        public static List<CL_Pedidos> buscaPedidos(int p_cod, string con)
        {
            return DB_Pedidos.buscaPedidos(p_cod, con);
        }

        public static bool attCondicPed(CL_Pedidos objPedidosCondic, string con)
        {
            return DB_Pedidos.attCondicPed(objPedidosCondic, con);
        }
    }
}