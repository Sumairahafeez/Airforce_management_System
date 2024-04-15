using AirForceLibrary.BL;
using AirForceLibrary.Utilis;

namespace AirForce.OC
{
    partial class OCMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCMain));
            this.button7 = new System.Windows.Forms.Button();
            this.Requestbt = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Missionbt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.userBoxT = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button7.Font = new System.Drawing.Font("Tempus Sans ITC", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(1239, 654);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(103, 55);
            this.button7.TabIndex = 62;
            this.button7.Text = "Back";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Requestbt
            // 
            this.Requestbt.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.Requestbt.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Requestbt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Requestbt.Location = new System.Drawing.Point(703, 654);
            this.Requestbt.Name = "Requestbt";
            this.Requestbt.Size = new System.Drawing.Size(240, 55);
            this.Requestbt.TabIndex = 60;
            this.Requestbt.Text = "CheckRequests";
            this.Requestbt.UseVisualStyleBackColor = false;
            this.Requestbt.Click += new System.EventHandler(this.Requestbt_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button3.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(437, 654);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(248, 55);
            this.button3.TabIndex = 59;
            this.button3.Text = "Assign Mission";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Missionbt
            // 
            this.Missionbt.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.Missionbt.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Missionbt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Missionbt.Location = new System.Drawing.Point(84, 652);
            this.Missionbt.Name = "Missionbt";
            this.Missionbt.Size = new System.Drawing.Size(328, 57);
            this.Missionbt.TabIndex = 58;
            this.Missionbt.Text = "Select UnderOfficer";
            this.Missionbt.UseVisualStyleBackColor = false;
            this.Missionbt.Click += new System.EventHandler(this.Missionbt_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button1.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(-7, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1385, 99);
            this.button1.TabIndex = 57;
            this.button1.Text = "Commanding Officers";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button5.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.Location = new System.Drawing.Point(1, 632);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(1377, 99);
            this.button5.TabIndex = 61;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-7, 94);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1385, 543);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 63;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button2.Font = new System.Drawing.Font("Tempus Sans ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(969, 654);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(230, 55);
            this.button2.TabIndex = 64;
            this.button2.Text = "AssignPosting";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // userBoxT
            // 
            this.userBoxT.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.userBoxT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userBoxT.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userBoxT.ForeColor = System.Drawing.Color.White;
            this.userBoxT.Location = new System.Drawing.Point(463, 3);
            this.userBoxT.Name = "userBoxT";
            this.userBoxT.Size = new System.Drawing.Size(295, 85);
            this.userBoxT.TabIndex = 65;
            this.userBoxT.Text = "";
            // 
            // OCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 729);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.Requestbt);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Missionbt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.userBoxT);
            this.Name = "OCMain";
            this.Text = "OCMain";
            this.Load += new System.EventHandler(this.OCMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button Requestbt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button Missionbt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox userBoxT;
    }
}