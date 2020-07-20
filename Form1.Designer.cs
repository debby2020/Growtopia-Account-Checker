namespace ACCOUNTCHECK
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.animaForm1 = new AnimaForm();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.animaStatusBar1 = new AnimaStatusBar();
            this.animaButton4 = new AnimaButton();
            this.animaExperimentalListView2 = new AnimaExperimentalListView();
            this.animaTextBox3 = new AnimaTextBox();
            this.animaButton5 = new AnimaButton();
            this.animaTextBox4 = new AnimaTextBox();
            this.animaButton6 = new AnimaButton();
            this.animaForm1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // animaForm1
            // 
            this.animaForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.animaForm1.Controls.Add(this.pictureBox1);
            this.animaForm1.Controls.Add(this.animaStatusBar1);
            this.animaForm1.Controls.Add(this.animaButton4);
            this.animaForm1.Controls.Add(this.animaExperimentalListView2);
            this.animaForm1.Controls.Add(this.animaTextBox3);
            this.animaForm1.Controls.Add(this.animaButton5);
            this.animaForm1.Controls.Add(this.animaTextBox4);
            this.animaForm1.Controls.Add(this.animaButton6);
            this.animaForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.animaForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.animaForm1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.animaForm1.Location = new System.Drawing.Point(0, 0);
            this.animaForm1.Name = "animaForm1";
            this.animaForm1.Size = new System.Drawing.Size(512, 307);
            this.animaForm1.TabIndex = 0;
            this.animaForm1.Text = "Account Checker";
            this.animaForm1.Click += new System.EventHandler(this.animaForm1_Click);
            this.animaForm1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.animaForm1_KeyDown);
            this.animaForm1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.animaForm1_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(445, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 17);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // animaStatusBar1
            // 
            this.animaStatusBar1.Location = new System.Drawing.Point(0, 281);
            this.animaStatusBar1.Name = "animaStatusBar1";
            this.animaStatusBar1.Size = new System.Drawing.Size(512, 23);
            this.animaStatusBar1.TabIndex = 12;
            this.animaStatusBar1.Text = "null";
            this.animaStatusBar1.Type = AnimaStatusBar.Types.Basic;
            // 
            // animaButton4
            // 
            this.animaButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.animaButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.animaButton4.ImageLocation = new System.Drawing.Point(30, 6);
            this.animaButton4.ImageSize = new System.Drawing.Size(14, 14);
            this.animaButton4.Location = new System.Drawing.Point(370, 81);
            this.animaButton4.Name = "animaButton4";
            this.animaButton4.Size = new System.Drawing.Size(128, 29);
            this.animaButton4.TabIndex = 11;
            this.animaButton4.Text = "About";
            this.animaButton4.UseVisualStyleBackColor = true;
            this.animaButton4.Click += new System.EventHandler(this.animaButton4_Click);
            // 
            // animaExperimentalListView2
            // 
            this.animaExperimentalListView2.Columns = new string[] {
        "GrowID",
        "Password",
        "World Locks",
        "Gems"};
            this.animaExperimentalListView2.ColumnWidth = 125;
            this.animaExperimentalListView2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.animaExperimentalListView2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.animaExperimentalListView2.Grid = false;
            this.animaExperimentalListView2.HandleItemsForeColor = false;
            this.animaExperimentalListView2.Highlight = -1;
            this.animaExperimentalListView2.Items = new System.Windows.Forms.ListViewItem[0];
            this.animaExperimentalListView2.ItemSize = 16;
            this.animaExperimentalListView2.Location = new System.Drawing.Point(10, 116);
            this.animaExperimentalListView2.Multiselect = false;
            this.animaExperimentalListView2.Name = "animaExperimentalListView2";
            this.animaExperimentalListView2.SelectedIndex = -1;
            this.animaExperimentalListView2.SelectedIndexes = ((System.Collections.Generic.List<int>)(resources.GetObject("animaExperimentalListView2.SelectedIndexes")));
            this.animaExperimentalListView2.Size = new System.Drawing.Size(488, 159);
            this.animaExperimentalListView2.TabIndex = 10;
            this.animaExperimentalListView2.Text = "animaExperimentalListView2";
            // 
            // animaTextBox3
            // 
            this.animaTextBox3.Dark = false;
            this.animaTextBox3.Font = new System.Drawing.Font("Segoe UI Semilight", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.animaTextBox3.Location = new System.Drawing.Point(10, 81);
            this.animaTextBox3.MaxLength = 32767;
            this.animaTextBox3.MultiLine = false;
            this.animaTextBox3.Name = "animaTextBox3";
            this.animaTextBox3.Numeric = false;
            this.animaTextBox3.ReadOnly = false;
            this.animaTextBox3.Size = new System.Drawing.Size(354, 29);
            this.animaTextBox3.TabIndex = 9;
            this.animaTextBox3.Text = "Password";
            this.animaTextBox3.UseSystemPasswordChar = false;
            // 
            // animaButton5
            // 
            this.animaButton5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.animaButton5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.animaButton5.ImageLocation = new System.Drawing.Point(30, 6);
            this.animaButton5.ImageSize = new System.Drawing.Size(14, 14);
            this.animaButton5.Location = new System.Drawing.Point(370, 46);
            this.animaButton5.Name = "animaButton5";
            this.animaButton5.Size = new System.Drawing.Size(128, 29);
            this.animaButton5.TabIndex = 8;
            this.animaButton5.Text = "Check";
            this.animaButton5.UseVisualStyleBackColor = true;
            this.animaButton5.Click += new System.EventHandler(this.animaButton5_Click);
            // 
            // animaTextBox4
            // 
            this.animaTextBox4.Dark = false;
            this.animaTextBox4.Font = new System.Drawing.Font("Segoe UI Semilight", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.animaTextBox4.Location = new System.Drawing.Point(10, 46);
            this.animaTextBox4.MaxLength = 32767;
            this.animaTextBox4.MultiLine = false;
            this.animaTextBox4.Name = "animaTextBox4";
            this.animaTextBox4.Numeric = false;
            this.animaTextBox4.ReadOnly = false;
            this.animaTextBox4.Size = new System.Drawing.Size(354, 29);
            this.animaTextBox4.TabIndex = 7;
            this.animaTextBox4.Text = "Username";
            this.animaTextBox4.UseSystemPasswordChar = false;
            // 
            // animaButton6
            // 
            this.animaButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.animaButton6.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.animaButton6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.animaButton6.ImageLocation = new System.Drawing.Point(30, 6);
            this.animaButton6.ImageSize = new System.Drawing.Size(14, 14);
            this.animaButton6.Location = new System.Drawing.Point(476, 9);
            this.animaButton6.Name = "animaButton6";
            this.animaButton6.Size = new System.Drawing.Size(26, 23);
            this.animaButton6.TabIndex = 6;
            this.animaButton6.Text = "X";
            this.animaButton6.UseVisualStyleBackColor = true;
            this.animaButton6.Click += new System.EventHandler(this.animaButton6_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 307);
            this.Controls.Add(this.animaForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.animaForm1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AnimaButton animaButton6;
        private AnimaTextBox animaTextBox4;
        private AnimaButton animaButton5;
        private AnimaTextBox animaTextBox3;
        private AnimaExperimentalListView animaExperimentalListView2;
        private AnimaButton animaButton4;
        private AnimaStatusBar animaStatusBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AnimaForm animaForm1;
    }
}

