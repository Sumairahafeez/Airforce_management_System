using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirForce;
using AirForceLibrary;
using AirForceLibrary.BL;
using AirForceLibrary.DL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;

namespace AirForce.IT
{   
    //IOC OCInterface = new DLCommandingOfficerDB();
    //IGDP GdpInterface = new DLGDPDB();
    public partial class AddOfficer : Form
    {
        public AddOfficer()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }

        private void Addbt_Click(object sender, EventArgs e)
        {   //When add button is clicked  this fucntion works as follow 
            try
            {
                // Retrieve input data from textboxes
                string name = InputName.Text;
                string Rank = InputRank.Text;
                int PakNO = int.Parse(InputPakNo.Text);
                string presentlyLocated = InputPosting.Text;
                string squadron = InputSquadron.Text;
                string branch = InputBranch.Text;

                // Check if the provided PakNo is valid
                bool isValid = Validations.IsValidPakNo(PakNO);

                if (isValid)
                {
                    // Check if the officer is an OC based on the provided Rank
                    bool IsOC = Validations.IsValidOC(Rank);

                    // Create a new officer object based on whether it's an OC or a GDPilot
                    if (IsOC)
                    {
                        // Create a new Commanding Officer object
                        CommandingOfficers newOC = new CommandingOfficers(name, Rank, PakNO, presentlyLocated, squadron);

                        // Store the new OC
                        Interfaces.GetOCInterface().StoreOC(newOC);

                        // Display a success message
                        MessageBox.Show("OC Added Successfully");

                        // Clear input data
                        ClearData();
                    }
                    else
                    {
                        // Create a new GDPilot object
                        GDPilot newgdp = new GDPilot(name, Rank, PakNO, presentlyLocated, squadron);

                        // Store the new GDPilot
                        Interfaces.GetGdpInterface().StoreGDP(newgdp);

                        // Display a success message
                        MessageBox.Show("Officer Added Successfully");

                        // Clear input data
                        ClearData();
                    }
                }
                else
                {
                    // Display a message if the provided PakNo already exists
                    MessageBox.Show("Pak No Already Exists");

                    // Clear input data
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }
        public void ClearData()
        {
            InputName.Text = string.Empty;
            InputPakNo.Text = string.Empty;
            InputRank.Text = string.Empty;
            InputSquadron.Text = string.Empty;
            InputPosting.Text = string.Empty;
            InputBranch.Text = string.Empty;
        }
       
    }
}
