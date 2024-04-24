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
    public partial class EditMission : Form
    {
        public EditMission()
        {
            InitializeComponent();
        }

        private void EditMission_Load(object sender, EventArgs e)
        {   //displays the user on the heading
            Misssionhdbt.Text = ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName() + "'s Mission Menu";
            try
            {
                //This will fill the datatable and the pak no comboboxes as soon as the page loads
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsCompleted", typeof(bool));
                dataTable.Columns.Add("SuccessRate", typeof(float));

                List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());
                for (int i = 0; i < missions.Count; i++)
                {
                    dataTable.Rows.Add(missions[i].GetDate(), missions[i].GetDetails(), missions[i].GetIsComplete(), missions[i].GetSuccessRate());
                }
                //assigns the datatable a source
                MissionDGV.DataSource = dataTable;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Backbt_Click(object sender, EventArgs e)
        {   //Takes back to main menu
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void MissionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the selected row.
            int SelectedRow = e.RowIndex;

            // Check if the selected row index is within a valid range.
            if (SelectedRow >= -2 && SelectedRow < MissionDGV.Rows.Count)
            {
                // Retrieve the selected row.
                DataGridViewRow row = MissionDGV.Rows[SelectedRow];

                // Populate dateTimePicker1 with the value from the "Date" cell of the selected row if it's not null.
                if (row.Cells["Date"].Value != null)
                    dateTimePicker1.Text = row.Cells["Date"].Value.ToString();
                else
                    dateTimePicker1.Text = string.Empty; // Clear the control if the cell is null.

                // Populate InputDetails with the value from the "Details" cell of the selected row if it's not null.
                if (row.Cells["Details"].Value != null)
                    InputDetails.Text = row.Cells["Details"].Value.ToString();
                else
                    InputDetails.Text = string.Empty; // Clear the control if the cell is null.

                // Clear the text of IsComplete TextBox.
                IsComplete.Text = string.Empty;

                // Populate InputSuccess with the value from the "SuccessRate" cell of the selected row if it's not null.
                if (row.Cells["SuccessRate"].Value != null)
                    InputSuccess.Text = row.Cells["SuccessRate"].Value.ToString();
                else
                    InputSuccess.Text = string.Empty; // Clear the control if the cell is null.
            }

        }

        private void SetIncompletebt_Click(object sender, EventArgs e)
        {
            try
            {
                // Uncheck the IsComplete checkbox.
                IsComplete.Checked = false;

                // Retrieve the mission object associated with the selected date from the dateTimePicker1 control.
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(DateTime.Parse(dateTimePicker1.Text));

                // Set the IsComplete property of the mission object to false.
                mission.SetIsComplete(false);

                // Set the SuccessRate property of the mission object to 0.
                mission.SetSuccessRate(0);

                // Update the mission entry in the database with the modified mission object.
                Interfaces.GetMissionInterface().UpdateMission(DateTime.Parse(dateTimePicker1.Text), mission);

                // Show a success message to the user.
                MessageBox.Show("Set Successfully");
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur in a MessageBox.
                MessageBox.Show(ex.Message);
            }


        }

        private void Updatebt_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the text value from InputSuccess TextBox into a float.
                float success = float.Parse(InputSuccess.Text);

                // Retrieve the mission object associated with the selected date from the dateTimePicker1 control.
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(DateTime.Parse(dateTimePicker1.Text));

                // Set the SuccessRate property of the mission object to the parsed success rate.
                mission.SetSuccessRate(success);

                // Update the mission entry in the database with the modified mission object.
                Interfaces.GetMissionInterface().UpdateMission(dateTimePicker1.Value, mission);

                // Show a success message to the user.
                MessageBox.Show("Updated successfully");
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur in a MessageBox.
                MessageBox.Show(ex.Message);
            }

        }
    }
}
