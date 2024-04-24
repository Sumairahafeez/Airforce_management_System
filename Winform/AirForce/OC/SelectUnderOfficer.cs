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
    public partial class SelectUnderOfficer : Form
    {
        public int CurrentOCPakNo;
        public SelectUnderOfficer()
        {
            InitializeComponent();
        }
        public SelectUnderOfficer(int PakNo)
        {
            InitializeComponent();
            CurrentOCPakNo = PakNo;
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCMain main = new OCMain(CurrentOCPakNo);
            main.Show();
        }

        private void SelectUnderOfficer_Load(object sender, EventArgs e)
        {
            try
            {
                // This section populates a DataTable with data from a list of GDPilot objects.
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                dataTable.Columns.Add("Squadron", typeof(string));
                List<GDPilot> GDPS = Interfaces.GetGdpInterface().GetAllGdps();
                for (int i = 0; i < GDPS.Count; i++)
                {
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetPresentlyPosted(), GDPS[i].GetSquadron());
                }

                // Bind the DataTable to the DataGridView
                OfficerGV.DataSource = dataTable;
                // This section populates a ComboBox with data from a database query.
                if (ConnectionClass.GetIsUsingDB())
                {
                    string query1 = "SELECT * FROM  GDP g,AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + CurrentOCPakNo + "))\r\n";
                    PakNoCB.DataSource = Validations.GetData(query1);
                    PakNoCB.DisplayMember = "PakNo";
                }
                
                
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur in a MessageBox
                MessageBox.Show(ex.Message);
            }

        }

        private void Addbt_Click(object sender, EventArgs e)
        {
           // try
            {
                InputBranch.Text = "GDP";
                int PakNo = int.Parse(PakNoCB.Text);
                bool isValid = Validations.IsValidAFPersonalle(PakNo);//It will check if the AF Personal is valid or not
                if (isValid)
                {
                    GDPilot AF = Interfaces.GetGdpInterface().GetGDPThroughPakNo(PakNo);//From ID the personal is fetched
                    string Squadron = InputSquadron.Text;
                    //If valid it is checked if the squadrons and location of oc and underofficer matches of not
                    CommandingOfficers OC = Interfaces.GetOCInterface().GetOCbyId(CurrentOCPakNo);

                    bool Valid = Validations.IsFitForTHEOC(OC, AF, Squadron);
                    if (Valid)
                    {   // if the under office is added show message otherwise show the errrors
                        //bool isAdded = OC.AskForApproval(AF);
                      
                       if (OC.AskForApproval(AF) && (OC.IsValidUnderOfficer(AF)))
                        //if(isAdded)
                        {
                            AF.SetCommandingOfficer(OC);
                            Interfaces.GetGdpInterface().UpdateGDP(PakNo, AF);

                            Interfaces.GetOCInterface().UpdateOC(CurrentOCPakNo, OC);
                            MessageBox.Show("Officer Added Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Your Request was not approved you might have full capacity of under officers or the officer is already added");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your Squadrons or current locations Doesnot Match");
                    }

                }
                else
                {
                    MessageBox.Show("Officer Doesnot exists");
                }


            }
           // catch(Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }




        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // This event handler is triggered when a row is selected in the OfficerGV DataGridView.
                // It extracts the data from the selected row and displays it in various input fields.

                // Get the index of the selected row.
                int SelectedRow = e.RowIndex;

                // Check if the selected row index is within a valid range.
                if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
                {
                    // Retrieve the selected row.
                    DataGridViewRow row = OfficerGV.Rows[SelectedRow];

                    // Check if the 'PakNo' cell is not null and assign its value to the PakNoCB ComboBox.
                    if (row.Cells["PakNo"].Value != null)
                        PakNoCB.Text = row.Cells["PakNo"].Value.ToString();

                    // Check if the 'Name' cell is not null and assign its value to the InputName TextBox.
                    if (row.Cells["Name"].Value != null)
                        InputName.Text = row.Cells["Name"].Value.ToString();
                    else
                        InputName.Text = string.Empty; // Clear the input field if the cell is null.

                    // Check if the 'Rank' cell is not null and assign its value to the InputRank TextBox.
                    if (row.Cells["Rank"].Value != null)
                        InputRank.Text = row.Cells["Rank"].Value.ToString();
                    else
                        InputRank.Text = string.Empty; // Clear the input field if the cell is null.

                    // Check if the 'Posted' cell is not null and assign its value to the InputPosting TextBox.
                    if (row.Cells["Posted"].Value != null)
                        InputPosting.Text = row.Cells["Posted"].Value.ToString();
                    else
                        InputPosting.Text = string.Empty; // Clear the input field if the cell is null.

                    // Check if the 'Squadron' cell is not null and assign its value to the InputSquadron TextBox.
                    if (row.Cells["Squadron"].Value != null)
                        InputSquadron.Text = row.Cells["Squadron"].Value.ToString();
                    else
                        InputSquadron.Text = string.Empty; // Clear the input field if the cell is null.
                }
            }
            catch (Exception ex)
            {
                // Display any exceptions that occur in a MessageBox.
                MessageBox.Show(ex.Message);
            }

        }
    }
}
