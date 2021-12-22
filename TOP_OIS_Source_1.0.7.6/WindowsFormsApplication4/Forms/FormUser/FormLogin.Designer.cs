namespace XModule.Forms.FormUser
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblPassWord = new System.Windows.Forms.Label();
            this.BTN_PasswordChenge = new System.Windows.Forms.Button();
            this.BTN_LOGIN = new System.Windows.Forms.Button();
            this.btn_Master = new System.Windows.Forms.Button();
            this.btn_Maint = new System.Windows.Forms.Button();
            this.btn_Opp = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassWord.Location = new System.Drawing.Point(164, 279);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(391, 33);
            this.txtPassWord.TabIndex = 409;
            this.txtPassWord.Text = "0000";
            this.txtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPassWord
            // 
            this.lblPassWord.AutoSize = true;
            this.lblPassWord.Font = new System.Drawing.Font("맑은 고딕", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblPassWord.ForeColor = System.Drawing.Color.Black;
            this.lblPassWord.Location = new System.Drawing.Point(6, 282);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(119, 25);
            this.lblPassWord.TabIndex = 408;
            this.lblPassWord.Text = "PASSWORD";
            // 
            // BTN_PasswordChenge
            // 
            this.BTN_PasswordChenge.BackColor = System.Drawing.Color.White;
            this.BTN_PasswordChenge.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PasswordChenge.ForeColor = System.Drawing.Color.Black;
            this.BTN_PasswordChenge.Image = ((System.Drawing.Image)(resources.GetObject("BTN_PasswordChenge.Image")));
            this.BTN_PasswordChenge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_PasswordChenge.Location = new System.Drawing.Point(366, 359);
            this.BTN_PasswordChenge.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.BTN_PasswordChenge.Name = "BTN_PasswordChenge";
            this.BTN_PasswordChenge.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.BTN_PasswordChenge.Size = new System.Drawing.Size(191, 100);
            this.BTN_PasswordChenge.TabIndex = 411;
            this.BTN_PasswordChenge.Text = "PASSWORD\r\nCHANGE";
            this.BTN_PasswordChenge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTN_PasswordChenge.UseVisualStyleBackColor = false;
            this.BTN_PasswordChenge.Click += new System.EventHandler(this.swLogin_Clcik);
            // 
            // BTN_LOGIN
            // 
            this.BTN_LOGIN.BackColor = System.Drawing.Color.White;
            this.BTN_LOGIN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOGIN.ForeColor = System.Drawing.Color.Black;
            this.BTN_LOGIN.Image = ((System.Drawing.Image)(resources.GetObject("BTN_LOGIN.Image")));
            this.BTN_LOGIN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_LOGIN.Location = new System.Drawing.Point(164, 359);
            this.BTN_LOGIN.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.BTN_LOGIN.Name = "BTN_LOGIN";
            this.BTN_LOGIN.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.BTN_LOGIN.Size = new System.Drawing.Size(191, 100);
            this.BTN_LOGIN.TabIndex = 410;
            this.BTN_LOGIN.Text = "LOGIN";
            this.BTN_LOGIN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTN_LOGIN.UseVisualStyleBackColor = false;
            this.BTN_LOGIN.Click += new System.EventHandler(this.swLogin_Clcik);
            // 
            // btn_Master
            // 
            this.btn_Master.BackColor = System.Drawing.Color.White;
            this.btn_Master.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Master.Image = ((System.Drawing.Image)(resources.GetObject("btn_Master.Image")));
            this.btn_Master.Location = new System.Drawing.Point(432, 116);
            this.btn_Master.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Master.Name = "btn_Master";
            this.btn_Master.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btn_Master.Size = new System.Drawing.Size(125, 140);
            this.btn_Master.TabIndex = 407;
            this.btn_Master.Tag = "2";
            this.btn_Master.Text = "MASTER";
            this.btn_Master.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Master.UseVisualStyleBackColor = false;
            this.btn_Master.Click += new System.EventHandler(this.swLoginLevel_Click);
            // 
            // btn_Maint
            // 
            this.btn_Maint.BackColor = System.Drawing.Color.White;
            this.btn_Maint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Maint.Image = ((System.Drawing.Image)(resources.GetObject("btn_Maint.Image")));
            this.btn_Maint.Location = new System.Drawing.Point(298, 116);
            this.btn_Maint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Maint.Name = "btn_Maint";
            this.btn_Maint.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btn_Maint.Size = new System.Drawing.Size(125, 140);
            this.btn_Maint.TabIndex = 406;
            this.btn_Maint.Tag = "1";
            this.btn_Maint.Text = "ENGINEER";
            this.btn_Maint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Maint.UseVisualStyleBackColor = false;
            this.btn_Maint.Click += new System.EventHandler(this.swLoginLevel_Click);
            // 
            // btn_Opp
            // 
            this.btn_Opp.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btn_Opp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Opp.Image = ((System.Drawing.Image)(resources.GetObject("btn_Opp.Image")));
            this.btn_Opp.Location = new System.Drawing.Point(164, 116);
            this.btn_Opp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Opp.Name = "btn_Opp";
            this.btn_Opp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btn_Opp.Size = new System.Drawing.Size(125, 140);
            this.btn_Opp.TabIndex = 405;
            this.btn_Opp.Tag = "0";
            this.btn_Opp.Text = "OP";
            this.btn_Opp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Opp.UseVisualStyleBackColor = false;
            this.btn_Opp.Click += new System.EventHandler(this.swLoginLevel_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(582, 553);
            this.ControlBox = false;
            this.Controls.Add(this.BTN_PasswordChenge);
            this.Controls.Add(this.BTN_LOGIN);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.lblPassWord);
            this.Controls.Add(this.btn_Master);
            this.Controls.Add(this.btn_Maint);
            this.Controls.Add(this.btn_Opp);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Click += new System.EventHandler(this.FrmLogin_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button BTN_PasswordChenge;
        internal System.Windows.Forms.Button BTN_LOGIN;
        internal System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblPassWord;
        private System.Windows.Forms.Button btn_Master;
        private System.Windows.Forms.Button btn_Maint;
        private System.Windows.Forms.Button btn_Opp;
        private System.Windows.Forms.Timer timer1;


    }
}