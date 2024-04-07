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
    public partial class GDPMissionMENU : Form
    {
        public GDPMissionMENU()
        {
            InitializeComponent();
        }

        private void GDPMissionMENU_Load(object sender, EventArgs e)
        {

        }

        private void ViewMissionbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewMission viewMission = new ViewMission();
            viewMission.Show();
        }

        private void CompleteMissionbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            CompleteMission mission = new CompleteMission();
            mission.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Editbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditMission mission = new EditMission();
            mission.Show();
        }

        private void FlyingHoursbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPFlyingHoursMenu menu = new GDPFlyingHoursMenu();
            menu.Show();
        }
    }
}
