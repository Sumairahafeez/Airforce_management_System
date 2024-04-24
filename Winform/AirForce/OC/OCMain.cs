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
        {   //it sets the page as which oc is using it.
            InitializeComponent();
            CurrentOCPakNo = PakNo;
        }

        private void button3_Click(object sender, EventArgs e)
        {   //to show mission page
            this.Hide();
            AssignMission mission = new AssignMission(CurrentOCPakNo);
            mission.Show();
        }

        private void OCMain_Load(object sender, EventArgs e)
        {       //to display text on the main heading
            try
            {
                CommandingOfficers CO = Interfaces.GetOCInterface().GetOCbyId(CurrentOCPakNo);
                if (CO != null)
                {
                    Textbt.Text ="Commanding Officer "+ CO.GetRank() + " " + CO.GetName();
                    
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Missionbt_Click(object sender, EventArgs e)
        {   //selecting underofficers
            this.Hide();
            SelectUnderOfficer select = new SelectUnderOfficer(CurrentOCPakNo);
            select.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {   //shoes form
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void Requestbt_Click(object sender, EventArgs e)
        {   //it opens request page
            this.Hide();
            CheckReq req = new CheckReq(CurrentOCPakNo);
            req.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //assign posting page
            this.Hide();
            AssignPosting assign = new AssignPosting(CurrentOCPakNo);
            assign.Show();
        }
    }
}
