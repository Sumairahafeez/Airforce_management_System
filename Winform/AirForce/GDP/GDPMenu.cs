using AirForceLibrary.Utilis;
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
            GDPMenu menu = new GDPMenu();
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
            ViewMissionbt.Visible = true;
            CompleteMissionbt.Visible = true;
            Editbt.Visible = true;
            ViewFHbt.Visible = false;
            Addbt.Visible = false;
            Updatebt.Visible = false;
            ViewRequest.Visible = false;
            AddRequestbt.Visible = false;
            Delbt.Visible = false;
        }

        private void FlyingHoursbt_Click(object sender, EventArgs e)
        {   ViewFHbt.Visible = true;
            Addbt.Visible = true;
            Updatebt.Visible = true;
            ViewRequest.Visible = false;
            AddRequestbt.Visible = false;
            Delbt.Visible = false;
            ViewMissionbt.Visible = false;
            CompleteMissionbt.Visible = false;
            Editbt.Visible = false;
        }

        private void Requestbt_Click(object sender, EventArgs e)
        {
             ViewRequest.Visible = true;
             AddRequestbt.Visible = true;
             Delbt.Visible = true;
             ViewMissionbt.Visible = false;
             CompleteMissionbt.Visible = false;
             Editbt.Visible = false;
             ViewFHbt.Visible = false;
             Addbt.Visible = false;
             Updatebt.Visible = false;
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

        private void Editbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditMission mission = new EditMission();
            mission.Show();
        }

        private void ViewFHbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewFH view = new ViewFH();
            view.ShowDialog();
        }

        private void Addbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ADDFH add = new ADDFH();
            add.ShowDialog();
        }

        private void Updatebt_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateFH update = new UpdateFH();
            update.ShowDialog();
        }

        private void ViewRequest_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewApplication view = new ViewApplication();
            view.Show();
        }

        private void AddRequestbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddApplication Add = new AddApplication();
            Add.Show();
        }

        private void Delbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeletReq del = new DeletReq();
            del.Show();
        }
    }
}
