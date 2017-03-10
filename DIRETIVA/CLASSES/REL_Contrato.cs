using System;

namespace CLASSES
{
    public class REL_Contrato
    {
        public int p_clicod { get; set; }
        public string p_cliente { get; set; }
        public string p_fornec { get; set; }
        public string p_transp { get; set; }
        public string p_cgc { get; set; }
        public string p_nome { get; set; }
        public DateTime Ip_movdig { get; set; }
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
        public string emp_nome { get; set; }
        public string emp_ende { get; set; }
        public string emp_comend { get; set; }
        public int emp_nr { get; set; }
        public string emp_bairro { get; set; }
        public string emp_cida { get; set; }
        public string emp_cep { get; set; }
        public string emp_est { get; set; }
        public string emp_fone { get; set; }
        public string emp_dirt { get; set; }
        public string emp_cgc { get; set; }
        public string emp_iscest { get; set; }
        public string emp_ibge { get; set; }
        public string emp_fantas { get; set; }
        public string emp_imunic { get; set; }
        public string emp_email { get; set; }
        public string emp_site { get; set; }
        public string emp_foto { get; set; }

        public int e_cod { get; set; }
        public int e_ncontrato { get; set; }
        public string e_nPatrimon { get; set; }
        public int e_nloca { get; set; }
        public int e_renova { get; set; }
        public string e_inutiliza { get; set; }
        public int e_nmodelo { get; set; }
        public int e_nmarca { get; set; }
        public string e_marca { get; set; }
        public string e_nserie { get; set; }
        public string e_descri { get; set; }
        public string e_modelo { get; set; }
        public int e_nInutiliza { get; set; }
        public int e_clicod { get; set; }
        public string e_cliente { get; set; }

        public int l_cod { get; set; }
        public int l_clicod { get; set; }
        public string l_clinome { get; set; }
        public int l_contr { get; set; }
        public DateTime l_emis { get; set; }
        public DateTime l_dev { get; set; }
        public int l_tempo { get; set; }
        public string l_temp { get; set; }
        public double l_valor { get; set; }
        public string l_vend { get; set; }
        public int l_codVend { get; set; }
        public double l_comis { get; set; }
        public double l_vlComis { get; set; }
        public string l_descri { get; set; }
        public string l_patrimon { get; set; }
        public string serie { get; set; }
        public string l_dmy { get; set; }
        public string l_situac { get; set; }
    }
}