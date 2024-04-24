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
    public partial class UpdateFH : Form
    {
        public UpdateFH()
        {
            InitializeComponent();
        }

        private void checkbt_Click(object sender, EventArgs e)
        {   // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "FlyingHours Menu"
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s FlyingHours Menu";

            // Check if the entered PakNo is valid
            bool isValid = Validations.IsValidGDP(int.Parse(InputPakNo.Text));
            if (isValid)
            {
                // Show the RichTextBoxes and TextBoxes for input
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

                    // Populate the appropriate TextBox with flying hours based on the squadron
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
                // Inform the user that the GDPilot does not exist
                MessageBox.Show("GDP Does not Exist");
            }


        }

        private void Backbt_Click(object sender, EventArgs e)
        {//go back to main menu
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Updatebt_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve GDPilot data using the entered PakNo
                GDPilot G = Interfaces.GetGdpInterface().GetGDPThroughPakNo(int.Parse(InputPakNo.Text));
                string squadron = G.GetSquadron();

                // Update the flying hours based on the squadron
                if (squadron == "No 2 Minhas" || squadron == "No 27 Zarrars")
                {
                    G.SetFlyingHours(int.Parse(InputJF17.Text));
                }
                else if (squadron == "No 5 Falcons" || squadron == "No 9 Griffins")
                {
                    G.SetFlyingHours(int.Parse(InputF16.Text));
                }
                else if (squadron == "No 15 Cobras")
                {
                    G.SetFlyingHours(int.Parse(InputMirage.Text));
                }

                // Update the GDPilot's flying hours in the database
                Interfaces.GetGdpInterface().UpdateGDP(G.GetPakNo(), G);
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void UpdateFH_Load(object sender, EventArgs e)
        {
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
            // Retrieve the index of the selected row
            int SelectedRow = e.RowIndex;

            // Check if the selected row index is valid
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];

                // Check if the "PakNo" cell of the selected row has a value
                if (row.Cells["PakNo"].Value != null)
                {
                    // Populate the InputPakNo TextBox with the value from the "PakNo" cell
                    InputPakNo.Text = row.Cells["PakNo"].Value.ToString();
                }
            }

        }
    }
}
