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
    public partial class DeletReq : Form
    {
        public DeletReq()
        {
            InitializeComponent();
        }

        private void DeletReq_Load(object sender, EventArgs e)
        {    // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Mission Menu"
             // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Request Menu"
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s Request Menu";

            try
            {
                // Create a DataTable to hold request data
                DataTable data = new DataTable();
                data.Columns.Add("ReqId", typeof(int));
                data.Columns.Add("Context", typeof(string));
                data.Columns.Add("PakNO", typeof(int));
                data.Columns.Add("Status", typeof(string));

                // Retrieve all requests associated with the current GDPilot
                List<Requests> requ = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());

                // Populate the DataTable with request data
                foreach (Requests request in requ)
                {
                    data.Rows.Add(request.GetRequestId(), request.GetContext(), request.GetPakNo(), request.GetStatus());
                }

                // Bind the DataTable to the ApplicationDV DataGridView
                ApplicationDV.DataSource = data;
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void ApplicationDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve the index of the selected row
            int select = e.RowIndex;

            // Check if the selected row index is valid
            if (select >= -2)
            {
                // Retrieve the selected row
                DataGridViewRow row = ApplicationDV.Rows[select];

                // Populate InputPakNo with the PakNO value from the selected row
                InputPakNo.Text = row.Cells["PakNO"].Value.ToString();

                // Populate InputStatusT with the Status value from the selected row
                InputStatusT.Text = row.Cells["Status"].Value.ToString();

                // Populate InputContextT with the Context value from the selected row
                InputContextT.Text = row.Cells["Context"].Value.ToString();

                // Populate InputId with the ReqId value from the selected row
                InputId.Text = row.Cells["ReqId"].Value.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the PakNo and ID from the InputPakNo and InputId TextBoxes
                int Pakno = int.Parse(InputPakNo.Text);
                int id = int.Parse(InputId.Text);

                // Check if the request with the given PakNo and ID exists
                Requests req = Validations.IsValidRequest(Pakno, id);

                // If the request exists, populate the relevant TextBoxes with its context and status
                if (req != null)
                {
                    // Show the TextBoxes for context and status
                    InputContextT.Visible = true;
                    contextBoxT.Visible = true;
                    InputStatusT.Visible = true;
                    StatusBoxT.Visible = true;

                    // Populate InputContextT with the context of the request
                    InputContextT.Text = req.GetContext();

                    // Populate InputStatusT with the status of the request
                    InputStatusT.Text = req.GetStatus();
                }
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void delbt_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the ApplicationId from the InputId TextBox
                int ApplicationId = int.Parse(InputId.Text);

                // Delete the request with the specified ApplicationId
                Interfaces.GetRequestInterface().DeleteRequests(ApplicationId);

                // Show a success message
                MessageBox.Show("Request Deleted Successfully");
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void Backbt_Click(object sender, EventArgs e)
        {   //go back to main menu
            this.Hide();
            GDPMenu gDPMenu = new GDPMenu();
            gDPMenu.Show();
        }
    }
}
