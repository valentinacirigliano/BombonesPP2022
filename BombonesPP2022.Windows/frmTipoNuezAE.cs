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

namespace BombonesPP2022.Windows
{
    public partial class frmTipoNuezAE : Form
    {
        public frmTipoNuezAE()
        {
            InitializeComponent();
        }
        private TipoDeNuez tipoDeNuez;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (tipoDeNuez != null)
            {
                NuezTextBox.Text = tipoDeNuez.Nuez;
            }
        }

        public TipoDeNuez GetTipoNuez()
        {
            return tipoDeNuez;
        }

        private void CancelarIconButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OKIconButton_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {
                if (tipoDeNuez == null)
                {
                    tipoDeNuez = new TipoDeNuez();
                }

                tipoDeNuez.Nuez = NuezTextBox.Text;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(NuezTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(NuezTextBox, "El nombre de la nuez es requerido");
            }

            return valido;
        }

        public void SetTipoNuez(TipoDeNuez tipoDeNuez)
        {
            this.tipoDeNuez = tipoDeNuez;
        }
    }
}
