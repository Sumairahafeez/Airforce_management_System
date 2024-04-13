using AirForceLibrary.BL;
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

namespace AirForce.OC
{
   
    public partial class OCMain : Form
    {
        public int CurrentOCPakNo;
        public OCMain()
        {
            InitializeComponent();
        }
        public OCMain(int PakNo)
        {
            InitializeComponent();
            CurrentOCPakNo = PakNo;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignMission mission = new AssignMission(CurrentOCPakNo);
            mission.Show();
        }

        private void OCMain_Load(object sender, EventArgs e)
        {     
            try
            {
                CommandingOfficers CO = Interfaces.OCInterface.GetOCbyId(CurrentOCPakNo);
                if (CO != null)
                {
                    userBoxT.BringToFront();
                    userBoxT.Text = "Welcome" + CO.GetRank() + " " + CO.GetName();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Missionbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            SelectUnderOfficer select = new SelectUnderOfficer(CurrentOCPakNo);
            select.Show();
        }
    }
}
