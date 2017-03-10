using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Pedido
    {
        public static int buscaCod(int p_cod, string con)
        {
            return DB_Pedido.buscaCod(p_cod, con);
        }

        public static bool cadPedido(CL_Pedido objPedido, string con)
        {
            return DB_Pedido.cad_Pedido(objPedido, con);
        }

        public static bool conferePedidoApp(long p_idumov, string con)
        {
            return DB_Pedido.conferePedidoApp(p_idumov, con);
        }

        public static CL_Pedido buscaPedidoIDUmov(int idUmov, string con)
        {
            return DB_Pedido.buscaPedidoIDUmov(idUmov, con);
        }
    }
}