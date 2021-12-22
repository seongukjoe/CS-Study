namespace XModule.Forms.FormWorking
{
    partial class FormDoor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDoor));
            this.lbDoorState = new System.Windows.Forms.Label();
            this.lbDoorRear = new System.Windows.Forms.Label();
            this.panelRear1 = new System.Windows.Forms.Panel();
            this.label_RearMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_FrontMessage = new System.Windows.Forms.Label();
            this.lbDoorFront = new System.Windows.Forms.Label();
            this.glassButton_DoorLock = new Glass.GlassButton();
            this.glassButton_DoorOpen = new Glass.GlassButton();
            this.glassButton_Close = new Glass.GlassButton();
            this.timerColor = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbDoor3 = new System.Windows.Forms.Label();
            this.lbDoor4 = new System.Windows.Forms.Label();
            this.lbDoor1 = new System.Windows.Forms.Label();
            this.lbDoor2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panelRear1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDoorState
            // 
            this.lbDoorState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lbDoorState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoorState.Font = new System.Drawing.Font("굴림", 9F);
            this.lbDoorState.ForeColor = System.Drawing.Color.Black;
            this.lbDoorState.Location = new System.Drawing.Point(12, 12);
            this.lbDoorState.Name = "lbDoorState";
            this.lbDoorState.Size = new System.Drawing.Size(390, 46);
            this.lbDoorState.TabIndex = 1680;
            this.lbDoorState.Tag = "";
            this.lbDoorState.Text = "DOOR STATUS";
            this.lbDoorState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoorRear
            // 
            this.lbDoorRear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.lbDoorRear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoorRear.Font = new System.Drawing.Font("굴림", 9F);
            this.lbDoorRear.ForeColor = System.Drawing.Color.Black;
            this.lbDoorRear.Location = new System.Drawing.Point(3, 3);
            this.lbDoorRear.Name = "lbDoorRear";
            this.lbDoorRear.Size = new System.Drawing.Size(382, 46);
            this.lbDoorRear.TabIndex = 1681;
            this.lbDoorRear.Tag = "";
            this.lbDoorRear.Text = "DOOR REAR";
            this.lbDoorRear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelRear1
            // 
            this.panelRear1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRear1.Controls.Add(this.label_RearMessage);
            this.panelRear1.Controls.Add(this.lbDoorRear);
            this.panelRear1.Location = new System.Drawing.Point(12, 64);
            this.panelRear1.Name = "panelRear1";
            this.panelRear1.Size = new System.Drawing.Size(390, 185);
            this.panelRear1.TabIndex = 1682;
            // 
            // label_RearMessage
            // 
            this.label_RearMessage.BackColor = System.Drawing.Color.White;
            this.label_RearMessage.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold);
            this.label_RearMessage.ForeColor = System.Drawing.Color.Black;
            this.label_RearMessage.Location = new System.Drawing.Point(3, 55);
            this.label_RearMessage.Name = "label_RearMessage";
            this.label_RearMessage.Size = new System.Drawing.Size(382, 129);
            this.label_RearMessage.TabIndex = 1682;
            this.label_RearMessage.Tag = "";
            this.label_RearMessage.Text = "MESSAGE REAR";
            this.label_RearMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label_FrontMessage);
            this.panel1.Controls.Add(this.lbDoorFront);
            this.panel1.Location = new System.Drawing.Point(12, 255);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 185);
            this.panel1.TabIndex = 1683;
            // 
            // label_FrontMessage
            // 
            this.label_FrontMessage.BackColor = System.Drawing.Color.White;
            this.label_FrontMessage.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold);
            this.label_FrontMessage.ForeColor = System.Drawing.Color.Black;
            this.label_FrontMessage.Location = new System.Drawing.Point(3, 55);
            this.label_FrontMessage.Name = "label_FrontMessage";
            this.label_FrontMessage.Size = new System.Drawing.Size(382, 129);
            this.label_FrontMessage.TabIndex = 1682;
            this.label_FrontMessage.Tag = "";
            this.label_FrontMessage.Text = "MESSAGE FRONT";
            this.label_FrontMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoorFront
            // 
            this.lbDoorFront.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.lbDoorFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoorFront.Font = new System.Drawing.Font("굴림", 9F);
            this.lbDoorFront.ForeColor = System.Drawing.Color.Black;
            this.lbDoorFront.Location = new System.Drawing.Point(3, 3);
            this.lbDoorFront.Name = "lbDoorFront";
            this.lbDoorFront.Size = new System.Drawing.Size(382, 46);
            this.lbDoorFront.TabIndex = 1681;
            this.lbDoorFront.Tag = "";
            this.lbDoorFront.Text = "DOOR FRONT";
            this.lbDoorFront.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // glassButton_DoorLock
            // 
            this.glassButton_DoorLock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.glassButton_DoorLock.Font = new System.Drawing.Font("굴림", 9F);
            this.glassButton_DoorLock.ForeColor = System.Drawing.Color.Black;
            this.glassButton_DoorLock.GlowColor = System.Drawing.Color.White;
            this.glassButton_DoorLock.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glassButton_DoorLock.InnerBorderColor = System.Drawing.Color.White;
            this.glassButton_DoorLock.Location = new System.Drawing.Point(12, 446);
            this.glassButton_DoorLock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.glassButton_DoorLock.Name = "glassButton_DoorLock";
            this.glassButton_DoorLock.OuterBorderColor = System.Drawing.Color.Gray;
            this.glassButton_DoorLock.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.glassButton_DoorLock.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.glassButton_DoorLock.Size = new System.Drawing.Size(391, 46);
            this.glassButton_DoorLock.TabIndex = 1684;
            this.glassButton_DoorLock.TabStop = false;
            this.glassButton_DoorLock.Tag = "0";
            this.glassButton_DoorLock.Text = "DOOR LOCK";
            this.glassButton_DoorLock.Click += new System.EventHandler(this.glassButton_DoorLock_Click);
            // 
            // glassButton_DoorOpen
            // 
            this.glassButton_DoorOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.glassButton_DoorOpen.Font = new System.Drawing.Font("굴림", 9F);
            this.glassButton_DoorOpen.ForeColor = System.Drawing.Color.Black;
            this.glassButton_DoorOpen.GlowColor = System.Drawing.Color.White;
            this.glassButton_DoorOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glassButton_DoorOpen.InnerBorderColor = System.Drawing.Color.White;
            this.glassButton_DoorOpen.Location = new System.Drawing.Point(12, 498);
            this.glassButton_DoorOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.glassButton_DoorOpen.Name = "glassButton_DoorOpen";
            this.glassButton_DoorOpen.OuterBorderColor = System.Drawing.Color.Gray;
            this.glassButton_DoorOpen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.glassButton_DoorOpen.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.glassButton_DoorOpen.Size = new System.Drawing.Size(391, 46);
            this.glassButton_DoorOpen.TabIndex = 1685;
            this.glassButton_DoorOpen.TabStop = false;
            this.glassButton_DoorOpen.Tag = "0";
            this.glassButton_DoorOpen.Text = "DOOR OPEN";
            this.glassButton_DoorOpen.Click += new System.EventHandler(this.glassButton_DoorOpen_Click);
            // 
            // glassButton_Close
            // 
            this.glassButton_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.glassButton_Close.Font = new System.Drawing.Font("굴림", 9F);
            this.glassButton_Close.ForeColor = System.Drawing.Color.Black;
            this.glassButton_Close.GlowColor = System.Drawing.Color.White;
            this.glassButton_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glassButton_Close.InnerBorderColor = System.Drawing.Color.White;
            this.glassButton_Close.Location = new System.Drawing.Point(12, 550);
            this.glassButton_Close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.glassButton_Close.Name = "glassButton_Close";
            this.glassButton_Close.OuterBorderColor = System.Drawing.Color.Gray;
            this.glassButton_Close.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.glassButton_Close.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.glassButton_Close.Size = new System.Drawing.Size(391, 46);
            this.glassButton_Close.TabIndex = 1686;
            this.glassButton_Close.TabStop = false;
            this.glassButton_Close.Tag = "0";
            this.glassButton_Close.Text = "CLOSE";
            this.glassButton_Close.Click += new System.EventHandler(this.glassButton_Close_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.lbDoor3);
            this.groupBox1.Controls.Add(this.lbDoor4);
            this.groupBox1.Controls.Add(this.lbDoor1);
            this.groupBox1.Controls.Add(this.lbDoor2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(409, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 553);
            this.groupBox1.TabIndex = 1687;
            this.groupBox1.TabStop = false;
            // 
            // lbDoor3
            // 
            this.lbDoor3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDoor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDoor3.ForeColor = System.Drawing.Color.Black;
            this.lbDoor3.Location = new System.Drawing.Point(196, 13);
            this.lbDoor3.Name = "lbDoor3";
            this.lbDoor3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDoor3.Size = new System.Drawing.Size(86, 38);
            this.lbDoor3.TabIndex = 1531;
            this.lbDoor3.Text = "DOOR #3";
            this.lbDoor3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDoor4
            // 
            this.lbDoor4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDoor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDoor4.ForeColor = System.Drawing.Color.Black;
            this.lbDoor4.Location = new System.Drawing.Point(401, 13);
            this.lbDoor4.Name = "lbDoor4";
            this.lbDoor4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDoor4.Size = new System.Drawing.Size(86, 38);
            this.lbDoor4.TabIndex = 1532;
            this.lbDoor4.Text = "DOOR #4";
            this.lbDoor4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDoor1
            // 
            this.lbDoor1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDoor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDoor1.ForeColor = System.Drawing.Color.Black;
            this.lbDoor1.Location = new System.Drawing.Point(208, 507);
            this.lbDoor1.Name = "lbDoor1";
            this.lbDoor1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDoor1.Size = new System.Drawing.Size(86, 38);
            this.lbDoor1.TabIndex = 1529;
            this.lbDoor1.Text = "DOOR  #1";
            this.lbDoor1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDoor2
            // 
            this.lbDoor2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDoor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDoor2.ForeColor = System.Drawing.Color.Black;
            this.lbDoor2.Location = new System.Drawing.Point(388, 507);
            this.lbDoor2.Name = "lbDoor2";
            this.lbDoor2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDoor2.Size = new System.Drawing.Size(86, 38);
            this.lbDoor2.TabIndex = 1530;
            this.lbDoor2.Text = "DOOR #2";
            this.lbDoor2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(880, 647);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 19);
            this.button2.TabIndex = 989;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // FormDoor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1056, 610);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.glassButton_Close);
            this.Controls.Add(this.glassButton_DoorOpen);
            this.Controls.Add(this.glassButton_DoorLock);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelRear1);
            this.Controls.Add(this.lbDoorState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDoor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDoor_FormClosed);
            this.Load += new System.EventHandler(this.FormDoor_Load);
            this.Shown += new System.EventHandler(this.FormDoor_Shown);
            this.panelRear1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbDoorState;
        private System.Windows.Forms.Label lbDoorRear;
        private System.Windows.Forms.Panel panelRear1;
        private System.Windows.Forms.Label label_RearMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_FrontMessage;
        private System.Windows.Forms.Label lbDoorFront;
        private Glass.GlassButton glassButton_DoorLock;
        private Glass.GlassButton glassButton_DoorOpen;
        private Glass.GlassButton glassButton_Close;
        private System.Windows.Forms.Timer timerColor;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbDoor3;
        private System.Windows.Forms.Label lbDoor4;
        private System.Windows.Forms.Label lbDoor1;
        private System.Windows.Forms.Label lbDoor2;
        private System.Windows.Forms.Button button2;
    }
}