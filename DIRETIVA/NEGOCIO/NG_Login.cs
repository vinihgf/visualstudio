using BANCO;
using CLASSES;

namespace NEGOCIO
{
    public class NG_Login
    {
        public static string validaLogin(CL_Login objLogin)
        {
            return DB_Login.validaLogin(objLogin);
        }
        public static CL_Login buscaSenha(CL_Login objLogin)
        {
            return DB_Login.buscaSenha(objLogin);
        }
        public static string validaLoginFlavio(CL_Login objLogin)
        {
            return DB_Login.validaLoginFlavio(objLogin);
        }
    }
}