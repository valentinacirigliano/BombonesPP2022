using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BombonesPP2022.Entidades;

namespace BombonesPP2022.Windows.Helpers
{
    public static class HelperGrid
    {
        public static DataGridViewRow ConstruirFila(DataGridView dataGrid)
        {
            var r = new DataGridViewRow();
            r.CreateCells(dataGrid);
            return r;
        }

        public static void AgregarFila(DataGridView dataGrid, DataGridViewRow r)
        {
            dataGrid.Rows.Add(r);
        }

        public static void SetearFila(DataGridViewRow r, Object obj)
        {
            if (obj is Pais)
            {
                r.Cells[0].Value = ((Pais)obj).NombrePais;

            }
            else if (obj is TipoDeNuez)
            {
                r.Cells[0].Value = ((TipoDeNuez)obj).Nuez;
            }

            r.Tag = obj;

        }
    }
}
