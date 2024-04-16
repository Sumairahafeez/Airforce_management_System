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

namespace AirForce.LandingPage
{
    public partial class AskDataStorer : Form
    {
        public AskDataStorer()
        {
            InitializeComponent();
        }

        private void dbbt_Click(object sender, EventArgs e)
        {
            ConnectionClass.SetIsUsingDB(true);
            this.Hide();
            signIn sign = new signIn();
            sign.Show();
           
        }

        private void fhbt_Click(object sender, EventArgs e)
        {
            ConnectionClass.SetIsUsingDB(false);
            this.Hide();
            signIn sign = new signIn();
            sign.Show();
        }
    }
}
