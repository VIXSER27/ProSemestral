using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumirLogin
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegistrar frmRegistrar = new FrmRegistrar();
            frmRegistrar.MdiParent = this;
            frmRegistrar.WindowState = FormWindowState.Maximized;
            frmRegistrar.Show();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.MdiParent = this;
            frmLogin.WindowState = FormWindowState.Maximized;
            frmLogin.Show();
        }

        private void getToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGetUsuarios frmGetUsuarios = new FrmGetUsuarios();
            frmGetUsuarios.MdiParent = this;
            frmGetUsuarios.WindowState = FormWindowState.Maximized;
            frmGetUsuarios.Show();
        }
    }
}
