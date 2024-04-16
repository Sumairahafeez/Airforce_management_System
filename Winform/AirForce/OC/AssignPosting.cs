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
                // Create a DataTable to store GDPilot data and populate it
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                dataTable.Columns.Add("Squadron", typeof(string));
                List<GDPilot> GDPS = Interfaces.GetGdpInterface().GetAllUFofOC(currentOC);
                for (int i = 0; i < GDPS.Count; i++)
                {
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetPresentlyPosted(), GDPS[i].GetSquadron());
                }

                // Set the DataSource of OfficersDV DataGridView to the populated DataTable
                OfficersDV.DataSource = dataTable;

                // Create a DataTable to store CommandingOfficers data and populate it
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("Name", typeof(string));
                dataTable2.Columns.Add("PakNo", typeof(int));
                dataTable2.Columns.Add("Rank", typeof(string));
                dataTable2.Columns.Add("Posted", typeof(string));
                dataTable2.Columns.Add("Squadron", typeof(string));
                List<CommandingOfficers> OC = Interfaces.GetOCInterface().GetAll();
                for (int i = 0; i < OC.Count; i++)
                {
                    dataTable2.Rows.Add(OC[i].GetName(), OC[i].GetPakNo(), OC[i].GetRank(), OC[i].GetPresentlyPosted(), OC[i].GetSquadron());
                }

                // Set the DataSource of OCGV DataGridView to the populated DataTable
                OCGV.DataSource = dataTable2;

                // Populate PakNoCB ComboBox with available data
                string query1 = "SELECT * FROM  GDP g, AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + currentOC + "))\r\n";
                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";

                // Populate InputOCCB ComboBox with available data
                InputOCCB.DataSource = dataTable2;
                InputOCCB.DisplayMember = "PakNo";
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }

        private void AssigmMissionbt_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the PakNo selected in the PakNoCB ComboBox
                int PkNo = int.Parse(PakNoCB.Text);

                // Check if the selected GDPilot's PakNo is valid
                bool isValid = Validations.IsValidGDP(PkNo);

                // Get the PostedTo information from the InputPosting TextBox
                string PostedTo = InputPosting.Text;

                // If the selected GDPilot's PakNo is valid
                if (isValid)
                {
                    // Parse the PakNo selected in the InputOCCB ComboBox (assuming it's for selecting a new OC)
                    int PakNo = int.Parse(InputOCCB.Text);

                    // Check if the selected Commanding Officer's PakNo is valid
                    bool isValidOC = Validations.IsValidOC(PakNo);

                    // If the selected Commanding Officer's PakNo is valid
                    if (isValidOC)
                    {
                        // Retrieve the new Commanding Officer based on the selected PakNo
                        CommandingOfficers newOC = Interfaces.GetOCInterface().GetOCbyId(PakNo);

                        // Retrieve the current Commanding Officer based on the currentOC
                        CommandingOfficers OC = Interfaces.GetOCInterface().GetOCbyId(currentOC);

                        // Set the posting for the GDPilot to the new Commanding Officer
                        bool set = OC.SetPosting(PkNo, PostedTo, newOC);

                        // If the posting is set successfully
                        if (set)
                        {
                            // Retrieve the GDPilot based on the PakNo
                            GDPilot G = Interfaces.GetGdpInterface().GetGDPThroughPakNo(PkNo);

                            // Set the Commanding Officer for the GDPilot to the new Commanding Officer
                            G.SetCommandingOfficer(newOC);

                            // Update the GDPilot's information in the database
                            Interfaces.GetGdpInterface().UpdateGDP(PkNo, G);

                            // Display a success message
                            MessageBox.Show("Posted Successfully");
                        }
                        else
                        {
                            // Display a message indicating that the GDPilot can't be posted
                            MessageBox.Show("Can't be posted");
                        }
                    }
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
