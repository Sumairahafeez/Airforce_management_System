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
    public partial class ViewApplication : Form
    {
        public ViewApplication()
        {
            InitializeComponent();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {// Parse the PakNo and ID from the InputPakNo and InputId TextBoxes
            int Pakno = int.Parse(InputPakNo.Text);
            int id = int.Parse(InputId.Text);

            // Check if a request with the given PakNo and ID exists
            Requests req = Validations.IsValidRequest(Pakno, id);

            // If a request exists, display its context and status
            if (req != null)
            {
                // Show TextBoxes for context and status
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

        private void Backbt_Click(object sender, EventArgs e)
        {   //go back to main Menu
            this.Hide();
            GDPMenu Menu = new GDPMenu();
            Menu.Show();
        }

        private void ViewApplication_Load(object sender, EventArgs e)
        {    // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Mission Menu"
             // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Request Menu"
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s Request Menu";

            try
            {
                // Create a new DataTable to hold request data
                DataTable data = new DataTable();

                // Add columns to the DataTable
                data.Columns.Add("ReqId", typeof(int));
                data.Columns.Add("Context", typeof(string));
                data.Columns.Add("PakNO", typeof(int));
                data.Columns.Add("Status", typeof(string));

                // Retrieve requests of a specific officer (GDPilot) from the database
                List<Requests> requ = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());

                // Populate the DataTable with request data
                foreach (Requests request in requ)
                {
                    data.Rows.Add(request.GetRequestId(), request.GetContext(), request.GetPakNo(), request.GetStatus());
                }

                // Set the DataSource of the DataGridView to the populated DataTable
                ApplicationDV.DataSource = data;
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void ApplicationDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { // Get the index of the selected row
            int select = e.RowIndex;

            // Check if the selected row index is valid
            if (select >= -2)
            {
                // Retrieve the selected row
                DataGridViewRow row = ApplicationDV.Rows[select];

                // Populate InputPakNo with the value from the "PakNO" cell of the selected row
                InputPakNo.Text = row.Cells["PakNO"].Value.ToString();

                // Populate InputStatusT with the value from the "Status" cell of the selected row
                InputStatusT.Text = row.Cells["Status"].Value.ToString();

                // Populate InputContextT with the value from the "Context" cell of the selected row
                InputContextT.Text = row.Cells["Context"].Value.ToString();

                // Populate InputId with the value from the "ReqId" cell of the selected row
                InputId.Text = row.Cells["ReqId"].Value.ToString();
            }

        }
    }
}
