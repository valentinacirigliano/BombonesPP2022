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
    public partial class frmTipoNuez : Form
    {
        public frmTipoNuez()
        {
            InitializeComponent();
        }
        private TiposDeNuezServicios servicio;
        private List<TipoDeNuez> lista;
        private void frmTipoNuez_Load(object sender, EventArgs e)
        {
            //splitContainer1.FixedPanel = FixedPanel.Panel1;
            servicio = new TiposDeNuezServicios();
            try
            {
                lista = servicio.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void MostrarDatosEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var tipoDeNuez in lista)
            {
                var r = HelperGrid.ConstruirFila(DatosDataGridView);
                HelperGrid.SetearFila(r, tipoDeNuez);
                HelperGrid.AgregarFila(DatosDataGridView, r);
            }
        }

        private void NuevoIconButton_Click(object sender, EventArgs e)
        {
            frmTipoNuezAE frm = new frmTipoNuezAE() { Text = "Agregar un tipo de nuez" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                TipoDeNuez tipoDeNuez = frm.GetTipoNuez();
                int registrosAfectados = servicio.Agregar(tipoDeNuez);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se agregaron registros...",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    RecargarGrilla();
                }
                else
                {
                    var r = HelperGrid.ConstruirFila(DatosDataGridView);
                    HelperGrid.SetearFila(r, tipoDeNuez);
                    HelperGrid.AgregarFila(DatosDataGridView, r);
                    MessageBox.Show("Registro agregado",
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
                TipoDeNuez tipoDeNuez = (TipoDeNuez)r.Tag;
                DialogResult dr = MessageBox.Show($"¿Desea borrar el registro seleccionado de {tipoDeNuez.Nuez}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }

                int registrosAfectados = servicio.Borrar(tipoDeNuez);
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

            RecargarGrilla();
        }

        private void EditarIconButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            var r = DatosDataGridView.SelectedRows[0];
            TipoDeNuez tipoDeNuez = (TipoDeNuez)r.Tag;
            TipoDeNuez tipoDeNuezAuxiliar = (TipoDeNuez)tipoDeNuez.Clone();
            try
            {
                frmTipoNuezAE frm = new frmTipoNuezAE() { Text = "Editar una nuez" };
                frm.SetTipoNuez(tipoDeNuez);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                tipoDeNuez = frm.GetTipoNuez();
                int registrosAfectados = servicio.Editar(tipoDeNuez);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se editaron registros...",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    RecargarGrilla();

                }
                else
                {
                    HelperGrid.SetearFila(r, tipoDeNuez);
                    MessageBox.Show("Registro modificado",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                HelperGrid.SetearFila(r, tipoDeNuezAuxiliar);
                MessageBox.Show(exception.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
