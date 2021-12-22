using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XModule.Standard;


namespace XModule.Forms.FormConfig
{
    public partial class FrmPageConfigAnalog : Form
    {
        private int FSelected = 0;

        public FrmPageConfigAnalog()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FrmPageConfigAnalog_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            FSelected = 0;
            int i;
            String GridValue = "";
            String Temp;
            TfpAnalogItem AnalogItem;

            gridStatus.RowCount = 2;
            grid.RowTemplate.Height = 30;
            gridStatus.RowTemplate.Height = 30;

            gridStatus.Rows[0].Cells[0].Value = "LOW";
            gridStatus.Rows[1].Cells[0].Value = "HIGH";

            if (grid.RowCount != cDEF.Run.Analog.Count + 1)
                grid.RowCount = cDEF.Run.Analog.Count;


            for (i = 0; i < cDEF.Run.Analog.Count; i++)
            {
                AnalogItem = cDEF.Run.Analog.Items[i];
                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                Temp = (AnalogItem.Style == TfpAnalogStyle.fdsAIO_RTEX) ? "AI_RTEX" : "AI_4RB";
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                Temp = AnalogItem.Key.ToUpper();
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                Temp = AnalogItem.No.ToString();
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
            }
 
            UpdateDetailInformation();
        }
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row = 0;

            Col = grid.CurrentCell.ColumnIndex;
            Row = grid.CurrentCell.RowIndex;
            if (Col > -1 && Row > -1)
            {
                if (FSelected != Row)
                {
                    FSelected = Row;
                    UpdateDetailInformation();
                }
            }
        }

        void ActionClick(object sender, EventArgs e)
        {
	        TfpAnalogItem AnalogItem = cDEF.Run.Analog.Items[FSelected];

            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        AnalogItem.Active = true;
			        break;
		        case 1:
			        AnalogItem.Active = false;
			        break;
	        }
        }
        void ConfigurationClick(object sender, EventArgs e)
        {
	        int i;
            String Items = "";
	        TfpAnalogItem AnalogItem;

	        switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        for(i = 0; i < cDEF.Run.Analog.Count; i ++)
			        {
				        if(i > 0)
					        Items += ",";
				        Items += cDEF.Run.Analog.Items[i].Key.ToUpper()+ "|" + (i + 1).ToString();
			        }
			        if(XModuleMain.frmBox.SelectBox("INDEX", Items, ref FSelected) == DialogResult.No)
				        break;
			        UpdateDetailInformation();
			        break;
		        case 1:
			        AnalogItem = cDEF.Run.Analog.Items[FSelected];
			        i = AnalogItem.No;
			        if(!XModuleMain.frmBox.fpIntegerEdit("BOARD NO.",ref i, "", 0, 99))
				        return;
			        AnalogItem.No = i;
                    cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
			        UpdateDetailInformation();
			        break;
	        }
        }


        private void UpdateDetailInformation()
        {
            int i;
            String GridValue = "";
            String Temp;
            TfpAnalogItem AnalogItem;

            for (i = 0; i < cDEF.Run.Analog.Count; i++)
            {
                AnalogItem = cDEF.Run.Analog.Items[i];
                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                Temp = (AnalogItem.Style == TfpAnalogStyle.fdsAIO_RTEX) ? "AI_RTEX" : "AI_4RB";
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                Temp = AnalogItem.Key.ToUpper();
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                Temp = AnalogItem.No.ToString();
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
            }

	        AnalogItem = cDEF.Run.Analog.Items[FSelected];

	        lbIndexValue.Text = (FSelected + 1).ToString();
	        lbDescriptionValue.Text = AnalogItem.Key.ToUpper();
	        lbStyleValue.Text = (AnalogItem.Style == TfpAnalogStyle.fdsAIO_RTEX) ? "AI_RTEX" : "AI_4RB";
	        lbNoValue.Text = AnalogItem.No.ToString();
            gridStatus.Columns[0].HeaderText = (cDEF.Run.Analog.Items[FSelected].Style == TfpAnalogStyle.fdsAIO_RTEX) ? "AI_RTEX" : "AI_4RB";
        }
        public void UpdateInformation()
        {
	        if(!Visible)
		        return;

	        int i;
	        String Temp;
            String GridValue = "";
	        TfpAnalogItem AnalogItem;

	        for(i = 0; i < cDEF.Run.Analog.Count; i ++)
	        {
                AnalogItem = cDEF.Run.Analog.Items[i];

		         Temp = (AnalogItem.Active) ? "READY" : "NONE";
                 if (grid.Rows[i].Cells[4].Value != null)
                     GridValue = grid.Rows[i].Cells[4].Value.ToString();
                 if (GridValue != Temp)
                     grid.Rows[i].Cells[4].Value = Temp;
	        }
	        AnalogItem = cDEF.Run.Analog.Items[FSelected];
	        for(i = 0; i < cDEF.Run.Analog.Count; i ++)
	        {
                Temp = Math.Round( AnalogItem.Value[i], 3).ToString();
                gridStatus.Rows[i / 16].Cells[1 + i % 16].Value = Temp;
	        }
	        btnNo.Enabled = !AnalogItem.Active;
	        btnEnable.Enabled = !AnalogItem.Active;
	        btnDisable.Enabled = AnalogItem.Active;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            UpdateInformation();
            timer1.Enabled = true;
        }

    
    }
}
