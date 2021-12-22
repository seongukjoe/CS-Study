namespace XModule.Forms.FormWorking
{
    partial class FormPagePM
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
            this.btnDoor = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::XModule.Properties.Resources.PM;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.panel1.Location = new System.Drawing.Point(424, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1083, 448);
            this.panel1.TabIndex = 2;
            // 
            // btnDoor
            // 
            this.btnDoor.BackColor = System.Drawing.Color.Silver;
            this.btnDoor.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoor.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnDoor.GlowColor = System.Drawing.Color.Transparent;
            this.btnDoor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDoor.InnerBorderColor = System.Drawing.Color.White;
            this.btnDoor.Location = new System.Drawing.Point(522, 684);
            this.btnDoor.Name = "btnDoor";
            this.btnDoor.OuterBorderColor = System.Drawing.Color.Black;
            this.btnDoor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDoor.ShineColor = System.Drawing.Color.Silver;
            this.btnDoor.Size = new System.Drawing.Size(873, 166);
            this.btnDoor.TabIndex = 1496;
            this.btnDoor.TabStop = false;
            this.btnDoor.Tag = "0";
            this.btnDoor.Text = "Door";
            this.btnDoor.Visible = false;
            this.btnDoor.Click += new System.EventHandler(this.btnDoor_Click);
            // 
            // FormPagePM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1930, 1092);
            this.Controls.Add(this.btnDoor);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPagePM";
            this.Text = "FormPagePM";
            this.Load += new System.EventHandler(this.FormPageWorkingInterface_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Glass.GlassButton btnDoor;
    }
}