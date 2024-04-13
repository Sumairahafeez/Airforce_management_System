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
using static System.Net.Mime.MediaTypeNames;

namespace AirForce.OC
{
    public partial class CheckReq : Form
    { public int CurrentOCPakNo;
        public CheckReq()
        {
            InitializeComponent();
        }
        public CheckReq(int Pakno)
        {
            CurrentOCPakNo = Pakno;
            InitializeComponent();
        }

        private void CheckReq_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                data.Columns.Add("PakNO", typeof(int));
                data.Columns.Add("Name",typeof(string));
                data.Columns.Add("ReqId", typeof(int));
                data.Columns.Add("Context", typeof(string));
               
                data.Columns.Add("Status", typeof(string));
                List<AFPersonalle> Under = Interfaces.AFInterface.GetAllAFofOC(CurrentOCPakNo);
               
                foreach(AFPersonalle a in  Under)
                {
                    
                    List<Requests> requ = Interfaces.RequestInterface.GetRequestsOfSpecificOfficer(a.GetPakNo());
                    for (int i = 0; i < requ.Count; i++)
                    {
                        data.Rows.Add(a.GetPakNo(),a.GetName(),requ[i].GetRequestId(), requ[i].GetContext(), requ[i].GetStatus());
                    }
                }
               
                ApplicationDV.DataSource = data;
                //Fill the combobox with pakNo of under officers
                string query1 = "SELECT * FROM  GDP g,AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + CurrentOCPakNo + "))\r\n";

                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Checkbt_Click(object sender, EventArgs e)
        {
            int Pakno = int.Parse(PakNoCB.Text);
            int id = int.Parse(InputId.Text);
            Requests req = Validations.IsValidRequest(Pakno, id);
            if (req != null)
            {
               
                InputContext.Text = req.GetContext();
               
               
                InputStatusCB.Text = req.GetStatus();
            }
        }

        private void AssigmMissionbt_Click(object sender, EventArgs e)
        {
            int Pakno = int.Parse(PakNoCB.Text);
            int id = int.Parse(InputId.Text);
            Requests req = Validations.IsValidRequest(Pakno, id);
            if (req != null)
            {

                InputContext.Text = req.GetContext();
                string status = InputStatusCB.Text;
                req.SetStatus(status);
                Interfaces.RequestInterface.UpdateRequests(id, req);
                MessageBox.Show("Status SetSuccessfully");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCMain main = new OCMain(CurrentOCPakNo);
            main.Show();
        }

        private void ApplicationDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (select >= -2)
            {
                DataGridViewRow row = ApplicationDV.Rows[select];
                PakNoCB.Text = row.Cells["PakNO"].ToString();
                InputStatusCB.Text = row.Cells["Status"].ToString();
                InputContext.Text = row.Cells["Context"].ToString();
                InputId.Text = row.Cells["ReqId"].ToString();
            }
        }
    }
}
