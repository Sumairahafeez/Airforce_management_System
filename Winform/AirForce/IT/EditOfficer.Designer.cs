﻿namespace AirForce.IT
{
    partial class EditOfficer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditOfficer));
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.OfficerGV = new System.Windows.Forms.DataGridView();
            this.PakNoCB = new System.Windows.Forms.ComboBox();
            this.InputBranchCB = new System.Windows.Forms.ComboBox();
            this.BranchT = new System.Windows.Forms.RichTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.Deletebt = new System.Windows.Forms.Button();
            this.TextSquadron = new System.Windows.Forms.RichTextBox();
            this.InputPosting = new System.Windows.Forms.ComboBox();
            this.InputRank = new System.Windows.Forms.ComboBox();
            this.InputName = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.InputSquadron = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.OfficerGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.DimGray;
            this.richTextBox5.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox5.Location = new System.Drawing.Point(870, 140);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(195, 39);
            this.richTextBox5.TabIndex = 144;
            this.richTextBox5.Text = "All Officers";
            // 
            // OfficerGV
            // 
            this.OfficerGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OfficerGV.Location = new System.Drawing.Point(870, 202);
            this.OfficerGV.Name = "OfficerGV";
            this.OfficerGV.Size = new System.Drawing.Size(377, 209);
            this.OfficerGV.TabIndex = 143;
            this.OfficerGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OfficerGV_CellContentClick);
            // 
            // PakNoCB
            // 
            this.PakNoCB.BackColor = System.Drawing.Color.DimGray;
            this.PakNoCB.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PakNoCB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PakNoCB.FormattingEnabled = true;
            this.PakNoCB.Location = new System.Drawing.Point(415, 152);
            this.PakNoCB.Name = "PakNoCB";
            this.PakNoCB.Size = new System.Drawing.Size(195, 38);
            this.PakNoCB.TabIndex = 142;
            // 
            // InputBranchCB
            // 
            this.InputBranchCB.BackColor = System.Drawing.Color.DimGray;
            this.InputBranchCB.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBranchCB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputBranchCB.FormattingEnabled = true;
            this.InputBranchCB.Items.AddRange(new object[] {
            "GDP"});
            this.InputBranchCB.Location = new System.Drawing.Point(415, 534);
            this.InputBranchCB.Name = "InputBranchCB";
            this.InputBranchCB.Size = new System.Drawing.Size(195, 38);
            this.InputBranchCB.TabIndex = 141;
            // 
            // BranchT
            // 
            this.BranchT.BackColor = System.Drawing.Color.DimGray;
            this.BranchT.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BranchT.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BranchT.Location = new System.Drawing.Point(81, 534);
            this.BranchT.Name = "BranchT";
            this.BranchT.Size = new System.Drawing.Size(195, 39);
            this.BranchT.TabIndex = 140;
            this.BranchT.Text = "Branch";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.DimGray;
            this.button7.Font = new System.Drawing.Font("Tempus Sans ITC", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(877, 611);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(103, 39);
            this.button7.TabIndex = 139;
            this.button7.Text = "Back";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Deletebt
            // 
            this.Deletebt.BackColor = System.Drawing.Color.DimGray;
            this.Deletebt.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Deletebt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Deletebt.Location = new System.Drawing.Point(415, 610);
            this.Deletebt.Name = "Deletebt";
            this.Deletebt.Size = new System.Drawing.Size(113, 37);
            this.Deletebt.TabIndex = 138;
            this.Deletebt.Text = "Update";
            this.Deletebt.UseVisualStyleBackColor = false;
            this.Deletebt.Click += new System.EventHandler(this.Updatebt_Click);
            // 
            // TextSquadron
            // 
            this.TextSquadron.BackColor = System.Drawing.Color.DimGray;
            this.TextSquadron.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextSquadron.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TextSquadron.Location = new System.Drawing.Point(81, 451);
            this.TextSquadron.Name = "TextSquadron";
            this.TextSquadron.Size = new System.Drawing.Size(195, 39);
            this.TextSquadron.TabIndex = 136;
            this.TextSquadron.Text = "Squadron";
            // 
            // InputPosting
            // 
            this.InputPosting.BackColor = System.Drawing.Color.DimGray;
            this.InputPosting.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputPosting.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputPosting.FormattingEnabled = true;
            this.InputPosting.Items.AddRange(new object[] {
            "Mushaf Base, Sargodha",
            "Air Headquarters,Islamabad",
            "Rafiqui Base,Shorkot",
            ""});
            this.InputPosting.Location = new System.Drawing.Point(415, 381);
            this.InputPosting.Name = "InputPosting";
            this.InputPosting.Size = new System.Drawing.Size(195, 38);
            this.InputPosting.TabIndex = 135;
            // 
            // InputRank
            // 
            this.InputRank.BackColor = System.Drawing.Color.DimGray;
            this.InputRank.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputRank.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputRank.FormattingEnabled = true;
            this.InputRank.Items.AddRange(new object[] {
            "FlyingOfficer",
            "Flight Lieutanat",
            "Squadron Leader",
            "Wing Commander",
            "Group Captain",
            "Air Commander",
            "Air Vice Marshal",
            "Vice Marshal",
            "Air Chielf"});
            this.InputRank.Location = new System.Drawing.Point(415, 301);
            this.InputRank.Name = "InputRank";
            this.InputRank.Size = new System.Drawing.Size(195, 38);
            this.InputRank.TabIndex = 134;
            // 
            // InputName
            // 
            this.InputName.BackColor = System.Drawing.Color.DimGray;
            this.InputName.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputName.Location = new System.Drawing.Point(415, 221);
            this.InputName.Name = "InputName";
            this.InputName.Size = new System.Drawing.Size(195, 39);
            this.InputName.TabIndex = 133;
            this.InputName.Text = "";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.DimGray;
            this.richTextBox4.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox4.Location = new System.Drawing.Point(81, 380);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(195, 39);
            this.richTextBox4.TabIndex = 132;
            this.richTextBox4.Text = "Posted";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.DimGray;
            this.richTextBox3.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox3.Location = new System.Drawing.Point(81, 300);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(195, 39);
            this.richTextBox3.TabIndex = 131;
            this.richTextBox3.Text = "Rank";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.DimGray;
            this.richTextBox2.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox2.Location = new System.Drawing.Point(81, 152);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(195, 39);
            this.richTextBox2.TabIndex = 130;
            this.richTextBox2.Text = "PakNo";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.DimGray;
            this.richTextBox1.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox1.Location = new System.Drawing.Point(81, 221);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(195, 39);
            this.richTextBox1.TabIndex = 129;
            this.richTextBox1.Text = "Name";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(23, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(812, 557);
            this.button2.TabIndex = 128;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(1, -6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1377, 99);
            this.button1.TabIndex = 126;
            this.button1.Text = "Edit Officer";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-7, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1385, 644);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 127;
            this.pictureBox1.TabStop = false;
            // 
            // InputSquadron
            // 
            this.InputSquadron.BackColor = System.Drawing.Color.DimGray;
            this.InputSquadron.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputSquadron.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputSquadron.FormattingEnabled = true;
            this.InputSquadron.Items.AddRange(new object[] {
            "No 2 Minhas",
            "No 5 Falcons",
            "No 9 Griffins",
            "No 14 Tail Choppers",
            "No 15 Cobras",
            "No 19 Sherdils",
            "No 27 Zarrars"});
            this.InputSquadron.Location = new System.Drawing.Point(415, 452);
            this.InputSquadron.Name = "InputSquadron";
            this.InputSquadron.Size = new System.Drawing.Size(195, 38);
            this.InputSquadron.TabIndex = 145;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DimGray;
            this.button3.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(81, 610);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(205, 37);
            this.button3.TabIndex = 146;
            this.button3.Text = "Assign Password";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // EditOfficer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 729);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.InputSquadron);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.OfficerGV);
            this.Controls.Add(this.PakNoCB);
            this.Controls.Add(this.InputBranchCB);
            this.Controls.Add(this.BranchT);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.Deletebt);
            this.Controls.Add(this.TextSquadron);
            this.Controls.Add(this.InputPosting);
            this.Controls.Add(this.InputRank);
            this.Controls.Add(this.InputName);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "EditOfficer";
            this.Text = "EditOfficer";
            this.Load += new System.EventHandler(this.EditOfficer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OfficerGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.DataGridView OfficerGV;
        private System.Windows.Forms.ComboBox PakNoCB;
        private System.Windows.Forms.ComboBox InputBranchCB;
        private System.Windows.Forms.RichTextBox BranchT;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button Deletebt;
        private System.Windows.Forms.RichTextBox TextSquadron;
        private System.Windows.Forms.ComboBox InputPosting;
        private System.Windows.Forms.ComboBox InputRank;
        private System.Windows.Forms.RichTextBox InputName;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox InputSquadron;
        private System.Windows.Forms.Button button3;
    }
}