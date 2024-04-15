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
    public partial class ADDFH : Form
    {
        public ADDFH()
        {
            InitializeComponent();
        }

        private void checkbt_Click(object sender, EventArgs e)
        {
            richTextBox3.Visible = true;
            richTextBox2.Visible = true;
            richTextBox6.Visible = true;
            InputJF17.Visible = true;
            InputF16.Visible = true;
            InputMirage.Visible = true;
        }

        private void backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu menu = new GDPMenu();
            menu.Show();
        }

        private void Savebt_Click(object sender, EventArgs e)
        {   try
            {
                GDPilot G = Interfaces.GdpInterface.GetGDPThroughPakNo(ConnectionClass.GetCurrentGDP().GetPakNo());
                string squadron = G.GetSquadron();
                if (squadron == "No 2 Minhas")
                {
                    G.SetFlyingHours(int.Parse(InputJF17.Text));
                }
                else if (squadron == "No 5 Falcons")
                {
                    G.SetFlyingHours(int.Parse(InputF16.Text));

                }
                else if (squadron == "No 9 Griffins")
                {
                    G.SetFlyingHours(int.Parse(InputF16.Text));
                }
                else if (squadron == "No 15 Cobras")
                {
                    G.SetFlyingHours(int.Parse(InputMirage.Text));
                }
                else if (squadron == "No 27 Zarrars")
                {
                    G.SetFlyingHours(int.Parse(InputJF17.Text));
                }
                Interfaces.GdpInterface.UpdateGDP(ConnectionClass.CurrentGDP.GetPakNo(), G);
            }
            catch(Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
            
           
        }

        private void ADDFH_Load(object sender, EventArgs e)
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

                    InputPakNO.Text = row.Cells["PakNo"].Value.ToString();



               
            }
        }
    }
}
