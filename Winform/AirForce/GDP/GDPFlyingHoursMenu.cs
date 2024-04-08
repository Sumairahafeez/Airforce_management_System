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
    public partial class GDPFlyingHoursMenu : Form
    {
        public GDPFlyingHoursMenu()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void ViewFHbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewFH view = new ViewFH();
            view.ShowDialog();
        }

        private void UpdateFHbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateFH update = new UpdateFH();
            update.ShowDialog();
        }

        private void AddFHbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ADDFH add = new ADDFH();
            add.ShowDialog();
        }

        private void Missionbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMissionMENU menu = new GDPMissionMENU();
            menu.ShowDialog();
        }

        private void Requestbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPRequestMenu menu = new GDPRequestMenu();
            menu.ShowDialog();
        }
    }
}
