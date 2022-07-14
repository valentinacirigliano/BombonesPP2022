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
    public partial class frmBombonAE : Form
    {
        public frmBombonAE()
        {
            InitializeComponent();
        }
        private Bombon bombon;
        private BombonesServicios servicio;
        private TiposChocolateServicios servicioTipChoc;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new BombonesServicios();
            servicioTipChoc = new TiposChocolateServicios();
            CargarDatosCombo(ref ChocolateComboBox, servicioTipChoc);
            if (bombon != null)
            {
                nombreBombonTextBox.Text = bombon.NombreBombon;
                ChocolateComboBox.SelectedValue = bombon.TipoChocolateId;
                NuezComboBox.SelectedValue = bombon.TipoNuezId;
                RellenoComboBox.SelectedValue=bombon.TipoRellenoId;
                FabricaComboBox.SelectedValue = bombon.FabricaId;
                precioTextBox.Text = bombon.PrecioVenta.ToString();
            }

        }

        private void CargarDatosCombo(ref ComboBox combo, TiposChocolateServicios servicios)
        {
            var lista = servicios.GetLista();
            combo.DataSource = lista;
            combo.DisplayMember = "Chocolate";
            combo.ValueMember = "TipoChocolateId";
            combo.SelectedIndex = 0;
        }
        

        public Bombon GetBombon()
        {
            return bombon;
        }

        private void CancelarIconButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OKIconButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (bombon == null)
                {
                    bombon = new Bombon();
                }

                bombon.NombreBombon = nombreBombonTextBox.Text;
                bombon.TipoDeChocolate = (TipoDeChocolate)ChocolateComboBox.SelectedItem;
                bombon.TipoChocolateId = ((TipoDeChocolate)ChocolateComboBox.SelectedItem).TipoChocolateId;
                bombon.TipoDeNuez = (TipoDeNuez)NuezComboBox.SelectedItem;
                bombon.TipoNuezId = ((TipoDeNuez)NuezComboBox.SelectedItem).TipoNuezId;
                bombon.TipoDeRelleno= (TipoDeRelleno)RellenoComboBox.SelectedItem;
                bombon.TipoRellenoId = ((TipoDeRelleno)RellenoComboBox.SelectedItem).TipoRellenoId;
                bombon.Fabrica= (Fabrica)FabricaComboBox.SelectedItem;
                bombon.FabricaId = ((Fabrica)FabricaComboBox.SelectedItem).FabricaId;
                bombon.PrecioVenta = double.Parse(precioTextBox.Text);

                DialogResult = DialogResult.OK;

            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (ChocolateComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(ChocolateComboBox, "Debe seleccionar un chocolate");

            }
            if (NuezComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(NuezComboBox, "Debe seleccionar una nuez");

            }
            if (RellenoComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(RellenoComboBox, "Debe seleccionar un relleno");

            }
            if (FabricaComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(FabricaComboBox, "Debe seleccionar una fábrica");

            }

            if (string.IsNullOrEmpty(precioTextBox.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(precioTextBox, "El precio es requerido");
            }

            if (string.IsNullOrEmpty(nombreBombonTextBox.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(nombreBombonTextBox, "El nombre del bombon es requerido");
            }

            return valido;
        }

        internal void SetBombon(Bombon bombon)
        {
            this.bombon = bombon;
        }

        
    }
}
