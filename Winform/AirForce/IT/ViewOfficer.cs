using AirForceLibrary.BL;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForce.IT
{
    public partial class ViewOfficer : Form
    {
        public ViewOfficer()
        {
            InitializeComponent();
        }

        private void ViewOfficer_Load(object sender, EventArgs e)
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

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //This function serves to select a data from a given datagrid view and show it on th edattable
            int SelectedRow = e.RowIndex;
            if (SelectedRow >= -2  && SelectedRow < OfficerGV.Rows.Count)
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

        private void Checkbt_Click(object sender, EventArgs e)
        {   //This fucntion fills the remaining text boxes by only taking the pak no by itself
            try
            {
                int PakNo = int.Parse(PakNoCB.Text);
                bool isValidGDP = Validations.IsValidGDP(PakNo);
                if (isValidGDP)
                {   //If it is an GDP it will fill the given boxes with its data
                    GDPilot GDP = Interfaces.GdpInterface.GetGDPThroughPakNo(PakNo);
                    InputName.Text = GDP.GetName();
                    InputRank.Text = GDP.GetRank();
                    InputSquadron.Text = GDP.GetSquadron();
                    InputPosting.Text = GDP.GetPresentlyPosted();
                }
                else
                {   //Otherwise it will fill the given boxes with OC Data
                    bool isVallidOC = Validations.IsValidOC(PakNo);
                    CommandingOfficers Commander = Interfaces.OCInterface.GetOCbyId(PakNo);
                    InputName.Text = Commander.GetName();
                    InputRank.Text = Commander.GetRank();
                    InputSquadron.Text = Commander.GetSquadron();
                    InputPosting.Text = Commander.GetPresentlyPosted();
                }
            }
            catch(Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
           
           
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }
    }
}
