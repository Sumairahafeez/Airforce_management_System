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
           
            Date.Visible = true;
            DateT.Visible = true;
            InputDetails.Visible = true;
            DetailT.Visible = true;
            IsCompleteT.Visible = true;
            InputIsComplete.Visible = true;
            SuccessT.Visible = true;
            InputSuccessRate.Visible = true;
            int PakNo =int.Parse(PakNoCB.Text);
            string Name = InputName.Text;
            try
            {
                //This will fill the datatable and the pak no comboboxes as soon as the button is pressed
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Date", typeof(string));
                dataTable.Columns.Add("Details", typeof(string));
                dataTable.Columns.Add("IsComplete", typeof(string));
                dataTable.Columns.Add("SuccessRate", typeof(float));
                List<Mission> missions = Interfaces.MissionInterface.GetAllMissionsOfSpecificOfficer(PakNo);
                for (int i = 0; i < missions.Count; i++)
                {
                    dataTable.Rows.Add(Name,PakNo,missions[i].GetDate(), missions[i].GetDetails(), missions[i].GetIsComplete(), missions[i].GetSuccessRate());
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Assignbt_Click(object sender, EventArgs e)
        {
           
            Date.Visible = true;
            DateT.Visible = true;
            InputDetails.Visible = true;
            DetailT.Visible = true;
           assign2.Visible = true;

        }

        private void assign2_Click(object sender, EventArgs e)
        {
            DateTime date = Date.Value;
            string Details = InputDetails.Text;
            int PakNO = int.Parse(PakNoCB.Text);
            string name = InputName.Text;
            bool isValid = Validations.IsValidAFPersonalle(PakNO);
            if (isValid)
            {
                AFPersonalle AF = Interfaces.AFInterface.GetAFPersonalleByID(PakNO);
                CommandingOfficers OC = ConnectionClass.CurrentOC;
                bool isUnderOfficer = OC.IsValidUnderOfficer(AF);
                if(isUnderOfficer)
                {   Mission newMission = new Mission(date,Details);
                    OC.AssignMission(PakNO, newMission);
                    Interfaces.MissionInterface.StoreMission(newMission,PakNO);
                    MessageBox.Show("Mission Assigned Successfully");
                }
            }
            else
            {
                MessageBox.Show("Invalid PakNo");
            }
           
        }

        private void MissionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedRow = e.RowIndex;
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];
                if (row.Cells["PakNo"].Value != null)

                    PakNoCB.Text = row.Cells["PakNo"].Value.ToString();



                if (row.Cells["Name"].Value != null)
                    InputName.Text = row.Cells["Name"].Value.ToString();
                else
                    InputName.Text = string.Empty;

                if (row.Cells["Date"].Value != null)
                    Date.Text = row.Cells["Date"].Value.ToString();
                else
                    Date.Text = string.Empty;

                if (row.Cells["Details"].Value != null)
                    InputDetails.Text = row.Cells["Details"].Value.ToString();
                else
                    InputDetails.Text = string.Empty;

                if (row.Cells["IsComplete"].Value != null)
                    InputIsComplete.Text = row.Cells["IsComplete"].Value.ToString();
                else
                    InputIsComplete.Text = string.Empty;
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
                //This will fill the datatable and the pak no comboboxes as soon as the page loads
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                CommandingOfficers OC = Interfaces.OCInterface.GetOCbyId(CurrentOCPakNo);
                List<AFPersonalle> Unders = OC.GetUnderOfficer();
                for (int i = 0; i < Unders.Count; i++)
                {
                    dataTable.Rows.Add(Unders[i].GetName(), Unders[i].GetPakNo(), Unders[i].GetRank(), Unders[i].GetPresentlyPosted());
                }
                OfficerGV.DataSource = dataTable;
                //Now fill the combo boxes with available data
                string query1 = "SELECT PakNo From GDP g,OC o,AFPersonalle a WHERE g.OfficerId = a.Id AND g.OCId = o.Id";
                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCMain main = new OCMain(CurrentOCPakNo);
            main.Show();
        }
    }
}
