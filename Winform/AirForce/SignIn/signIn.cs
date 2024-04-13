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
                string name = InputName.Text;
                int PakNo = int.Parse(InputPakNo.Text);
                string Password = InputPassword.Text;
                bool IsValid = Validations.IsValidIT(name, PakNo, Password);
                if (IsValid)
                {
                    this.Hide();
                    ITMain menu = new ITMain();
                    menu.Show();
                }
                else//if it is not an IT then it should check if it is an GDP
                {   bool IsValidGDP = Validations.IsValidGDP(name, PakNo, Password);
                    if(IsValidGDP)
                    {
                        GDPilot GDP = Interfaces.GdpInterface.GetGDPThroughPakNo(PakNo);
                        ConnectionClass.CurrentGDP = GDP;
                        this.Hide();
                        GDPMenu menu = new GDPMenu();
                        menu.Show();
                    }
                    else//If it is not a GDP then it should check if it is an OC
                    {
                        bool IsValidOC = Validations.IsValidOC(name,PakNo, Password);
                        if(IsValidOC)
                        {
                            CommandingOfficers CO = Interfaces.OCInterface.GetOCbyId(PakNo);
                            ConnectionClass.SetCurrentOC(CO);
                            this.Hide();
                            OCMain main = new OCMain(PakNo);
                            
                            //Users.CurrentOC = CO;
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("InValid Input");
                        }
                    }
                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
