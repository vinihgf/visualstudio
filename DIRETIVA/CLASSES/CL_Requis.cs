using System;

namespace CLASSES
{
    public class CL_Requis
    {
        public CL_Requis() { this.req_est = new CL_Est(); }
        public CL_Est req_est { get; set; }
        public int req_cod { get; set; }
        public int req_vend { get; set; }
        public int req_codcli { get; set; }
        public int req_oserv { get; set; }
        public DateTime req_data { get; set; }
        public double req_qtdade { get; set; }
        public double req_preco { get; set; }
        public int req_nota { get; set; }
        public DateTime req_dtfat { get; set; }
        public double req_desc { get; set; }
        public double req_custo { get; set; }
        public string req_ntoper { get; set; }
        public int req_lcto { get; set; }
        public int req_vended { get; set; }
        public string req_tpserv { get; set; }
        public double req_tdesc { get; set; }
        public string req_impr { get; set; }
        public string req_local { get; set; }
        public int req_condic { get; set; }
        public string req_situac { get; set; }
        public double req_vlrUnit { get; set; }
        public double req_vlrTot { get; set; }
        public double req_vldesc { get; set; }
        public string req_cnpj { get; set; }
        public string req_iest { get; set; }
        public double req_qtfat { get; set; }
        public string req_issqn { get; set; }
        public int req_tribut { get; set; }
        public string req_pcfixo { get; set; }
        public string req_ctrreg { get; set; }
        public int req_recno { get; set; }
        public string req_estcod { get; set; }
        public string req_estnome { get; set; }
        public string req_usudac { get; set; }
        public DateTime req_movdig { get; set; }
        public string req_commit { get; set; }
        public double req_qtdDev { get; set; }
        public int req_codend { get; set; }
    }
}