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
                string name = InputName.Text;
                string Rank = InputRank.Text;
                int PakNO = int.Parse(InputPakNo.Text);
                string presentlyLocated = InputPosting.Text;
                //After taking inout it checks if the given User is an OC through its Rank and the proceeds
                bool IsOC = Validations.IsValidOC(Rank);
                string squadron = InputSquadron.Text;
                bool isValid = Validations.IsValidPakNo(PakNO);
                string branch = InputBranch.Text;
                if(isValid)
                {
                    if (IsOC)
                    {

                        CommandingOfficers newOC = new CommandingOfficers(name, Rank, PakNO, presentlyLocated, squadron);
                        IOC Oc = new DLCommandingOfficerDB();
                        Oc.StoreOC(newOC);
                        MessageBox.Show("OC Added SuccessFully");
                    }
                    else
                    {
                        GDPilot newgdp = new GDPilot(name, Rank, PakNO, presentlyLocated, squadron);
                        Interfaces.GdpInterface.StoreGDP(newgdp);
                        MessageBox.Show("Officer Added SuccessFully");
                    }

                   
                }
                else
                {
                    MessageBox.Show("Pak No Already Exists");
                }
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
       
    }
}
