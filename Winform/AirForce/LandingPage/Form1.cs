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
            // This line initializes the components of your application.
            InitializeComponent();

            // Paths to different files used in the application.
            string AFPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\AFPersonalle.txt";
            string GDPPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\GDPilot.txt";
            string OCPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Commanders.txt";
            string MissionPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Mission.txt";
            string ReportPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Requests.txt";

            // Setting the paths for different files in the ConnectionClass.
            ConnectionClass.SetAFFile(AFPath); // Sets the path for the Air Force personnel file.
            ConnectionClass.SetGDPFile(GDPPath); // Sets the path for the GDPilot file.
            ConnectionClass.SetMissionFile(MissionPath); // Sets the path for the Mission file.
            ConnectionClass.SetReportFile(ReportPath); // Sets the path for the Requests file.
            ConnectionClass.SetOCFile(OCPath); // Sets the path for the Commanders file.

        }

        private void button5_Click(object sender, EventArgs e)
        {

            // Check if the count of connections in the ConnectionClass is greater than or equal to 1.
            if (ConnectionClass.Count >= 1)
            {
                // If there is at least one connection:

                // Hide the current form.
                this.Hide();

                // Create an instance of the signIn form.
                signIn sign = new signIn();

                // Show the signIn form as a dialog box.
                sign.ShowDialog();
            }
            else
            {
                // If there are no connections:

                // Hide the current form.
                this.Hide();

                // Create an instance of the AskDataStorer form.
                AskDataStorer store = new AskDataStorer();

                // Show the AskDataStorer form as a dialog box.
                store.ShowDialog();

                // Increment the count of connections in the ConnectionClass.
                ConnectionClass.Count++;
            }
            //store.TopMost = true; // This line is commented out, so it doesn't have any effect on the code execution.

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
