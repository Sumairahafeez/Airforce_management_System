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
    public partial class ViewFH : Form
    {
        public ViewFH()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {
            bool isValid = Validations.IsValidGDP(int.Parse(InputPakNo.Text));
            if (isValid)
            {
                richTextBox3.Visible = true;
                richTextBox2.Visible = true;
                richTextBox6.Visible = true;
                InputJF17.Visible = true;
                InputF16.Visible = true;
                InputMirage.Visible = true;
                try
                {
                    GDPilot G = Interfaces.GetGdpInterface().GetGDPThroughPakNo(int.Parse(InputPakNo.Text));
                    string squadron = G.GetSquadron();
                    if (squadron == "No 2 Minhas")
                    {
                        (InputJF17.Text) = G.GetFlyingHours().ToString();
                    }
                    else if (squadron == "No 5 Falcons")
                    {
                        (InputF16.Text) = G.GetFlyingHours().ToString();

                    }
                    else if (squadron == "No 9 Griffins")
                    {
                        (InputF16.Text) = G.GetFlyingHours().ToString();
                    }
                    else if (squadron == "No 15 Cobras")
                    {
                        (InputMirage.Text) = G.GetFlyingHours().ToString();
                    }
                    else if (squadron == "No 27 Zarrars")
                    {
                        (InputJF17.Text) = G.GetFlyingHours().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("GDP Doesnot Exist");
            }
        }

        private void ViewFH_Load(object sender, EventArgs e)
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
                List<GDPilot> GDPS = Interfaces.GetGdpInterface().GetAllGdps();
                for (int i = 0; i < GDPS.Count; i++)
                {
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetSquadron(), GDPS[i].GetFlyingHours());
                }

                OfficerGV.DataSource = dataTable;
                //Now fill the combo boxes with available data


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedRow = e.RowIndex;
            if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
            {
                DataGridViewRow row = OfficerGV.Rows[SelectedRow];
                if (row.Cells["PakNo"].Value != null)

                    InputPakNo.Text = row.Cells["PakNo"].Value.ToString();




            }
        }
    }
}
