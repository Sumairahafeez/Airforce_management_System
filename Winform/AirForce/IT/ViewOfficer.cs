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
    public partial class ViewOfficer : Form
    {
        public ViewOfficer()
        {
            InitializeComponent();
        }

        private void ViewOfficer_Load(object sender, EventArgs e)
        {
            //try
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
            //catch (Exception ex)
            {
                // Display an error message if an exception occurs
                //MessageBox.Show(ex.Message);
            }

        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedRow = e.RowIndex;
            
            // This function serves to select data from a given DataGridView and display it on the DataTable
            //int SelectedRow = e.RowIndex; // Get the index of the selected row
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
            }

        }

        private void Checkbt_Click(object sender, EventArgs e)
        {   //This fucntion fills the remaining text boxes by only taking the pak no by itself
            try
            {
                int PakNo = int.Parse(PakNoCB.Text);
                bool isValidGDP = Validations.IsValidGDP(PakNo);
                if (isValidGDP)
                {   //If it is an GDP it will fill the given boxes with its data
                    GDPilot GDP = Interfaces.GetGdpInterface().GetGDPThroughPakNo(PakNo);
                    InputName.Text = GDP.GetName();
                    InputRank.Text = GDP.GetRank();
                    InputSquadron.Text = GDP.GetSquadron();
                    InputPosting.Text = GDP.GetPresentlyPosted();
                }
                else
                {   //Otherwise it will fill the given boxes with OC Data
                    bool isVallidOC = Validations.IsValidOC(PakNo);
                    CommandingOfficers Commander = Interfaces.GetOCInterface().GetOCbyId(PakNo);
                    InputName.Text = Commander.GetName();
                    InputRank.Text = Commander.GetRank();
                    InputSquadron.Text = Commander.GetSquadron();
                    InputPosting.Text = Commander.GetPresentlyPosted();
                }
            }
           catch(Exception ex)

            {
               MessageBox.Show(ex.Message);
            }
           
           
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }
    }
}
