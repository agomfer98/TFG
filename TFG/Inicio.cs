using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFG
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
          
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            subpanel1.Visible = false;
            pane_menu.BorderStyle = BorderStyle.FixedSingle;
        }

        private void butt_compo_Click(object sender, EventArgs e)
        {
            if (subpanel1.Visible)
            {
                subpanel1.Visible = false;
            }
            else
            {
                subpanel1.Visible = true;
            }

        }

        private void butt_part_Click(object sender, EventArgs e)
        {
            if (subpanel2.Visible)
            {
                subpanel2.Visible = false;
            }
            else
            {
                subpanel2.Visible = true;
            }
            

        }

        private void butt_altcom_Click(object sender, EventArgs e)
        {
            NuevoComp nuevoComp = new NuevoComp();
            nuevoComp.ShowDialog();
        }

        private void butt_viscom_Click(object sender, EventArgs e)
        {
            VisComp v= new VisComp();
            v.ShowDialog();
        }

        private void buttnewcarp_Click(object sender, EventArgs e)
        {
            CrearPartitura c= new CrearPartitura();
            c.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarPartituras buscarPartituras = new BuscarPartituras();
            buscarPartituras.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
