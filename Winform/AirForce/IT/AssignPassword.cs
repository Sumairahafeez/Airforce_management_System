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
            // Parse the text value from PakNoCB ComboBox into an integer.
            int PakNo = int.Parse(PakNoCB.Text);

            // Retrieve the password entered by the user.
            string Password = InputPassword.Text;

            // Retrieve the confirmation password entered by the user.
            string ConfirmPassword = InputConfirmPassword.Text;

            // Check if the entered password matches the confirmed password.
            if (Password == ConfirmPassword)
            {
                // Retrieve the Air Force personnel object associated with the given PakNo.
              
                GDPilot Pilot = Interfaces.GetGdpInterface().GetGDPThroughPakNo(PakNo);
                if(Pilot != null)
                {   //create an instance of ITPersonalle to manage passwords
                    ITPersonalle personalle = ITPersonalle.GetValidInstance();

                    // Attempt to assign the password to the Air Force personnel.
                    bool isAssigned = personalle.AssignPassword(Password, Pilot);

                    // If the password is successfully assigned:
                    if (isAssigned)
                    {
                        // Update the password of the Air Force personnel in the database.
                       
                        Interfaces.GetGdpInterface().UpdateGDP(PakNo,Pilot);
                        // Show a success message to the user.
                        MessageBox.Show("Password Assigned Successfully");
                    }
                    else
                    {
                        // Show a message indicating failure to assign the password.
                        MessageBox.Show("Unable to Assign");
                    }
                }
                else
                {
                    CommandingOfficers CO = Interfaces.GetOCInterface().GetOCbyId(PakNo);
                    if(CO != null)
                    {
                        //create an instance of ITPersonalle to manage passwords
                        ITPersonalle personalle = ITPersonalle.GetValidInstance();

                        // Attempt to assign the password to the Air Force personnel.
                        bool isAssigned = personalle.AssignPassword(Password, CO);

                        // If the password is successfully assigned:
                        if (isAssigned)
                        {
                            // Update the password of the Air Force personnel in the database.
                           
                            Interfaces.GetOCInterface().UpdateOC(PakNo,CO);
                            // Show a success message to the user.
                            MessageBox.Show("Password Assigned Successfully");
                        }
                        else
                        {
                            // Show a message indicating failure to assign the password.
                            MessageBox.Show("Unable to Assign");
                        }
                    }
                }
               
            }
            else
            {
                // Show a message indicating that the passwords don't match.
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
