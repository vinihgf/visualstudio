using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Bancos
    {
        public List<CL_Bancos> listagemSimples(string con)
        {
            return DB_Bancos.listagemSimples(con);
        }
    }
}