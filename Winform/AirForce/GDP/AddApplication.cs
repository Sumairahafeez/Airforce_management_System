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
    public partial class AddApplication : Form
    {
        public AddApplication()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu Menu = new GDPMenu();
            Menu.Show();
        }

        private void Savebt_Click(object sender, EventArgs e)
        {
            int PakNo = int.Parse(InputPakNo.Text);
            string context = InputContextT.Text;
            int Id = int.Parse(InputId.Text);
            bool isValid = Validations.IsValidGDP(PakNo);
            if (isValid)
            {
                try
                {
                    Requests newReq = new Requests(Id, context, PakNo);
                    Interfaces.GetRequestInterface().StoreRequests(newReq);
                    MessageBox.Show("Request Sent Successfully");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else
            {
                MessageBox.Show("InValid PakNo");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void AddApplication_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                data.Columns.Add("ReqId", typeof(int));
                data.Columns.Add("Context", typeof(string));
                data.Columns.Add("PakNO", typeof(int));
                data.Columns.Add("Status", typeof(string));
                List<Requests> requ = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(ConnectionClass.CurrentGDP.GetPakNo());
                for(int i = 0; i < requ.Count; i++)
                {
                    data.Rows.Add(requ[i].GetRequestId(), requ[i].GetContext(), requ[i].GetPakNo(), requ[i].GetStatus());
                }
                ApplicationsGV.DataSource = data;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
