namespace XModule.Forms.FormHistory
{
    partial class FrmMenuHistory
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMessageEdit = new System.Windows.Forms.Button();
            this.btnWorking = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.btnEvent = new System.Windows.Forms.Button();
            this.btnWarning = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.btnModify);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnMessageEdit);
            this.panel1.Controls.Add(this.btnWorking);
            this.panel1.Controls.Add(this.btnData);
            this.panel1.Controls.Add(this.btnEvent);
            this.panel1.Controls.Add(this.btnWarning);
            this.panel1.Controls.Add(this.btnAlarm);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 998);
            this.panel1.TabIndex = 1;
            // 
            // btnInsert
            // 
            this.btnInsert.BackColor = System.Drawing.Color.White;
            this.btnInsert.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnInsert.ForeColor = System.Drawing.Color.Black;
            this.btnInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsert.Location = new System.Drawing.Point(4, 686);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInsert.Size = new System.Drawing.Size(124, 60);
            this.btnInsert.TabIndex = 976;
            this.btnInsert.TabStop = false;
            this.btnInsert.Tag = "0";
            this.btnInsert.Text = "ADD";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Visible = false;
            this.btnInsert.Click += new System.EventHandler(this.EditingClick);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.White;
            this.btnModify.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnModify.ForeColor = System.Drawing.Color.Black;
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModify.Location = new System.Drawing.Point(4, 748);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModify.Name = "btnModify";
            this.btnModify.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnModify.Size = new System.Drawing.Size(124, 60);
            this.btnModify.TabIndex = 976;
            this.btnModify.TabStop = false;
            this.btnModify.Tag = "1";
            this.btnModify.Text = "MODIFY";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(4, 810);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDelete.Size = new System.Drawing.Size(124, 60);
            this.btnDelete.TabIndex = 976;
            this.btnDelete.TabStop = false;
            this.btnDelete.Tag = "2";
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            // 
            // btnMessageEdit
            // 
            this.btnMessageEdit.BackColor = System.Drawing.Color.Silver;
            this.btnMessageEdit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnMessageEdit.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnMessageEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMessageEdit.Location = new System.Drawing.Point(3, 872);
            this.btnMessageEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMessageEdit.Name = "btnMessageEdit";
            this.btnMessageEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMessageEdit.Size = new System.Drawing.Size(124, 60);
            this.btnMessageEdit.TabIndex = 976;
            this.btnMessageEdit.TabStop = false;
            this.btnMessageEdit.Tag = "4";
            this.btnMessageEdit.Text = "MESSAGE EDIT";
            this.btnMessageEdit.UseVisualStyleBackColor = false;
            this.btnMessageEdit.Visible = false;
            this.btnMessageEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.btnMessageEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            // 
            // btnWorking
            // 
            this.btnWorking.BackColor = System.Drawing.Color.White;
            this.btnWorking.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnWorking.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnWorking.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWorking.Location = new System.Drawing.Point(3, 255);
            this.btnWorking.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWorking.Name = "btnWorking";
            this.btnWorking.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnWorking.Size = new System.Drawing.Size(124, 60);
            this.btnWorking.TabIndex = 976;
            this.btnWorking.TabStop = false;
            this.btnWorking.Tag = "4";
            this.btnWorking.Text = "WORKING";
            this.btnWorking.UseVisualStyleBackColor = false;
            this.btnWorking.Visible = false;
            this.btnWorking.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.btnWorking.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.White;
            this.btnData.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnData.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnData.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnData.Location = new System.Drawing.Point(4, 193);
            this.btnData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnData.Name = "btnData";
            this.btnData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnData.Size = new System.Drawing.Size(124, 60);
            this.btnData.TabIndex = 976;
            this.btnData.TabStop = false;
            this.btnData.Tag = "3";
            this.btnData.Text = "DATA";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.btnData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            // 
            // btnEvent
            // 
            this.btnEvent.BackColor = System.Drawing.Color.White;
            this.btnEvent.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnEvent.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEvent.Location = new System.Drawing.Point(3, 131);
            this.btnEvent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEvent.Size = new System.Drawing.Size(124, 60);
            this.btnEvent.TabIndex = 976;
            this.btnEvent.TabStop = false;
            this.btnEvent.Tag = "2";
            this.btnEvent.Text = "EVENT";
            this.btnEvent.UseVisualStyleBackColor = false;
            this.btnEvent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.btnEvent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            // 
            // btnWarning
            // 
            this.btnWarning.BackColor = System.Drawing.Color.White;
            this.btnWarning.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnWarning.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnWarning.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWarning.Location = new System.Drawing.Point(3, 69);
            this.btnWarning.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWarning.Name = "btnWarning";
            this.btnWarning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnWarning.Size = new System.Drawing.Size(124, 60);
            this.btnWarning.TabIndex = 976;
            this.btnWarning.TabStop = false;
            this.btnWarning.Tag = "1";
            this.btnWarning.Text = "WARNNING";
            this.btnWarning.UseVisualStyleBackColor = false;
            this.btnWarning.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.btnWarning.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            // 
            // btnAlarm
            // 
            this.btnAlarm.BackColor = System.Drawing.Color.White;
            this.btnAlarm.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(150)));
            this.btnAlarm.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAlarm.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlarm.Location = new System.Drawing.Point(3, 7);
            this.btnAlarm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAlarm.Size = new System.Drawing.Size(124, 60);
            this.btnAlarm.TabIndex = 976;
            this.btnAlarm.TabStop = false;
            this.btnAlarm.Tag = "0";
            this.btnAlarm.Text = "ALARM";
            this.btnAlarm.UseVisualStyleBackColor = false;
            this.btnAlarm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.btnAlarm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            // 
            // FrmMenuHistory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(130, 995);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMenuHistory";
            this.Text = "FormMenuHistory";
            this.Load += new System.EventHandler(this.FormMenuHistory_Load);
            this.VisibleChanged += new System.EventHandler(this.FormMenuHistory_VisibleChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMenuHistory_MouseUp);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMessageEdit;
        private System.Windows.Forms.Button btnWorking;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnEvent;
        private System.Windows.Forms.Button btnWarning;
        private System.Windows.Forms.Button btnAlarm;
    }
}