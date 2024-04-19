using AirForce.GDP;
using AirForce.LandingPage;
using AirForce.SignIn;
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

namespace AirForce
{
    public partial class Form1 : Form
    {
        public int Count = 0;
        public Form1()
        {
            InitializeComponent();
            string AFPath = "AFPersonalle.txt";
            string GDPPath = "GDPilot.txt";
            string OCPath = "Commanders.txt";
            string MissionPath = "Mission.txt";
            string ReportPath = "Requests.txt";
            ConnectionClass.SetAFFile(AFPath);
            ConnectionClass.SetGDPFile(GDPPath);
            ConnectionClass.SetMissionFile(MissionPath);
            ConnectionClass.SetReportFile(ReportPath);
            ConnectionClass.SetOCFile(OCPath);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            if(ConnectionClass.Count>=1)
            {
                this.Hide();
                signIn sign = new signIn();
                sign.ShowDialog();
            }
            else
            {
                this.Hide();
                AskDataStorer store = new AskDataStorer();
                store.ShowDialog();
                ConnectionClass.Count++;
            }
            //store.TopMost = true;



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
