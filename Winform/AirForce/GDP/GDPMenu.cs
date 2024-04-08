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
    public partial class GDPMenu : Form
    {
        public GDPMenu()
        {
            InitializeComponent();
        }

        private void GDPMenu_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMissionMENU menu = new GDPMissionMENU();
            menu.Show();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Missionbt_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            GDPMissionMENU menu = new GDPMissionMENU();
            menu.Show();
        }

        private void FlyingHoursbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPFlyingHoursMenu menu = new GDPFlyingHoursMenu();
            menu.Show();
        }

        private void Requestbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPRequestMenu Menu = new GDPRequestMenu();
            Menu.Show();
        }
    }
}
