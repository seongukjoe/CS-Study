namespace XModule.Forms.FormOperation
{
    partial class FormTaskLog
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
            this.btnClean = new Glass.GlassButton();
            this.btnAllClean = new Glass.GlassButton();
            this.pbLog = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnClose = new Glass.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbLog)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnClean.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClean.ForeColor = System.Drawing.Color.Black;
            this.btnClean.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClean.Location = new System.Drawing.Point(1005, 550);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(140, 40);
            this.btnClean.TabIndex = 120;
            this.btnClean.Text = "CLEAN";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnAllClean
            // 
            this.btnAllClean.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnAllClean.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAllClean.ForeColor = System.Drawing.Color.Black;
            this.btnAllClean.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllClean.Location = new System.Drawing.Point(1151, 549);
            this.btnAllClean.Name = "btnAllClean";
            this.btnAllClean.Size = new System.Drawing.Size(140, 40);
            this.btnAllClean.TabIndex = 119;
            this.btnAllClean.Text = "ALL CLEAN";
            this.btnAllClean.Click += new System.EventHandler(this.btnAllClean_Click);
            // 
            // pbLog
            // 
            this.pbLog.Location = new System.Drawing.Point(2, 12);
            this.pbLog.Name = "pbLog";
            this.pbLog.Size = new System.Drawing.Size(1289, 445);
            this.pbLog.TabIndex = 114;
            this.pbLog.TabStop = false;
            this.pbLog.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 463);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1288, 80);
            this.flowLayoutPanel1.TabIndex = 133;
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.BtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnClose.ForeColor = System.Drawing.Color.Black;
            this.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClose.Location = new System.Drawing.Point(3, 549);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(140, 40);
            this.BtnClose.TabIndex = 134;
            this.BtnClose.Text = "CLOSE";
            this.BtnClose.Click += new System.EventHandler(this.BtnEXIT_Click);
            // 
            // FormTaskLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 595);
            this.ControlBox = false;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnAllClean);
            this.Controls.Add(this.pbLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTaskLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormTaskLog";
            this.Load += new System.EventHandler(this.FormTaskLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Glass.GlassButton btnClean;
        private Glass.GlassButton btnAllClean;
        private System.Windows.Forms.PictureBox pbLog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Glass.GlassButton BtnClose;
    }
}