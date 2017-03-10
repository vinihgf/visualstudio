using System;

namespace CLASSES
{
    public class REL_encerraRequis
    {
        public int req_cod { get; set; }
        public DateTime req_data { get; set; }
        public int u_codigo { get; set; }
        public int p_clicod { get; set; }
        public string p_clinome { get; set; }
        public string est_cod { get; set; }
        public string est_nome { get; set; }
        public double req_qtd { get; set; }
        public double req_qtdFat { get; set; }
        public double req_devolver { get; set; }

        public string mec_nome { get; set; }
    }
}