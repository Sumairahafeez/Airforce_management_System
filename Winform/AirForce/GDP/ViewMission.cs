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
            try
            {
                //This will fill the datatable and the pak no comboboxes as soon as the page loads
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsCompleted", typeof(bool));
                dataTable.Columns.Add("SuccessRate", typeof(float));

                List<Mission> missions = Interfaces.MissionInterface.GetAllMissionsOfSpecificOfficer(ConnectionClass.CurrentGDP.GetPakNo());
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {
            Mission mission = Interfaces.MissionInterface.GetMissionFromDate(dateTimePicker1.Value);
           string details = mission.GetDetails();
            InputDetails.Text  = details;

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

                
            }
        }
    }
}
