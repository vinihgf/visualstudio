using BANCO;
using CLASSES;
using System.Collections.Generic;
using System;

namespace NEGOCIO
{
    public class NG_Usudac
    {
        public static int buscaCodUsudac(int u_codigo, string con)
        {
            return DB_Usudac.buscaCodUsudac(u_codigo, con);
        }
        public static CL_Usudac buscaUsudac(string u_codigo, string con)
        {
            return DB_Usudac.buscaUsudac(u_codigo, con);
        }
        public static bool incluiUsudac(CL_Usudac objUsudac, string con)
        {
            return DB_Usudac.incluiUsudac(objUsudac, con);
        }
        public static bool alteraUsudac(CL_Usudac objUsudac, string con)
        {
            return DB_Usudac.alteraUsudac(objUsudac, con);
        }

        public static CL_Usudac buscaUsudacEmail(string email, string con)
        {
            return DB_Usudac.buscaUsudacEmail(email, con);
        }

        public static bool alteraSenhaUsudac(CL_EsqueciSenha objEsqueciSenha, string con)
        {
            return DB_Usudac.alteraSenhaUsudac(objEsqueciSenha, con);
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Usudac.conferePermissao(email, con);
        }

        public static bool excluiUsudac(CL_Usudac objUsudac, string con)
        {
            return DB_Usudac.excluiUsudac(objUsudac, con);
        }
        public List<CL_Usudac> listar(string con)
        {
            return new DB_Usudac().listar(con);
        }
    }
}