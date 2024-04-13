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
                    dataTable.Rows.Add(GDPS[i].GetName(), GDPS[i].GetPakNo(), GDPS[i].GetRank(), GDPS[i].GetPresentlyPosted(), GDPS[i].GetSquadron());
                }
               
                OfficerGV.DataSource = dataTable;
                //Now fill the combo boxes with available data
                string query1 = "SELECT PakNo From GDP g,OC o,AFPersonalle a WHERE g.OfficerId = a.Id OR o.OffId = a.Id";
                PakNoCB.DataSource = Validations.GetData(query1);
                PakNoCB.DisplayMember = "PakNo";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Addbt_Click(object sender, EventArgs e)
        {
           
                int PakNo = int.Parse(PakNoCB.Text);
                bool isValid = Validations.IsValidAFPersonalle(PakNo);//It will check if the AF Personal is valid or not
                if (isValid)
                {
                    AFPersonalle AF = Interfaces.AFInterface.GetAFPersonalleByID(PakNo);//From ID the personal is fetched
                    string Squadron = InputSquadron.Text;
                    //If valid it is checked if the squadrons and location of oc and underofficer matches of not
                    CommandingOfficers OC = Interfaces.OCInterface.GetOCbyId(CurrentOCPakNo);

                    bool Valid = Validations.IsFitForTHEOC(OC, AF, Squadron);
                    if (Valid)
                    {   // if the under office is added show message otherwise show the errrors
                        bool isAdded = OC.AskForApproval(AF);
                        if (isAdded)
                        {
                            GDPilot gdp = Interfaces.GdpInterface.GetGDPThroughPakNo(PakNo);
                            gdp.SetCommandingOfficer(Interfaces.OCInterface.GetOCbyId(CurrentOCPakNo));
                            Interfaces.GdpInterface.UpdateGDP(PakNo, gdp);
                            MessageBox.Show("Officer Added Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Your Request was not approved you might have full capacity of under officers");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your Squadrons Doesnot Match");
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Officer");
                }
            
           

           
        }

        private void OfficerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   try
            {
                //This function serves to select a data from a given datagrid view and show it on th edattable
                int SelectedRow = e.RowIndex;
                if (SelectedRow >= -2 && SelectedRow < OfficerGV.Rows.Count)
                {
                    DataGridViewRow row = OfficerGV.Rows[SelectedRow];
                    if (row.Cells["PakNo"].Value != null)

                        PakNoCB.Text = row.Cells["PakNo"].Value.ToString();



                    if (row.Cells["Name"].Value != null)
                        InputName.Text = row.Cells["Name"].Value.ToString();
                    else
                        InputName.Text = string.Empty;

                    if (row.Cells["Rank"].Value != null)
                        InputRank.Text = row.Cells["Rank"].Value.ToString();
                    else
                        InputRank.Text = string.Empty;

                    if (row.Cells["Posted"].Value != null)
                        InputPosting.Text = row.Cells["Posted"].Value.ToString();
                    else
                        InputPosting.Text = string.Empty;

                    if (row.Cells["Squadron"].Value != null)
                        InputSquadron.Text = row.Cells["Squadron"].Value.ToString();
                    else
                        InputSquadron.Text = string.Empty;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
    }
}