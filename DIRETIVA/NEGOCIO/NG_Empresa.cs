using System;
using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Empresa
    {
        public static bool alteraEmpresa(CL_Empresa objEmpresa, string con)
        {
            return DB_Empresa.alteraEmpresa(objEmpresa, con);
        }

        public CL_Empresa buscaEmpresa(string con)
        {
            return DB_Empresa.buscaEmpresa(con);
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Empresa.conferePermissao(email, con);
        }
    }
}