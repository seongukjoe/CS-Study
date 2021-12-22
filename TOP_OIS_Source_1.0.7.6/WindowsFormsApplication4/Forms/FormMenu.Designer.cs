namespace XModule.Forms
{
    partial class FrmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPMmode = new System.Windows.Forms.CheckBox();
            this.btnLog = new System.Windows.Forms.CheckBox();
            this.btnSetup = new System.Windows.Forms.CheckBox();
            this.btnExit = new System.Windows.Forms.CheckBox();
            this.btnRecipe = new System.Windows.Forms.CheckBox();
            this.btnWorking = new System.Windows.Forms.CheckBox();
            this.btnOperator = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(40, 36);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnPMmode);
            this.panel1.Controls.Add(this.btnLog);
            this.panel1.Controls.Add(this.btnSetup);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnRecipe);
            this.panel1.Controls.Add(this.btnWorking);
            this.panel1.Controls.Add(this.btnOperator);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 61);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(0, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1000, 2);
            this.label5.TabIndex = 992;
            // 
            // btnPMmode
            // 
            this.btnPMmode.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPMmode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnPMmode.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPMmode.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPMmode.FlatAppearance.BorderSize = 2;
            this.btnPMmode.Font = new System.Drawing.Font("굴림", 9F);
            this.btnPMmode.ForeColor = System.Drawing.Color.Black;
            this.btnPMmode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPMmode.ImageKey = "Main5.png";
            this.btnPMmode.Location = new System.Drawing.Point(714, 4);
            this.btnPMmode.Name = "btnPMmode";
            this.btnPMmode.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnPMmode.Size = new System.Drawing.Size(135, 50);
            this.btnPMmode.TabIndex = 991;
            this.btnPMmode.Tag = "6";
            this.btnPMmode.Text = "PM";
            this.btnPMmode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPMmode.UseVisualStyleBackColor = false;
            this.btnPMmode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnPMmode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnLog
            // 
            this.btnLog.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnLog.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLog.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLog.FlatAppearance.BorderSize = 2;
            this.btnLog.Font = new System.Drawing.Font("굴림", 9F);
            this.btnLog.ForeColor = System.Drawing.Color.Black;
            this.btnLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLog.ImageKey = "Main5.png";
            this.btnLog.Location = new System.Drawing.Point(573, 4);
            this.btnLog.Name = "btnLog";
            this.btnLog.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnLog.Size = new System.Drawing.Size(135, 50);
            this.btnLog.TabIndex = 989;
            this.btnLog.Tag = "3";
            this.btnLog.Text = "STATISTICS";
            this.btnLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnLog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnSetup
            // 
            this.btnSetup.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnSetup.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSetup.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSetup.FlatAppearance.BorderSize = 2;
            this.btnSetup.Font = new System.Drawing.Font("굴림", 9F);
            this.btnSetup.ForeColor = System.Drawing.Color.Black;
            this.btnSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetup.ImageKey = "Main5.png";
            this.btnSetup.Location = new System.Drawing.Point(432, 4);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnSetup.Size = new System.Drawing.Size(135, 50);
            this.btnSetup.TabIndex = 988;
            this.btnSetup.Tag = "5";
            this.btnSetup.Text = "SETUP";
            this.btnSetup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSetup.UseVisualStyleBackColor = false;
            this.btnSetup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnSetup.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnExit
            // 
            this.btnExit.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnExit.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExit.FlatAppearance.BorderSize = 2;
            this.btnExit.Font = new System.Drawing.Font("굴림", 9F);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(855, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnExit.Size = new System.Drawing.Size(135, 50);
            this.btnExit.TabIndex = 987;
            this.btnExit.Tag = "0";
            this.btnExit.Text = "EXIT";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRecipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnRecipe.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRecipe.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRecipe.FlatAppearance.BorderSize = 2;
            this.btnRecipe.Font = new System.Drawing.Font("굴림", 9F);
            this.btnRecipe.ForeColor = System.Drawing.Color.Black;
            this.btnRecipe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipe.ImageKey = "Main5.png";
            this.btnRecipe.Location = new System.Drawing.Point(150, 4);
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnRecipe.Size = new System.Drawing.Size(135, 50);
            this.btnRecipe.TabIndex = 986;
            this.btnRecipe.Tag = "4";
            this.btnRecipe.Text = "RECIPE";
            this.btnRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRecipe.UseVisualStyleBackColor = false;
            this.btnRecipe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnRecipe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnWorking
            // 
            this.btnWorking.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnWorking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnWorking.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnWorking.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnWorking.FlatAppearance.BorderSize = 2;
            this.btnWorking.Font = new System.Drawing.Font("굴림", 9F);
            this.btnWorking.ForeColor = System.Drawing.Color.Black;
            this.btnWorking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWorking.ImageKey = "Main5.png";
            this.btnWorking.Location = new System.Drawing.Point(291, 4);
            this.btnWorking.Name = "btnWorking";
            this.btnWorking.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnWorking.Size = new System.Drawing.Size(135, 50);
            this.btnWorking.TabIndex = 984;
            this.btnWorking.Tag = "2";
            this.btnWorking.Text = "TEACH";
            this.btnWorking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnWorking.UseVisualStyleBackColor = false;
            this.btnWorking.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnWorking.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnOperator
            // 
            this.btnOperator.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnOperator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnOperator.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOperator.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOperator.FlatAppearance.BorderSize = 2;
            this.btnOperator.Font = new System.Drawing.Font("굴림", 9F);
            this.btnOperator.ForeColor = System.Drawing.Color.Black;
            this.btnOperator.Image = ((System.Drawing.Image)(resources.GetObject("btnOperator.Image")));
            this.btnOperator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOperator.Location = new System.Drawing.Point(9, 4);
            this.btnOperator.Name = "btnOperator";
            this.btnOperator.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnOperator.Size = new System.Drawing.Size(135, 50);
            this.btnOperator.TabIndex = 985;
            this.btnOperator.Tag = "0";
            this.btnOperator.Text = "    AUTO";
            this.btnOperator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOperator.UseVisualStyleBackColor = false;
            this.btnOperator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnOperator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // FrmMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 61);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMenu";
            this.Text = "FormMenu";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox btnRecipe;
        private System.Windows.Forms.CheckBox btnWorking;
        private System.Windows.Forms.CheckBox btnOperator;
        private System.Windows.Forms.CheckBox btnPMmode;
        private System.Windows.Forms.CheckBox btnLog;
        private System.Windows.Forms.CheckBox btnSetup;
        private System.Windows.Forms.CheckBox btnExit;
        private System.Windows.Forms.Label label5;
    }
}