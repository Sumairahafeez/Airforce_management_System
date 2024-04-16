using AirForceLibrary.BL;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForce.IT
{
    public partial class RemoveOfficer : Form
    {
        public static int SelectedRow;
        public RemoveOfficer()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }

        private void RemoveOfficer_Load(object sender, EventArgs e)
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
                List<GDPilot> GDPS = Interfaces.GetGdpInterface().GetAllGdps();
                foreach (var gdp in GDPS)
                {
                    dataTable.Rows.Add(gdp.GetName(), gdp.GetPakNo(), gdp.GetRank(), gdp.GetPresentlyPosted(), gdp.GetSquadron());
                }

                // Retrieve all Commanding Officers (OCs) and add their information to the DataTable
                List<CommandingOfficers> ALLOc = Interfaces.GetOCInterface().GetAll();
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

        private void Deletebt_Click(object sender, EventArgs e)
        {  // This function deletes a specific officer
            try
            {
                // Retrieve PakNo and Name from the input fields
                int PakNO = int.Parse(PakNoCB.Text);
                string Name = InputName.Text;

                // Check if the provided PakNo and Name correspond to a valid Commanding Officer (OC)
                bool IsValidOC = Validations.IsValidOC(PakNO, Name);

                // If the provided PakNo and Name correspond to a valid Commanding Officer (OC)
                if (IsValidOC)
                {
                    // Delete the Commanding Officer (OC) with the specified PakNo
                    Interfaces.GetOCInterface().DeleteOC(PakNO);

                    // Display a success message
                    MessageBox.Show("OC DELETED SUCCESSFULLY");

                    // Clear input data
                    ClearData();
                }
                else
                {
                    // If the provided PakNo and Name do not correspond to a Commanding Officer (OC), check if it corresponds to a GDPilot
                    bool IsValidGDP = Validations.IsValidGDP(PakNO, Name);

                    // If the provided PakNo and Name correspond to a GDPilot
                    if (IsValidGDP)
                    {
                        // Delete the GDPilot with the specified PakNo
                        Interfaces.GetGdpInterface().DeleteGDP(PakNO);

                        // Display a success message
                        MessageBox.Show("GDP DELETED SUCCESSFULLY");

                        // Clear input data
                        ClearData();
                    }
                    else
                    {
                        // If neither a Commanding Officer (OC) nor a GDPilot was found with the provided PakNo and Name, display an error message
                        MessageBox.Show("Invalid Input");

                        // Clear input data
                        ClearData();
                    }
                }
                // And remove it from DataGridView too
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   // This function serves to select a row of data from a given DataGridView and display it on the corresponding input fields
            int SelectedRow = e.RowIndex; // Get the index of the selected row
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                // Check if the selected row index is valid (greater than or equal to -2 and less than the total number of rows)
                DataGridViewRow row = OfficerGV.Rows[SelectedRow]; // Retrieve the selected row

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

                // Set the InputBranch TextBox to "GDP"
                InputBranch.Text = "GDP";
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
    }
    
}
