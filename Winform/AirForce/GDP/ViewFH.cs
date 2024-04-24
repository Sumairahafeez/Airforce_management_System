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
    public partial class ViewFH : Form
    {
        public ViewFH()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {   //go back to mainmenu

            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {
            // Check if the entered PakNo corresponds to a valid GDPilot
            bool isValid = Validations.IsValidGDP(int.Parse(InputPakNo.Text));

            // If the PakNo is valid, show relevant controls and populate them with data
            if (isValid)
            {
                // Show relevant RichTextBoxes and TextBoxes
                richTextBox3.Visible = true;
                richTextBox2.Visible = true;
                richTextBox6.Visible = true;
                InputJF17.Visible = true;
                InputF16.Visible = true;
                InputMirage.Visible = true;

                try
                {
                    // Retrieve GDPilot data using the entered PakNo
                    GDPilot G = Interfaces.GetGdpInterface().GetGDPThroughPakNo(int.Parse(InputPakNo.Text));
                    string squadron = G.GetSquadron();

                    // Update the appropriate TextBox with the flying hours based on the squadron
                    if (squadron == "No 2 Minhas" || squadron == "No 27 Zarrars")
                    {
                        InputJF17.Text = G.GetFlyingHours().ToString();
                    }
                    else if (squadron == "No 5 Falcons" || squadron == "No 9 Griffins")
                    {
                        InputF16.Text = G.GetFlyingHours().ToString();
                    }
                    else if (squadron == "No 15 Cobras")
                    {
                        InputMirage.Text = G.GetFlyingHours().ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Display any exceptions that occur
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // If the PakNo is not valid, show a message indicating that the GDP does not exist
                MessageBox.Show("GDP Does Not Exist");
            }

        }

        private void ViewFH_Load(object sender, EventArgs e)
        {    // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Mission Menu"
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s FlyingHours Menu";

            try
            {
                //This will fill the datatable and the pak no comboboxes as soon as the page loads
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                dataTable.Columns.Add("Squadron", typeof(string));
                List<GDPilot> GDPS = Interfaces.GetGdpInterface().GetAllGdps();
                for (int i = 0; i < GDPS.Count; i++)
                {
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetSquadron(), GDPS[i].GetFlyingHours());
                }

                OfficerGV.DataSource = dataTable;
                //Now fill the combo boxes with available data


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the selected row
            int SelectedRow = e.RowIndex;

            // Check if the selected row index is valid
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];

                // Check if the "PakNo" cell of the selected row contains a value
                if (row.Cells["PakNo"].Value != null)
                {
                    // Populate InputPakNo with the value from the "PakNo" cell of the selected row
                    InputPakNo.Text = row.Cells["PakNo"].Value.ToString();
                }
            }

        }
    }
}
