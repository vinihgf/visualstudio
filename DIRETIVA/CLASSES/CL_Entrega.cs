using System;

namespace CLASSES
{
    public class CL_Entrega
    {
        public int e_idEntregador { get; set; }
        public int e_id { get; set; }
        public string e_remetent { get; set; }
        public string e_awb { get; set; }
        public DateTime e_dataenco { get; set; }
        public int e_qtdvol { get; set; }
        public string e_rota { get; set; }
        public DateTime e_datastat { get; set; }
        public string e_status { get; set; }
        public int e_diasprev { get; set; }
        public int e_clicod { get; set; }
        public string e_situac { get; set; }
        public string e_clinome { get; set; }
        public string e_clicida { get; set; }
        public string e_clibairro { get; set; }
        public string e_modelo { get; set; }
        public string e_cliende { get; set; }
        public string e_clinr { get; set; }
        public string e_clilocaliz { get; set; }
        public string e_nomeEntregador { get; set; }
        public object e_cliest { get; set; }
        public double e_vlrreceb { get; set; }
        public double e_vlrpago { get; set; }
        public string e_recebido { get; set; }
        public string e_pago { get; set; }
        public string e_localizEntreg { get; set; }
    }
}