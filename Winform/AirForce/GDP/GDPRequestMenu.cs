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
    public partial class GDPRequestMenu : Form
    {
        public GDPRequestMenu()
        {
            InitializeComponent();
        }

        private void FlyingHoursbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPFlyingHoursMenu menu = new GDPFlyingHoursMenu();
            menu.Show();
        }

        private void Missionbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMissionMENU menu = new GDPMissionMENU();
            menu.Show();
        }

        private void ViewFHbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewApplication view = new ViewApplication();
            view.Show();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Addbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddApplication Add = new AddApplication();
            Add.Show();
        }

        private void Updatebt_Click(object sender, EventArgs e)
        {

        }
    }
}
