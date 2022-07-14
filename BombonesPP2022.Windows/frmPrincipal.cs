using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombonesPP2022.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void NuecesButton_Click(object sender, EventArgs e)
        {
            frmTipoNuez frm = new frmTipoNuez() { Text = "Tipos de nueces" };
            frm.ShowDialog(this);

            
        }

        private void CerrarButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BombonesButton_Click(object sender, EventArgs e)
        {
            frmBombones frm = new frmBombones() { Text = "Bombones" };
            frm.ShowDialog(this);

            
        }
    }
}
