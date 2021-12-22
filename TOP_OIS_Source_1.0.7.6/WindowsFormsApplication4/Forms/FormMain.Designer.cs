namespace XModule
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_ToStop = new System.Windows.Forms.Panel();
            this.lbMain = new System.Windows.Forms.Label();
            this.btnCylinder = new System.Windows.Forms.Button();
            this.btnDigitalLink = new System.Windows.Forms.Button();
            this.btnDigital = new System.Windows.Forms.Button();
            this.btnMotion = new System.Windows.Forms.Button();
            this.swClose = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlMotionStatus = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel_ToStop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel_ToStop);
            this.panel1.Controls.Add(this.btnCylinder);
            this.panel1.Controls.Add(this.btnDigitalLink);
            this.panel1.Controls.Add(this.btnDigital);
            this.panel1.Controls.Add(this.btnMotion);
            this.panel1.Controls.Add(this.swClose);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.TabIndex = 966;
            // 
            // panel_ToStop
            // 
            this.panel_ToStop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_ToStop.Controls.Add(this.lbMain);
            this.panel_ToStop.Location = new System.Drawing.Point(453, 247);
            this.panel_ToStop.Name = "panel_ToStop";
            this.panel_ToStop.Size = new System.Drawing.Size(819, 348);
            this.panel_ToStop.TabIndex = 974;
            this.panel_ToStop.Visible = false;
            // 
            // lbMain
            // 
            this.lbMain.BackColor = System.Drawing.Color.White;
            this.lbMain.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbMain.Location = new System.Drawing.Point(71, 86);
            this.lbMain.Name = "lbMain";
            this.lbMain.Size = new System.Drawing.Size(682, 175);
            this.lbMain.TabIndex = 0;
            this.lbMain.Text = "설비가 정지 준비중입니다.\r\n\r\n잠시만 기다려 주십시오.";
            this.lbMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCylinder
            // 
            this.btnCylinder.Location = new System.Drawing.Point(41, 457);
            this.btnCylinder.Name = "btnCylinder";
            this.btnCylinder.Size = new System.Drawing.Size(112, 45);
            this.btnCylinder.TabIndex = 969;
            this.btnCylinder.Text = "Cylinder";
            this.btnCylinder.UseVisualStyleBackColor = true;
            this.btnCylinder.Visible = false;
            this.btnCylinder.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnDigitalLink
            // 
            this.btnDigitalLink.Location = new System.Drawing.Point(41, 406);
            this.btnDigitalLink.Name = "btnDigitalLink";
            this.btnDigitalLink.Size = new System.Drawing.Size(112, 45);
            this.btnDigitalLink.TabIndex = 970;
            this.btnDigitalLink.Text = "DigitalLINK";
            this.btnDigitalLink.UseVisualStyleBackColor = true;
            this.btnDigitalLink.Visible = false;
            this.btnDigitalLink.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDigital
            // 
            this.btnDigital.Location = new System.Drawing.Point(41, 355);
            this.btnDigital.Name = "btnDigital";
            this.btnDigital.Size = new System.Drawing.Size(112, 45);
            this.btnDigital.TabIndex = 971;
            this.btnDigital.Text = "Digital";
            this.btnDigital.UseVisualStyleBackColor = true;
            this.btnDigital.Visible = false;
            this.btnDigital.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnMotion
            // 
            this.btnMotion.Location = new System.Drawing.Point(44, 286);
            this.btnMotion.Name = "btnMotion";
            this.btnMotion.Size = new System.Drawing.Size(109, 60);
            this.btnMotion.TabIndex = 972;
            this.btnMotion.Text = "Motion";
            this.btnMotion.UseVisualStyleBackColor = true;
            this.btnMotion.Visible = false;
            this.btnMotion.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // swClose
            // 
            this.swClose.BackColor = System.Drawing.Color.Black;
            this.swClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swClose.ForeColor = System.Drawing.Color.Red;
            this.swClose.Location = new System.Drawing.Point(56, 499);
            this.swClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swClose.Name = "swClose";
            this.swClose.Size = new System.Drawing.Size(97, 41);
            this.swClose.TabIndex = 968;
            this.swClose.TabStop = false;
            this.swClose.Text = "Close";
            this.swClose.UseVisualStyleBackColor = false;
            this.swClose.Visible = false;
            this.swClose.Click += new System.EventHandler(this.swClose_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "image1.png");
            this.imageList1.Images.SetKeyName(1, "Image2.png");
            this.imageList1.Images.SetKeyName(2, "Image3.png");
            this.imageList1.Images.SetKeyName(3, "Image4.png");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.pnlMotionStatus);
            this.panel2.Location = new System.Drawing.Point(450, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 200);
            this.panel2.TabIndex = 975;
            this.panel2.Visible = false;
            // 
            // pnlMotionStatus
            // 
            this.pnlMotionStatus.BackColor = System.Drawing.Color.White;
            this.pnlMotionStatus.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pnlMotionStatus.Location = new System.Drawing.Point(74, 28);
            this.pnlMotionStatus.Name = "pnlMotionStatus";
            this.pnlMotionStatus.Size = new System.Drawing.Size(682, 136);
            this.pnlMotionStatus.TabIndex = 0;
            this.pnlMotionStatus.Text = "Check Motion Status";
            this.pnlMotionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "show";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel_ToStop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCylinder;
        private System.Windows.Forms.Button btnDigitalLink;
        private System.Windows.Forms.Button btnDigital;
        private System.Windows.Forms.Button btnMotion;
        internal System.Windows.Forms.Button swClose;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel_ToStop;
        private System.Windows.Forms.Label lbMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label pnlMotionStatus;
    }
}

