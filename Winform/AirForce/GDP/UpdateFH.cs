﻿using System;
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
    public partial class UpdateFH : Form
    {
        public UpdateFH()
        {
            InitializeComponent();
        }

        private void checkbt_Click(object sender, EventArgs e)
        {
            richTextBox3.Visible = true;
            richTextBox2.Visible = true;
            richTextBox6.Visible = true;
            richTextBox7.Visible = true;
            richTextBox8.Visible = true;
            richTextBox9.Visible = true;
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            GDPFlyingHoursMenu menu = new GDPFlyingHoursMenu();
            menu.Show();
        }
    }
}
