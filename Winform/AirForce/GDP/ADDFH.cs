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
    public partial class ADDFH : Form
    {
        public ADDFH()
        {
            InitializeComponent();
        }

        private void checkbt_Click(object sender, EventArgs e)
        {

            try
            {
                // Retrieve the current GDPilot
                GDPilot G = Interfaces.GetGdpInterface().GetGDPThroughPakNo(ConnectionClass.GetCurrentGDP().GetPakNo());

                // Determine the squadron of the GDPilot
                string squadron = G.GetSquadron();

                // Show the appropriate RichTextBox based on the squadron
                if (squadron == "No 2 Minhas" || squadron == "No 27 Zarrars")
                {
                    richTextBox2.Visible = true;
                }
                else if (squadron == "No 5 Falcons" || squadron == "No 9 Griffins")
                {
                    richTextBox3.Visible = true;
                }
                else if (squadron == "No 15 Cobras")
                {
                    richTextBox6.Visible = true;
                }
                else
                {
                    // Inform the user if the squadron jets are not included yet
                    MessageBox.Show("Your Squadron Jets Are not included yet");
                }

                // Update the GDPilot's data in the database
                Interfaces.GetGdpInterface().UpdateGDP(ConnectionClass.GetCurrentGDP().GetPakNo(), G);
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }


        }

        private void backbt_Click(object sender, EventArgs e)
        {//takes back to main menu
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Savebt_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the current GDPilot
                GDPilot G = Interfaces.GetGdpInterface().GetGDPThroughPakNo(ConnectionClass.GetCurrentGDP().GetPakNo());

                // Determine the squadron of the GDPilot
                string squadron = G.GetSquadron();

                // Update the flying hours based on the squadron
                if (squadron == "No 2 Minhas")
                {
                    // Update flying hours for squadron No 2 Minhas
                    G.SetFlyingHours(int.Parse(InputJF17.Text));
                }
                else if (squadron == "No 5 Falcons" || squadron == "No 9 Griffins" || squadron == "No 27 Zarrars")
                {
                    // Update flying hours for squadrons No 5 Falcons, No 9 Griffins, and No 27 Zarrars
                    G.SetFlyingHours(int.Parse(InputF16.Text));
                }
                else if (squadron == "No 15 Cobras")
                {
                    // Update flying hours for squadron No 15 Cobras
                    G.SetFlyingHours(int.Parse(InputMirage.Text));
                }

                // Update the GDPilot's flying hours in the database
                Interfaces.GetGdpInterface().UpdateGDP(ConnectionClass.GetCurrentGDP().GetPakNo(), G);
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }


        }

        private void ADDFH_Load(object sender, EventArgs e)
        {
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
            int SelectedRow = e.RowIndex;
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                // Retrieve the selected row.
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];

                // Check if the "PakNo" cell of the selected row has a value.
                if (row.Cells["PakNo"].Value != null)
                {
                    // Populate the text of the InputPakNO TextBox with the value from the "PakNo" cell.
                    InputPakNO.Text = row.Cells["PakNo"].Value.ToString();
                }
            }

        }
    }
}
