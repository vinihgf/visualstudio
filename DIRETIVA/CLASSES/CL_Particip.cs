using System;

namespace CLASSES
{
    public class CL_Particip
    {
        public int p_complend { get; set; }
        public int p_clicod { get; set; }
        public string p_cliente { get; set; }
        public string p_fornec { get; set; }
        public string p_transp { get; set; }
        public string p_cgc { get; set; }
        public string p_nome { get; set; }
        public DateTime p_movdig { get; set; }
        public string p_fantas { get; set; }
        public string p_cep { get; set; }
        public string p_ende { get; set; }
        public string p_nr { get; set; }
        public string p_comend { get; set; }
        public string p_bairro { get; set; }
        public string p_cida { get; set; }
        public string p_est { get; set; }
        public string p_pais { get; set; }
        public string p_ibge { get; set; }
        public string p_fone { get; set; }
        public string p_contat { get; set; }
        public string p_iest { get; set; }
        public string p_celul { get; set; }
        public string p_email { get; set; }
        public string p_situac { get; set; }
        public int p_idumov { get; set; }
        public double p_lcred { get; set; }
        public string p_localiz { get; set; }
        public int p_idEntregador { get; set; }
        public string p_ramo { get; set; }
        public string p_codNome { get; set; }
        public string p_cultura { get; set; }
        public string p_rg { get; set; }
        public DateTime p_nasc { get; set; }

        public string trocaPais(string pais)
        {
            if (pais.Trim() == "1058")
            {
                return "BRASIL";
            }
            else
            {
                return "";
            }
        }
        public string acertaTel(string tel)
        {
            tel = tel.Replace(" ", "");
            tel = tel.Replace("(", "");
            tel = tel.Replace(")", "");
            tel = tel.Replace("-", "");
            return tel;
        }
        public string trocaNum(string nr)
        {
            if (nr.Trim() == "SN" || nr.Trim() == "S/N")
            {
                return "0";
            }
            else
            {
                return nr;
            }
        }
        public string trocaPaisNum(string pais)
        {
            if (pais == "BRASIL")
            {
                return "1058";
            }
            else
            {
                return "EX";
            }
        }
    }
}