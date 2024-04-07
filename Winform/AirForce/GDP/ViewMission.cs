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
    public partial class ViewMission : Form
    {
        public ViewMission()
        {
            InitializeComponent();
        }

        private void ViewMission_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMissionMENU menu = new GDPMissionMENU();
            menu.Show();
        }
    }
}
