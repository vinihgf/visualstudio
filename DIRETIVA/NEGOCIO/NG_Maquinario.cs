using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Maquinario
    {
        public static List<CL_Maquinario> listar(string con)
        {
            return DB_Maquinario.listar(con);
        }

        public static CL_Maquinario buscaMaquinario(string chassi, string con)
        {
            return DB_Maquinario.buscaMaquinario(chassi, con);
        }

        public static bool cadMaquinario(CL_Maquinario objMaquinario, string con)
        {
            return DB_Maquinario.cadMaquinario(objMaquinario, con);
        }

        public static bool alteraMaquinario(CL_Maquinario objMaquinario, string con)
        {
            return DB_Maquinario.alteraMaquinario(objMaquinario, con);
        }

        public static bool excluiMaquinario(CL_Maquinario objMaquinario, string con)
        {
            return DB_Maquinario.excluiMaquinario(objMaquinario, con);
        }
    }
}