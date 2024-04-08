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
    public partial class EditMission : Form
    {
        public EditMission()
        {
            InitializeComponent();
        }

        private void EditMission_Load(object sender, EventArgs e)
        {

        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMissionMENU menu = new GDPMissionMENU();
            menu.Show();
        }
    }
}
