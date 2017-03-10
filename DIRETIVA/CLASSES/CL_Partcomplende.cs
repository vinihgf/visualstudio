using System;

namespace CLASSES
{
    public class CL_Partcomplende
    {
        public string pc_situac { get; set; }
        public int pc_codigo { get; set; }
        public int pc_codpart { get; set; }
        public string pc_nome { get; set; }
        public string pc_cnpj { get; set; }
        public string pc_ende { get; set; }
        public string pc_nr { get; set; }
        public string pc_compl { get; set; }
        public string pc_bairro { get; set; }
        public string pc_ibge { get; set; }
        public string pc_cida { get; set; }
        public string pc_uf { get; set; }
        public string pc_respons { get; set; }
        public string pc_matric { get; set; }
        public string pc_email { get; set; }
        public string pc_fone { get; set; }
        public string pc_cep { get; set; }
        public string pc_iest { get; set; }
        public string pc_ativo { get; set; }

        public object trocaNum(object p_nr)
        {
            throw new NotImplementedException();
        }

        public string acertaTel(string pc_fone)
        {
            throw new NotImplementedException();
        }
    }
}