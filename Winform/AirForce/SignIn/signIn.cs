using AirForce.GDP;
using AirForce.IT;
using AirForce.OC;
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

namespace AirForce.SignIn
{
    public partial class signIn : Form
    {
        public signIn()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //this function goes for first check if it is an IT then it goes for the GDP and then it goes on for OC
            try
            {
                // Retrieve input values from text boxes
                string name = InputName.Text;
                int PakNo = int.Parse(InputPakNo.Text);
                string Password = InputPassword.Text;

                // Check if the provided credentials are valid for an IT user
                bool IsValid = Validations.IsValidIT(name, PakNo, Password);

                // If the credentials are valid for an IT user
                if (IsValid)
                {
                    // Hide the current form
                    this.Hide();

                    // Show the main menu for IT users
                    ITMain menu = new ITMain();
                    menu.Show();
                }
                else
                {
                    // Check if the credentials are valid for a GDPilot user
                    bool IsValidGDP = Validations.IsValidGDP(name, PakNo, Password);

                    // If the credentials are valid for a GDPilot user
                    if (IsValidGDP)
                    {
                        // Retrieve the GDPilot object based on the PakNo
                        GDPilot GDP = Interfaces.GdpInterface.GetGDPThroughPakNo(PakNo);

                        // Set the current GDPilot
                        ConnectionClass.CurrentGDP = GDP;

                        // Hide the current form
                        this.Hide();

                        // Show the GDPilot menu
                        GDPMenu menu = new GDPMenu();
                        menu.Show();
                    }
                    else
                    {
                        // Check if the credentials are valid for a Commanding Officer (OC)
                        bool IsValidOC = Validations.IsValidOC(name, PakNo, Password);

                        // If the credentials are valid for a Commanding Officer (OC)
                        if (IsValidOC)
                        {
                            // Retrieve the Commanding Officer (OC) object based on the PakNo
                            CommandingOfficers CO = Interfaces.OCInterface.GetOCbyId(PakNo);

                            // Set the current Commanding Officer (OC)
                            ConnectionClass.SetCurrentOC(CO);

                            // Hide the current form
                            this.Hide();

                            // Show the Commanding Officer (OC) main menu
                            OCMain main = new OCMain(PakNo);
                            main.Show();
                        }
                        else
                        {
                            // Display a message indicating invalid PakNo or password
                            MessageBox.Show("Invalid PakNo or Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }




        }
    }
}
