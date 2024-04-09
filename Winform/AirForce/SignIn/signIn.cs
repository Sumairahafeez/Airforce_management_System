using AirForce.GDP;
using AirForce.IT;
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
        {
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
                else
                {
                    MessageBox.Show("InValid Input");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
