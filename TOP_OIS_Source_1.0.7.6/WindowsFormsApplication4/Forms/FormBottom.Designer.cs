namespace XModule.Forms
{
    partial class FrmBottom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBottom));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.lbTimeValue = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbSicCheck = new System.Windows.Forms.Label();
            this.lbFileVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user-properties.png");
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(1034, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 22);
            this.label7.TabIndex = 8;
            this.label7.Text = "시간";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTimeValue
            // 
            this.lbTimeValue.BackColor = System.Drawing.Color.Gainsboro;
            this.lbTimeValue.Font = new System.Drawing.Font("맑은 고딕", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTimeValue.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbTimeValue.Location = new System.Drawing.Point(1688, 0);
            this.lbTimeValue.Name = "lbTimeValue";
            this.lbTimeValue.Size = new System.Drawing.Size(220, 22);
            this.lbTimeValue.TabIndex = 8;
            this.lbTimeValue.Text = "                                   TIME";
            this.lbTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbUserName
            // 
            this.lbUserName.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUserName.Font = new System.Drawing.Font("맑은 고딕", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbUserName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbUserName.Location = new System.Drawing.Point(1413, 0);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(188, 22);
            this.lbUserName.TabIndex = 8;
            this.lbUserName.Text = "                    ADMINISTATOR";
            this.lbUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.ImageList = this.imageList1;
            this.label6.Location = new System.Drawing.Point(1286, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 22);
            this.label6.TabIndex = 8;
            this.label6.Text = "USER";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // lbSicCheck
            // 
            this.lbSicCheck.BackColor = System.Drawing.Color.Gainsboro;
            this.lbSicCheck.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSicCheck.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbSicCheck.Location = new System.Drawing.Point(167, 0);
            this.lbSicCheck.Name = "lbSicCheck";
            this.lbSicCheck.Size = new System.Drawing.Size(1102, 22);
            this.lbSicCheck.TabIndex = 5;
            this.lbSicCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFileVersion
            // 
            this.lbFileVersion.BackColor = System.Drawing.Color.Gainsboro;
            this.lbFileVersion.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbFileVersion.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbFileVersion.Location = new System.Drawing.Point(0, 0);
            this.lbFileVersion.Name = "lbFileVersion";
            this.lbFileVersion.Size = new System.Drawing.Size(169, 21);
            this.lbFileVersion.TabIndex = 3;
            this.lbFileVersion.Text = "1.0.0.0";
            this.lbFileVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1920, 2);
            this.label5.TabIndex = 993;
            // 
            // FrmBottom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1920, 22);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbTimeValue);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbSicCheck);
            this.Controls.Add(this.lbFileVersion);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "FrmBottom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormTitle";
            this.Load += new System.EventHandler(this.FrmTitle_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbFileVersion;
        private System.Windows.Forms.Label lbSicCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbTimeValue;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label5;
    }
}