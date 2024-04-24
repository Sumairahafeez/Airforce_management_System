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
    public partial class ViewMission : Form
    {
        public ViewMission()
        {
            InitializeComponent();
        }

        private void ViewMission_Load(object sender, EventArgs e)
        {
            // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Mission Menu"
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s Mission Menu";

            try
            {
                // Create a new DataTable to store mission data
                DataTable dataTable = new DataTable();

                // Add columns to the DataTable
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsCompleted", typeof(bool));
                dataTable.Columns.Add("SuccessRate", typeof(float));

                // Retrieve all missions of the specific officer and populate the DataTable
                List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());
                foreach (Mission mission in missions)
                {
                    dataTable.Rows.Add(mission.GetDate(), mission.GetDetails(), mission.GetIsComplete(), mission.GetSuccessRate());
                }

                // Set the DataTable as the data source for the MissionDGV (DataGridView)
                MissionDGV.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {   //go back to mainMenu
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {   //fill in details of the mission
            Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(dateTimePicker1.Value);
           string details = mission.GetDetails();
            InputDetails.Text  = details;

        }

        private void MissionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {// Get the index of the selected row
            int SelectedRow = e.RowIndex;

            // Check if the selected row index is valid
            if (SelectedRow >= -2 && SelectedRow < MissionDGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = MissionDGV.Rows[SelectedRow];

                // Set the value of the dateTimePicker1 to the "Date" cell value of the selected row
                // If the value is null, set the dateTimePicker1 text to empty
                if (row.Cells["Date"].Value != null)
                    dateTimePicker1.Text = row.Cells["Date"].Value.ToString();
                else
                    dateTimePicker1.Text = string.Empty;

                // Set the value of the InputDetails TextBox to the "Details" cell value of the selected row
                // If the value is null, set the InputDetails TextBox text to empty
                if (row.Cells["Details"].Value != null)
                    InputDetails.Text = row.Cells["Details"].Value.ToString();
                else
                    InputDetails.Text = string.Empty;
            }

        }
    }
}
