using System.Collections.Generic;
using CLASSES;
using BANCO;

namespace NEGOCIO
{
    public class NG_Equipamento
    {
        public static int buscaCod(int e_cod, string con)
        {
            return DB_Equipamento.buscaCod(e_cod, con);
        }
        public static CL_Equipamento buscaEquip(CL_Equipamento objEquipamento, string con)
        {
            return DB_Equipamento.buscaEquip(objEquipamento, con);
        }
        public List<CL_Equipamento> listar(CL_ComlModelo objComlModelo, string con)
        {
            if (objComlModelo.m_codigo > 0 && objComlModelo.m_marca > 0)
            {
                return DB_Equipamento.listar(objComlModelo, con);
            }
            else
            {
                return null;
            }

        }
        public static bool cadEquip(CL_Equipamento objEquipamento, string con)
        {
            return DB_Equipamento.cadEquip(objEquipamento, con);
        }
        public static bool alteraEquip(CL_Equipamento objEquipamento, string con)
        {
            return DB_Equipamento.alteraEquip(objEquipamento, con);
        }
        public static bool excluiEquip(CL_Equipamento objEquipamento, string con)
        {
            if (objEquipamento.e_cod > 0)
            {
                return DB_Equipamento.excluiEquip(objEquipamento, con);
            }
            else
            {
                return false;
            }
        }
        public static CL_Equipamento buscaEquipPatrimon(CL_Equipamento objEquipamento, string con)
        {
            if (objEquipamento.e_nPatrimon != "")
            {
                return DB_Equipamento.buscaEquipPatrimon(objEquipamento, con);
            }
            else
            {
                objEquipamento = null;
                return objEquipamento;
            }
        }
        public static bool verificaPatrimon(string e_nPatrimon, string con)
        {
            return DB_Equipamento.verificaPatrimon(e_nPatrimon, con);
        }
        public static bool verificaSerie(string e_nSerie, string con)
        {
            return DB_Equipamento.verificaSerie(e_nSerie, con);
        }
    }
}