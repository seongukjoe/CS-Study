namespace XModule.Forms.FormWorking
{
    partial class FrmMenuWorking
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
            this.btnCuring = new System.Windows.Forms.Button();
            this.btnUnloader = new System.Windows.Forms.Button();
            this.btnBonder = new System.Windows.Forms.Button();
            this.btnUnloaderPicker = new System.Windows.Forms.Button();
            this.btnIndex = new System.Windows.Forms.Button();
            this.btnLensPicker = new System.Windows.Forms.Button();
            this.btnLensLoader = new System.Windows.Forms.Button();
            this.btnVCMPicker = new System.Windows.Forms.Button();
            this.btnVCMLoader = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnCuring);
            this.panel1.Controls.Add(this.btnUnloader);
            this.panel1.Controls.Add(this.btnBonder);
            this.panel1.Controls.Add(this.btnUnloaderPicker);
            this.panel1.Controls.Add(this.btnIndex);
            this.panel1.Controls.Add(this.btnLensPicker);
            this.panel1.Controls.Add(this.btnLensLoader);
            this.panel1.Controls.Add(this.btnVCMPicker);
            this.panel1.Controls.Add(this.btnVCMLoader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 1000);
            this.panel1.TabIndex = 980;
            // 
            // btnCuring
            // 
            this.btnCuring.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCuring.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCuring.Location = new System.Drawing.Point(3, 337);
            this.btnCuring.Name = "btnCuring";
            this.btnCuring.Size = new System.Drawing.Size(125, 50);
            this.btnCuring.TabIndex = 1010;
            this.btnCuring.Tag = "8";
            this.btnCuring.Text = "CURING";
            this.btnCuring.UseVisualStyleBackColor = false;
            this.btnCuring.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnCuring.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnUnloader
            // 
            this.btnUnloader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnUnloader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloader.Location = new System.Drawing.Point(2, 393);
            this.btnUnloader.Name = "btnUnloader";
            this.btnUnloader.Size = new System.Drawing.Size(125, 50);
            this.btnUnloader.TabIndex = 1009;
            this.btnUnloader.Tag = "7";
            this.btnUnloader.Text = "UNLOADER";
            this.btnUnloader.UseVisualStyleBackColor = false;
            this.btnUnloader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnUnloader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnBonder
            // 
            this.btnBonder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnBonder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBonder.Location = new System.Drawing.Point(3, 281);
            this.btnBonder.Name = "btnBonder";
            this.btnBonder.Size = new System.Drawing.Size(125, 50);
            this.btnBonder.TabIndex = 1008;
            this.btnBonder.Tag = "6";
            this.btnBonder.Text = "BONDER";
            this.btnBonder.UseVisualStyleBackColor = false;
            this.btnBonder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnBonder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnUnloaderPicker
            // 
            this.btnUnloaderPicker.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnUnloaderPicker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloaderPicker.Location = new System.Drawing.Point(2, 449);
            this.btnUnloaderPicker.Name = "btnUnloaderPicker";
            this.btnUnloaderPicker.Size = new System.Drawing.Size(125, 50);
            this.btnUnloaderPicker.TabIndex = 1007;
            this.btnUnloaderPicker.Tag = "5";
            this.btnUnloaderPicker.Text = "UNLOADER\r\nPICKER";
            this.btnUnloaderPicker.UseVisualStyleBackColor = false;
            this.btnUnloaderPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnUnloaderPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnIndex
            // 
            this.btnIndex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnIndex.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIndex.Location = new System.Drawing.Point(3, 225);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(125, 50);
            this.btnIndex.TabIndex = 1006;
            this.btnIndex.Tag = "4";
            this.btnIndex.Text = "INDEX";
            this.btnIndex.UseVisualStyleBackColor = false;
            this.btnIndex.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnIndex.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnLensPicker
            // 
            this.btnLensPicker.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLensPicker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLensPicker.Location = new System.Drawing.Point(3, 169);
            this.btnLensPicker.Name = "btnLensPicker";
            this.btnLensPicker.Size = new System.Drawing.Size(125, 50);
            this.btnLensPicker.TabIndex = 1005;
            this.btnLensPicker.Tag = "3";
            this.btnLensPicker.Text = "LENS PICKER";
            this.btnLensPicker.UseVisualStyleBackColor = false;
            this.btnLensPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnLensPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnLensLoader
            // 
            this.btnLensLoader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLensLoader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLensLoader.Location = new System.Drawing.Point(3, 113);
            this.btnLensLoader.Name = "btnLensLoader";
            this.btnLensLoader.Size = new System.Drawing.Size(125, 50);
            this.btnLensLoader.TabIndex = 1004;
            this.btnLensLoader.Tag = "2";
            this.btnLensLoader.Text = "LENS LOADER";
            this.btnLensLoader.UseVisualStyleBackColor = false;
            this.btnLensLoader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnLensLoader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnVCMPicker
            // 
            this.btnVCMPicker.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnVCMPicker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVCMPicker.Location = new System.Drawing.Point(3, 57);
            this.btnVCMPicker.Name = "btnVCMPicker";
            this.btnVCMPicker.Size = new System.Drawing.Size(125, 50);
            this.btnVCMPicker.TabIndex = 1003;
            this.btnVCMPicker.Tag = "1";
            this.btnVCMPicker.Text = "VCM PICKER";
            this.btnVCMPicker.UseVisualStyleBackColor = false;
            this.btnVCMPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnVCMPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btnVCMLoader
            // 
            this.btnVCMLoader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnVCMLoader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVCMLoader.Location = new System.Drawing.Point(3, 1);
            this.btnVCMLoader.Name = "btnVCMLoader";
            this.btnVCMLoader.Size = new System.Drawing.Size(125, 50);
            this.btnVCMLoader.TabIndex = 1001;
            this.btnVCMLoader.Tag = "0";
            this.btnVCMLoader.Text = "VCM LOADER";
            this.btnVCMLoader.UseVisualStyleBackColor = false;
            this.btnVCMLoader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnVCMLoader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // FrmMenuWorking
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(130, 1000);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMenuWorking";
            this.Text = "FormMenuWorking";
            this.Load += new System.EventHandler(this.FrmMenuWorking_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmMenuWorking_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVCMLoader;
        private System.Windows.Forms.Button btnVCMPicker;
        private System.Windows.Forms.Button btnCuring;
        private System.Windows.Forms.Button btnUnloader;
        private System.Windows.Forms.Button btnBonder;
        private System.Windows.Forms.Button btnUnloaderPicker;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Button btnLensPicker;
        private System.Windows.Forms.Button btnLensLoader;
    }
}