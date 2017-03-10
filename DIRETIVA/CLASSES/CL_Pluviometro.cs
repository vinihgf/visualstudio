using System;

namespace CLASSES
{
    public class CL_Pluviometro
    {
        public int p_id { get; set; }
        public DateTime p_data { get; set; }
        public string p_turno { get; set; }
        public double p_duracao { get; set; }
        public double p_qtdade { get; set; }
        public int p_idlavoura { get; set; }
    }
}