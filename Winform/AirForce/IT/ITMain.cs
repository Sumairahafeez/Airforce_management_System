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
    public partial class ITMain : Form
    {
        public ITMain()
        {
            InitializeComponent();
        }

        private void Backbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void Addbt_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddOfficer add = new AddOfficer();
            add.Show();
        }
    }
}
