namespace XModule.Forms.FormRecipe
{
    partial class FrmMenuRecipe
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
            this.btnProject = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnProject);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 995);
            this.panel1.TabIndex = 979;
            // 
            // btnProject
            // 
            this.btnProject.BackColor = System.Drawing.Color.White;
            this.btnProject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnProject.Location = new System.Drawing.Point(3, 6);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(125, 61);
            this.btnProject.TabIndex = 992;
            this.btnProject.Tag = "0";
            this.btnProject.Text = "PROJECT";
            this.btnProject.UseVisualStyleBackColor = false;
            this.btnProject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // FrmMenuRecipe
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(130, 995);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMenuRecipe";
            this.Text = "FormMenuRecipe";
            this.Load += new System.EventHandler(this.FormMenuRecipe_Load);
            this.VisibleChanged += new System.EventHandler(this.FormMenuRecipe_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnProject;
    }
}