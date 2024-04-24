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
using static System.Net.Mime.MediaTypeNames;

namespace AirForce.OC
{
    public partial class CheckReq : Form
    { public int CurrentOCPakNo;
        public CheckReq()
        {
            InitializeComponent();
        }
        public CheckReq(int Pakno)
        {
            CurrentOCPakNo = Pakno;
            InitializeComponent();
        }

        private void CheckReq_Load(object sender, EventArgs e)
        {
            try
            {
                // Create a new DataTable to hold the data.
                DataTable data = new DataTable();

                // Add columns to the DataTable.
                data.Columns.Add("PakNO", typeof(int));
                data.Columns.Add("Name", typeof(string));
                data.Columns.Add("ReqId", typeof(int));
                data.Columns.Add("Context", typeof(string));
                data.Columns.Add("Status", typeof(string));

                // Retrieve a list of GDPilot objects who are under the current officer.
                List<GDPilot> Under = Interfaces.GetGdpInterface().GetAllUFofOC(CurrentOCPakNo);

                // Iterate through each under officer.
                foreach (AFPersonalle a in Under)
                {
                    // Retrieve requests of the specific officer.
                    List<Requests> requ = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(a.GetPakNo());

                    // Iterate through each request of the officer and add it to the DataTable.
                    for (int i = 0; i < requ.Count; i++)
                    {
                        data.Rows.Add(a.GetPakNo(), a.GetName(), requ[i].GetRequestId(), requ[i].GetContext(), requ[i].GetStatus());
                    }
                }

                // Bind the DataTable to the ApplicationDV DataGridView.
                ApplicationDV.DataSource = data;

                // Fill the combobox with PakNo of under officers if the application is using a database.
                if (ConnectionClass.GetIsUsingDB())
                {
                    string query1 = "SELECT * FROM  GDP g,AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + CurrentOCPakNo + "))";
                    PakNoCB.DataSource = Validations.GetData(query1);
                    PakNoCB.DisplayMember = "PakNo";
                }
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur in a MessageBox.
                MessageBox.Show(ex.Message);
            }

        }

        private void Checkbt_Click(object sender, EventArgs e)
        {
            // Parse the text value from PakNoCB ComboBox into an integer.
            int Pakno = int.Parse(PakNoCB.Text);

            // Parse the text value from InputId TextBox into an integer.
            int id = int.Parse(InputId.Text);

            // Call the IsValidRequest method to check if a request exists for the given PakNo and id.
            Requests req = Validations.IsValidRequest(Pakno, id);

            // If a request object is found:
            if (req != null)
            {
                // Set the text of InputContext TextBox to the context of the request.
                InputContext.Text = req.GetContext();

                // Set the text of InputStatusCB ComboBox to the status of the request.
                InputStatusCB.Text = req.GetStatus();
            }

        }

        private void AssigmMissionbt_Click(object sender, EventArgs e)
        {
            // Parse the text value from PakNoCB ComboBox into an integer.
            int Pakno = int.Parse(PakNoCB.Text);

            // Parse the text value from InputId TextBox into an integer.
            int id = int.Parse(InputId.Text);

            // Call the IsValidRequest method to check if a request exists for the given PakNo and id.
            Requests req = Validations.IsValidRequest(Pakno, id);

            // If a request object is found:
            if (req != null)
            {
                // Set the text of InputContext TextBox to the context of the request.
                InputContext.Text = req.GetContext();

                // Get the status from InputStatusCB ComboBox.
                string status = InputStatusCB.Text;

                // Set the status of the request object to the new status.
                req.SetStatus(status);

                // Update the request in the database using the UpdateRequests method from the request interface.
                Interfaces.GetRequestInterface().UpdateRequests(id, req);

                // Show a success message to the user.
                MessageBox.Show("Status Set Successfully");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {   //takes back to main page
            this.Hide();
            OCMain main = new OCMain(CurrentOCPakNo);
            main.Show();
        }

        private void ApplicationDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the selected row.
            int select = e.RowIndex;

            // Check if the selected row index is within a valid range.
            if (select >= -2)
            {
                // Retrieve the selected row.
                DataGridViewRow row = ApplicationDV.Rows[select];

                // Set the text of PakNoCB ComboBox to the value in the "PakNO" cell of the selected row.
                PakNoCB.Text = row.Cells["PakNO"].Value.ToString();

                // Set the text of InputStatusCB ComboBox to the value in the "Status" cell of the selected row.
                InputStatusCB.Text = row.Cells["Status"].Value.ToString();

                // Set the text of InputContext TextBox to the value in the "Context" cell of the selected row.
                InputContext.Text = row.Cells["Context"].Value.ToString();

                // Set the text of InputId TextBox to the value in the "ReqId" cell of the selected row.
                InputId.Text = row.Cells["ReqId"].Value.ToString();
            }

        }
    }
}
