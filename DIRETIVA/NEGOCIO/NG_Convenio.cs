using BANCO;
using CLASSES;
using System.Collections.Generic;
using System;
using System.Web;

namespace NEGOCIO
{
    public class NG_Convenio
    {
        public static CL_Convenio buscaConvenio(string con_cod, string con)
        {
            return DB_Convenio.buscaConvenio(con_cod, con);
        }
        public static int buscaCod(int con_cod, string con)
        {
            return DB_Convenio.buscaCod(con);
        }
        public static bool cadConvenio(CL_Convenio objConvenio, string con)
        {
            if (objConvenio.con_cod > 0)
                return DB_Convenio.cadConvenio(objConvenio, con);
            else
                return false;
        }
        public static bool alteraConvenio(CL_Convenio objConvenio, string con)
        {
            if (objConvenio.con_cod > 0)
                return DB_Convenio.alteraConvenio(objConvenio, con);
            else
                return false;
        }
        public static bool excluiConvenio(CL_Convenio objConvenio, string con)
        {
            if (objConvenio.con_cod > 0)
                return DB_Convenio.excluiConvenio(objConvenio, con);
            else
                return false;
        }

        public static bool conferePermissao(string email, string con)
        {
            return DB_Convenio.conferePermissao(email, con);
        }

        public static List<CL_Convenio> listar(List<CL_Convenio> objListCon, string con)
        {
            return DB_Convenio.listar(objListCon, con);
        }
        public static CL_Convenio buscaConSenha(CL_Convenio objConvenio, string con)
        {
            return DB_Convenio.buscaConSenha(objConvenio, con);
        }

        public static List<CL_Convenio> listarApp(string con)
        {
            return DB_Convenio.listarApp(con);
        }
    }
}