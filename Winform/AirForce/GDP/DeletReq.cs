﻿using AirForceLibrary.BL;
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
    public partial class DeletReq : Form
    {
        public DeletReq()
        {
            InitializeComponent();
        }

        private void DeletReq_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                data.Columns.Add("ReqId", typeof(int));
                data.Columns.Add("Context", typeof(string));
                data.Columns.Add("PakNO", typeof(int));
                data.Columns.Add("Status", typeof(string));
                List<Requests> requ = Interfaces.RequestInterface.GetRequestsOfSpecificOfficer(ConnectionClass.CurrentGDP.GetPakNo());
                for (int i = 0; i < requ.Count; i++)
                {
                    data.Rows.Add(requ[i].GetRequestId(), requ[i].GetContext(), requ[i].GetPakNo(), requ[i].GetStatus());
                }
                ApplicationDV.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplicationDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (select >= -2)
            {
                DataGridViewRow row = ApplicationDV.Rows[select];
                InputPakNo.Text = row.Cells["PakNO"].ToString();
                InputStatusT.Text = row.Cells["Status"].ToString();
                InputContextT.Text = row.Cells["Context"].ToString();
                InputId.Text = row.Cells["ReqId"].ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   try
            {
                int Pakno = int.Parse(InputPakNo.Text);
                int id = int.Parse(InputId.Text);
                Requests req = Validations.IsValidRequest(Pakno, id);
                if (req != null)
                {
                    InputContextT.Visible = true;
                    InputContextT.Text = req.GetContext();
                    contextBoxT.Visible = true;
                    InputStatusT.Visible = true;
                    StatusBoxT.Visible = true;
                    InputStatusT.Text = req.GetStatus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delbt_Click(object sender, EventArgs e)
        {
            try
            {
                int ApplicationId = int.Parse(InputId.Text);
                Interfaces.RequestInterface.DeleteRequests(ApplicationId);
                MessageBox.Show("Request Deleted Successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPMenu gDPMenu = new GDPMenu();
            gDPMenu.Show();
        }
    }
}