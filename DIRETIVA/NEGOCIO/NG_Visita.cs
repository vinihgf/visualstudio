using BANCO;
using CLASSES;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class NG_Visita
    {
        public static bool incluiVisita(CL_Visita objVisita, string con)
        {
            return DB_Visita.incluiVisita(objVisita, con);
        }
        public static int buscaCod(int v_cod, string con)
        {
            return DB_Visita.buscaCod(v_cod, con);
        }
        public static bool verificaUmov(int idUmov, string con)
        {
            return DB_Visita.verificaUmov(idUmov, con);
        }
        public static List<CL_Visita> pesqVisita(string dataI, string dataF, int cliente, int vendedor, string modelo, string con)
        {
            string sql = "";
            if (modelo == "S")
            {
                if (vendedor > 0)
                {
                    if (cliente > 0)
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_clicod =" + cliente + " AND v_vend=" + vendedor + " AND v_prxvis>='" + dataI + "' AND v_prxvis<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                    else
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_vend=" + vendedor + " AND v_prxvis>='" + dataI + "' AND v_prxvis<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                }
                else
                {
                    if (cliente > 0)
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_clicod=" + cliente + " AND v_prxvis>='" + dataI + "' AND v_prxvis<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                    else
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_prxvis>='" + dataI + "' AND v_prxvis<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                }
            }
            else
            {
                if (vendedor > 0)
                {
                    if (cliente > 0)
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_clicod=" + cliente + " AND v_vend=" + vendedor + " AND v_ultvisit>='" + dataI + "' AND v_ultvisit<='" + dataF + "' AND v_ultvisit is not null";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                    else
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_vend=" + vendedor + "v_ultvisit>='" + dataI + "' AND v_ultvisit<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                }
                else
                {
                    if (cliente > 0)
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_clicod=" + cliente + " AND v_ultvisit>='" + dataI + "' AND v_ultvisit<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                    else
                    {
                        sql = "SELECT v_lcto, v_clicod, p_nome, p_cgc, v_ultvisit, v_vend, con_nome, v_desc, v_prxvis, v_hist1, v_idumov, v_situac FROM visitas, particip, convenio WHERE v_ultvisit IS NOT NULL AND v_prxvis IS NOT NULL AND con_cod = v_vend AND v_clicod = p_cod AND v_ultvisit>='" + dataI + "' AND v_ultvisit<='" + dataF + "'";
                        return DB_Visita.pesqVisita(sql, con);
                    }
                }
            }
        }

        public static bool verificaVisita(string v_lcto, string con)
        {
            return DB_Visita.verificaVisita(v_lcto, con);
        }

        public static CL_Visita buscaVistaIDUmov(int idUmov, string con)
        {
            return DB_Visita.buscaVisitaIDUmov(idUmov, con);
        }
    }
}