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

namespace AirForce.GDP
{
    public partial class AddApplication : Form
    {
        public AddApplication()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {   //go back to main menu
            this.Hide();
            GDPMenu Menu = new GDPMenu();
            Menu.Show();
        }

        private void Savebt_Click(object sender, EventArgs e)
        {   //take inputs and check its validations
            int PakNo = int.Parse(InputPakNo.Text);
            string context = InputContextT.Text;
            int Id = int.Parse(InputId.Text);
            bool isValid = Validations.IsValidGDP(PakNo);
            // If the PakNo is valid:
            if (isValid)
            {
                try
                {
                    // Create a new Requests object with the provided Id, context, and PakNo.
                    Requests newReq = new Requests(Id, context, PakNo);

                    // Store the new request using the StoreRequests method from the request interface.
                    Interfaces.GetRequestInterface().StoreRequests(newReq);

                    // Show a success message to the user.
                    MessageBox.Show("Request Sent Successfully");
                }
                catch (Exception ex)
                {
                    // Display any exceptions that occur in a MessageBox.
                    MessageBox.Show(ex.Message);
                }
            }
            // If the PakNo is invalid:
            else
            {
                // Show a message indicating an invalid PakNo.
                MessageBox.Show("Invalid PakNo");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void AddApplication_Load(object sender, EventArgs e)
        {    //displays the user on the heading
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s Request Menu";
            // Create a new DataTable to hold request data.
            DataTable data = new DataTable();
            data.Columns.Add("ReqId", typeof(int));
            data.Columns.Add("Context", typeof(string));
            data.Columns.Add("PakNO", typeof(int));
            data.Columns.Add("Status", typeof(string));

            // Retrieve requests associated with the current GDP.
            List<Requests> requ = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());

            // Iterate through each request and add its data to the DataTable.
            for (int i = 0; i < requ.Count; i++)
            {
                data.Rows.Add(requ[i].GetRequestId(), requ[i].GetContext(), requ[i].GetPakNo(), requ[i].GetStatus());
            }

            // Bind the DataTable to the ApplicationsGV DataGridView.
            ApplicationsGV.DataSource = data;

        }
    }
}
