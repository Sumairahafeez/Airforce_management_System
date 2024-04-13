using AirForceLibrary.BL;
using AirForceLibrary.DL;
using AirForceLibrary.Interfaces;
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

namespace AirForce.IT
{
    public partial class EditOfficer : Form
    {
        DataTable dataTable = new DataTable();
        public EditOfficer()
        {
            InitializeComponent();
        }

        private void EditOfficer_Load(object sender, EventArgs e)
        {
            try
            {
                //This will fill the datatable and the pak no comboboxes as soon as the page loads
               
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                dataTable.Columns.Add("Squadron", typeof(string));
                List<GDPilot> GDPS = Interfaces.GdpInterface.GetAllGdps();
                for (int i = 0; i < GDPS.Count; i++)
                {
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetPresentlyPosted(), GDPS[i].GetSquadron());
                }
                List<CommandingOfficers> ALLOc = Interfaces.OCInterface.GetAll();
                for (int i = 0; i < ALLOc.Count; i++)
                {
                    dataTable.Rows.Add(ALLOc[i].GetName(), ALLOc[i].GetPakNo(), ALLOc[i].GetRank(), ALLOc[i].GetPresentlyPosted(), ALLOc[i].GetSquadron());
                }
                OfficerGV.DataSource = dataTable;
                //Now fill the combo boxes with available data
                string query1 = "SELECT PakNo From GDP g,OC o,AFPersonalle a WHERE g.OfficerId = a.Id OR o.OffId = a.Id";
                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }

        private void Updatebt_Click(object sender, EventArgs e)
        {
            try
            {
                string name = InputName.Text;
                string Rank = InputRank.Text;
                int PakNO = int.Parse(PakNoCB.Text);
                string presentlyLocated = InputPosting.Text;
                //After taking inout it checks if the given User is an OC through its Rank and the proceeds
                bool IsOC = Validations.IsValidOC(Rank);
                string squadron = InputSquadron.Text;
                bool isValid = Validations.IsValidPakNo(PakNO);
                string branch = InputBranch.Text;

                if (IsOC)
                {

                    CommandingOfficers newOC = new CommandingOfficers(name, Rank, PakNO, presentlyLocated, squadron);

                    Interfaces.OCInterface.UpdateOC(PakNO, newOC);
                    MessageBox.Show("OC Updated SuccessFully");
                    ClearData();
                }
                else
                {
                    GDPilot newgdp = new GDPilot(name, Rank, PakNO, presentlyLocated, squadron);
                    Interfaces.GdpInterface.UpdateGDP(PakNO, newgdp);
                    MessageBox.Show("Officer Updated SuccessFully");
                    ClearData();
                }
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }







        }
        public void ClearData()
        {
            InputName.Text = string.Empty;
            PakNoCB.Text = string.Empty;
            InputRank.Text = string.Empty;
            InputSquadron.Text = string.Empty;
            InputPosting.Text = string.Empty;
            InputBranch.Text = string.Empty;
        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

                if (row.Cells["Rank"].Value != null)
                    InputRank.Text = row.Cells["Rank"].Value.ToString();
                else
                    InputRank.Text = string.Empty;

                if (row.Cells["Posted"].Value != null)
                    InputPosting.Text = row.Cells["Posted"].Value.ToString();
                else
                    InputPosting.Text = string.Empty;

                if (row.Cells["Squadron"].Value != null)
                    InputSquadron.Text = row.Cells["Squadron"].Value.ToString();
                else
                    InputSquadron.Text = string.Empty;
            }
        }
    }
}
