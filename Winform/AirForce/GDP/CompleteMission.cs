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
    public partial class CompleteMission : Form
    {
        public CompleteMission()
        {
            InitializeComponent();
        }

        private void CompleteMission_Load(object sender, EventArgs e)
        {
            // Set the text of Missionhdbt to display the current GDPilot's rank, name, and "Mission Menu"
            Missionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s Mission Menu";

            try
            {
                // Create a DataTable to hold mission data
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsCompleted", typeof(bool));
                dataTable.Columns.Add("SuccessRate", typeof(float));

                // Retrieve all missions associated with the current GDPilot
                List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());

                // Populate the DataTable with mission data
                foreach (Mission mission in missions)
                {
                    dataTable.Rows.Add(mission.GetDate(), mission.GetDetails(), mission.GetIsComplete(), mission.GetSuccessRate());
                }

                // Bind the DataTable to the MissionDGV DataGridView
                MissionDGV.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {   //shows specific buttons on click
            if (IsComplete.Checked)
            {
                richTextBox5.Visible = true;
                InputSuccess.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {   //takes back to main menu
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //This will update the mission and set its completion and success rate
            try
            {
                // Parse the date selected in the dateTimePicker1 control
                DateTime Date = DateTime.Parse(dateTimePicker1.Text);

                // Retrieve the mission associated with the selected date
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(Date);

                // Set the success rate of the mission
                mission.SetSuccessRate(float.Parse(InputSuccess.Text));

                // Set the completion status of the mission based on the IsComplete CheckBox
                mission.SetIsComplete(IsComplete.Checked);

                // Update the mission in the database
                Interfaces.GetMissionInterface().UpdateMission(Date, mission);

                // Show a success message to the user
                MessageBox.Show("Mission Updated Successfully");
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur
                MessageBox.Show(ex.Message);
            }

        }

        private void MissionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve the index of the selected row
            int SelectedRow = e.RowIndex;

            // Check if the selected row index is within a valid range
            if (SelectedRow >= -2 && SelectedRow < MissionDGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = MissionDGV.Rows[SelectedRow];

                // Populate dateTimePicker1 with the date from the selected row, if available
                if (row.Cells["Date"].Value != null)
                    dateTimePicker1.Text = row.Cells["Date"].Value.ToString();
                else
                    dateTimePicker1.Text = string.Empty;

                // Populate InputDetails with the details from the selected row, if available
                if (row.Cells["Details"].Value != null)
                    InputDetails.Text = row.Cells["Details"].Value.ToString();
                else
                    InputDetails.Text = string.Empty;

                // Populate InputSuccess with the success rate from the selected row, if available
                if (row.Cells["SuccessRate"].Value != null)
                    InputSuccess.Text = row.Cells["SuccessRate"].Value.ToString();
                else
                    InputSuccess.Text = string.Empty;
            }

        }
    }
}
