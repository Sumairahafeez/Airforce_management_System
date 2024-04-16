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
            try
            {
                //This will fill the datatable and the pak no comboboxes as soon as the page loads
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsCompleted", typeof(bool));
                dataTable.Columns.Add("SuccessRate", typeof(float));

                List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.CurrentGDP.GetPakNo());
                for (int i = 0; i < missions.Count; i++)
                {
                    dataTable.Rows.Add(missions[i].GetDate(), missions[i].GetDetails(), missions[i].GetIsComplete(), missions[i].GetSuccessRate());
                }
                
                MissionDGV.DataSource = dataTable;
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(IsComplete.Checked)
            {
                richTextBox5.Visible = true;
                InputSuccess.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                //This will update the mission and set its completion and success rate
                DateTime Date = DateTime.Parse(dateTimePicker1.Text);
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(Date);
                mission.SetSuccessRate(float.Parse(InputSuccess.Text));
                if (IsComplete.Checked)
                {
                    mission.SetIsComplete(true);
                }
                else
                {
                    mission.SetIsComplete(false);
                }
                Interfaces.GetMissionInterface().UpdateMission(Date, mission);
                MessageBox.Show("Mission Updated Successfully");
            
        }

        private void MissionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedRow = e.RowIndex;
            if (SelectedRow >= -2 && SelectedRow < MissionDGV.Rows.Count)
            {
                DataGridViewRow row = MissionDGV.Rows[SelectedRow];
               

                if (row.Cells["Date"].Value != null)
                    dateTimePicker1.Text = row.Cells["Date"].Value.ToString();
                else
                    dateTimePicker1.Text = string.Empty;

                if (row.Cells["Details"].Value != null)
                    InputDetails.Text = row.Cells["Details"].Value.ToString();
                else
                    InputDetails.Text = string.Empty;

                if (row.Cells["SuccessRate"].Value != null)
                    InputSuccess.Text = row.Cells["SuccessRate"].Value.ToString();
                else
                    InputSuccess.Text = string.Empty;
            }
        }
    }
}
