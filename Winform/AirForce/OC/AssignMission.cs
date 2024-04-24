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

namespace AirForce.OC
{
    public partial class AssignMission : Form
    {
        int CurrentOCPakNo;
        public AssignMission()
        {
            InitializeComponent();
        }
        public AssignMission(int OCPakNo)
        {
            CurrentOCPakNo = OCPakNo;
            InitializeComponent();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {   //This check button will work to check the recent missions of the given under officer 

            // Set visibility of UI elements to true to make them visible to the user
            Date.Visible = true;
            DateT.Visible = true;
            InputDetails.Visible = true;
            DetailT.Visible = true;
            IsCompleteT.Visible = true;
            InputIsComplete.Visible = true;
            SuccessT.Visible = true;
            InputSuccessRate.Visible = true;
            MissionDGV.Visible = true;

            // Retrieve the officer's PakNo from the combo box
            int PakNo = int.Parse(PakNoCB.Text);
            // Retrieve the officer's name from the input field
            string Name = InputName.Text;

            try
            {
                // Create a DataTable to store mission details
                DataTable dataTable = new DataTable();
                // Add columns to the DataTable for mission attributes
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Date", typeof(string));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsComplete", typeof(string));
                dataTable.Columns.Add("SuccessRate", typeof(float));

                // Retrieve a list of missions associated with the officer's PakNo
                List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(PakNo);

                // Iterate over each mission and add its details to the DataTable
                for (int i = 0; i < missions.Count; i++)
                {
                    dataTable.Rows.Add(Name, PakNo, missions[i].GetDate(), missions[i].GetDetails(), missions[i].GetIsComplete(), missions[i].GetSuccessRate());
                }

                // Set the DataSource of the DataGridView to the populated DataTable
                MissionDGV.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                // Display an error message box if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }

        private void Assignbt_Click(object sender, EventArgs e)
        {

            // Set visibility of date label and its associated input control to true
            Date.Visible = true;
            DateT.Visible = true;

            // Set visibility of input details label and its associated input control to true
            InputDetails.Visible = true;
            DetailT.Visible = true;

            // Set visibility of button3 (assuming it's a button control) to true
            button3.Visible = true;

            // Set visibility of completion status label and its associated input control to false
            IsCompleteT.Visible = false;
            InputIsComplete.Visible = false;

            // Set visibility of success rate label and its associated input control to false
            SuccessT.Visible = false;
            InputSuccessRate.Visible = false;

        }

        private void assignMissionbt_Click(object sender, EventArgs e)
        {
            // Get the selected date from a date picker control
            DateTime date = Date.Value;

            // Get the mission details from a text input control
            string Details = InputDetails.Text;

            // Get the officer's PakNo from a combo box and parse it to an integer
            int PakNO = int.Parse(PakNoCB.Text);

            // Get the officer's name from a text input control
            string name = InputName.Text;

            // Check if the provided PakNo is valid
            bool isValid = Validations.IsValidAFPersonalle(PakNO);

            // If the PakNo is valid
            if (isValid)
            {
                // Retrieve the GDPilot associated with the PakNo
                GDPilot AF = Interfaces.GetGdpInterface().GetGDPThroughPakNo(PakNO);

                // Retrieve the current commanding officer
                CommandingOfficers OC = ConnectionClass.GetCurrentOC();

                // Check if the GDPilot is under the command of the current officer
                bool isUnderOfficer = OC.IsValidUnderOfficer(AF);

                // If the GDPilot is under the command of the current officer
                if (isUnderOfficer)
                {
                    // Create a new mission object with the provided date and details
                    Mission newMission = new Mission(date, Details);

                    // Assign the mission to the officer with the provided PakNo
                    OC.AssignMission(PakNO, newMission);

                    // Store the mission in the database
                    Interfaces.GetMissionInterface().StoreMission(newMission, PakNO);

                    // Display a success message
                    MessageBox.Show("Mission Assigned Successfully");
                }
            }
            else
            {
                // Display a message indicating that the provided PakNo is invalid
                MessageBox.Show("Invalid PakNo");
            }

        }

        private void MissionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the selected row in the DataGridView
            int SelectedRow = e.RowIndex;

            // Check if the selected row index is within valid range
            if (SelectedRow >= -2 && SelectedRow < MissionDGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = MissionDGV.Rows[SelectedRow];

                // Populate PakNo combo box with the value from the selected row's "PakNo" cell, if not null
                if (row.Cells["PakNo"].Value != null)
                    PakNoCB.Text = row.Cells["PakNo"].Value.ToString();

                // Populate InputName text box with the value from the selected row's "Name" cell, if not null
                if (row.Cells["Name"].Value != null)
                    InputName.Text = row.Cells["Name"].Value.ToString();
                else
                    InputName.Text = string.Empty;

                // Populate InputDetails text box with the value from the selected row's "Details" cell, if not null
                if (row.Cells["Details"].Value != null)
                    InputDetails.Text = row.Cells["Details"].Value.ToString();
                else
                    InputDetails.Text = string.Empty;

                // Populate InputIsComplete text box with the value from the selected row's "IsComplete" cell, if not null
                if (row.Cells["IsComplete"].Value != null)
                    InputIsComplete.Text = row.Cells["IsComplete"].Value.ToString();
                else
                    InputIsComplete.Text = string.Empty;

                // Populate InputSuccessRate text box with the value from the selected row's "SuccessRate" cell, if not null
                if (row.Cells["SuccessRate"].Value != null)
                    InputSuccessRate.Text = row.Cells["SuccessRate"].Value.ToString();
                else
                    InputSuccessRate.Text = string.Empty;
            }

        }

        private void AssignMission_Load(object sender, EventArgs e)
        {
            try
            {
                // Create a DataTable to store officer data
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));

                // Retrieve the commanding officer based on the current officer's PakNo
                CommandingOfficers OC = Interfaces.GetOCInterface().GetOCbyId(CurrentOCPakNo);

                // Retrieve a list of subordinate GDPilots under the current commanding officer
                List<GDPilot> Unders = Interfaces.GetGdpInterface().GetAllUFofOC(CurrentOCPakNo);

                // Iterate over each subordinate GDPilot and add their details to the DataTable
                for (int i = 0; i < Unders.Count; i++)
                {
                    dataTable.Rows.Add(Unders[i].GetName(), Unders[i].GetPakNo(), Unders[i].GetRank(), Unders[i].GetPresentlyPosted());
                }

                // Set the DataSource of the OfficerGV DataGridView to the populated DataTable
                OfficerGV.DataSource = dataTable;

                // Now fill the PakNo combo box with available data
                string query1 = "SELECT * FROM GDP g, AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + CurrentOCPakNo + "))\r\n";

