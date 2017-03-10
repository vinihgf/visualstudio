using System;

namespace CLASSES
{
    public class CL_Locacao
    {
        public CL_Locacao() { this.l_equip = new CL_Equipamento(); }
        public CL_Equipamento l_equip { get; set; }
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
        public string descri { get; set; }
        public string patrimon { get; set; }
        public string serie { get; set; }
        public string l_dmy { get; set; }
        public string l_situac { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
    }
}