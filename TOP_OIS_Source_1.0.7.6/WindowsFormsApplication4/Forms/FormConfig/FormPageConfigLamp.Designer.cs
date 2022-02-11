﻿namespace XModule.Forms.FormConfig
{
    partial class FrmPageConfigLamp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.a1Panel11 = new Owf.Controls.A1Panel();
            this.lbItems = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.a1Panel2 = new Owf.Controls.A1Panel();
            this.lbTargetState = new System.Windows.Forms.Label();
            this.btnOutput = new Glass.GlassButton();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBlink = new Glass.GlassButton();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBuzzer = new Glass.GlassButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTarget = new Glass.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnIndex = new Glass.GlassButton();
            this.label7 = new System.Windows.Forms.Label();
            this.lbConfiguration = new System.Windows.Forms.Label();
            this.a1Panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.a1Panel1.SuspendLayout();
            this.a1Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // a1Panel11
            // 
            this.a1Panel11.BackColor = System.Drawing.Color.White;
            this.a1Panel11.BorderColor = System.Drawing.Color.Black;
            this.a1Panel11.BorderWidth = 2;
            this.a1Panel11.Controls.Add(this.lbItems);
            this.a1Panel11.Controls.Add(this.grid);
            this.a1Panel11.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel11.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel11.Image = null;
            this.a1Panel11.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel11.Location = new System.Drawing.Point(0, 0);
            this.a1Panel11.Name = "a1Panel11";
            this.a1Panel11.RoundCornerRadius = 12;
            this.a1Panel11.ShadowOffSet = 1;
            this.a1Panel11.Size = new System.Drawing.Size(1790, 777);
            this.a1Panel11.TabIndex = 192;
            // 
            // lbItems
            // 
            this.lbItems.BackColor = System.Drawing.Color.Blue;
            this.lbItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbItems.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbItems.ForeColor = System.Drawing.SystemColors.Window;
            this.lbItems.Location = new System.Drawing.Point(0, 0);
            this.lbItems.Name = "lbItems";
            this.lbItems.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbItems.Size = new System.Drawing.Size(1790, 21);
            this.lbItems.TabIndex = 7;
            this.lbItems.Text = "Items";
            this.lbItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("바탕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.ColumnHeadersHeight = 35;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("바탕체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.GridColor = System.Drawing.Color.Silver;
            this.grid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grid.Location = new System.Drawing.Point(8, 30);
            this.grid.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("바탕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grid.RowTemplate.Height = 35;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.ShowCellErrors = false;
            this.grid.ShowEditingIcon = false;
            this.grid.ShowRowErrors = false;
            this.grid.Size = new System.Drawing.Size(1770, 738);
            this.grid.StandardTab = true;
            this.grid.TabIndex = 6;
            this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.HeaderText = "INDEX";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "DISCRIPTION";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 700;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "TARGET";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 500;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "OUTPUT";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 200;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "STATUS";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 215;
            // 
            // a1Panel1
            // 
            this.a1Panel1.BackColor = System.Drawing.Color.White;
            this.a1Panel1.BorderColor = System.Drawing.Color.Black;
            this.a1Panel1.BorderWidth = 2;
            this.a1Panel1.Controls.Add(this.a1Panel2);
            this.a1Panel1.Controls.Add(this.btnIndex);
            this.a1Panel1.Controls.Add(this.label7);
            this.a1Panel1.Controls.Add(this.lbConfiguration);
            this.a1Panel1.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel1.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(0, 775);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.RoundCornerRadius = 12;
            this.a1Panel1.ShadowOffSet = 1;
            this.a1Panel1.Size = new System.Drawing.Size(1789, 220);
            this.a1Panel1.TabIndex = 194;
            // 
            // a1Panel2
            // 
            this.a1Panel2.BackColor = System.Drawing.Color.White;
            this.a1Panel2.BorderColor = System.Drawing.Color.Black;
            this.a1Panel2.BorderWidth = 2;
            this.a1Panel2.Controls.Add(this.lbTargetState);
            this.a1Panel2.Controls.Add(this.btnOutput);
            this.a1Panel2.Controls.Add(this.label8);
            this.a1Panel2.Controls.Add(this.btnBlink);
            this.a1Panel2.Controls.Add(this.label6);
            this.a1Panel2.Controls.Add(this.btnBuzzer);
            this.a1Panel2.Controls.Add(this.label4);
            this.a1Panel2.Controls.Add(this.btnTarget);
            this.a1Panel2.Controls.Add(this.label3);
            this.a1Panel2.Controls.Add(this.label5);
            this.a1Panel2.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel2.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel2.Image = null;
            this.a1Panel2.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel2.Location = new System.Drawing.Point(1210, 28);
            this.a1Panel2.Name = "a1Panel2";
            this.a1Panel2.RoundCornerRadius = 12;
            this.a1Panel2.ShadowOffSet = 1;
            this.a1Panel2.Size = new System.Drawing.Size(568, 172);
            this.a1Panel2.TabIndex = 964;
            // 
            // lbTargetState
            // 
            this.lbTargetState.BackColor = System.Drawing.Color.Black;
            this.lbTargetState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTargetState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbTargetState.Location = new System.Drawing.Point(148, 24);
            this.lbTargetState.Name = "lbTargetState";
            this.lbTargetState.Size = new System.Drawing.Size(19, 33);
            this.lbTargetState.TabIndex = 966;
            // 
            // btnOutput
            // 
            this.btnOutput.BackColor = System.Drawing.Color.DarkGray;
            this.btnOutput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOutput.ForeColor = System.Drawing.Color.Black;
            this.btnOutput.GlowColor = System.Drawing.Color.Transparent;
            this.btnOutput.Location = new System.Drawing.Point(148, 132);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.OuterBorderColor = System.Drawing.Color.Black;
            this.btnOutput.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnOutput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOutput.Size = new System.Drawing.Size(415, 34);
            this.btnOutput.TabIndex = 965;
            this.btnOutput.TabStop = false;
            this.btnOutput.Tag = "4";
            this.btnOutput.Text = "...";
            this.btnOutput.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnOutput.Click += new System.EventHandler(this.ConfigClick);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DimGray;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(4, 132);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label8.Size = new System.Drawing.Size(142, 34);
            this.label8.TabIndex = 964;
            this.label8.Text = "OUTPUT";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBlink
            // 
            this.btnBlink.BackColor = System.Drawing.Color.DarkGray;
            this.btnBlink.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBlink.ForeColor = System.Drawing.Color.Black;
            this.btnBlink.GlowColor = System.Drawing.Color.Transparent;
            this.btnBlink.Location = new System.Drawing.Point(148, 96);
            this.btnBlink.Name = "btnBlink";
            this.btnBlink.OuterBorderColor = System.Drawing.Color.Black;
            this.btnBlink.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnBlink.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBlink.Size = new System.Drawing.Size(415, 34);
            this.btnBlink.TabIndex = 965;
            this.btnBlink.TabStop = false;
            this.btnBlink.Tag = "3";
            this.btnBlink.Text = "...";
            this.btnBlink.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBlink.Click += new System.EventHandler(this.ConfigClick);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DimGray;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 96);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label6.Size = new System.Drawing.Size(142, 34);
            this.label6.TabIndex = 964;
            this.label6.Text = "BLINK";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBuzzer
            // 
            this.btnBuzzer.BackColor = System.Drawing.Color.DarkGray;
            this.btnBuzzer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBuzzer.ForeColor = System.Drawing.Color.Black;
            this.btnBuzzer.GlowColor = System.Drawing.Color.Transparent;
            this.btnBuzzer.Location = new System.Drawing.Point(148, 60);
            this.btnBuzzer.Name = "btnBuzzer";
            this.btnBuzzer.OuterBorderColor = System.Drawing.Color.Black;
            this.btnBuzzer.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnBuzzer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBuzzer.Size = new System.Drawing.Size(415, 34);
            this.btnBuzzer.TabIndex = 965;
            this.btnBuzzer.TabStop = false;
            this.btnBuzzer.Tag = "2";
            this.btnBuzzer.Text = "...";
            this.btnBuzzer.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuzzer.Click += new System.EventHandler(this.ConfigClick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DimGray;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label4.Size = new System.Drawing.Size(142, 34);
            this.label4.TabIndex = 964;
            this.label4.Text = "BUZZER";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTarget
            // 
            this.btnTarget.BackColor = System.Drawing.Color.DarkGray;
            this.btnTarget.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTarget.ForeColor = System.Drawing.Color.Black;
            this.btnTarget.GlowColor = System.Drawing.Color.Transparent;
            this.btnTarget.Location = new System.Drawing.Point(168, 24);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.OuterBorderColor = System.Drawing.Color.Black;
            this.btnTarget.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnTarget.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTarget.Size = new System.Drawing.Size(395, 34);
            this.btnTarget.TabIndex = 965;
            this.btnTarget.TabStop = false;
            this.btnTarget.Tag = "1";
            this.btnTarget.Text = "...";
            this.btnTarget.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnTarget.Click += new System.EventHandler(this.ConfigClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DimGray;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 24);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label3.Size = new System.Drawing.Size(142, 34);
            this.label3.TabIndex = 964;
            this.label3.Text = "TARGET";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label5.Size = new System.Drawing.Size(568, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "STATUS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnIndex
            // 
            this.btnIndex.BackColor = System.Drawing.Color.DarkGray;
            this.btnIndex.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnIndex.ForeColor = System.Drawing.Color.Black;
            this.btnIndex.GlowColor = System.Drawing.Color.Transparent;
            this.btnIndex.Location = new System.Drawing.Point(120, 28);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.OuterBorderColor = System.Drawing.Color.Black;
            this.btnIndex.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnIndex.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnIndex.Size = new System.Drawing.Size(420, 34);
            this.btnIndex.TabIndex = 963;
            this.btnIndex.TabStop = false;
            this.btnIndex.Tag = "0";
            this.btnIndex.Text = "...";
            this.btnIndex.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnIndex.Click += new System.EventHandler(this.ConfigClick);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 28);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label7.Size = new System.Drawing.Size(110, 34);
            this.label7.TabIndex = 7;
            this.label7.Text = "KEY";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbConfiguration
            // 
            this.lbConfiguration.BackColor = System.Drawing.Color.Blue;
            this.lbConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbConfiguration.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbConfiguration.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbConfiguration.ForeColor = System.Drawing.SystemColors.Window;
            this.lbConfiguration.Location = new System.Drawing.Point(0, 0);
            this.lbConfiguration.Name = "lbConfiguration";
            this.lbConfiguration.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbConfiguration.Size = new System.Drawing.Size(1789, 21);
            this.lbConfiguration.TabIndex = 7;
            this.lbConfiguration.Text = "CONFIGURATION";
            this.lbConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmPageConfigLamp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1790, 995);
            this.Controls.Add(this.a1Panel1);
            this.Controls.Add(this.a1Panel11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPageConfigLamp";
            this.Text = "FormPageConfigLamp";
            this.Load += new System.EventHandler(this.FormPageConfigLamp_Load);
            this.a1Panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.a1Panel1.ResumeLayout(false);
            this.a1Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Owf.Controls.A1Panel a1Panel11;
        private System.Windows.Forms.Label lbItems;
        internal System.Windows.Forms.DataGridView grid;
        private Owf.Controls.A1Panel a1Panel1;
        private Owf.Controls.A1Panel a1Panel2;
        private System.Windows.Forms.Label lbTargetState;
        private Glass.GlassButton btnOutput;
        private System.Windows.Forms.Label label8;
        private Glass.GlassButton btnBlink;
        private System.Windows.Forms.Label label6;
        private Glass.GlassButton btnBuzzer;
        private System.Windows.Forms.Label label4;
        private Glass.GlassButton btnTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Glass.GlassButton btnIndex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbConfiguration;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}