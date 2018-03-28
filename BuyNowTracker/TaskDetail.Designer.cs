namespace BuyNowTracker
{
    partial class TaskDetail
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblTaskName = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblStop = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblRun = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            this.btnStop.BackgroundImage = global::BuyNowTracker.Properties.Resources.BB9g7Qs_1_;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 48.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(20, 45);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(29, 27);
            this.btnStop.TabIndex = 2;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.Image = global::BuyNowTracker.Properties.Resources.settings_right_hover;
            this.btnStart.Location = new System.Drawing.Point(17, 45);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(32, 27);
            this.btnStart.TabIndex = 1;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblTaskName
            // 
            this.lblTaskName.AutoSize = true;
            this.lblTaskName.BackColor = System.Drawing.Color.Transparent;
            this.lblTaskName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskName.Location = new System.Drawing.Point(12, 2);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(39, 25);
            this.lblTaskName.TabIndex = 2;
            this.lblTaskName.Text = "klk";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.BackColor = System.Drawing.Color.Transparent;
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(10, 155);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(50, 24);
            this.lblStart.TabIndex = 3;
            this.lblStart.Text = "label";
            this.lblStart.Visible = false;
            // 
            // lblStop
            // 
            this.lblStop.AutoSize = true;
            this.lblStop.BackColor = System.Drawing.Color.Transparent;
            this.lblStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStop.Location = new System.Drawing.Point(10, 205);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(60, 24);
            this.lblStop.TabIndex = 4;
            this.lblStop.Text = "label1";
            this.lblStop.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblRun
            // 
            this.lblRun.AutoSize = true;
            this.lblRun.BackColor = System.Drawing.Color.Transparent;
            this.lblRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRun.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblRun.Location = new System.Drawing.Point(96, 148);
            this.lblRun.Name = "lblRun";
            this.lblRun.Size = new System.Drawing.Size(150, 31);
            this.lblRun.TabIndex = 5;
            this.lblRun.Text = "HH:MM:SS";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::BuyNowTracker.Properties.Resources.cross;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Location = new System.Drawing.Point(307, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // TaskDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BuyNowTracker.Properties.Resources.vistabg;
            this.ClientSize = new System.Drawing.Size(331, 275);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblRun);
            this.Controls.Add(this.lblStop);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblTaskName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TaskDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TaskDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblRun;
        private System.Windows.Forms.Button btnClose;
    }
}