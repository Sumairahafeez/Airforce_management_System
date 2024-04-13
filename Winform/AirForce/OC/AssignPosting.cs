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
    public partial class AssignPosting : Form
    {
        public int currentOC;
        public AssignPosting()
        {
            InitializeComponent();
        }
        public AssignPosting(int currentOC)
        {
            this.currentOC = currentOC;
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCMain main = new OCMain(currentOC);
            main.Show();
        }

        private void AssignPosting_Load(object sender, EventArgs e)
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
                List<GDPilot> GDPS = Interfaces.GdpInterface.GetAllUFofOC(currentOC);
                for (int i = 0; i < GDPS.Count; i++)
                {
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetPresentlyPosted(), GDPS[i].GetSquadron());
                }

                OfficersDV.DataSource = dataTable;
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("Name", typeof(string));
                dataTable2.Columns.Add("PakNo", typeof(int));
                dataTable2.Columns.Add("Rank", typeof(string));
                dataTable2.Columns.Add("Posted", typeof(string));
                dataTable2.Columns.Add("Squadron", typeof(string));
                List<CommandingOfficers> OC = Interfaces.OCInterface.GetAll();
                for (int i = 0; i < OC.Count; i++)
                {
                    dataTable2.Rows.Add(OC[i].GetName(), OC[i].GetPakNo(), OC[i].GetRank(), OC[i].GetPresentlyPosted(), OC[i].GetSquadron());
                }

                OCGV.DataSource = dataTable2;
                //Now fill the combo boxes with available data
                string query1 = "SELECT * FROM  GDP g,AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + currentOC + "))\r\n";

                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";
                InputOCCB.DataSource = dataTable2;
                InputOCCB.DisplayMember = "PakNo";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AssigmMissionbt_Click(object sender, EventArgs e)
        {
            try
            {
                int PkNo = int.Parse(PakNoCB.Text);
                bool isValid = Validations.IsValidGDP(PkNo);
                string PostedTo = InputPosting.Text;
                if (isValid)
                {
                    int PakNo = int.Parse(InputOCCB.Text);
                    bool isValidOC = Validations.IsValidOC(PakNo);
                    if (isValidOC)
                    {
                        CommandingOfficers newOC = Interfaces.OCInterface.GetOCbyId(PakNo);
                        CommandingOfficers OC = Interfaces.OCInterface.GetOCbyId(currentOC);
                        bool set = OC.SetPosting(PkNo, PostedTo, newOC);
                        if (set)
                        {   GDPilot G = Interfaces.GdpInterface.GetGDPThroughPakNo(PkNo);
                            G.SetCommandingOfficer(newOC);
                            Interfaces.GdpInterface.UpdateGDP(PkNo, G);
                            MessageBox.Show("Posted Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Can't be posted");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
