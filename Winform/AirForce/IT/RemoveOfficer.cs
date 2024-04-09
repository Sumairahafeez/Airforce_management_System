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
    public partial class RemoveOfficer : Form
    {
        public RemoveOfficer()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ITMain main = new ITMain();
            main.Show();
        }

        private void RemoveOfficer_Load(object sender, EventArgs e)
        {
            //This will fill the datatable and the pak no comboboxes as soon as the page loads
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name",typeof(string));
            dataTable.Columns.Add("PakNo",typeof(int));
            dataTable.Columns.Add("Rank",typeof(string));
            dataTable.Columns.Add("Posted",typeof (string));
            dataTable.Columns.Add("Squadron", typeof(string));
            List<GDPilot> GDPS = Interfaces.GdpInterface.GetAllGdps();
            for (int i = 0; i < GDPS.Count; i++)
            {
                dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetPresentlyPosted(), GDPS[i].GetSquadron());
            }
            List<CommandingOfficers> ALLOc = Interfaces.OCInterface.GetAll();
            for(int i = 0;i < ALLOc.Count;i++)
            {
                dataTable.Rows.Add(ALLOc[i].GetName());
            }
        }
    }
}
