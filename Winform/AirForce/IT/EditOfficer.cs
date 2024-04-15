using AirForceLibrary.BL;
using AirForceLibrary.DL;
using AirForceLibrary.Interfaces;
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

namespace AirForce.IT
{
    public partial class EditOfficer : Form
    {
        DataTable dataTable = new DataTable();
        public EditOfficer()
        {
            InitializeComponent();
        }

        private void EditOfficer_Load(object sender, EventArgs e)
        {
            try
            {
                // Create a DataTable to store officer data
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                dataTable.Columns.Add("Squadron", typeof(string));

                // Retrieve all GDPilots and add their information to the DataTable
                List<GDPilot> GDPS = Interfaces.GdpInterface.GetAllGdps();
                foreach (var gdp in GDPS)
                {
                    dataTable.Rows.Add(gdp.GetName(), gdp.GetPakNo(), gdp.GetRank(), gdp.GetPresentlyPosted(), gdp.GetSquadron());
                }

                // Retrieve all Commanding Officers (OCs) and add their information to the DataTable
                List<CommandingOfficers> ALLOc = Interfaces.OCInterface.GetAll();
                foreach (var oc in ALLOc)
                {
                    dataTable.Rows.Add(oc.GetName(), oc.GetPakNo(), oc.GetRank(), oc.GetPresentlyPosted(), oc.GetSquadron());
                }

                // Set the DataSource of OfficerGV DataGridView to the populated DataTable
                OfficerGV.DataSource = dataTable;

                // Fill the PakNoCB ComboBox with available PakNo data
                string query1 = "SELECT PakNo FROM GDP g, OC o, AFPersonalle a WHERE g.OfficerId = a.Id OR o.OffId = a.Id";
                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }

        private void Updatebt_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve input values from text boxes
                string name = InputName.Text;
                string Rank = InputRank.Text;
                int PakNO = int.Parse(PakNoCB.Text);
                string presentlyLocated = InputPosting.Text;
                string squadron = InputSquadron.Text;
                string branch = InputBranch.Text;
                
                // Check if the given user is an OC based on their rank
                bool IsOC = Validations.IsValidOC(Rank);

                // Validate the provided PakNo
                bool isValid = Validations.IsValidPakNo(PakNO);

                // If the user is identified as an OC
                if (IsOC)
                {
                    // Create a new CommandingOfficers object with the updated information
                    CommandingOfficers newOC = new CommandingOfficers(name, Rank, PakNO, presentlyLocated, squadron);

                    // Update the OC's information in the database
                    Interfaces.OCInterface.UpdateOC(PakNO, newOC);

                    // Display a success message
                    MessageBox.Show("OC Updated Successfully");

                    // Clear input data from text boxes
                    ClearData();
                }
                else // If the user is identified as a GDPilot
                {
                    // Create a new GDPilot object with the updated information
                    GDPilot newgdp = new GDPilot(name, Rank, PakNO, presentlyLocated, squadron);

                    // Update the GDPilot's information in the database
                    Interfaces.GdpInterface.UpdateGDP(PakNO, newgdp);

                    // Display a success message
                    MessageBox.Show("Officer Updated Successfully");

                    // Clear input data from text boxes
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
            PakNoCB.Text = string.Empty;
            InputRank.Text = string.Empty;
            InputSquadron.Text = string.Empty;
            InputPosting.Text = string.Empty;
            InputBranch.Text = string.Empty;
        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedRow = e.RowIndex; // Get the index of the selected row

            // Check if the selected row index is valid (greater than or equal to -2 and less than the total number of rows)
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];

                // Update the PakNoCB ComboBox with the PakNo value from the selected row, if available
                if (row.Cells["PakNo"].Value != null)
                    PakNoCB.Text = row.Cells["PakNo"].Value.ToString();
                else
                    PakNoCB.Text = string.Empty;

                // Update the InputName TextBox with the Name value from the selected row, if available
                if (row.Cells["Name"].Value != null)
                    InputName.Text = row.Cells["Name"].Value.ToString();
                else
                    InputName.Text = string.Empty;

                // Update the InputRank TextBox with the Rank value from the selected row, if available
                if (row.Cells["Rank"].Value != null)
                    InputRank.Text = row.Cells["Rank"].Value.ToString();
                else
                    InputRank.Text = string.Empty;

                // Update the InputPosting TextBox with the Posted value from the selected row, if available
                if (row.Cells["Posted"].Value != null)
                    InputPosting.Text = row.Cells["Posted"].Value.ToString();
                else
                    InputPosting.Text = string.Empty;

                // Update the InputSquadron TextBox with the Squadron value from the selected row, if available
                if (row.Cells["Squadron"].Value != null)
                    InputSquadron.Text = row.Cells["Squadron"].Value.ToString();
                else
                    InputSquadron.Text = string.Empty;
            }

        }
    }
}
