namespace BuyNowTracker
{
    partial class frmSummary
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblMemo = new System.Windows.Forms.Label();
            this.btnMemo = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.lblMemo.Location = new System.Drawing.Point(12, 91);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(65, 16);
            this.lblMemo.TabIndex = 8;
            this.lblMemo.Text = "Summary";
            // 
            // btnMemo
            // 
            this.btnMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnMemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemo.ForeColor = System.Drawing.Color.White;
            this.btnMemo.Location = new System.Drawing.Point(239, 246);
            this.btnMemo.Name = "btnMemo";
            this.btnMemo.Size = new System.Drawing.Size(128, 41);
            this.btnMemo.TabIndex = 11;
            this.btnMemo.Text = "Add  Memo";
            this.btnMemo.UseVisualStyleBackColor = false;
            this.btnMemo.Click += new System.EventHandler(this.btnMemo_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSkip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkip.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSkip.ForeColor = System.Drawing.Color.White;
            this.btnSkip.Location = new System.Drawing.Point(11, 246);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(128, 41);
            this.btnSkip.TabIndex = 12;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.txtMemo);
            this.pnlSummary.Location = new System.Drawing.Point(16, 110);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(352, 130);
            this.pnlSummary.TabIndex = 13;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(1, 1);
            this.txtMemo.MaxLength = 1000;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(350, 128);
            this.txtMemo.TabIndex = 8;
            this.txtMemo.MouseHover += new System.EventHandler(this.txtMemo_MouseHover);
            // 
            // frmSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(386, 297);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnMemo);
            this.Controls.Add(this.lblMemo);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(102)))), ((int)(((byte)(12)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Summary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSummary_FormClosing);
            this.Load += new System.EventHandler(this.frmSummary_Load);
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.Button btnMemo;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.TextBox txtMemo;
    }


}
