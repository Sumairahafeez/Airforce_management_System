using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForce.GDP
{
    public partial class ViewApplication : Form
    {
        public ViewApplication()
        {
            InitializeComponent();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {
           InputContextT.Visible = true;
            contextBoxT.Visible = true;
            InputStatusT.Visible = true;
            StatusBoxT.Visible=true;
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPRequestMenu Menu = new GDPRequestMenu();
            Menu.Show();
        }
    }
}