                // Retrieve data from the database based on the query
                PakNoCB.DataSource = Validations.GetData(query1);
                // Set the display member for the combo box
                PakNoCB.DisplayMember = "PakNo";
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCMain main = new OCMain(CurrentOCPakNo);
            main.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the selected date from the Date input field
                DateTime date = DateTime.Parse(Date.Text);

                // Get the mission details from the input field
                string Details = InputDetails.Text;

                // Get the selected PakNo from the PakNo combo box and parse it to an integer
                int PakNO = int.Parse(PakNoCB.Text);

                // Get the officer's name from the input field
                string name = InputName.Text;

                // Check if the provided PakNo is valid
                bool isValid = Validations.IsValidAFPersonalle(PakNO);

                // If the PakNo is valid
                if (isValid)
                {
                    // Retrieve the GDPilot associated with the PakNo
                    GDPilot AF = Interfaces.GetGdpInterface().GetGDPThroughPakNo(PakNO);

                    // Retrieve the commanding officer based on the current officer's PakNo
                    CommandingOfficers OC = Interfaces.GetOCInterface().GetOCbyId(CurrentOCPakNo);

                    // Create a new mission object with the provided date and details
                    Mission newMission = new Mission(date, Details);

                    // Assign the mission to the GDPilot with the provided PakNo
                    OC.AssignMission(PakNO, newMission);

                    // Store the mission in the database
                    Interfaces.GetMissionInterface().StoreMission(newMission, PakNO);

                    // Display a success message
                    MessageBox.Show("Mission Assigned Successfully");
                }
                else
                {
                    // Display a message indicating that the provided PakNo is invalid
                    MessageBox.Show("Invalid PakNo");
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }
    }
}
