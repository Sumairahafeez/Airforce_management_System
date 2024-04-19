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

namespace AirForce.IT
{
    public partial class AssignPassword : Form
    {
        public AssignPassword()
        {
            InitializeComponent();
        }

        private void AssignPassword_Load(object sender, EventArgs e)
        {
            try
            {
                // Create a DataTable to store officer data
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PakNo", typeof(int));
                dataTable.Columns.Add("Rank", typeof(string));
                dataTable.Columns.Add("Posted", typeof(string));
                dataTable.Columns.Add("Squadron", typeof(string));
                dataTable.Columns.Add("Branch", typeof(string));

                // Retrieve all GDPilots and add their information to the DataTable
                List<GDPilot> GDPS = Interfaces.GetGdpInterface().GetAllGdps();
                foreach (var gdp in GDPS)
                {
                    dataTable.Rows.Add(gdp.GetName(), gdp.GetPakNo(), gdp.GetRank(), gdp.GetPresentlyPosted(), gdp.GetSquadron(),gdp.GetBranch());
                }

                // Retrieve all Commanding Officers (OCs) and add their information to the DataTable
                List<CommandingOfficers> ALLOc = Interfaces.GetOCInterface().GetAll();
                foreach (var oc in ALLOc)
                {
                    dataTable.Rows.Add(oc.GetName(), oc.GetPakNo(), oc.GetRank(), oc.GetPresentlyPosted(), oc.GetSquadron());
                }

                // Set the DataSource of OfficerGV DataGridView to the populated DataTable
                OfficerGV.DataSource = dataTable;

                // Fill the PakNoCB ComboBox with available PakNo data
                string query1 = "SELECT PakNo FROM GDP g, OC o, AFPersonalle a WHERE g.OfficerId = a.Id OR o.OffId = a.Id";
                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show(ex.Message);
            }

        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedRow = e.RowIndex; // Get the index of the selected row

            // Check if the selected row index is valid (greater than or equal to -2 and less than the total number of rows)
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                // Retrieve the selected row
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];

                // Update the PakNoCB ComboBox with the PakNo value from the selected row, if available
                if (row.Cells["PakNo"].Value != null)
                    PakNoCB.Text = row.Cells["PakNo"].Value.ToString();
                else
                    PakNoCB.Text = string.Empty;

                // Update the InputName TextBox with the Name value from the selected row, if available
                if (row.Cells["Name"].Value != null)
                    InputName.Text = row.Cells["Name"].Value.ToString();
                else
                    InputName.Text = string.Empty;

                // Update the InputRank TextBox with the Rank value from the selected row, if available
                if (row.Cells["Rank"].Value != null)
                    InputRank.Text = row.Cells["Rank"].Value.ToString();
                else
                    InputRank.Text = string.Empty;

                // Update the InputPosting TextBox with the Posted value from the selected row, if available
               
            }

        }

        private void Setbt_Click(object sender, EventArgs e)
        {
            int PakNo = int.Parse(PakNoCB.Text);
            string Password = InputPassword.Text;
            string ConfirmPassword = InputConfirmPassword.Text;
            if(Password == ConfirmPassword)
            {
                AFPersonalle aFPersonalle = Interfaces.GetAFInterface().GetAFPersonalleByID(PakNo);
                if (aFPersonalle != null)
                {
                    ITPersonalle personalle = ITPersonalle.GetValidInstance();
                    bool isAssigned = personalle.AssignPassword(Password, aFPersonalle);
                    if(isAssigned)
                    {
                        aFPersonalle.SetPassword(Password);
                        Interfaces.GetAFInterface().UpdateAFPersonalle(PakNo, aFPersonalle);
                        MessageBox.Show("Password Assigned Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Unable to Assign");
                    }
                }
            }
            else
            {
                MessageBox.Show("Passwords don't match");
            }
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }
    }
}
