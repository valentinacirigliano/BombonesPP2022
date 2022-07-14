using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BombonesPP2022.Entidades;
using BombonesPP2022.Servicios.Servicios;
using BombonesPP2022.Windows.Helpers;

namespace BombonesPP2022.Windows
{
    public partial class frmBombones : Form
    {
        public frmBombones()
        {
            InitializeComponent();
        }
        private BombonesServicios servicio;
        private List<Bombon> lista;
        private void frmBombones_Load(object sender, EventArgs e)
        {
            servicio = new BombonesServicios();
            RecargarGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var bombon in lista)
            {
                var r = HelperGrid.ConstruirFila(DatosDataGridView);
                HelperGrid.SetearFila(r, bombon);
                HelperGrid.AgregarFila(DatosDataGridView, r);
            }
        }

        private void NuevoIconButton_Click(object sender, EventArgs e)
        {
            frmBombonAE frm = new frmBombonAE() { Text = "Agregar bombón" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                Bombon bombon = frm.GetBombon();
                int registrosAfectados = servicio.Agregar(bombon);
                if (registrosAfectados == 0)
                {
                    HelperMessage.Mensaje(TipoMensaje.Warning, "No se agregaron registros", "Advertencia");
                    RecargarGrilla();
                }
                else
                {
                    DataGridViewRow r = HelperGrid.ConstruirFila(DatosDataGridView);
                    HelperGrid.SetearFila(r, bombon);
                    HelperGrid.AgregarFila(DatosDataGridView, r);
                    HelperMessage.Mensaje(TipoMensaje.OK, "Registro agregado", "Mensaje");
                    RecargarGrilla();
                }

            }
            catch (Exception exception)
            {
                HelperMessage.Mensaje(TipoMensaje.Error, exception.Message, "Error");
            }
        }

        private void RecargarGrilla()
        {
            try
            {
                lista = servicio.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrarIconButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            try
            {
                var r = DatosDataGridView.SelectedRows[0];
                Bombon bombon = (Bombon)r.Tag;
                DialogResult dr = MessageBox.Show($"¿Desea borrar el registro seleccionado de {bombon.NombreBombon}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }

                int registrosAfectados = servicio.Borrar(bombon);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se borraron registros...",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    RecargarGrilla();

                }
                else
                {
                    DatosDataGridView.Rows.Remove(r);
                    MessageBox.Show("Registro eliminado",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void EditarIconButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            var r = DatosDataGridView.SelectedRows[0];
            Bombon bombon = (Bombon)r.Tag;
            Bombon bombonAuxiliar = (Bombon)bombon.Clone();
            try
            {
                frmBombonAE frm = new frmBombonAE() { Text = "Editar un bombón" };
                frm.SetBombon(bombon);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                bombon = frm.GetBombon();
                int registrosAfectados = servicio.Editar(bombon);
                if (registrosAfectados == 0)
                {
                    HelperMessage.Mensaje(TipoMensaje.Warning, "No se borraron registros", "Advertencia");
                    RecargarGrilla();
                }
                else
                {
                    HelperGrid.SetearFila(r, bombon);
                    MessageBox.Show("Registro modificado",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                HelperGrid.SetearFila(r, bombonAuxiliar);
                MessageBox.Show(exception.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
