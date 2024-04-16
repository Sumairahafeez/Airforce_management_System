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
            string AFPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\AFPersonalle.txt";
            string GDPPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\GDPilot.txt";
            string OCPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Commanders.txt";
            string MissionPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Mission.txt";
            string ReportPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Requests.txt";
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
