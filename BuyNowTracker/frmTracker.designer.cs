namespace BuyNowTracker
{
    partial class frmTracker

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTracker));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblUserName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picCross = new System.Windows.Forms.PictureBox();
            this.imgBack = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTaskTltle = new System.Windows.Forms.Label();
            this.lblhh = new System.Windows.Forms.Label();
            this.lblmm = new System.Windows.Forms.Label();
            this.lblhour = new System.Windows.Forms.Label();
            this.lblMinute = new System.Windows.Forms.Label();
            this.btnStopTimer = new System.Windows.Forms.Button();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.btnMemo = new System.Windows.Forms.Button();
            this.lblSS = new System.Windows.Forms.Label();
            this.lblSecond = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBack)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.lblUserName.Location = new System.Drawing.Point(5, 33);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 25);
            this.lblUserName.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.picCross);
            this.panel1.Controls.Add(this.imgBack);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblTaskTltle);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 133);
            this.panel1.TabIndex = 1;
            // 
            // picCross
            // 
            this.picCross.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCross.Image = ((System.Drawing.Image)(resources.GetObject("picCross.Image")));
            this.picCross.Location = new System.Drawing.Point(357, 5);
            this.picCross.Name = "picCross";
            this.picCross.Size = new System.Drawing.Size(22, 22);
            this.picCross.TabIndex = 8;
            this.picCross.TabStop = false;
            this.picCross.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imgBack
            // 
            this.imgBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgBack.Image = ((System.Drawing.Image)(resources.GetObject("imgBack.Image")));
            this.imgBack.Location = new System.Drawing.Point(341, 88);
            this.imgBack.Name = "imgBack";
            this.imgBack.Size = new System.Drawing.Size(33, 26);
            this.imgBack.TabIndex = 4;
            this.imgBack.TabStop = false;
            this.imgBack.Click += new System.EventHandler(this.imgBack_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(373, 2);
            this.label2.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.DarkGray;
            this.lblDescription.Location = new System.Drawing.Point(4, 4);
            this.lblDescription.MaximumSize = new System.Drawing.Size(270, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(181, 16);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "This is used to track user time";
            // 
            // lblTaskTltle
            // 
            this.lblTaskTltle.AutoSize = true;
            this.lblTaskTltle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskTltle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTaskTltle.Location = new System.Drawing.Point(9, 63);
            this.lblTaskTltle.Name = "lblTaskTltle";
            this.lblTaskTltle.Size = new System.Drawing.Size(85, 16);
            this.lblTaskTltle.TabIndex = 1;
            this.lblTaskTltle.Text = "Time Tracker";
            // 
            // lblhh
            // 
            this.lblhh.AutoSize = true;
            this.lblhh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblhh.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhh.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblhh.Location = new System.Drawing.Point(18, 150);
            this.lblhh.Name = "lblhh";
            this.lblhh.Size = new System.Drawing.Size(53, 39);
            this.lblhh.TabIndex = 2;
            this.lblhh.Text = "hh";
            // 
            // lblmm
            // 
            this.lblmm.AutoSize = true;
            this.lblmm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblmm.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmm.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblmm.Location = new System.Drawing.Point(79, 150);
            this.lblmm.Name = "lblmm";
            this.lblmm.Size = new System.Drawing.Size(71, 39);
            this.lblmm.TabIndex = 3;
            this.lblmm.Text = "mm";
            // 
            // lblhour
            // 
            this.lblhour.AutoSize = true;
            this.lblhour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblhour.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblhour.Location = new System.Drawing.Point(18, 192);
            this.lblhour.Name = "lblhour";
            this.lblhour.Size = new System.Drawing.Size(51, 39);
            this.lblhour.TabIndex = 4;
            this.lblhour.Text = "00";
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMinute.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblMinute.Location = new System.Drawing.Point(80, 192);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(51, 39);
            this.lblMinute.TabIndex = 5;
            this.lblMinute.Text = "00";
            // 
            // btnStopTimer
            // 
            this.btnStopTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStopTimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTimer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopTimer.ForeColor = System.Drawing.Color.White;
            this.btnStopTimer.Location = new System.Drawing.Point(244, 155);
            this.btnStopTimer.Name = "btnStopTimer";
            this.btnStopTimer.Size = new System.Drawing.Size(128, 41);
            this.btnStopTimer.TabIndex = 6;
            this.btnStopTimer.Text = "Stop Timer";
            this.btnStopTimer.UseVisualStyleBackColor = false;
            this.btnStopTimer.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(18, 267);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(180, 94);
            this.txtMemo.TabIndex = 7;
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.lblMemo.Location = new System.Drawing.Point(19, 251);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(76, 16);
            this.lblMemo.TabIndex = 8;
            this.lblMemo.Text = "Working on";
            // 
            // btnMemo
            // 
            this.btnMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnMemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemo.ForeColor = System.Drawing.Color.White;
            this.btnMemo.Location = new System.Drawing.Point(244, 267);
            this.btnMemo.Name = "btnMemo";
            this.btnMemo.Size = new System.Drawing.Size(128, 41);
            this.btnMemo.TabIndex = 11;
            this.btnMemo.Text = "Add Memo";
            this.btnMemo.UseVisualStyleBackColor = false;
            this.btnMemo.Click += new System.EventHandler(this.btnMemo_Click);
            // 
            // lblSS
            // 
            this.lblSS.AutoSize = true;
            this.lblSS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSS.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSS.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblSS.Location = new System.Drawing.Point(158, 150);
            this.lblSS.Name = "lblSS";
            this.lblSS.Size = new System.Drawing.Size(45, 39);
            this.lblSS.TabIndex = 12;
            this.lblSS.Text = "ss";
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSecond.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecond.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblSecond.Location = new System.Drawing.Point(151, 192);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(51, 39);
            this.lblSecond.TabIndex = 13;
            this.lblSecond.Text = "00";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.lblDescription);
            this.panel2.Location = new System.Drawing.Point(7, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 32);
            this.panel2.TabIndex = 9;
            // 
            // frmTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(386, 380);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.lblSS);
            this.Controls.Add(this.btnMemo);
            this.Controls.Add(this.lblMemo);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.btnStopTimer);
            this.Controls.Add(this.lblMinute);
            this.Controls.Add(this.lblhour);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblmm);
            this.Controls.Add(this.lblhh);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(102)))), ((int)(((byte)(12)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buy Now Tracker";
            this.Load += new System.EventHandler(this.frmTracker_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBack)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTaskTltle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblhh;
        private System.Windows.Forms.Label lblmm;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.Label lblhour;
        private System.Windows.Forms.Button btnStopTimer;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.Button btnMemo;
        private System.Windows.Forms.PictureBox imgBack;
        private System.Windows.Forms.PictureBox picCross;
        private System.Windows.Forms.Label lblSS;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Panel panel2;
    }


}

